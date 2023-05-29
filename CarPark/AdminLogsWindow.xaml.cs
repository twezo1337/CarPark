using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AdminLogsWindow.xaml
    /// </summary>
    public partial class AdminLogsWindow : Window
    {
        public AdminLogsWindow()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader("login_info.txt");
            string encryptData = sr.ReadToEnd();
            richtb_logs.AppendText(encryptData);
        }
    }
}
