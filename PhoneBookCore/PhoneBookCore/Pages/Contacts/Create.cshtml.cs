using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        public ContactInfo contact = new ContactInfo();
        public String errorMsg = string.Empty;
        public bool success = false;

        public void OnGet()
        {
        }

        public void OnPost() { 
            contact.FirstName = Request.Form["FirstName"];
            contact.LastName = Request.Form["LastName"];
            contact.EmailAdd = Request.Form["EmailAdd"];
            contact.PhoneNumber = Request.Form["PhoneNumber"];
            contact.Notes = Request.Form["Notes"];

            success = AddContactToDatabase();
            if(success)
            {
                //success
                errorMsg = string.Empty;
                Response.Redirect("/Contacts/View?id=" + contact.Id);
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
                        SqlParameter IdParam = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                        IdParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(IdParam);

                        cmd.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 30).Value
                            = string.IsNullOrEmpty(contact.FirstName) ? System.DBNull.Value : contact.FirstName;
                        cmd.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar, 30).Value
                            = string.IsNullOrEmpty(contact.LastName) ? System.DBNull.Value : contact.LastName;
                        cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 30).Value 
                            = string.IsNullOrEmpty(contact.EmailAdd) ? System.DBNull.Value : contact.EmailAdd;
                        cmd.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, 20).Value
                            = string.IsNullOrEmpty(contact.PhoneNumber) ? System.DBNull.Value : contact.PhoneNumber;
                        cmd.Parameters.Add("@notes", System.Data.SqlDbType.Text).Value
                            = string.IsNullOrEmpty(contact.Notes) ? System.DBNull.Value : contact.Notes;

                        cmd.ExecuteNonQuery();

                        contact.Id = "" + IdParam.Value;
                    }
                }

            }catch (Exception ex)
            {
                errorMsg = ex.Message;
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
