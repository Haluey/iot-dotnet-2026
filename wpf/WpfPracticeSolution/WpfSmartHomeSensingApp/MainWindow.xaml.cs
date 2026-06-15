using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

using Bogus;
using MahApps.Metro.Controls.Dialogs;
using WpfSmartHomeSensingApp.Models;
using System.Text.Json;
using WpfSmartHomeSensingApp.Helpers;
using System.Data;
using MQTTnet;
using System.Text;

namespace WpfSmartHomeSensingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool IsConnected { get; set; } = false;  // 접속여부 확인

        private CancellationTokenSource? _cts;  // 스레드 캔슬객체 : 비동기 작업 중지시켜주는 객체
        
        #region DummyData용 속성/변수들

        private string[] Rooms { get; set; }
        private string HomeId { get; set; }
        private Faker SmartHomeFaker { get; set; }

        #endregion

        #region MQTT 전송용 속성/변수들

        private IMqttClient? MqttClient { get; set; }
        private string MqttHost { get; set; } = "127.0.0.1";    // TxtMqttBrokerIp 텍스트박스의 IP로 변경되어야 함
        private int MqttPort { get; set; } = 1883;
        private string MqttUser { get; set; } = "root";
        private string MqttPassword { get; set; } = "mqtt123456";
        private string MqttTopic { get; set; } = "home/sensor";

        #endregion

        #region 생성자영역

        public MainWindow()
        {
            InitializeComponent();  // UI 초기화

            // 커스텀 초기화
            IsConnected = false;    // 접속하지 않은 상태

            InitFakeData(); // Bogus Faker 초기화
        }

        #endregion

        #region 이벤트핸들러 영역

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            /*
                // Bogus 테스트
                var faker = new Faker("ko");    // 한국어 더미데이터

                Console.WriteLine(faker.Name.FullName());
                Console.WriteLine(faker.Name.JobTitle());
                Console.WriteLine(faker.Phone.PhoneNumber());
                Console.WriteLine(faker.Address.FullAddress());
            */

            if (string.IsNullOrWhiteSpace(TxtMqttBrokerIp.Text))
            {
                await this.ShowMessageAsync("오류", "MQTT 브로커주소를 입력하세요.");

                Common.logger.Warn("MQTT 브로커주소 미입력!");
                return;
            }

            // MqttHost 값 127.0.0.1 -> UI에서 입력한 HostIP로 변경
            MqttHost = TxtMqttBrokerIp.Text.Trim(); 

            // TODO : IP주소 형식에 맞지 않으면 메시지창 출력

            if (!IsConnected)
            {
                // Mqtt브로커 접속 시도
                await ConnectMqttAsync();

                IsConnected = true;
                TxtStatus.Text = "DISCONNECT";

                Common.logger.Info("Bogus Faker 처리시작");
                SbiStatus.Text = "MQTT 연결 시작";
                await StartSensingAsync(); // 연결 후 처리시작
            }
            else
            {
                IsConnected = false;
                TxtStatus.Text = "CONNECT";
                
                StopSensing();  // 연결 종료 후 처리중지

                if (MqttClient != null && MqttClient.IsConnected)
                {
                    await MqttClient.DisconnectAsync();

                    AddLogs("SYSTEM", "MQTT 브로커 접속종료");

                    Common.logger.Info("Bogus Faker 처리종료");
                    SbiStatus.Text = "MQTT 연결 종료";
                }
            }
        }

        #endregion

        #region 커스텀메서드 영역

        private void InitFakeData()
        {
            Rooms = ["BED", "BATH", "LIVING", "DINING"];
            HomeId = "D101H703";

            SmartHomeFaker = new Faker();

            Common.logger.Info("Bogus Faker 초기화 완료.");
        }

        /// <summary>
        /// Dummy Sensing값 생성 시작메서드
        /// </summary>
        /// <returns></returns>
        private async Task StartSensingAsync()
        {
            _cts = new CancellationTokenSource();

            try
            {
                // TODO : while문 다른걸로 변경
                while (!_cts.Token.IsCancellationRequested)
                {
                    // Rooms 갯수(4개)만큼 Bogus.Faker 사용해서 임의값 생성
                    List<SensorData> lists = Rooms.Select(room => new SensorData
                    {
                        HomeId = HomeId,
                        RoomName = room,
                        SensingDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Temp = Math.Round(SmartHomeFaker.Random.Double(20, 30), 1),
                        Humid = Math.Round(SmartHomeFaker.Random.Double(40, 70), 1)
                    }).ToList();

                    // list 데이터를 json으로 변환. 직렬화 -> 네트워크로 전송
                    string json = JsonSerializer.Serialize(lists, new JsonSerializerOptions { WriteIndented = true });

                    // 데이터 Publish
                    await PublishMqttAsync(MqttTopic, json);

                    //Console.WriteLine(json);
                    AddLogs("home/sensor", json);
                    //Common.logger.Info($"{json}");

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception)
            {

            }
        }

        private void StopSensing()
        {
            _cts?.Cancel();
        }

        private void AddLogs(string topic, string payload)
        {
            // 언젠가 응답없음 발생함!
            //RtbLog.AppendText($"{topic} : {payload}\r\n");    // 이 방식으로 텍스트 입력 가능

            // RichTextBox 활용
            Dispatcher.Invoke(() =>
            {
                // UI스레드와 충돌없이 텍스트 출력방법
                Paragraph p = new Paragraph();

                p.Margin = new Thickness(0, 0, 0, 10);  // bottom에 10여백

                p.Inlines.Add(  // 출력시간표시
                    new Run($"[{DateTime.Now:HH:mm:ss}] ")
                    {
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Colors.Blue)
                    });

                p.Inlines.Add(  // 토픽
                    new Run($"TOPIC : {topic}\n")
                    {
                        FontWeight = FontWeights.Bold,
                    });

                p.Inlines.Add(  // json 페이로드
                    new Run(payload)
                    {
                        FontFamily = new FontFamily("Consolas")
                    });

                RtbLog.Document.Blocks.Add(p);

                if (RtbLog.Document.Blocks.Count > 50)
                {
                    RtbLog.Document.Blocks.Remove(
                        RtbLog.Document.Blocks.FirstBlock);
                }

                RtbLog.ScrollToEnd();   // 리치텍스트박스 가장 마지막으로 포커스
            });

        }

        /// <summary>
        /// MQTT 브로커 접속 메서드
        /// </summary>
        private async Task ConnectMqttAsync()
        {
            // MQTTnet으로 초기화할 때 동일한 방식
            var factory = new MqttClientFactory();

            // DesignPattern 중 Factory 메서드 방식으로 객체 생성
            MqttClient = factory.CreateMqttClient();

            // DesignPattern 중 Builder 사용
            var options = new MqttClientOptionsBuilder()
                .WithClientId($"WPF-SmartHome-{Guid.NewGuid()}")
                .WithTcpServer(MqttHost, MqttPort)
                .WithCredentials(MqttUser, MqttPassword)
                .WithCleanSession()
                .Build();

            await MqttClient.ConnectAsync(options);

            AddLogs("SYSTEM", "Mqtt Broker Connected");
        }

        /// <summary>
        /// MQTT Broker로 메시지 publish 메서드
        /// </summary>
        /// <param name="topic">토픽(주제)</param>
        /// <param name="payload">전송할 메시지(json타입)</param>
        private async Task PublishMqttAsync(string topic, string payload)
        {
            // MqttClient가 아직 초기화 안됐거나, 접속이 안되었다면
            if (MqttClient == null || !MqttClient.IsConnected)
            {
                AddLogs("ERROR", "MQTT Broker에 접속되지 않았습니다.");
            }

            // 실제 전달할 메시지(페이로드) 작성
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)   // 이 토픽으로 데이터 송수신
                .WithPayload(Encoding.UTF8.GetBytes(payload))   // string을 byte[]로 변경
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)  // 데이터 전송 후 체크 X
                .Build();

            await MqttClient.PublishAsync(message);
        }

        #endregion
    }
}