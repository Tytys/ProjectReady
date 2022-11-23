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

namespace Project.PageFolder.VacationerFolder
{
    /// <summary>
    /// Логика взаимодействия для VacationerAdminPage.xaml
    /// </summary>
    public partial class VacationerAdminPage : Page
    {
        DGClass dG;
        public VacationerAdminPage()
        {
            InitializeComponent();
            dG = new DGClass(ListUserDG);
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
                    VarialbleClass.IdVacationer = dG.SelectId();
                    StartWindow.OpenPage(new EditVacationerPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.LoadDG("Select * From dbo.VacationerView " +
                $"Where VacationerName Like '%{SearchTb.Text}%'");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AddVacationerPage());
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new AuthorizationPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dG.LoadDG("Select * From dbo.VacationerView");
        }

        private void ReceptionIm_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.OpenPage(new ServiceFolder.ServiceAdminPage());
        }
    }
}
