using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        public ContactInfo contact = new ContactInfo();
        public String errorMsg = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost() { 
            contact.FirstName = Request.Form["FirstName"];
            contact.LastName = Request.Form["LastName"];
            contact.EmailAdd = Request.Form["EmailAdd"];
            contact.PhoneNumber = Request.Form["PhoneNumber"];
            contact.Notes = Request.Form["Notes"];

            bool result = AddContactToDatabase();
            if(result)
            {
                //success
            }
            else
            {
                //failed
            }
        }

        private bool AddContactToDatabase()
        {
            try
            {
                String connectionString = GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = BuildInsertQuery();
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@firstname", contact.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", contact.LastName);
                        cmd.Parameters.AddWithValue("@email", contact.EmailAdd);
                        cmd.Parameters.AddWithValue("@phone", contact.PhoneNumber);
                        cmd.Parameters.AddWithValue("@notes", contact.Notes);

                        cmd.ExecuteNonQuery();
                    }
                }

            }catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private String BuildInsertQuery()
        {
            String query = string.Empty;

            query += "INSERT INTO Contact(";
            query += "FirstName, LastName, EmailAdd, PhoneNumber, Notes";
            query += ") VALUES (";
            query += "@firstname, @lastname, @email, @phone, @notes";
            query += ");";

            return query;
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
