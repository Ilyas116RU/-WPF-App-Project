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
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBase dataBase = new DataBase();

        private const string FilePath = "UserData.txt"; // Путь к файлу с данными пользователей 
        public MainWindow()
        {
            InitializeComponent();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            Login log = new Login();
            log.Show();
            this.Close();

        }
        private bool AuthenticateUser(string username, string password)
        {            // Здесь вы можете реализовать ваш собственный механизм проверки логина и пароля. 
            // В этом примере предполагается, что данные пользователей хранятся в текстовом файле. 
            string[] lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                string[] userData = line.Split(';');
                if (userData.Length == 2 && userData[0] == username && userData[1] == password)
                {
                    return true;
                }
            }
            return false;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            Register register = new Register();
            register.Show();
            this.Close();
            

        }


        private void RegisterUser(string username, string password)
        {            // Записываем данные пользователя в файл 
            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.WriteLine($"{username};{password}");
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Show();
            this.Close();
         
        }
    }
}
