using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        public List<ContactInfo> contacts = new List<ContactInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "SELECT * FROM Contact";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ReadData(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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

        private void ReadData(SqlDataReader reader)
        {
            while (reader.Read())
            {
                ContactInfo contact = new ContactInfo();
                contact.Id = reader.GetInt32(0);
                contact.FirstName = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    contact.LastName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    contact.EmailAdd = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    contact.PhoneNumber = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    contact.Notes = reader.GetString(5);

                contacts.Add(contact);
            }
        }
    }

    public class ContactInfo
    {
        public int Id;
        public string FirstName = String.Empty;
        public string? LastName;
        public string? EmailAdd;
        public string? PhoneNumber;
        public string? Notes;

        public string FullName
        {
            get
            {
                if(LastName == null)
                {
                    return FirstName;
                }else return FirstName + " " + LastName;
            }
        }

    }
}
