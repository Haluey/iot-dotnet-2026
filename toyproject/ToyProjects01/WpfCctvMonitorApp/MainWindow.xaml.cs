using LibVLCSharp.Shared;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using WpfCctvMonitorApp.Common;
using WpfCctvMonitorApp.Services;

namespace WpfCctvMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LibVLC libVLC;
        private readonly MediaPlayer mediaPlayer;

        private readonly ItsCctvService itsCctvService;

        // 지역 선택한 위경도 범위 저장할 변수
        private GeoBound selectedGeoBound;

        public MainWindow()
        {
            InitializeComponent();

            // LibVLCSharp 초기화
            libVLC = new LibVLC();
            mediaPlayer = new MediaPlayer(libVLC);

            VvwScreen.MediaPlayer = mediaPlayer;

            // OpenAPI 서비스 객체 생성
            itsCctvService = new ItsCctvService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO : 나중에 지울것. VvwScreen에 ITS페이지 스트리밍 띄우기
            var media = new Media(libVLC, new Uri("https://cctvsec.ktict.co.kr:8082/mstrm0802hls/155C48ACE14C5708C9DF794025D2B2D6/live/04CT000001340/002/SELF/playlist.m3u8?nimblesessionid=23802213&wmsAuthSign=c2VydmVyX3RpbWU9Ny8xLzIwMjYgNDowMToyNiBBTSZoYXNoX3ZhbHVlPXVudUx4aWdwRVBjWk9QZVgrZWJPUVE9PSZ2YWxpZG1pbnV0ZXM9MTIwJmlkPW1sdG0jbnRpY2xpdmUjNzI4OTE="));
            mediaPlayer.Play(media);

            Common.AppCommon.ItsApiKey = ConfigurationManager.AppSettings["ItsApiKey"];
            //MessageBox.Show(Common.AppCommon.ItsApiKey);

            InitComboItems();
        }

        private void InitComboItems()
        {
            //CboRegions.Items.Add("전국");
            CboRegions.ItemsSource = Common.AppCommon.Regions;
            CboRegions.SelectedIndex = 0;
        }

        private void BtnExpress_Click(object sender, RoutedEventArgs e)
        {
            Common.AppCommon.RoadType = "ex";
        }

        private void BtnNational_Click(object sender, RoutedEventArgs e)
        {
            Common.AppCommon.RoadType = "its";
        }

        private void CboRegions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Debug.WriteLine(CboRegions.SelectedItem);   // SelectedItem과 SelectedValue는 String이 아니면 다르게 나온다?
            if (CboRegions.SelectedIndex > 0)   // -- 선택 -- 외 선택되었을 때 이벤트 발생
            {
                //MessageBox.Show(CboRegions.SelectedValue.ToString());
                selectedGeoBound = GetRegionBounds(CboRegions.SelectedValue.ToString());

                Debug.WriteLine(selectedGeoBound.MinLat);
                Debug.WriteLine(selectedGeoBound.MaxLat);
                Debug.WriteLine(selectedGeoBound.MinLng);
                Debug.WriteLine(selectedGeoBound.MaxLng);
            }
            
        }

        // 위경도 범위 리턴 메서드
        private GeoBound GetRegionBounds(string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionName))
                return AppCommon.RegionBounds["전국"];

            /*AppCommon.RegionBounds.TryGetValue(regionName, out GeoBound bound);

            if (bound == null)
                return AppCommon.RegionBounds["전국"];
            else
                return bound;*/

            return AppCommon.RegionBounds.TryGetValue(regionName, out GeoBound bound)
                ? bound
                : AppCommon.RegionBounds["전국"];
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Common.AppCommon.MinX = selectedGeoBound.MinLng;
            Common.AppCommon.MaxX = selectedGeoBound.MaxLng;
            Common.AppCommon.MinY = selectedGeoBound.MinLat;
            Common.AppCommon.MaxY = selectedGeoBound.MaxLat;

            var totalApiUrl = Common.AppCommon.BuildCctvApiUrl();

            var result = await itsCctvService.GetCctvListAsync(totalApiUrl);
            Debug.WriteLine(result);

            MessageBox.Show(result.Response.DataCount.ToString());
        }
    }
}