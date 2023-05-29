using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
using Microsoft.Win32;
using Fernet;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

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
            if (GlobalVars.FirstLog)
            {
                SetInfo();
            }
            GlobalVars.FirstLog = true;
        }
        /// <summary>
        /// Ввнесение всех данных о пользователе в личном кабинете
        /// </summary>
        private void SetInfo()
        {
            log_label.Content = GlobalVars.Login;

            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                // Заполнение фамилии, имени, отчества и фото профиля
                string query_1 = "SELECT surname, name, patronymic, profileImage, IDt FROM driver WHERE IDdriver = @IDdriver";
                MySqlCommand command_1 = new(query_1, conn);

                command_1.Parameters.AddWithValue("@IDdriver", GlobalVars.ID_driver);

                conn.Open();

                using (MySqlDataReader reader_1 = command_1.ExecuteReader())
                {
                    while (reader_1.Read())
                    {
                        try
                        {
                            tb_surname.Text = reader_1.GetString(0);
                            tb_name.Text = reader_1.GetString(1);
                            tb_lastname.Text = reader_1.GetString(2);
                            GlobalVars.IDt = reader_1.GetInt32(4);
                            byte[] longBlobData = (byte[])reader_1["profileImage"];
                            using (var ms = new MemoryStream(longBlobData))
                            {
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = ms;
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.EndInit();

                                profile_img.Source = bitmapImage;
                            }
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }

                // Заполнение Гос номера
                string query_2 = "SELECT gosNum, IDmark, IDtypeT FROM transports WHERE IDt = @IDt";
                MySqlCommand command_2 = new(query_2, conn);

                command_2.Parameters.AddWithValue("@IDt", GlobalVars.IDt);

                using (MySqlDataReader reader_2 = command_2.ExecuteReader())
                {
                    while (reader_2.Read())
                    {
                        try
                        {
                            tb_gosNumber.Text = reader_2.GetString(0);
                            GlobalVars.IDmark = reader_2.GetInt32(1);
                            GlobalVars.IDtypeT = reader_2.GetInt32(2);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }

                // Заполнение Марки машины
                string query_4 = "SELECT IFNULL(mark, '') AS mark FROM marks WHERE IDmark = @IDmark";
                MySqlCommand command_4 = new(query_4, conn);

                command_4.Parameters.AddWithValue("@IDmark", GlobalVars.IDmark);

                using (MySqlDataReader reader_4 = command_4.ExecuteReader())
                {
                    if (reader_4.HasRows)
                    {
                        while (reader_4.Read())
                        {
                            tb_mark.Text = reader_4.GetString(0);
                        }
                    }
                }

                // Заполнение типа машины
                string query_6 = "SELECT IFNULL(typeT, '') AS typeT FROM type_transport WHERE IDtypeT = @IDtypeT";
                MySqlCommand command_6 = new(query_6, conn);

                command_6.Parameters.AddWithValue("@IDtypeT", GlobalVars.IDtypeT);

                using (MySqlDataReader reader_6 = command_6.ExecuteReader())
                {
                    if (reader_6.HasRows)
                    {
                        while (reader_6.Read())
                        {
                            tb_kindTransport.Text = reader_6.GetString(0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Сохранение информации о пользователе
        /// </summary>
        private void SaveInfo()
        {
            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                // Сохранение в БД фамилии, имени, отчества
                string query_1 = "UPDATE driver SET surname = @surname, name = @name, patronymic = @patronymic WHERE IDdriver = @IDdriver";
                MySqlCommand command_1 = new(query_1, conn);

                command_1.Parameters.AddWithValue("@IDdriver", GlobalVars.ID_driver);
                command_1.Parameters.AddWithValue("@surname", tb_surname.Text);
                command_1.Parameters.AddWithValue("@name", tb_name.Text);
                command_1.Parameters.AddWithValue("@patronymic", tb_lastname.Text);

                conn.Open();
                command_1.ExecuteNonQuery();

                // Сохранение в БД гос номера
                string query_2 = "UPDATE transports SET gosNum = @gosNum WHERE IDt = @IDt";
                MySqlCommand command_2 = new(query_2, conn);

                command_2.Parameters.AddWithValue("@IDt", GlobalVars.IDt);
                command_2.Parameters.AddWithValue("@gosNum", tb_gosNumber.Text);

                command_2.ExecuteNonQuery();

                // Сохранение в БД марки автомобиля
                string query_3 = "UPDATE marks SET mark = @mark WHERE IDmark = @IDmark";
                MySqlCommand command_3 = new(query_3, conn);

                command_3.Parameters.AddWithValue("@IDmark", GlobalVars.IDmark);
                command_3.Parameters.AddWithValue("@mark", tb_mark.Text);

                command_3.ExecuteNonQuery();

                // Сохранение в БД типа транспорта
                string query_4 = "UPDATE type_transport SET typeT = @typeT WHERE IDtypeT = @IDtypeT";
                MySqlCommand command_4 = new(query_4, conn);

                command_4.Parameters.AddWithValue("@IDtypeT", GlobalVars.IDtypeT);
                command_4.Parameters.AddWithValue("@typeT", tb_kindTransport.Text);

                command_4.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Возможность изменения данных пользователя
        /// </summary>
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            tb_surname.IsReadOnly = false;
            tb_name.IsReadOnly = false;
            tb_lastname.IsReadOnly = false;
            tb_gosNumber.IsReadOnly = false;
            tb_mark.IsReadOnly = false;
            tb_kindTransport.IsReadOnly = false;

            uploadImg_btn.IsEnabled = true;

            exit_btn.IsEnabled = false;
            edit_btn.IsEnabled = false;
        }
        /// <summary>
        /// Сохранение всех данных и выход из личного кабинета на главную страницу
        /// </summary>
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){1}\d{3}(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){2}$";

            string name = @"\w[А-я]$";

            if (Regex.IsMatch(tb_surname.Text, name) && tb_surname.Text.Length != 0)
            {
                if (Regex.IsMatch(tb_name.Text, name) && tb_name.Text.Length != 0)
                {
                    if (Regex.IsMatch(tb_lastname.Text, name))
                    {
                        if (tb_gosNumber.Text.Length == 0)
                        {
                            MessageBox.Show("Введите гос.номер автомобиля!");
                        }
                        else
                        {
                            try
                            {
                                if (Regex.IsMatch(tb_gosNumber.Text, pattern) && tb_gosNumber.Text.Length == 6)
                                {
                                    MessageBox.Show("Номер автомобиля введен правильно.");

                                    SaveInfo();

                                    tb_surname.IsReadOnly = true;
                                    tb_name.IsReadOnly = true;
                                    tb_lastname.IsReadOnly = true;
                                    tb_gosNumber.IsReadOnly = true;
                                    tb_mark.IsReadOnly = true;
                                    tb_kindTransport.IsReadOnly = true;

                                    uploadImg_btn.IsEnabled = false;

                                    exit_btn.IsEnabled = true;
                                    edit_btn.IsEnabled = true;

                                    MainPage mainPage = new();
                                    mainPage.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Номер автомобиля введен неправильно. Введите номер в формате X123XX. В номере может быть только 6 символов.");
                                    tb_gosNumber.Text = null;
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Введите номер в формате X123XX!!!");
                                tb_gosNumber.Text = null;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Отчество может содержать только русские буквы");
                        tb_lastname.Text = null;
                    }
                }
                else
                {
                    MessageBox.Show("Имя может содержать только русские буквы");
                    tb_name.Text = null;
                }
            }
            else
            {
                MessageBox.Show("Фамилия может содержать только русские буквы");
                tb_surname.Text = null;
            } 
        }
        /// <summary>
        /// Выход из данного профиля, возврат к окну авторизации
        /// </summary>
        /// 

        private void AccountLoginNotification(string login)
        {
            // Получение времени подключения
            DateTime loginTime = Process.GetCurrentProcess().StartTime;

            // Получение информации о системе
            OperatingSystem os = Environment.OSVersion;
            Version version = Environment.Version;

            string log = "Sign out: " + login + " signed out in at" + loginTime.ToString() + " from " + os.ToString() + " " + version.ToString();

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
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
        /// <summary>
        /// Загрузка нового изображения профиля
        /// </summary>
        private void uploadImg_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (openFileDialog.ShowDialog() == true)
            {
                profile_img.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                using (MySqlConnection conn = new(GlobalVars.ConnString))
                {
                    string query = "UPDATE driver SET profileImage = @profileImage WHERE IDdriver = @IDdriver";
                    MySqlCommand command = new(query, conn);

                    command.Parameters.AddWithValue("@IDdriver", GlobalVars.ID_driver);
                    byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                    command.Parameters.AddWithValue("@profileImage", imageData);

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }

            
        }
    }
}
