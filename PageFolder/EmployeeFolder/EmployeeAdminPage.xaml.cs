using Project.ClassFolder;
using Project.WindowFolder;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.PageFolder.EmployeeFolder
{
    /// <summary>
    /// Логика взаимодействия для ReceptionAdminPage.xaml
    /// </summary>
    public partial class EmployeeAdminPage : Page
    {
        DGClass dG;
        public EmployeeAdminPage()
        {
            InitializeComponent();
            dG = new DGClass(ListUserDG);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dG.LoadDG("SELECT * From dbo.EmployeeView");
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AuthorizationPage());
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AddEmployeePage());
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VarialbleClass.UserId = dG.SelectId();
                    StartWindow.OpenPage(new EditEmployeePage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.LoadDG($"SELECT * From dbo.EmployeeView Where EmployeeName Like '%{SearchTb.Text}%'");
        }
    }
}
