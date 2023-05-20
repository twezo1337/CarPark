using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
/*using System.Data.Odbc; // NEED TO BE INSTALLED*/
using MySql.Data.MySqlClient;

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
            
           

            

            /*string connectionString = "FILEDSN=C:\\Users\\whoami\\source\\repos\\CarPark\\CarPark\\App_Data\\zi_connection.dsn";
            string query = "INSERT INTO log_in (login, password) VALUES ('admin', 'admin');";

            using (OdbcConnection connection = new(connectionString))
            {
                connection.Open();

                using (OdbcCommand command = new(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
*/
            InitializeComponent();
        }

        private MySqlConnection connectMySQL()
        {
            String connString = "Server=" + host + ";Database=" + database
               + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*MySqlConnection conn = connectMySQL();

            conn.Open();

            string query = "SELECT marks.mark FROM marks";



                using (MySqlCommand command = new(query, conn))
                {
                    command.ExecuteNonQuery();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                        tbLogin.AppendText(reader.GetString(0));
                        tbPassword.AppendText(reader.GetString(0));
                        }

                    }
                }*/

        }
    }
}
