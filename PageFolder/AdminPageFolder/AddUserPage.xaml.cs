using Project.ClassFolder;
using Project.WindowFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Project.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        CBClass cBClass;
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=PrilutskiyProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        public AddUserPage()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.LoadCB(RoleCb, CBClass.CBType.Role);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordTb.Text;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Некоректный логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Некоректный пароль");
                PasswordTb.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать заглавную букву");
                PasswordTb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать маленькую букву");
                PasswordTb.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать цифру");
                PasswordTb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "один из этих знаков " + znak);
                PasswordTb.Focus();
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Не выбрана роль");
                RoleCb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[User] " +
                    "(UserLogin, UserPassword, IdRole) " +
                    $"Values ('{LoginTb.Text}'," +
                    $"'{PasswordTb.Text}'," +
                    $"'{RoleCb.SelectedValue.ToString()}')",
                    sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Пользователь " +
                    $"успешно добавлен");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AdminPage());
        }
    }
}
