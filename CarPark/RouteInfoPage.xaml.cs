using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
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
    /// Логика взаимодействия для RouteInfoPage.xaml
    /// </summary>
    public partial class RouteInfoPage : Window
    {
        public RouteInfoPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            log_label.Content = GlobalVars.Login;

            using (MySqlConnection conn = new(GlobalVars.ConnString))
            {
                string query = "SELECT surname, name FROM driver";
                MySqlCommand command = new(query, conn);
                conn.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tb_driver.Items.Add(reader.GetString(0) + " " + reader.GetString(1));
                    }
                }
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){1}\d{3}(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){2}$";

            string name = @"\w[А-я]$";

            if (Regex.IsMatch(tb_mark.Text, name) && tb_mark.Text.Length != 0)
            {
                string mark = tb_mark.Text;
                if (Regex.IsMatch(tb_kindTransport.Text, name) && tb_kindTransport.Text.Length != 0)
                {
                    string kindTransport = tb_kindTransport.Text;
                    if (Regex.IsMatch(tb_routeName.Text, name))
                    {
                        string routeName = tb_routeName.Text;
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

                                    string gosNomer = tb_gosNumber.Text;

                                    string routeLength = tb_routeLength.Text;
                                    string rasprDate = tb_rasprDate.Text;
                                    string driver = tb_driver.Text;
                                    string routeDate = tb_routeDate.Text;
                                    string pasCount = tb_pasCount.Text;

                                    // sql req
                                    

                                    using (MySqlConnection conn = new(GlobalVars.ConnString))
                                    {
                                        string query_1 = "INSERT INTO distribution (dateDis) VALUES (@dateDis)";
                                        MySqlCommand command_1 = new(query_1, conn);
                                        command_1.Parameters.AddWithValue("@dateDis", routeDate);
                                        conn.Open();
                                        command_1.ExecuteNonQuery();

                                        string query_2 = "INSERT INTO transportation (numPas) VALUES (@numPas)";
                                        MySqlCommand command_2 = new(query_2, conn);
                                        command_2.Parameters.AddWithValue("@numPas", pasCount);
                                        command_2.ExecuteNonQuery();

                                        /*string query_3 = "INSERT INTO transportation (numPas) VALUES (@numPas)";
                                        MySqlCommand command_3 = new(query_3, conn);
                                        command_2.Parameters.AddWithValue("@numPas", pasCount);
                                        command_2.ExecuteNonQuery();*/
                                    }

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
                        MessageBox.Show("Маршрут может содержать только русские буквы");
                        tb_routeName.Text = null;
                    }
                }
                else
                {
                    MessageBox.Show("Тип транспорта может содержать только русские буквы");
                    tb_kindTransport.Text = null;
                }
            }
            else
            {
                MessageBox.Show("Марка может содержать только русские буквы");
                tb_mark.Text = null;
            }
        }
    }
}
