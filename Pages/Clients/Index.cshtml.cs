using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace TaskManagement.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=task;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            { 
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.address = reader.GetString(3);
                            clientInfo.created_at = reader.GetDateTime(4).ToString();

                            listClients.Add(clientInfo);
                        }
                    }
                }
            }
        }
        catch(Exception ex) 
        {
            Console.WriteLine("Exception: " + ex.ToString());
        }
    }
}

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String address;
        public String created_at;
    }
}
