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
using System.Data.SQLite;
using System.Data;

namespace DOT_Logs
{
    // Created by Pratik Chougule - Final Year B.Tech CST , DOT-SUK - Year 2015-16
    public partial class ViewLogPage : Page
    {
        SQLiteConnection connection;
        string SqlQuery;
        public ViewLogPage()
        {
            InitializeComponent();
            SqlQuery = @"SELECT RollNo AS 'Roll No',ClassName AS 'Class',ProgrammeName 'Branch',EquipmentName AS 'Equipment Name',IssueDate AS 'Issue Date',IssueTime AS 'Issue Time',ReturnDate AS 'Return Date',ReturnTime AS 'Return Time'" +
                        "FROM StudentLog";
            this.createDatabaseConnecion();
            this.UpdateDataGrid();
        }
        public void createDatabaseConnecion()
        {
            connection = new SQLiteConnection("Data Source=StudentLog.sqlite;Version=3;");
            connection.Open();

        }
        public void closeDatabaseConnection()
        {
            connection.Close();
        }
        public void backButtonClicked(object sender, RoutedEventArgs e)
        {
            WelcomePage welcome = new WelcomePage();
            this.NavigationService.Navigate(welcome);
        }
        private void UpdateDataGrid()
        {
            try
            {
                DataSet dataSet = new DataSet();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SqlQuery, connection);
                dataAdapter.Fill(dataSet);
                dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
                this.closeDatabaseConnection();
            }
            catch(SQLiteException error)
            {
                Console.WriteLine(error.Message);
                MessageBox.Show(error.Message, "Error");
                this.closeDatabaseConnection();
            }
            
        }
    }
}
