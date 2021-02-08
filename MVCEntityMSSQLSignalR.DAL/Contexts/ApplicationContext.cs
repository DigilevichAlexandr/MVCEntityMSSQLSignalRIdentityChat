using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Entities;

namespace MVCEntityMSSQLSignalR.DAL.Contexts
{
    /// <summary>
    /// Context for working with data in main application db
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Set of records from table of Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Set of records from table of Messages
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Constructor of Application Context
        /// </summary>
        /// <param name="options">Options for this context and for base</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(c => c.Messages)
                    .WithOne(s => s.User);
        }
    }
}
