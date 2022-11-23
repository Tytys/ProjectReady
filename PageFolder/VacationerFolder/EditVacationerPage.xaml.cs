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

namespace Project.PageFolder.VacationerFolder
{
    /// <summary>
    /// Логика взаимодействия для EditVacationerPage.xaml
    /// </summary>
    public partial class EditVacationerPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=PrilutskiyProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        private string idUser;
        public EditVacationerPage()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new VacationerAdminPage());
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
            else if (string.IsNullOrWhiteSpace(SecondNameTb.Text))
            {
                MBClass.ErrorMB("Введите отчество");
                SecondNameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(LastNameTb.Text))
            {
                MBClass.ErrorMB("Введите фамилию");
                LastNameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PeriodTb.Text))
            {
                MBClass.ErrorMB("Введите период");
                PeriodTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(DateInTb.Text))
            {
                MBClass.ErrorMB("Введите дату въезда");
                DateInTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(DateOutTb.Text))
            {
                MBClass.ErrorMB("Введите дату отъезда");
                DateOutTb.Focus();
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
                        $"UserPassword='{PasswordTb.Text}' " +
                        $"Where UserId='{idUser}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.Vacationer " +
                        $"Set IdUser='{idUser}'," +
                        $"VacationerPeriodSatying='{PeriodTb.Text}'," +
                        $"VacationerDateIn='{DateInTb.Text}'," +
                        $"VacationerDateOut='{DateOutTb.Text}'," +
                        $"VacationerName='{NameTb.Text}'," +
                        $"VacationerSecondName='{SecondNameTb.Text}'," +
                        $"VacationerLastName='{LastNameTb.Text}' " +
                        $"Where IdVacationer='{VarialbleClass.IdVacationer}'",
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.Vacationer " +
                    $"Where IdVacationer='{VarialbleClass.IdVacationer}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                idUser = dataReader[1].ToString();
                PeriodTb.Text = dataReader[2].ToString();
                DateInTb.Text = dataReader[3].ToString();
                DateOutTb.Text = dataReader[4].ToString();
                NameTb.Text = dataReader[5].ToString();
                SecondNameTb.Text = dataReader[6].ToString();
                LastNameTb.Text = dataReader[7].ToString();
                dataReader.Close();
                sqlCommand = new SqlCommand("Select * From dbo.[User] " +
                    $"Where UserId='{idUser}'", sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
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
}
