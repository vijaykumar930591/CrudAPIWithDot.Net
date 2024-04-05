using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace ClientStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet() // allows to get the data of the current client
        {

        }

        public void OnPost()  //allows to update the data of client
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            clientInfo.age = Request.Form["age"];
            clientInfo.salary = Request.Form["salary"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0
                || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0 || clientInfo.age.Length == 0 ||
                    clientInfo.salary.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            //save the new client into the database

            try
            {
                using (SqlConnection conn = ConnectionManager.CreateSqlConnection())
                //this line for connection establish..
                {
                    {
                        conn.Open();
                        String sql = "INSERT INTO clients " +
                                     "(name,email,phone,address,age,salary) VALUES " +
                                     "(@name,@email,@phone,@address,@age,@salary);";
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {        //stored procedure is a prepared SQL code that you can save, so the code can be reused over and over again.  
                            command.Parameters.AddWithValue("@name", clientInfo.name);
                            command.Parameters.AddWithValue("@email", clientInfo.email);
                            command.Parameters.AddWithValue("@phone", clientInfo.phone);
                            command.Parameters.AddWithValue("@address", clientInfo.address);
                            command.Parameters.AddWithValue("@age", clientInfo.age);
                            command.Parameters.AddWithValue("@salary", clientInfo.salary);
                            //     Represents a collection of parameters associated with a System.Data.SqlClient
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage += ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = "";
            clientInfo.address = ""; clientInfo.age = ""; clientInfo.salary = "";

            successMessage = "New client added successfully";

            Response.Redirect("/Clients/Index");  //     Returns a temporary redirect response  to the client.
        }
    }
}
