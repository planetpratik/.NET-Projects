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
        SQLiteConnection connection;
        int rowID;
        public AddOrUpdatePage()
        {
            InitializeComponent();
            this.loadComboBoxValues();
        }

        public void initDatabase()
        {
            if (!System.IO.File.Exists("StudentLog.sqlite"))
            {
                SQLiteConnection.CreateFile("StudentLog.sqlite");
            }                        

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
                          [RollNo] INTEGER(3) NOT NULL,
                          [ClassName] VARCHAR(15) NOT NULL,
                          [ProgrammeName] VARCHAR(50) NOT NULL,
                          [BatchName] VARCHAR(2) NOT NULL,
                          [SemesterName] VARCHAR(3) NOT NULL,
                          [SubjectName] VARCHAR(30) NULL,
                          [EquipmentName] VARCHAR(35) NULL,
                          [IssueDate] VARCHAR(20) NULL,
                          [IssueTime] VARCHAR(20) NULL,
                          [ReturnDate] VARCHAR(20) NULL,
                          [ReturnTime] VARCHAR(20) NULL
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
            this.initDatabase();
            this.createDatabaseConnecion();
            this.createTable();
            string addNewEntryQuery = @"INSERT INTO StudentLog(
                                        RollNo,ClassName,ProgrammeName,
                                        BatchName,SemesterName,SubjectName,
                                        EquipmentName,IssueDate,IssueTime,ReturnDate,ReturnTime) 
                                        Values ("+rollNoTextbox.Text+","
                                        +"'"+(string)nameOfClassCombobox.SelectedItem+"',"
                                        +"'"+(string)nameOfProgrammeComboBox.SelectedItem+"',"
                                        +"'"+(string)batchCombobox.SelectedItem+"',"
                                        +"'"+(string)semesterComboBox.SelectedItem+"',"
                                        +"'"+(string)nameOfSubjectCombobox.SelectedItem+"',"
                                        +"'"+nameOfTheEquipmentTextBox.Text+"',"
                                        +"'"+issueDatePicker.Text + "',"
                                        +"'"+issueTimeTextbox.Text + "',"
                                        +"'"+returnDatePicker.Text+"',"
                                        + "'"+returnTimeTextbox.Text+"')";
            Console.WriteLine(addNewEntryQuery);
            try
            {
                SQLiteCommand addNewEntryCommand = new SQLiteCommand(addNewEntryQuery, connection);
                addNewEntryCommand.ExecuteNonQuery();
                MessageBox.Show("New Entry Added Successfully", "Success !");
                this.closeDatabaseConnection();
            }
            catch(SQLiteException error)
            {
                Console.WriteLine(error.Message);
                MessageBox.Show(error.Message,"Insert Command Error");
                this.closeDatabaseConnection();
            }
           
        }
        public void searchButtonClicked(object sender, RoutedEventArgs e)
        {

        }
        public void clearButtonClicked(object sender, RoutedEventArgs e)
        {
            
            rollNoTextbox.Text = "";
            nameOfClassCombobox.Items.Clear();
            nameOfProgrammeComboBox.Items.Clear();
            batchCombobox.Items.Clear();
            semesterComboBox.Items.Clear();
            nameOfSubjectCombobox.Items.Clear();
            nameOfTheEquipmentTextBox.Text = "";
            issueDatePicker.Text = "";
            issueTimeTextbox.Text = "";
            returnDatePicker.Text = "";
            returnTimeTextbox.Text = "";
            this.loadComboBoxValues();
        }

        public void updateButtonClicked(object sender,RoutedEventArgs e)
        {
            this.createDatabaseConnecion();
            this.createTable();
            string updateEntryQuery = @"UPDATE StudentLog SET
                                        RollNo = " + rollNoTextbox.Text + ","
                                        + "ClassName = " + (string)nameOfClassCombobox.SelectedItem + ","
                                        + "ProgrammeName = " + (string)nameOfProgrammeComboBox.SelectedItem + ","
                                        + "BatchName = " + (string)batchCombobox.SelectedItem + ","
                                        + "SemesterName = " + (string)semesterComboBox.SelectedItem + ","
                                        + "SubjectName = " + (string)nameOfSubjectCombobox.SelectedItem + ","
                                        + "EquipmentName = " + nameOfTheEquipmentTextBox.Text + ","
                                        + "IssueDate = " + issueDatePicker.Text + ","
                                        + "IssueTime = " + issueTimeTextbox.Text + ","
                                        + "ReturnDate = " + returnDatePicker.Text + ","
                                        + "ReturnTime = " + returnTimeTextbox.Text
                                        + " WHERE ID = " + rowID;

            Console.WriteLine(updateEntryQuery);
            try
            {
                SQLiteCommand updateEntryCommand = new SQLiteCommand(updateEntryQuery, connection);
                updateEntryCommand.ExecuteNonQuery();
                MessageBox.Show("Table Entry Is Updated Successfully", "Success !");
                this.closeDatabaseConnection();
            }
            catch (SQLiteException error)
            {
                Console.WriteLine(error.Message);
                MessageBox.Show(error.Message, "Update Command Error");
                this.closeDatabaseConnection();
            }
    

        }

        public void removeExistingEntryButtonClicked(object sender, RoutedEventArgs e)
        {
            this.createDatabaseConnecion();
            this.createTable();
            string deleteEntryQuery = @"DELETE FROM StudentLog
                                        WHERE RollNo=" + rollNoTextbox.Text + "AND EquipmentName='" + nameOfTheEquipmentTextBox.Text + "'";

            Console.WriteLine(deleteEntryQuery);
            try
            {
                SQLiteCommand deleteEntryCommand = new SQLiteCommand(deleteEntryQuery, connection);
                deleteEntryCommand.ExecuteNonQuery();
            }
            catch (SQLiteException error)
            {
                Console.WriteLine(error.Message);
                MessageBox.Show(error.Message, "Remove Entry Command Error");
                this.closeDatabaseConnection();
            }
            this.closeDatabaseConnection();

        }

        public void viewLogButtonClicked(object sender, RoutedEventArgs e)
        {
            ViewLogPage viewLog = new ViewLogPage();
            this.NavigationService.Navigate(viewLog);
        }

        public void loadComboBoxValues()
        {
            nameOfProgrammeComboBox.Items.Add("Computer Science & Technology");
            nameOfProgrammeComboBox.Items.Add("Electronics & Telecommunication Technology");
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

        public void nameOfSubjectsToLoad(object sender, EventArgs e)
        {
            nameOfSubjectCombobox.Items.Clear();
            if ((string)nameOfClassCombobox.SelectedItem =="First Year B.Tech"&&(string)semesterComboBox.SelectedItem == "Semester I")
            {
               
                nameOfSubjectCombobox.Items.Add("Lab–I Engineering Chemistry");
                nameOfSubjectCombobox.Items.Add("Lab–I Engineering Physics");
                nameOfSubjectCombobox.Items.Add("Lab-II Professional Communication");
                nameOfSubjectCombobox.Items.Add("Lab-III Electronic Components & Devices");
                nameOfSubjectCombobox.Items.Add("Lab-IV Engineering Mechanics");
                nameOfSubjectCombobox.Items.Add("Lab-V Engineering Graphics");

            }
            else if ((string)nameOfClassCombobox.SelectedItem == "First Year B.Tech" && (string)semesterComboBox.SelectedItem == "Semester II")
            {
               
                nameOfSubjectCombobox.Items.Add("Lab–I Engineering Chemistry");
                nameOfSubjectCombobox.Items.Add("Lab–I Engineering Physics");
                nameOfSubjectCombobox.Items.Add("Lab-II Workshop Practice");
                nameOfSubjectCombobox.Items.Add("Lab-III Fundamentals Of Mechanical Engineering");
                nameOfSubjectCombobox.Items.Add("Lab-IV Fundamentals Of Civil Engineering");
                nameOfSubjectCombobox.Items.Add("Lab-V Fundamentals Of Electrical Engineering");
                nameOfSubjectCombobox.Items.Add("Lab-VI Computer Programming");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Computer Science & Technology" && (string)semesterComboBox.SelectedItem == "Semester III")
            {
                nameOfSubjectCombobox.Items.Add("Digital System And Microprocessor Lab");
                nameOfSubjectCombobox.Items.Add("Data Structures Lab");
                nameOfSubjectCombobox.Items.Add("Unix & Shell Programming");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Electronics & Telecommunication Technology" && (string)semesterComboBox.SelectedItem == "Semester III")
            {
                nameOfSubjectCombobox.Items.Add("Electrical Technology Laboratory");
                nameOfSubjectCombobox.Items.Add("Electronics Circuit Analysis & Design - I Laboratory");
                nameOfSubjectCombobox.Items.Add("Digital Techniques");
                nameOfSubjectCombobox.Items.Add("Programming Techniques - I Laboratory");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Chemical Technology" && (string)semesterComboBox.SelectedItem == "Semester III")
            {
                nameOfSubjectCombobox.Items.Add("Applied Chemistry - I Laboratory");
                nameOfSubjectCombobox.Items.Add("Particulate Technology Laboratory");
                nameOfSubjectCombobox.Items.Add("Process Fluid Mechanics Laboratory");
                nameOfSubjectCombobox.Items.Add("Programming Practices For Chemical Engineers Laboratory");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Civil Technology" && (string)semesterComboBox.SelectedItem == "Semester III")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Surveying");
                nameOfSubjectCombobox.Items.Add("Lab II - Strength Of Materials");
                nameOfSubjectCombobox.Items.Add("Lab III - Construction Technology");
                nameOfSubjectCombobox.Items.Add("Lab IV - Fluid Mechanics - I");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Food Technology" && (string)semesterComboBox.SelectedItem == "Semester III")
            {
                nameOfSubjectCombobox.Items.Add("Principles Of Food Preservation Lab");
                nameOfSubjectCombobox.Items.Add("Food Chemistry Lab");
                nameOfSubjectCombobox.Items.Add("Food Mictobiology Lab");
                nameOfSubjectCombobox.Items.Add("Process Fluid Mechanics Lab");
                nameOfSubjectCombobox.Items.Add("Programming Practices");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Mechanical Technology" && (string)semesterComboBox.SelectedItem == "Semester III")
            {
                nameOfSubjectCombobox.Items.Add("Power Lab");
                nameOfSubjectCombobox.Items.Add("Electrical Technology And Computer Programming C++");
                nameOfSubjectCombobox.Items.Add("Machine Drawing");
                nameOfSubjectCombobox.Items.Add("Fluid Mechanics Lab");
                nameOfSubjectCombobox.Items.Add("Workshop Practice - I");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Computer Science & Technology" && (string)semesterComboBox.SelectedItem == "Semester IV")
            {
                nameOfSubjectCombobox.Items.Add("Advanced Microprocessor Lab");
                nameOfSubjectCombobox.Items.Add("Computer Graphics Lab");
                nameOfSubjectCombobox.Items.Add("Object Oriented Lab");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Electronics & Telecommunication Technology" && (string)semesterComboBox.SelectedItem == "Semester IV")
            {
                nameOfSubjectCombobox.Items.Add("Electronics Circuit Analysis & Design - II Laboratory");
                nameOfSubjectCombobox.Items.Add("Communication Technology Laboratory");
                nameOfSubjectCombobox.Items.Add("Processor Architecture Laboratory");
                nameOfSubjectCombobox.Items.Add("Measurement Techniques Laboratory");
                nameOfSubjectCombobox.Items.Add("Programming Techniques - II Laboratory");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Chemical Technology" && (string)semesterComboBox.SelectedItem == "Semester IV")
            {
                nameOfSubjectCombobox.Items.Add("Applied Chemistry - II Laboratory");
                nameOfSubjectCombobox.Items.Add("Fundamentals And Applications Of Heat Transfer Laboratory");
                nameOfSubjectCombobox.Items.Add("Chromatography And Instrumental Methods Of Analysis Laboratory");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Civil Technology" && (string)semesterComboBox.SelectedItem == "Semester IV")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Engineering Geology");
                nameOfSubjectCombobox.Items.Add("Lab II - Fluid Mechanics");
                nameOfSubjectCombobox.Items.Add("Lab III - Concrete Technology");
                nameOfSubjectCombobox.Items.Add("Lab IV - Building Planning And Design");
               
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Food Technology" && (string)semesterComboBox.SelectedItem == "Semester IV")
            {
                nameOfSubjectCombobox.Items.Add("Food Process Engineering - I Lab");
                nameOfSubjectCombobox.Items.Add("Food Additives And Contaminants Lab");
                nameOfSubjectCombobox.Items.Add("Food Biochemistry Lab");
                nameOfSubjectCombobox.Items.Add("Human Nutrition Lab");
                nameOfSubjectCombobox.Items.Add("Fundamentals And Applications Of Heat Transfer Lab");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Second Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Mechanical Technology" && (string)semesterComboBox.SelectedItem == "Semester IV")
            {
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Computer Science & Technology" && (string)semesterComboBox.SelectedItem == "Semester V")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - System Programming");
                nameOfSubjectCombobox.Items.Add("Lab II - Computer Algorithms");
                nameOfSubjectCombobox.Items.Add("Lab III - Seminar I");
                nameOfSubjectCombobox.Items.Add("Lab IV - Java Programming");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Electronics & Telecommunication Technology" && (string)semesterComboBox.SelectedItem == "Semester V")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Linear Integrated Circuits");
                nameOfSubjectCombobox.Items.Add("Lab II - Microcontrollers");
                nameOfSubjectCombobox.Items.Add("Lab III - Signals & Systems");
                nameOfSubjectCombobox.Items.Add("Lab IV - Computer Network & Data Communication");
                nameOfSubjectCombobox.Items.Add("Electronic System Design");
           

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Chemical Technology" && (string)semesterComboBox.SelectedItem == "Semester V")
            {
                nameOfSubjectCombobox.Items.Add("Mass Transfer - I Laboratory");
                nameOfSubjectCombobox.Items.Add("Process Instrumentation Dynamics And Control Laboratory");
                nameOfSubjectCombobox.Items.Add("Industrial Electronics And Measurements Laboratory");
              
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Civil Technology" && (string)semesterComboBox.SelectedItem == "Semester V")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Transportation Engineering - I");
                nameOfSubjectCombobox.Items.Add("Lab II - Geotechnical Engineering - I");
                nameOfSubjectCombobox.Items.Add("Lab III - Environmental Engineering - I");
                nameOfSubjectCombobox.Items.Add("Seminar");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Food Technology" && (string)semesterComboBox.SelectedItem == "Semester V")
            {
                nameOfSubjectCombobox.Items.Add("Process Instrumentation, Dynamics & Control");
                nameOfSubjectCombobox.Items.Add("Food Process Engineering - II Lab");
                nameOfSubjectCombobox.Items.Add("Fruits & Vegetables Processing Technology Lab");
                nameOfSubjectCombobox.Items.Add("Technology Of Cereals & Bakery Products Lab");
                nameOfSubjectCombobox.Items.Add("Dairy Technology Lab");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Mechanical Technology" && (string)semesterComboBox.SelectedItem == "Semester V")
            {
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Computer Science & Technology" && (string)semesterComboBox.SelectedItem == "Semester VI")
            {
                nameOfSubjectCombobox.Items.Add("Object Oriented Modelling & Designing Lab");
                nameOfSubjectCombobox.Items.Add("Advanced Programming Lab");
                nameOfSubjectCombobox.Items.Add("Database Lab");
                nameOfSubjectCombobox.Items.Add("Mini Project - I");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Electronics & Telecommunication Technology" && (string)semesterComboBox.SelectedItem == "Semester VI")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Digital Signal Processing");
                nameOfSubjectCombobox.Items.Add("Lab II - Digital Communication Technology");
                nameOfSubjectCombobox.Items.Add("Lab III - Optical Fiber Communication");
                nameOfSubjectCombobox.Items.Add("Lab IV - VLSI Design");
                nameOfSubjectCombobox.Items.Add("Lab V - Mini Project And Seminar");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Chemical Technology" && (string)semesterComboBox.SelectedItem == "Semester VI")
            {
                nameOfSubjectCombobox.Items.Add("Mass Transfer - II Laboratory");
                nameOfSubjectCombobox.Items.Add("Reaction Engineering I Laboratory");
                nameOfSubjectCombobox.Items.Add("Mini Project");

       

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Civil Technology" && (string)semesterComboBox.SelectedItem == "Semester VI")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Geotechnical Engineering - II");
                nameOfSubjectCombobox.Items.Add("Lab II - Environmental Engineering - II");
                nameOfSubjectCombobox.Items.Add("Lab III - Structural Design And Drawing - I");
                nameOfSubjectCombobox.Items.Add("Mini Project");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Food Technology" && (string)semesterComboBox.SelectedItem == "Semester VI")
            {
                nameOfSubjectCombobox.Items.Add("Sugar And Confectionery Technology Lab");
                nameOfSubjectCombobox.Items.Add("Food Packaging Lab");
                nameOfSubjectCombobox.Items.Add("Biochemical Engineering");
                nameOfSubjectCombobox.Items.Add("Process Equipment Design & Drawing Lab");
                nameOfSubjectCombobox.Items.Add("Mini Project");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Third Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Mechanical Technology" && (string)semesterComboBox.SelectedItem == "Semester VI")
            {
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
               
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Computer Science & Technology" && (string)semesterComboBox.SelectedItem == "Semester VII")
            {
                nameOfSubjectCombobox.Items.Add("Web Technology Lab-I");
                nameOfSubjectCombobox.Items.Add("Network Engineering Lab");
                nameOfSubjectCombobox.Items.Add("Major Project Phase - I");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Electronics & Telecommunication Technology" && (string)semesterComboBox.SelectedItem == "Semester VII")
            {
                nameOfSubjectCombobox.Items.Add("Major Project ( Phase-I )");
                nameOfSubjectCombobox.Items.Add("Lab I - Audio And Video Engineering");
                nameOfSubjectCombobox.Items.Add("Lab II - Industrial And Power Electronics");
                nameOfSubjectCombobox.Items.Add("Lab III - Microwave Engineering");
                nameOfSubjectCombobox.Items.Add("Lab IV - Mobile And Cellular Communication");
         
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Chemical Technology" && (string)semesterComboBox.SelectedItem == "Semester VII")
            {
                nameOfSubjectCombobox.Items.Add("Advanced Separation Techniques Laboratory");
                nameOfSubjectCombobox.Items.Add("Reaction Engineering - II Laboratory");
                nameOfSubjectCombobox.Items.Add("Plant Design And Case Studies");
                nameOfSubjectCombobox.Items.Add("Major Project - Phase I");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Civil Technology" && (string)semesterComboBox.SelectedItem == "Semester VII")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Structural Design And Drawing - II");
                nameOfSubjectCombobox.Items.Add("Lab II - Estimating And Costing");
                nameOfSubjectCombobox.Items.Add("Lab III - Earthquake Engineering");
                nameOfSubjectCombobox.Items.Add("Lab IV - Elective-I");
                nameOfSubjectCombobox.Items.Add("Major Project ( Phase-II )");

            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Food Technology" && (string)semesterComboBox.SelectedItem == "Semester VII")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Meat, Poultry And Fish Technology Lab");
                nameOfSubjectCombobox.Items.Add("Lab II - Legume And Oilseed Technology Lab");
                nameOfSubjectCombobox.Items.Add("Lab III - Food Quality & Safety Management Lab");
                nameOfSubjectCombobox.Items.Add("Lab IV - Food Biotechnology Lab");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Mechanical Technology" && (string)semesterComboBox.SelectedItem == "Semester VII")
            {
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
               
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Computer Science & Technology" && (string)semesterComboBox.SelectedItem == "Semester VIII")
            {
                nameOfSubjectCombobox.Items.Add("Web Technology Lab-II");
                nameOfSubjectCombobox.Items.Add("Soft Computing Lab");
                nameOfSubjectCombobox.Items.Add("Major Project Phase-II");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Electronics & Telecommunication Technology" && (string)semesterComboBox.SelectedItem == "Semester VIII")
            {
                nameOfSubjectCombobox.Items.Add("Major Project ( Phase-II )");
                nameOfSubjectCombobox.Items.Add("Lab I - Broadband Communication");
                nameOfSubjectCombobox.Items.Add("Lab II - Satellite Communication");
                nameOfSubjectCombobox.Items.Add("Lab III - Antennas And Radar Engineering");
                nameOfSubjectCombobox.Items.Add("Lab IV - ARM And Embedded System");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Chemical Technology" && (string)semesterComboBox.SelectedItem == "Semester VIII")
            {
                nameOfSubjectCombobox.Items.Add("Process Modelling And Simulation Laboratory");
                nameOfSubjectCombobox.Items.Add("Major Project - Phase II");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Civil Technology" && (string)semesterComboBox.SelectedItem == "Semester VIII")
            {
                nameOfSubjectCombobox.Items.Add("Lab I - Structural Design & Drawing - III");
                nameOfSubjectCombobox.Items.Add("Lab II - Elective - II");
                nameOfSubjectCombobox.Items.Add("Lab III - Elective - III");
                nameOfSubjectCombobox.Items.Add("Major Project ( Phase-II )");
               
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Food Technology" && (string)semesterComboBox.SelectedItem == "Semester VIII")
            {
                nameOfSubjectCombobox.Items.Add("Post Harvest Technology Of Plantation Crops Lab");
                nameOfSubjectCombobox.Items.Add("Design And Development Of New Products Lab");
                nameOfSubjectCombobox.Items.Add("Waste Management Of Food Industries Lab");
                
            }

            else if ((string)nameOfClassCombobox.SelectedItem == "Final Year B.Tech" && (string)nameOfProgrammeComboBox.SelectedItem == "Mechanical Technology" && (string)semesterComboBox.SelectedItem == "Semester VIII")
            {
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
                nameOfSubjectCombobox.Items.Add("");
               
            }
        }
    }
}
