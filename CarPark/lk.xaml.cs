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
    /// Логика взаимодействия для lk.xaml
    /// </summary>
    public partial class lk : Window
    {
        public lk()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_surname.IsEnabled = false;
            tb_name.IsEnabled = false;
            tb_lastname.IsEnabled = false;
            tb_gosNumber.IsEnabled = false;
            tb_mark.IsEnabled = false;
            tb_kindTransport.IsEnabled = false;
        }
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            // can edit the data

            tb_surname.IsEnabled = true;
            tb_name.IsEnabled = true;
            tb_lastname.IsEnabled = true;
            tb_gosNumber.IsEnabled = true;
            tb_mark.IsEnabled = true;
            tb_kindTransport.IsEnabled = true;
        }
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            // save all data to DB

            tb_surname.IsEnabled = false;
            tb_name.IsEnabled = false;
            tb_lastname.IsEnabled = false;
            tb_gosNumber.IsEnabled = false;
            tb_mark.IsEnabled = false;
            tb_kindTransport.IsEnabled = false;
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            // exit from profile

            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
    }
}
