using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhonebookV2.Models;

namespace PhonebookV2.Data
{
    public class ContactsContext : DbContext
    {
        //entities
        public DbSet<Contact>? Contact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Phonebook;Trusted_Connection=True;");
            }
        }
    }
}