using Project.ClassFolder;
using Project.PageFolder.EmployeeFolder;
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

namespace Project.PageFolder.ServiceFolder
{
    /// <summary>
    /// Логика взаимодействия для ServiceAdminPage.xaml
    /// </summary>
    public partial class ServiceAdminPage : Page
    {
        DGClass dGClass;
        public ServiceAdminPage()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AddServicePage());
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new VacationerFolder.VacationerAdminPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.ReceptionView " +
                $"Where ServiceName Like '%{SearchTb.Text}%'");
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
                    VarialbleClass.IdReception = dGClass.SelectId();
                    StartWindow.OpenPage(new EditServicePage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.ReceptionView");
        }

        private void AddTypeIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AddServiceTypePage());
        }
    }
}
