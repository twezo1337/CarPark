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
using MySql.Data.MySqlClient;
using System.Data;
using System.ComponentModel.Design;
using Fernet;
using System.Diagnostics;
using System.IO;

namespace CarPark
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    /// 
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            log_label.Content = GlobalVars.Login;

            /*using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                string query = "SELECT IDlog FROM log_in WHERE login = @login";
                MySqlCommand command = new(query, conn);

                command.Parameters.AddWithValue("@login", GlobalVars.Login);

                conn.Open();
                MySqlDataReader reader_4 = command.ExecuteReader();

                while (reader_4.Read())
                {
                     = reader_4.GetString(0);
                }
                reader_4.Close();

            }*/
        }
        private void tbLoginReg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void mainDataTable_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                string query = "SELECT * FROM transportation_info_view";
                MySqlCommand command = new(query, conn);

                conn.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                mainDataTable.ItemsSource = dataTable.DefaultView;
            }
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
            this.Close();
        }
        private void view_route_btn_Click(object sender, RoutedEventArgs e)
        {
            RouteInfoPage routeInfo = new RouteInfoPage();
            routeInfo.Show();
        }
        private void refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                string query = "SELECT * FROM transportation_info_view";
                MySqlCommand command = new(query, conn);

                conn.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                mainDataTable.ItemsSource = dataTable.DefaultView;
            }
        }
        private void AccountLoginNotification(string login)
        {
            // Получение времени подключения
            DateTime loginTime = Process.GetCurrentProcess().StartTime;

            // Получение информации о системе
            OperatingSystem os = Environment.OSVersion;
            Version version = Environment.Version;

            string log = "Exit App: " + login + " exited app in at" + loginTime.ToString() + " from " + os.ToString() + " " + version.ToString();

            StreamReader sr = new StreamReader("logKey.key");
            string strKey = sr.ReadToEnd();
            var byteKey = strKey.UrlSafe64Decode();
            sr.Close();

            var src64 = log.ToBase64String();
            var token = SimpleFernet.Encrypt(byteKey, src64.UrlSafe64Decode());

            File.AppendAllText("LogFile.log", token + "\n");

            GlobalVars.Login = login; // Добавление логина в глобальную переменную

        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            AccountLoginNotification(GlobalVars.Login);
            this.Close();
        }

        private void admin_logs_Click(object sender, RoutedEventArgs e)
        {
            AdminLogsWindow logWindow = new AdminLogsWindow();
            logWindow.Show();
        }
    }
}
