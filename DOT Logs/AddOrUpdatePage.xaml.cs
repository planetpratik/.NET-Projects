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

namespace DOT_Logs
{
    /// <summary>
    /// Interaction logic for AddOrUpdatePage.xaml
    /// </summary>
    public partial class AddOrUpdatePage : Page
    {
        Boolean isDatabaseAlreadyCreated = false;
        SQLiteConnection connection;
        public AddOrUpdatePage()
        {
            InitializeComponent();
            this.loadComboBoxValues();
        }

        public void initDatabase()
        {
            SQLiteConnection.CreateFile("StudentLog.sqlite");
            isDatabaseAlreadyCreated = true;
            

        }

        public void createDatabaseConnecion()
        {
            connection = new SQLiteConnection("Data Source=StudentLog.sqlite;Version=3;");
            connection.Open();

        }

        public void createTable()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [StudentLog] (
                          [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [StudName] VARCHAR(30) NOT NULL,
                          [ClassName] VARCHAR(15) NOT NULL,
                          [ProgrammeName] VARCHAR(50) NOT NULL,
                          [BatchName] VARCHAR(2) NOT NULL,
                          [SemesterName] VARCHAR(3) NOT NULL,
                          [SubjectName] VARCHAR(30) NULL,
                          [EquipmentName] VARCHAR(35) NULL
                          [Date] DATE NULL
                          )";
            SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection);
            createTableCommand.ExecuteNonQuery();
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

        public void addNewEntryButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!isDatabaseAlreadyCreated)
            {
                this.initDatabase();
            }

            this.createDatabaseConnecion();
            this.createTable();
            string addNewEntryQuery = @"INSERT INTO StudentLog(
                                        StudName,ClassName,ProgrammeName,
                                        BatchName,SemesterName,SubjectName,
                                        EquipmentName,Date) 
                                        Values ('"+nameOfTheStudentTextbox.Text+"',"
                                        +"'"+nameOfClassCombobox.Text+"',"
                                        +"'"+nameOfProgrammeComboBox.Text+"',"
                                        +"'"+batchCombobox.Text+"',"
                                        +"'"+semesterComboBox.Text+"',"
                                        +"'"+nameOfSubjectCombobox.Text+"',"
                                        +"'"+nameOfTheEquipmentComboBox.Text+"',"
                                        +"'"+datePicker.Text+"')";
            SQLiteCommand addNewEntryCommand = new SQLiteCommand(addNewEntryQuery, connection);
            addNewEntryCommand.ExecuteNonQuery();

        }

        public void loadComboBoxValues()
        {
            nameOfProgrammeComboBox.Items.Add("Computer Science & Technology");
            nameOfProgrammeComboBox.Items.Add("Electronics & Telecommunication Technology ");
            nameOfProgrammeComboBox.Items.Add("Chemical Technology");
            nameOfProgrammeComboBox.Items.Add("Civil Technology");
            nameOfProgrammeComboBox.Items.Add("Food Technology");
            nameOfProgrammeComboBox.Items.Add("Mechanical Technology");

            nameOfClassCombobox.Items.Add("First Year B.Tech");
            nameOfClassCombobox.Items.Add("Second Year B.Tech");
            nameOfClassCombobox.Items.Add("Third Year B.Tech");
            nameOfClassCombobox.Items.Add("Final Year B.Tech");

            batchCombobox.Items.Add("B1");
            batchCombobox.Items.Add("B2");
            batchCombobox.Items.Add("B3");
            batchCombobox.Items.Add("B4");

        }

        public void nameOfClassChanged(object sender, EventArgs e)
        {
            semesterComboBox.Items.Clear();
            if ((string)nameOfClassCombobox.SelectedItem == "First Year B.Tech")
            {
                semesterComboBox.Items.Add("Semester I");
                semesterComboBox.Items.Add("Semester II");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech")
            {
                semesterComboBox.Items.Add("Semester III");
                semesterComboBox.Items.Add("Semester IV");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech")
            {
                semesterComboBox.Items.Add("Semester V");
                semesterComboBox.Items.Add("Semester VI");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech")
            {
                semesterComboBox.Items.Add("Semester VII");
                semesterComboBox.Items.Add("Semester VIII");
            }
        }
    }
}
