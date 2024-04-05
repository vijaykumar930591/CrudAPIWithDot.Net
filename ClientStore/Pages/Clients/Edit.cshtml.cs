using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientStore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet() //allows to get the data of current client
        {
            String id = Request.Query["id"]; //to read the particular detail of client,i.e, in this case, id
        
            try
            {
                using (SqlConnection conn = ConnectionManager.CreateSqlConnection())
                {
                    {
                        conn.Open();
                        String sql = "SELECT * FROM clients WHERE id=@id";
                        using (SqlCommand command = new SqlCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("@id", id);//replace with id which it received from request
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    clientInfo.id = "" + reader.GetInt32(0);//as id is string but in database it is int,so to convert it in string we added empty string
                                    clientInfo.name = reader.GetString(1);
                                    clientInfo.email = reader.GetString(2);
                                    clientInfo.phone = reader.GetString(3);
                                    clientInfo.address = reader.GetString(4);

                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            { 
                errorMessage = ex.Message;
                
            }
        }
        public void OnPost()  //allows to update the data of client(fill the client info with the data that we received from form)
        {
            clientInfo.id = "" + Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.email.Length == 0
               || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Encrypt=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");        //     Returns a temporary redirect response  to the client.
        }

    }
}


