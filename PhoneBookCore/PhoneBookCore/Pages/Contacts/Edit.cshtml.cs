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

        public void OnPost()
        {
            contact.Id = Request.Form["id"];
            contact.FirstName = Request.Form["FirstName"];
            contact.LastName = Request.Form["LastName"];
            contact.EmailAdd = Request.Form["EmailAdd"];
            contact.PhoneNumber = Request.Form["PhoneNumber"];
            contact.Notes = Request.Form["Notes"];

            success = UpdateDatabase();
            if (success)
            {
                errorMsg = string.Empty;
                Response.Redirect("/Contacts/View?id=" + contact.Id);
            }
        }

        private bool UpdateDatabase()
        {
            try
            {
                String connectionString = GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "UPDATE Contact ";
                    query += "SET FirstName=@firstname, LastName=@lastname, ";
                    query += "PhoneNumber=@phone, EmailAdd=@email, ";
                    query += "Notes=@notes ";
                    query += "WHERE ContactId = @ID;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = contact.Id;
                        cmd.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar).Value = contact.FirstName;
                        cmd.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar).Value
                            = string.IsNullOrEmpty(contact.LastName) ? DBNull.Value : contact.LastName;
                        cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value
                            = string.IsNullOrEmpty(contact.EmailAdd) ? DBNull.Value : contact.EmailAdd;
                        cmd.Parameters.Add("@phone", System.Data.SqlDbType.VarChar).Value
                            = string.IsNullOrEmpty(contact.PhoneNumber) ? DBNull.Value : contact.PhoneNumber;
                        cmd.Parameters.Add("@notes", System.Data.SqlDbType.Text).Value
                            = string.IsNullOrEmpty(contact.Notes) ? DBNull.Value : contact.Notes;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
            return true;
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
