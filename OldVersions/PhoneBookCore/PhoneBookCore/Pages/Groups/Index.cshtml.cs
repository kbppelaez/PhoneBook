using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Groups
{
    public class IndexModel : PageModel
    {
        public List<GroupInfo> Groups = new List<GroupInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "SELECT * FROM ContactsGroup;";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GroupInfo group = new GroupInfo();
                                group.GId = "" + reader.GetInt32(0);
                                group.Name = reader.GetString(1);
                                group.Description = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);

                                Groups.Add(group);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
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
