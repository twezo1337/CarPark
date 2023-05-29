using Fernet;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        int linesCount = 0;

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_all.IsEnabled = false;
            StreamReader srKey = new StreamReader("logKey.key");
            string strKey = srKey.ReadToEnd();
            var byteKey = strKey.UrlSafe64Decode();
            srKey.Close();

            StreamReader srLogs = new StreamReader("LogFile.log");
            while (!srLogs.EndOfStream)
            {
                string encryptLog = srLogs.ReadLine();
                var decoded64 = SimpleFernet.Decrypt(byteKey, encryptLog, out var timestamp);
                var decoded = decoded64.UrlSafe64Encode().FromBase64String();
                tb_logs.AppendText(decoded + "\n");
                linesCount++;

            }
            srLogs.Close();  
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime? date = datePicker.SelectedDate;
            string[] logLines = tb_logs.Text.Split("\n");
            if (date != null && logLines.Length > 0 && logLines.Length != linesCount)
            {
                tb_logs.Clear();

                string dateStr = "";
                for (int j = 0; j < logLines.Length; j++)
                {
                    for (int i = 0; i < logLines[0].Split(" ").Length; i++)
                    {
                        if (logLines[0].Split(" ")[i] == "at")
                        {
                            dateStr += logLines[0].Split(" ")[i + 1];
                            dateStr += " ";
                            dateStr += logLines[0].Split(" ")[i + 2];
                            break;
                        }
                    }
                    if (dateStr != "" && DateOnly.FromDateTime(Convert.ToDateTime(dateStr)) == DateOnly.FromDateTime(Convert.ToDateTime(date)))
                    {
                        tb_logs.AppendText(logLines[j] + "\n");
                    }
                    dateStr = "";
                }
            } 
            else
            {
                StreamReader srKey = new StreamReader("logKey.key");
                string strKey = srKey.ReadToEnd();
                var byteKey = strKey.UrlSafe64Decode();
                srKey.Close();
                StreamReader srLogs = new StreamReader("LogFile.log");
                while (!srLogs.EndOfStream)
                {
                    string encryptLog = srLogs.ReadLine();
                    var decoded64 = SimpleFernet.Decrypt(byteKey, encryptLog, out var timestamp);
                    var decoded = decoded64.UrlSafe64Encode().FromBase64String();
                    tb_logs.AppendText(decoded + "\n");

                }
                srLogs.Close();
            }
            btn.IsEnabled = false;
            btn_all.IsEnabled = true;
        }

        private void btn_all_Click(object sender, RoutedEventArgs e)
        {
                tb_logs.Clear();
                StreamReader srKey = new StreamReader("logKey.key");
                string strKey = srKey.ReadToEnd();
                var byteKey = strKey.UrlSafe64Decode();
                srKey.Close();

                StreamReader srLogs = new StreamReader("LogFile.log");
                while (!srLogs.EndOfStream)
                {
                    string encryptLog = srLogs.ReadLine();
                    var decoded64 = SimpleFernet.Decrypt(byteKey, encryptLog, out var timestamp);
                    var decoded = decoded64.UrlSafe64Encode().FromBase64String();
                    tb_logs.AppendText(decoded + "\n");

                }
                srLogs.Close();
                btn_all.IsEnabled = false;
                btn.IsEnabled = true;
        }
    }
}
