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

            if (GlobalVars.IDlog == 1)
            {
                view_route_btn.IsEnabled = true;
                delete_route_btn.IsEnabled = true;
                add_new_route_btn.IsEnabled = true;
                users_table_btn.IsEnabled = true;
                admin_logs.IsEnabled = true;
            }
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
            /*var ind = mainDataTable.CurrentCell;
            MessageBox.Show(ind.Column.GetValue);*/
        }
        private void users_table_btn_Click(object sender, RoutedEventArgs e)
        {
            Users users = new();
            users.Show();
            this.Close();
        }
        private void lk_btn_Click(object sender, RoutedEventArgs e)
        {
            lk LK = new();
            LK.Show();
            this.Close();
        }
        private void view_route_btn_Click(object sender, RoutedEventArgs e)
        {
            
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
        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void admin_logs_Click(object sender, RoutedEventArgs e)
        {
            AdminLogsWindow logWindow = new AdminLogsWindow();
            logWindow.Show();
            this.Close();
        }

        private void add_new_route_btn_Click(object sender, RoutedEventArgs e)
        {
            RouteInfoPage routeInfo = new RouteInfoPage();
            routeInfo.Show();
            this.Close();
        }
    }
}
