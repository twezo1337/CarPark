using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace CarPark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string host = "localhost";
        string database = "zi";
        string port = "3306";
        string username = "root";
        string password = "qwerty";
       
        public MainWindow()
        {
            InitializeComponent();
        }
        public void AccountLoginNotification(string login)
        {
            // Получение времени подключения
            DateTime loginTime = Process.GetCurrentProcess().StartTime;

            // Получение информации о системе
            OperatingSystem os = Environment.OSVersion;
            Version version = Environment.Version;


            // Запись информации в файл
            string filePath = "login_info.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                writer.WriteLine("Account login: " + login + " was logged in at " + loginTime.ToString() + " from " + os.ToString() + " " + version.ToString());
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
        public string Hashfunc(string hashpassword)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(hashpassword);
            byte[] hashBytes;

            using (SHA256Managed sha256 = new SHA256Managed())
            {
                hashBytes = sha256.ComputeHash(passwordBytes);
            }

            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String connString = "Server=" + host + ";Database=" + database
              + ";port=" + port + ";User Id=" + username + ";password=" + password;

            string query = "SELECT login, password, IDlog FROM log_in WHERE login = @login AND password = @password";

            using (MySqlConnection conn = new(connString))
            {
                bool invalid_log = true;
                MySqlCommand command = new(query, conn);

                command.Parameters.AddWithValue("@login", tbLogin.Text);
                command.Parameters.AddWithValue("@password", Hashfunc(tbPassword.Password));
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    try
                    {
                        string log = reader.GetString(0);
                        string pass = reader.GetString(1);
                        int idlog = reader.GetInt32(2);
                        MainPage main = new MainPage();
                        switch (idlog)
                        {
                            case 0:
                                invalid_log = false;
                                MessageBox.Show("Здравствуйте, Водитель " + log + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                AccountLoginNotification(tbLogin.Text);
                                main.Show();
                                this.Close();
                                break;
                            case 1:
                                invalid_log = false;
                                MessageBox.Show("Здравствуйте, Администратор " + log + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                AccountLoginNotification(tbLogin.Text);
                                main.Show();
                                this.Close();
                                break;
                             default:
                                MessageBox.Show("Ошибка");
                                break;
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                reader.Close();
                if (invalid_log)
                {
                    MessageBox.Show($"Введен неверный логин или пароль!", "Ошибка авторизации!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Логин")
            {
                textBox.Text = "";
            }
        }
    }
}
