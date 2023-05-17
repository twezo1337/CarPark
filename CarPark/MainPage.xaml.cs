using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarPark
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    /// 
    class MyTable
    {
        public MyTable(string gosNumber, string mark, string kindOfTransport, string date, string routeName, string length, string routeDate, string pasCount)
        {
            this.gosNumber = gosNumber;
            this.mark = mark;
            this.kindOfTransport = kindOfTransport;
            this.date = date;
            this.routeName = routeName;
            this.length = length;
            this.routeDate = routeDate;
            this.pasCount = pasCount;
        }
        public string gosNumber { get; set; }
        public string mark { get; set; }
        public string kindOfTransport { get; set; }
        public string date { get; set; }
        public string routeName { get; set; }
        public string length { get; set; }
        public string routeDate { get; set; }
        public string pasCount { get; set; }
    }
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void tbLoginReg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mainDataTable_Loaded(object sender, RoutedEventArgs e)
        {
            List<MyTable> result = new List<MyTable>(7);
            result.Add(new MyTable("с068хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "12"));
            result.Add(new MyTable("с069хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "14"));
            result.Add(new MyTable("с067хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "11"));
            result.Add(new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"));
            result.Add(new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"));
            result.Add(new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"));
            result.Add(new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"));
            result.Add(new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"));
            mainDataTable.ItemsSource = result;
        }

        private void mainDataTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void users_table_btn_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            users.Show();
        }

        private void lk_btn_Click(object sender, RoutedEventArgs e)
        {
            lk LK = new lk();
            LK.Show();
        }

        private void view_route_btn_Click(object sender, RoutedEventArgs e)
        {
            RouteInfoPage routeInfo = new RouteInfoPage();
            routeInfo.Show();
        }
    }
}
