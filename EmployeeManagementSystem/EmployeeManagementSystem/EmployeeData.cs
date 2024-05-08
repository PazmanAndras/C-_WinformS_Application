using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystem
{
    class EmployeeData
    {

        public int ID { set; get; } 
        public string EmployeeID { set; get; } 
        public string Name { set; get; } 
        public string Gender { set; get; } 
        public string Contact { set; get; } 
        public string Position { set; get; } 
        public string Image { set; get; } 
        public int Salary { set; get; } 
        public string Status { set; get; } 


        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bandy\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30");




        //Employee data list from db

        public List<EmployeeData> employeeListData()
        {
            // Initialize a list to hold EmployeeData objects
            List<EmployeeData> listdata = new List<EmployeeData>();

            // Check database connection is not open
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    
                    connect.Open();

                    // SQL query to select employee data where delete_date is NULL
                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    // Execute the SQL command
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        // Execute the command and retrieve data
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Iterate through each row in the result set
                        while (reader.Read())


                        {
                            // Create a new EmployeeData object
                            EmployeeData ed = new EmployeeData();

                            // Populate the object with data from the database
                            ed.ID = (int)reader["id"];
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.Name = reader["full_name"].ToString();
                            ed.Gender = reader["gender"].ToString();
                            ed.Contact = reader["contact_number"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.Image = reader["image"].ToString();
                            ed.Salary = (int)reader["salary"];
                            ed.Status = reader["status"].ToString();

                            // Add the EmployeeData object to the list
                            listdata.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during database access
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    // Close the database connection
                    connect.Close();
                }
            }
            // Return the list of EmployeeData objects
            return listdata;
        }

        public List<EmployeeData> salaryEmployeeListData()
        {
            List<EmployeeData> listdata = new List<EmployeeData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EmployeeData ed = new EmployeeData();
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.Name = reader["full_name"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.Salary = (int)reader["salary"];

                            listdata.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }
    }
}
