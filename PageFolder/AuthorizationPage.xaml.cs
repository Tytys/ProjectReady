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
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=PrilutskiyProject;" +
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
                            StartWindow.OpenPage(new AdminPageFolder.AdminPage());
                            ChangeXML();
                            break;
                        case "2":
                            StartWindow.OpenPage(new VacationerFolder.VacationerAdminPage());
                            ChangeXML();
                            break;
                        case "3":
                            StartWindow.OpenPage(new ReceptionFolder.ReceptionPage());
                            ChangeXML();
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
            load = new XmlDocument();
            load.Load(@"C:\Users\ififi\Source\Repos\Project\ResourceFolder\LoadFile.xml");
            xmlElement = load.DocumentElement;
            if (xmlElement != null)
            {
                foreach (XmlNode node in xmlElement)
                {
                    if (node?.FirstChild.InnerText == "1")
                    {
                        RememberChB.IsChecked = true;
                    }
                    else if (node?.FirstChild.InnerText == "0")
                    {
                        break;
                    }
                    if (node.Name == "user")
                    {
                        foreach (XmlElement el in node)
                        {
                            if (el?.Name == "login")
                            {
                                LoginTb.Text = el.InnerText;
                            }
                            if (el?.Name == "password")
                            {
                                PasswordTb.Text = el.InnerText;
                            }
                        }
                    }
                }
            }
        }

        private void ChangeXML()
        {
            load = new XmlDocument();
            load.Load(@"C:\Users\ififi\Source\Repos\Project\ResourceFolder\LoadFile.xml");
            xmlElement = load.DocumentElement;
            bool Check = false;
            if (xmlElement != null)
            {
                foreach (XmlNode node in xmlElement)
                {
                    if (RememberChB.IsChecked == false)
                    {
                        node.FirstChild.InnerText = "0";
                        break;
                    }
                    else if(RememberChB.IsChecked == true)
                    {
                        node.FirstChild.InnerText = "1";
                        Check = true;
                    }
                    if(Check)
                    {
                        if (node.Name == "user")
                        {
                            foreach (XmlElement el in node)
                            {
                                if (el?.Name == "login")
                                {
                                    el.InnerText = LoginTb.Text;
                                }
                                if (el?.Name == "password")
                                {
                                    el.InnerText = PasswordTb.Text;
                                }
                            }
                        }
                    }
                }
            }
            load.Save(@"C:\Users\ififi\Source\Repos\Project\ResourceFolder\LoadFile.xml");
        }
    }
}
