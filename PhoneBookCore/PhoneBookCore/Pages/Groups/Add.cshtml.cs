using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBookCore.Pages.Groups
{
    public class AddModel : PageModel
    {
        public String SearchString = string.Empty;
        public List<GroupInfo> Groups = new List<GroupInfo>();
        public String ContactId = String.Empty;
        public void OnGet()
        {
            ContactId = Request.Query["id"];

            try
            {
                String connectionString = GetConnectionString();

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "SELECT * FROM ContactsGroup;";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
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
            }catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void OnPost() {
            String cid = Request.Form["cid"];
            String gid = Request.Form["gid"];

            bool success = AddToGroup(cid, gid);
            if (success)
            {
                Response.Redirect("/Groups/View?id=" + gid);
            }
        }

        private bool AddToGroup(String cid, String gid)
        {
            try
            {
                String connectionString = GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "INSERT INTO Belongs(ContactId, GroupId) VALUES(@cid, @gid);";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@cid", System.Data.SqlDbType.Int).Value = cid;
                        cmd.Parameters.Add("@gid", System.Data.SqlDbType.Int).Value = gid;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

    public class  GroupInfo
    {
        public string GId = String.Empty;
        public string Name = string.Empty;
        public string? Description;
    }
}
