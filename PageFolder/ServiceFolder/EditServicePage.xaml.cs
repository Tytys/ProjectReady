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
    /// Логика взаимодействия для EditServicePage.xaml
    /// </summary>
    public partial class EditServicePage : Page
    {
        CBClass cB;
        SqlConnection sqlConnection = new SqlConnection(
           @"Data Source=(local)\SQLEXPRESS;" +
           "Initial Catalog=PrilutskiyProject;" +
           "Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public EditServicePage()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new ServiceAdminPage());
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RoleCb.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите отидыхающего");
                RoleCb.Focus();
            }
            else if (RoleCb2.SelectedIndex < 0)
            {
                MBClass.ErrorMB("Выберите услугу");
                RoleCb2.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.[User] " +
                        $"Set IdVacationer ='{RoleCb.SelectedValue.ToString()}'," +
                        $"IdService='{RoleCb2.SelectedValue.ToString()}' " +
                        $"Where IdReception='{VarialbleClass.IdReception}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Данные услуги " +
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
            cB.LoadCB(RoleCb, CBClass.CBType.Vacationer);
            cB.LoadCB(RoleCb2, CBClass.CBType.Service);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.[Reception] " +
                    $"Where IdReception='{VarialbleClass.IdReception}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                RoleCb.SelectedValue = dataReader[1].ToString();
                RoleCb2.SelectedValue = dataReader[2].ToString();
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
