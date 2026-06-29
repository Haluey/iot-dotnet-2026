using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using WpfProductAdmin.Models;
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

        // 이벤트 핸들러는 async를 써도 void 리턴을 유지필수, Task로 바뀌면 컴파일 오류
        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow
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

        private async void DgrProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Validation Check

            // product 데이터 전달때문에 생성자가 차이남
            Product product = DgrProduct.SelectedItem as Product;

            var window = new ProductWindow(product)
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