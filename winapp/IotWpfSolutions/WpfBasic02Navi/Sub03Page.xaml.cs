using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBasic02Navi
{
    public class Car
    {
        public double Speed { get; set; }

        public Color Color { get; set; }
    }

    /// <summary>
    /// Sub03Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Sub03Page : Page
    {
        List<Employee> employees;   // employee 컬렉션 선언

        public Employee SelectedEmployee { get; set; }
        public Sub03Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 초기화
            Car car = new Car
            {
                Speed = 10.0,
                Color = Colors.Black
            };

            // 데이터컨텍스트
            //DataContext = car;  // 전체 page에 데이터컨텍스트에 car를 지정, this.DataContext == DataContext
            GrbInfo.DataContext = car;  // 그룹박스 하위에서 car라는 변수의 데이터를 사용할 수 있다.

            // WinForms에서 데이터바인딩하던 방식
            //TxtSpeed.Text = car.Speed.ToString();
        }
    }
}
