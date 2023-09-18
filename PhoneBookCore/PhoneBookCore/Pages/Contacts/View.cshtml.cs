using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class ViewModel : PageModel
    {
        public ContactInfo contact = new ContactInfo();
        public void OnGet()
        {
            string Id = Request.Query["id"];

            try
            {
                String connectionString = GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "SELECT * FROM Contact WHERE ContactId = @Id;";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlParameter IdParam = new SqlParameter("@Id", System.Data.SqlDbType.Int, 4);
                        IdParam.Value = Id;
                        cmd.Parameters.Add(IdParam);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contact.Id = "" + reader.GetInt32(0);
                                contact.FirstName = reader.GetString(1);
                                contact.LastName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                contact.EmailAdd = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                contact.PhoneNumber = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                                contact.Notes = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private String GetConnectionString()
        {
            String connectionString = "Data Source=.\\sqlexpress;";
            connectionString += "Initial Catalog=Phonebook;";
            connectionString += "User ID=sa;";
            connectionString += "Password=t1g3r";

            return connectionString;
        }
    }
}
