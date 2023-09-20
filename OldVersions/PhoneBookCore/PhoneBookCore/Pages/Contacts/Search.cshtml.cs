using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class SearchModel : PageModel
    {
        public string SearchString = string.Empty;
        public List<ContactInfo> Contacts = new List<ContactInfo>();

        public void OnGet()
        {
            SearchString = Request.Query["search"];

            if (string.IsNullOrEmpty(SearchString)) {
                Contacts.Clear();
            }
            else
            {
                try
                {
                    String connectionString = GetConnectionString();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        String query = GetQueryString();

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.Add("@searchV", System.Data.SqlDbType.VarChar).Value = "%" + SearchString + "%";
                            cmd.Parameters.Add("@searchT", System.Data.SqlDbType.Text).Value = "%" + SearchString + "%";

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ContactInfo contact = new ContactInfo();
                                    contact.Id = "" + reader.GetInt32(0);
                                    contact.FirstName = reader.GetString(1);
                                    contact.LastName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                    contact.EmailAdd = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                    contact.PhoneNumber = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                                    contact.Notes = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);

                                    Contacts.Add(contact);
                                }
                            }
                        }

                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

        private String GetQueryString()
        {
            String query = "SELECT * FROM Contact WHERE ";
            query += "FirstName LIKE @searchV OR ";
            query += "LastName LIKE @searchV OR ";
            query += "PhoneNumber LIKE @searchV OR ";
            query += "EmailAdd LIKE @searchV OR ";
            query += "Notes LIKE @searchT;";

            return query;
        }
    }
}
