using Microsoft.EntityFrameworkCore;

namespace PhonebookV3.Data
{
    public class PhonebookDbContext : DbContext
    {
        /* Constructors */
        public PhonebookDbContext() { }

        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options)
            : base(options)
        {

        }

        /* Properties */
        public DbSet<Contact> Contact { get; set; }
        public DbSet<User> User { get; set; }

        /* Methods */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Phonebook;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
