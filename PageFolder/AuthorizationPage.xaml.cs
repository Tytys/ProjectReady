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
using System.Xml;

namespace Project.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=ASUS583\SQLEXPRESS;" +
            "Initial Catalog=Prilutskiy;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        XmlDocument load;
        XmlElement xmlElement;

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "SELECT * From dbo.[User] " +
                    $"Where UserLogin='{LoginTb.Text}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                if (dataReader[2].ToString() != PasswordTb.Text)
                {
                    MBClass.ErrorMB("Неверный пароль");
                    PasswordTb.Focus();
                }
                else
                {
                    switch (dataReader[3].ToString())
                    {
                        case "1":
                            MBClass.InfoMB("Администратор");
                            break;
                        case "2":
                            MBClass.InfoMB("Гость");
                            break;
                    }
                }
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

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new RegistrationPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
