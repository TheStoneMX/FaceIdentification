using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;


namespace BioHuellas
{
    public partial class TestADOfom : Form
    {
        private static string connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\DataBase\\Biometrics.mdf;Integrated Security=True;User Instance=True";
        public TestADOfom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1. Create a connection.
            //2. Create a command that holds the SQL Query.
            //3. Open the connection.
            //4. Run the command.
            //5. Close the connection.
            //6. Show the results.    
            // The Easy Way to Any Connection String
            // Create a new text file on your hard disk. Name it myfile.udl.
            // Double-click myfile.udl to bring up the Data Link Properties dialog box,

            using (SqlConnection testConnection = new SqlConnection(connectionString))
            {
                SqlCommand testCommand = testConnection.CreateCommand();
                testCommand.CommandText = "Select F_NAME from CIUDADANO where SUBJECT_ID = 1";
                testConnection.Open();
                string result = (string)testCommand.ExecuteScalar();
                testConnection.Close();
            }
        }

 
        private void TestADOfom_Load(object sender, EventArgs e)
        {

        }
    }
}


/*
 * 
    Three Possible Ways of Instantiating the Command Object in C#
    // Instantiate Command and specify Connection in two steps
    SqlCommand testCommand = new SqlCommand();
    testCommand.Connection = testConnection;
    // Instantiate Comamnd and specify Connection in single step
    SqlCommand testCommand = new SqlCommand("<<commandtext here>>", testConnection);
    // Using CreateCommand method
    SqlCommand testCommand = testConnection.CreateCommand();
 * 
 * 
    What to Execute
    // Instantiate Command and specify command text in two steps
    SqlCommand testCommand = new SqlCommand();
    testCommand.CommandText = "SELECT COUNT(*) FROM TestDemo";
    // Instantiate Command and specify command text in single step
    SqlCommand testCommand = new SqlCommand("SELECT COUNT(*) FROM TestDemo");
 * 
 * 
    Retrieving a Result Set
    ExecuteReader returns an object of IDataReader data type, and IDataReader allows you to
    iterate through the various rows and columns in a result set in a read-only/forward-only fashion. 
 
 *  
    CloseConnection: When the command is done executing, both the DataReader and the
    connection are closed.
 * 
    SqlDataReader sqlDr = testCommand.ExecuteReader(CommandBehavior.CloseConnection);
    if (sqlDr.HasRows)
    {
        while (sqlDr.Read())
        {
            Console.WriteLine("TestDemo: " + sqlDr.GetInt32(0)
            + " and Description : " + sqlDr.GetString(1));
        }
    } 
 * 
 * 
 * The SqlDataReader class contains a method called GetEnumerator. What GetEnumerator 
 * allows you to do is use it in a foreach construct. 
 * 
    string connectionString ="Data Source=(local); Initial Catalog=Test;Integrated Security=SSPI";
    using (SqlConnection testConnection = new SqlConnection(connectionString))
    {
        SqlCommand testCommand = new SqlCommand("SELECT * FROM TESTDEMO", testConnection);
        testConnection.Open();
        SqlDataReader sqlDr = testCommand.ExecuteReader(CommandBehavior.CloseConnection);
 
        if (sqlDr.HasRows)
        {
            foreach (DbDataRecord rec in sqlDr)
            {
                dbRecordsHolder.Add(rec); // dbRecordsHolder is an ArrayList
            }
        }
    } // testConnection.Dispose is called automatically
  
 * Finally, add another button to the form, call it btnDataBind, change its text to DataBind, 
 * and double-click it to add the following code: 
           myDataGrid.DataSource = dbRecordsHolder;
 * 
 * 
 * 
 1. Starting with the code you have written for Example 5.3, replace the call to ExecuteReader
    with a call to BeginExecuteReader as shown here: 
  
    AsyncCallback callback = new AsyncCallback(DataReaderIsReady);
    IAsyncResult asyncresult = testCommand.BeginExecuteReader(callback, testCommand);
 
 2. Add code for DataReaderIsReady, which is the method specified in the new AsyncCallback
    call. This is the method that’s called when BeginExecuteReader is done processing. This
    code is shown in Listings 5-9 and 5-10.
    
    private void DataReaderIsReady(IAsyncResult result)
    {
        MessageBox.Show("Results Load Complete", "I'm Done");
        SqlCommand testCommand = (SqlCommand)result.AsyncState;
        SqlDataReader sqlDr = testCommand.EndExecuteReader(result);
        if (sqlDr.HasRows)
        {
            foreach (DbDataRecord rec in sqlDr)
            {
                dbRecordsHolder.Add(rec);
            }
        }
        sqlDr.Close();
        cmd.Connection.Dispose(); //Do not forget to at least close, if not dispose
    } 
 
--- Querying the Database for Multiple Result Sets --- 
SqlCommand cmd = new SqlCommand("SELECT * FROM USERBASICINFORMATION" + ";" + "SELECT * FROM PERMISSIONSTABLE", conn); 
 
if (sqlDr.HasRows)
{
    do
    {
        Console.WriteLine(" ");
        while (sqlDr.Read())
        {
            Console.WriteLine(sqlDr.GetInt32(0)
            + " : " + sqlDr.GetString(1));
        }
    } while (sqlDr.NextResult());
} 

 * 
 * 
 * you can create a stored procedure that contains no output parameter, but ends with 'Select Scope_Identity()'. 
 * This version requires ExecuteScalar(), and requires less ADO.NET code and a shorter Stored Procedure.
    string query = "AddCategory";
    int ID;
    string connect = @"Server=.\SQLExpress;Database=Northwind;Trusted_Connection=Yes;";
    using (SqlConnection conn = new SqlConnection(connect))
    {
      using (SqlCommand cmd = new SqlCommand(query, conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Category", Category.Text);
        conn.Open();
        ID = (int)cmd.ExecuteScalar();
      }
    }

    string query = "Insert Into Categories (CategoryName) Values (@CategoryName);" + "Select Scope_Identity()";
    int ID;
    string connect = @"Server=.\SQLExpress;AttachDbFilename=|DataDirectory|Northwind.mdf;" + "Database=Northwind;Trusted_Connection=Yes;";
 * 
    using (SqlConnection conn = new SqlConnection(connect))
    {
      using (SqlCommand cmd = new SqlCommand(query, conn))
      {
        cmd.Parameters.AddWithValue("@CategoryName", Category.Text);
        conn.Open();
        ID = (int)cmd.ExecuteScalar();
      }
    }
 * 
 * 
    string query = "AddCategory";
    int ID;
    string connect = @"Server=.\SQLExpress;Database=Northwind;Trusted_Connection=Yes;";
    using (SqlConnection conn = new SqlConnection(connect))
    {
      using (SqlCommand cmd = new SqlCommand(query, conn))
      {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Category", Category.Text);
        cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 0, "CategoryID");
        cmd.Parameters["@CategoryID"].Direction = ParameterDirection.Output;
        conn.Open();
        cmd.ExecuteNonQuery();
        ID = (int)cmd.Parameters["@CategoryID"].Value;
        }
    }


 */