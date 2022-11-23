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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        CBClass cBClass;
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=PrilutskiyProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        public AddServicePage()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            if(RoleCb.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите отидыхающего");
                RoleCb.Focus();
            }
            else if(RoleCb2.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите услугу");
                RoleCb2.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[Reception] " +
                    "(IdVacationer,IdService) " +
                    $"Values ('{RoleCb.SelectedValue.ToString()}'," +
                    $"'{RoleCb2.SelectedValue.ToString()}')",
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

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new ServiceAdminPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.LoadCB(RoleCb, CBClass.CBType.Vacationer);
            cBClass.LoadCB(RoleCb2, CBClass.CBType.Service);
        }
    }
}
