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
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        DataBase dataBase = new DataBase();

        private const string FilePath = "UserData.txt"; // Путь к файлу с данными пользователей 
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            string loginUser = UsernameTextBox.Text;
            string passUser = PasswordBox.Password;

            string querystring = $"insert into register(login_user, password_user) values('{loginUser}','{passUser}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            dataBase.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Пользователь зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                Login log = new Login();
                log.Show(); // Переход на окно 
                this.Close(); // Закрытие текущего окна 

            }
            else
            {
                MessageBox.Show("Аккаунт не создан!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            dataBase.closeConnection();

            if (!string.IsNullOrEmpty(loginUser) && !string.IsNullOrEmpty(passUser))
            {
                Regist(loginUser, passUser); MessageBox.Show("Пользователь зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                Login log = new Login();
                log.Show(); // Переход на окно 
                this.Close(); // Закрытие текущего окна 
            }
            else
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Boolean checkuser()
        {
            string loginUser = UsernameTextBox.Text;
            string passUser = PasswordBox.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring , dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if(dataTable.Rows.Count > 0)
            {
              MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        private void RegisterUser(string loginUser, string passUser)
        {
            throw new NotImplementedException();
        }

        private void Regist(string loginUser, string passUser)
        {            // Записываем данные пользователя в файл 
            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.WriteLine($"{loginUser};{passUser}");
            }
        }

      
    }
}
