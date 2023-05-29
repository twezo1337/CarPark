using MySql.Data.MySqlClient;
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
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

namespace CarPark
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void AccountLoginNotification(string login)
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
                writer.WriteLine("Account: " + login + " was registered in at " + loginTime.ToString() + " from " + os.ToString() + " " + version.ToString());
            }

            GlobalVars.Login = login; // Добавление логина в глобальную переменную

        }
        public bool IsLoginExists(string login)
        {

            bool result = true;

            string query = "SELECT COUNT(*) FROM log_in WHERE login = @login";

            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                MySqlCommand command = new(query, conn);
                command.Parameters.AddWithValue("@login", login);
                conn.Open();
                long count = (long)command.ExecuteScalar();
                if (count > 0)
                {
                    result = false;
                }
            }
            return result;
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
        public void InsertInDB(string login, string psswrd)
        {
            string query = "INSERT INTO log_in (login, password) VALUES (@login, @password)";

            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                MySqlCommand command = new(query, conn);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", psswrd);
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
        private void GetSetIDdriver(string login)
        {
            string query = "SELECT IDdriver FROM log_in WHERE login = @login";

            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                MySqlCommand command = new(query, conn);

                command.Parameters.AddWithValue("@login", login);
                conn.Open();

                GlobalVars.ID_driver = (int)command.ExecuteScalar(); // Добавление IDdriver в глобальную переменную
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoginExists(tbLoginReg.Text))
            {
                if (tbPasswordReg.Password.Length >= 6 && tbLoginReg.Text.Length > 0)
                {
                    bool en = true; // английская раскладка
                    bool symbol = false; // символ
                    bool number = false; // цифра

                    for (int i = 0; i < tbPasswordReg.Password.Length; i++) // перебираем символы
                    {
                        if (tbPasswordReg.Password[i] >= 'А' && tbPasswordReg.Password[i] <= 'Я') en = false; // если русская раскладка
                        if (tbPasswordReg.Password[i] >= '0' && tbPasswordReg.Password[i] <= '9') number = true; // если цифры
                        if (tbPasswordReg.Password[i] == '_' || tbPasswordReg.Password[i] == '-' || tbPasswordReg.Password[i] == '!') symbol = true; // если символ
                    }

                    for (int i = 0; i < tbLoginReg.Text.Length; i++) // перебираем символы
                    {
                        if (tbLoginReg.Text[i] >= 'А' && tbLoginReg.Text[i] <= 'Я') en = false; // если русская раскладка
                    }

                    if (!en)
                        MessageBox.Show("Используйте только английскую раскладку!");
                    else if (!symbol)
                        MessageBox.Show("Добавьте один из следующих символов: _ - !");
                    else if (!number)
                        MessageBox.Show("Добавьте хотя бы одну цифру.");
                    if (en && symbol && number) // проверяем соответствие
                    {
                        InsertInDB(tbLoginReg.Text, Hashfunc(tbPasswordReg.Password));

                        GlobalVars.Login = tbLoginReg.Text; // Добавление логина в глобальную переменную
                        GetSetIDdriver(GlobalVars.Login);
                        AccountLoginNotification(GlobalVars.Login);

                        lk personalCab = new();
                        personalCab.Show();
                        this.Close();
                    }
                }
                else MessageBox.Show("Пароль слишком короткий, необходимо минимум 6 символов или введите логин.");
            }
            else MessageBox.Show("Такой логин уже существует!");
        }
        private void tbPasswordReg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tbRepeatPasswordReg.Password = null;
        }
        private void tbRepeatPasswordReg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tbRepeatPasswordReg.Password != tbPasswordReg.Password)
            {
                regButton.IsEnabled = false;
                tbRepeatPasswordReg.Background = Brushes.LightCoral;
                tbRepeatPasswordReg.BorderBrush = Brushes.Red;

            }
            else
            {
                regButton.IsEnabled = true;
                tbRepeatPasswordReg.Background = Brushes.LightGreen;
                tbRepeatPasswordReg.BorderBrush = Brushes.Green;
            }
        }
        private void tbLoginReg_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Логин")
            {
                textBox.Text = "";
            }
        }
    }
}
