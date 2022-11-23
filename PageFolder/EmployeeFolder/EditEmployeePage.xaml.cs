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

namespace Project.PageFolder.EmployeeFolder
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeePage.xaml
    /// </summary>
    public partial class EditEmployeePage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=PrilutskiyProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cB;
        SqlDataReader dataReader;
        public EditEmployeePage()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cB.LoadCB(RoleChB, CBClass.CBType.Role);
            cB.LoadCB(PostChB, CBClass.CBType.Post);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.Employee " +
                    $"Where IdEmployee='{VarialbleClass.UserId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                PostChB.SelectedValue = dataReader[2].ToString();
                NameTb.Text = dataReader[3].ToString();
                SurnameTb.Text = dataReader[4].ToString();
                sqlCommand = new SqlCommand("Select * From dbo.[User] " +
                    $"Where UserId='{dataReader[1]}'", sqlConnection);
                dataReader.Close();
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                RoleChB.SelectedValue = dataReader[3].ToString();
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

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string znak = "!@#$%^&";
            string cif = "1234567890";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string bol = "QWERTYUIOPASDFGHJKLZXCVBNM";

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMB("Введите имя");
                NameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(SurnameTb.Text))
            {
                MBClass.ErrorMB("Введите фамилию");
                SurnameTb.Focus();
            }
            else if (PostChB.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите должность");
                PostChB.Focus();
            }
            else if (RoleChB.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите роль");
                RoleChB.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(znak.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " !@#$%^&");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(cif.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " цифру");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(mal.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " строчную букву");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(bol.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " заглавную букву");
                PasswordTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.[User] " +
                        $"Set UserLogin ='{LoginTb.Text}'," +
                        $"UserPassword='{PasswordTb.Text}'," +
                        $"IdRole='{RoleChB.SelectedValue.ToString()}' " +
                        $"Where UserId='{VarialbleClass.UserId}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.Employee " +
                        $"Set IdPost='{PostChB.SelectedValue.ToString()}'," +
                        $"EmployeeName='{NameTb.Text}'," +
                        $"EmployeeSurname='{SurnameTb.Text}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Данные работника " +
                        $"успешно отредактированы");
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
            StartWindow.OpenPage(new EmployeeAdminPage());
        }
    }
}
