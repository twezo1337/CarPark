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
using System.Collections;

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
            this.GosNumber = gosNumber;
            this.Mark = mark;
            this.KindOfTransport = kindOfTransport;
            this.Date = date;
            this.RouteName = routeName;
            this.Length = length;
            this.RouteDate = routeDate;
            this.PasCount = pasCount;
        }
        public string GosNumber { get; set; }
        public string Mark { get; set; }
        public string KindOfTransport { get; set; }
        public string Date { get; set; }
        public string RouteName { get; set; }
        public string Length { get; set; }
        public string RouteDate { get; set; }
        public string PasCount { get; set; }
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
            List<MyTable> result = new(7)
            {
                new MyTable("с068хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "12"),
                new MyTable("с069хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "14"),
                new MyTable("с067хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "11"),
                new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"),
                new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"),
                new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"),
                new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13"),
                new MyTable("с065хм", "Газ", "Микроавтобус", "8.03.2023", "Красноярск-Канск", "300", "15.03.2023", "13")
            };
            mainDataTable.ItemsSource = result;
        }

        private void mainDataTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void users_table_btn_Click(object sender, RoutedEventArgs e)
        {
            Users users = new();
            users.Show();
        }

        private void lk_btn_Click(object sender, RoutedEventArgs e)
        {
            lk LK = new();
            LK.Show();
        }
    }
}
