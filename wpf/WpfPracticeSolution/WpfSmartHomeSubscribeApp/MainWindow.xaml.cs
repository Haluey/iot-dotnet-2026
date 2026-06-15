using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

using MahApps.Metro.Controls.Dialogs;
using WpfSmartHomeSubscribeApp.Helpers;
using MQTTnet;
using System.Text;

namespace WpfSmartHomeSubscribeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool IsConnected { get; set; } = false;  // 접속여부 확인

        private CancellationTokenSource? _cts;  // 스레드 캔슬객체 : 비동기 작업 중지시켜주는 객체

        #region MQTT/DB 전송용 속성/변수들

        private IMqttClient? MqttClient { get; set; }
        private string MqttHost { get; set; } = "127.0.0.1";    // TxtMqttBrokerIp 텍스트박스의 IP로 변경되어야 함
        private int MqttPort { get; set; } = 1883;
        private string MqttUser { get; set; } = "root";
        private string MqttPassword { get; set; } = "mqtt123456";
        private string MqttTopic { get; set; } = "home/sensor";

        private string DbHost { get; set; } = "127.0.0.1";
        private string DbUser { get; set; } = "root";
        private string DbPassword { get; set; } = "my123456";
        private string DbName { get; set; } = "smarthome";

        #endregion

        #region 생성자영역

        public MainWindow()
        {
            InitializeComponent();  // UI 초기화

            // 커스텀 초기화
            IsConnected = false;    // 접속하지 않은 상태
        }

        #endregion

        #region 이벤트핸들러 영역

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            // 입력검증(Validation Check)
            if (string.IsNullOrWhiteSpace(TxtMqttBrokerIp.Text))
            {
                await this.ShowMessageAsync("오류", "MQTT 브로커주소를 입력하세요.");

                Common.logger.Warn("MQTT 브로커주소 미입력!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtMqttTopic.Text))
            {
                await this.ShowMessageAsync("오류", "MQTT 토픽을 입력하세요.");

                Common.logger.Warn("MQTT 토픽 미입력!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtDatabaseIp.Text))
            {
                await this.ShowMessageAsync("오류", "Database IP를 입력하세요.");

                Common.logger.Warn("Database IP 미입력!");
                return;
            }

            MqttHost = TxtMqttBrokerIp.Text.Trim(); // MqttHost 값 127.0.0.1 -> UI에서 입력한 HostIP로 변경
            MqttTopic = TxtMqttTopic.Text.Trim();
            DbHost = TxtDatabaseIp.Text.Trim();

            if (!IsConnected)
            {
                // Mqtt브로커 접속 시도
                await ConnectMqttAsync();

                IsConnected = true;
                TxtStatus.Text = "DISCONNECT";

                AddLogs("SYSTEM", "MQTT Subsribe 접속시작");

                Common.logger.Info("MQTT Subscribe 시작");
                SbiStatus.Text = "MQTT 연결 시작";
            }
            else
            {
                IsConnected = false;
                TxtStatus.Text = "CONNECT";
                

                if (MqttClient != null && MqttClient.IsConnected)
                {
                    await MqttClient.DisconnectAsync();

                    AddLogs("SYSTEM", "MQTT Subsribe 접속종료");

                    Common.logger.Info("MQTT Subscribe 접속종료");
                    SbiStatus.Text = "MQTT 연결 종료";
                }
            }
        }

        #endregion

        #region 커스텀메서드 영역

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
            var factory = new MqttClientFactory();
            MqttClient = factory.CreateMqttClient();

            // Subscribe 핵심 - 데이터가 Publish(배포)되면 곧바로 Subscribe(구독)
            // Subscribe 실행된 후 Payload가 넘어왔을 때 이벤트 처리
            MqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                AddLogs(topic, payload);
            };

            var options = new MqttClientOptionsBuilder()
                .WithClientId($"WPF-Subscriber-{Guid.NewGuid()}")
                .WithTcpServer(MqttHost, MqttPort)
                .WithCredentials(MqttUser, MqttPassword)
                .WithCleanSession()
                .Build();

            await MqttClient.ConnectAsync(options);

            // Subscribe 옵션
            var subscribeOptions = factory
                .CreateSubscribeOptionsBuilder()
                .WithTopicFilter(MqttTopic)
                .Build();

            // Subscribe를 실행
            await MqttClient.SubscribeAsync(subscribeOptions);

            AddLogs("SYSTEM", "MQTT 구독 시작!");
        }

        #endregion
    }
}