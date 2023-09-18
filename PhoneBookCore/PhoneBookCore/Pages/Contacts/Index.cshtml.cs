using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        List<ContactInfo> contacts = new List<ContactInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Phonebook;User ID=sa;Password=***********";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "SELECT * FROM Contact;";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactInfo contact = new ContactInfo();
                                contact.Id = reader.GetInt32(0);
                                contact.FirstName = reader.GetString(1);
                                contact.LastName = reader.GetString(2);
                                contact.EmailAdd = reader.GetString(3);
                                contact.PhoneNumber = reader.GetString(4);
                                contact.Notes = reader.GetString(5);

                                contacts.Add(contact);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class ContactInfo
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string EmailAdd;
        public string PhoneNumber;
        public string Notes;

    }
}
