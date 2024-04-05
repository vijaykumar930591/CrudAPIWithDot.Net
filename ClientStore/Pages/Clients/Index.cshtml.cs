using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ClientStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();  //list to store all clients data
        public void OnGet()
        {
            try  
            {
                using (SqlConnection conn = ConnectionManager.CreateSqlConnection()) { 
                    //this line for establish the connection of the database


                    // String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Encrypt=False";//in this we connect to the database using connection string

                    //stored procedure is a prepared SQL code that you can save, so the code can be reused over and over again.   

                    conn.Open();
                    String sql = "Select * FROM clients"; //read all rows from clients table
                    using (SqlCommand command = new SqlCommand(sql,conn)) //to execute sql query
                    {           //stored procedure is a prepared SQL code that you can save, so the code can be reused over and over again. 

                        using (SqlDataReader reader = command.ExecuteReader())
                       {               // Provides a way of reading a forward - only stream of rows from a SQL Server database.
                            while (reader.Read()) 
                            { 
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                clientInfo.age = "" + reader.GetInt32(6);
                                clientInfo.salary ="" + reader.GetDecimal(7).ToString();


                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }

            }

            catch (Exception ex) 
            { 
                Console.WriteLine(" Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String age;
        public String salary;
        public String email;    
        public String phone; 
        public String address;
        public String created_at;
    }
}
