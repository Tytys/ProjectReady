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

namespace Project.PageFolder.ServiceFolder
{
    /// <summary>
    /// Логика взаимодействия для AddServiceTypePage.xaml
    /// </summary>
    public partial class AddServiceTypePage : Page
    {
        CBClass cBClass;
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=PrilutskiyProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        public AddServiceTypePage()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new ServiceAdminPage());
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RoleCb.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите работника");
                RoleCb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите название услуги");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Введите цену услуги");
                PasswordTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[Service] " +
                    "(ServiceName,ServicePrice,IdEmployee) " +
                    $"Values ('{LoginTb.Text}', " +
                    $"'{PasswordTb.Text}', " +
                    $"'{RoleCb.SelectedValue.ToString()}')",
                    sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Услуга " +
                    $"успешно добавлена");
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
            cBClass.LoadCB(RoleCb, CBClass.CBType.Employee);
        }
    }
}
