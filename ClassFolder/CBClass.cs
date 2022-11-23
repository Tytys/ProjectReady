using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection = new SqlConnection(
             @"Data Source=(local)\SQLEXPRESS;" +
             "Initial Catalog=PrilutskiyProject;" +
             "Integrated Security=True");
        SqlDataAdapter sqlData;
        DataSet dataSet;

        public enum CBType
        {
            Role,
            Post,
            Service,
            Vacationer,
            Employee
        }

        private void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdRole, RoleName " +
                    "From dbo.[Role] Order by IdRole ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Role]");
                comboBox.ItemsSource = dataSet.Tables["[Role]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Role]"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Role]"].Columns["IdRole"].ToString();
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
        
        private void ServiceNameCbLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdService, ServiceName " +
                    "From dbo.[Service] Order by IdService ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Service]");
                comboBox.ItemsSource = dataSet.Tables["[Service]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Service]"].Columns["ServiceName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Service]"].Columns["IdService"].ToString();
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

        private void VacationerNameCbLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdVacationer, VacationerName " +
                    "From dbo.[Vacationer] Order by IdVacationer ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Vacationer]");
                comboBox.ItemsSource = dataSet.Tables["[Vacationer]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Vacationer]"].Columns["VacationerName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Vacationer]"].Columns["IdVacationer"].ToString();
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
        private void EmployeeNameCbLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdEmployee, EmployeeName " +
                    "From dbo.[Employee] Order by IdEmployee ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Employee]");
                comboBox.ItemsSource = dataSet.Tables["[Employee]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Employee]"].Columns["EmployeeName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Employee]"].Columns["IdEmployee"].ToString();
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

        private void PostCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdPost, PostName " +
                    "From dbo.[Post] Order by IdPost ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Post]");
                comboBox.ItemsSource = dataSet.Tables["[Post]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Post]"].Columns["PostName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Post]"].Columns["IdPost"].ToString();
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

        public void LoadCB(ComboBox comboBox, CBType type)
        {
            switch (type)
            {
                case CBType.Role:
                    RoleCBLoad(comboBox);
                    break;
                case CBType.Post:
                    PostCBLoad(comboBox);
                    break;
                case CBType.Service:
                    ServiceNameCbLoad(comboBox);
                    break;
                case CBType.Vacationer:
                    VacationerNameCbLoad(comboBox);
                    break;
                case CBType.Employee:
                    EmployeeNameCbLoad(comboBox);
                    break;
            }
        }
    }
}
