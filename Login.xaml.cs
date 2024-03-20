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
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    /// 

    public partial class Login : Window
    {
        DataBase dataBase = new DataBase();

        private const string FilePath = "UserData.txt"; // Путь к файлу с данными пользователей

        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = UsernameTextBox.Text;
            string passUser = PasswordBox.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1 ) 
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

                Calc calcWindow = new Calc();
                calcWindow.Show(); // Переход на окно Calc
                this.Close(); // Закрытие текущего окна Login
            }
            else
            {
                MessageBox.Show("Такого аккаунта нет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (AuthenticateUser(loginUser, passUser))
            {
                SuccessPopup.IsOpen = true;

                Calc calcWindow = new Calc();
                calcWindow.Show(); // Переход на окно Calc
                this.Close(); // Закрытие текущего окна Login
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }




        private bool AuthenticateUser(string loginUser, string passUser)
        {            // Здесь вы можете реализовать ваш собственный механизм проверки логина и пароля. 
            // В этом примере предполагается, что данные пользователей хранятся в текстовом файле. 
            string[] lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                string[] userData = line.Split(';');
                if (userData.Length == 2 && userData[0] == loginUser && userData[1] == passUser)
                {
                    return true;
                }
            }
            return false;
        }

        private void RegisterUser(string loginUser, string passUser)
        {            // Записываем данные пользователя в файл 
            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.WriteLine($"{loginUser};{passUser}");
            }
        }
    }
}
