using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Database_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connString = new SqlConnection())
            {
                //Create the connection to the database and open the connection
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();

                Connection();
                 void Connection()
                 {
                    string instance = null;
                    string dbname = null;
                    string username = null;
                    string password = null;

                    Console.WriteLine("Enter your SQL instance.");
                    instance = Console.ReadLine();
                    Console.WriteLine("Enter your Database name");
                    dbname = Console.ReadLine();
                    Console.WriteLine("Enter your SQL Username");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your Password");
                    password = Console.ReadLine();

                    builder["Data Source"] = instance;
                    builder["Integrated Security"] = false;
                    builder["Initial Catalog"] = dbname;
                    builder["User ID"] = username;
                    builder["Password"] = password;

                    Console.WriteLine(builder.ConnectionString);

                    connString.ConnectionString = builder.ConnectionString;

                    try
                    {
                        connString.Open();
                        Console.WriteLine("You're connected to the SPS database.");

                        ConnectionState DBstate = connString.State;

                        while (DBstate == ConnectionState.Open)
                        {
                            Query();
                        }
                    }
                    catch (SqlException)
                    {
                        ConnectionState DBState = connString.State;

                        if (DBState == ConnectionState.Open)
                        {
                            Console.WriteLine("Invalid Query, try again");

                        }
                        else if (DBState == ConnectionState.Closed)
                        {
                            Console.WriteLine("Unable to connect, please try again.");
                            Connection();
                        }
                    }
                    
                    void Query()
                    {
                        ConnectionState dbstate = connString.State;

                        while (dbstate == ConnectionState.Open)
                        {
                            Console.WriteLine("Enter Query");

                            string query = Console.ReadLine();

                            SqlCommand command = new SqlCommand(query, connString);

                            //SqlCommand command = new SqlCommand("SELECT * FROM Users", connString);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                //Console.WriteLine("Genre ID | \t Name");
                                while (reader.Read())
                                {
                                    Console.WriteLine(string.Format("{0, -15}| {1, -15}", reader[0], reader[1]));
                                }
                            }
                        }
                    }                
                    Console.WriteLine("Data displayed! Now press enter to move to the next section!");
                 }
            }
        }
    }
}
