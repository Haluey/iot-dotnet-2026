using System.Windows;
using MahApps.Metro.Controls;
using WpfProductAdmin.Services;

namespace WpfProductAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ApiService service;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            service = new ApiService(); // 객체 생성

            await SearchProductAsync();
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchProductAsync();
        }

        private async Task SearchProductAsync()
        {
            var result = await service.GetProductsAsync();

            DgrProduct.ItemsSource = result;
        }

        // 이벤트 핸들러는 async를 써도 void 리턴을 유지, Task로 바뀌면 컴파일 오류
        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductCreateWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = window.ShowDialog();

            if (result == true)
            {
                await SearchProductAsync();
            }
        }
    }
}