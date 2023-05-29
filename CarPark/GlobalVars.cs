using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark
{
    public static class GlobalVars
    {
        static readonly string host = "localhost";
        static readonly string database = "zi";
        static readonly string port = "3306";
        static readonly string username = "root";
        static readonly string password = "qwerty";

        static string connString = "Server=" + GlobalVars.Host + ";Database=" + GlobalVars.Database
               + ";port=" + GlobalVars.Port + ";User Id=" + GlobalVars.Username + ";password=" + GlobalVars.Password;

        static string login;
        static int id_driver;
        static int id_t;
        static int id_mark;
        static int id_typeT;

        public static string Login
        {
            get { return login; }
            set { login = value; }
        }
        public static int ID_driver
        {
            get { return id_driver; }
            set { id_driver = value; }
        }
        public static int IDt
        {
            get { return id_t; }
            set { id_t = value; }
        }
        public static int IDmark
        {
            get { return id_mark; }
            set { id_mark = value; }
        }
        public static int IDtypeT
        {
            get { return id_typeT; }
            set { id_typeT = value; }
        }
        public static string Host
        {
            get { return host; }
        }
        public static string Database
        {
            get { return database; }
        }
        public static string Port
        {
            get { return port; }
        }
        public static string Username
        {
            get { return username; }
        }
        public static string Password
        {
            get { return password; }
        }
        public static string ConnString
        {
            get { return connString; }
        }
        /*public static string Password
        {
            get { return password; }
            set { password = value; }
        }
        public static string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public static string Name
        {
            get { return name; }
            set { name = value; }
        }
        public static string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }
        public static string GosNum
        {
            get { return gosNum; }
            set { gosNum = value; }
        }
        public static string Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        public static string ProfileImage
        {
            get { return profileImage; }
            set { profileImage = value; }
        }*/
    }
}
