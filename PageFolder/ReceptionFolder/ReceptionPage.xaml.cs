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

namespace Project.PageFolder.ReceptionFolder
{
    /// <summary>
    /// Логика взаимодействия для ReceptionPage.xaml
    /// </summary>
    public partial class ReceptionPage : Page
    {
        ClassFolder.DGClass dG;
        public ReceptionPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dG = new ClassFolder.DGClass(ListUserDG);
            dG.LoadDG("Select * From dbo.ServiceView");
            dG = new ClassFolder.DGClass(ListUserDG2);
            dG.LoadDG("Select IdEmployee,PostName," +
                "EmployeeName,EmployeeSurname From dbo.EmployeeView");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG = new ClassFolder.DGClass(ListUserDG);
            dG.LoadDG("Select * From dbo.ServiceView " +
                $"Where ServiceName Like '%{SearchTb.Text}%'");
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            WindowFolder.StartWindow.OpenPage(new PageFolder.AuthorizationPage());
        }

        private void SearchTb2_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG = new ClassFolder.DGClass(ListUserDG2);
            dG.LoadDG("Select IdEmployee,PostName," +
                 "EmployeeName,EmployeeSurname From dbo.EmployeeView " +
                 $"Where EmployeeName Like '%{SearchTb2.Text}%'");
        }
    }
}
