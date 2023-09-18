using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class EditModel : PageModel
    {
        public ContactInfo contact = new ContactInfo();
        public String errorMsg = string.Empty;
        public bool success = false;

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = GetConnectionString();

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "SELECT * FROM Contact WHERE ContactId = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlParameter IdParam = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                        IdParam.Value = id;
                        cmd.Parameters.Add(IdParam);

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
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
            }catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private String GetConnectionString()
        {
            String conString = "Data Source=.\\sqlexpress;";
            conString += "Initial Catalog=Phonebook;";
            conString += "User ID=sa;";
            conString += "Password=t1g3r";

            return conString;
        }
    }
}
