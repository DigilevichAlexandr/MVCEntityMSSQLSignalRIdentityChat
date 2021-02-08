using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Entities;

namespace MVCEntityMSSQLSignalR.DAL.Contexts
{
    /// <summary>
    /// Context for working with files db
    /// </summary>
    public class FileContext : DbContext
    {
        /// <summary>
        /// Set of records from table of Files
        /// </summary>
        public DbSet<File> Files { get; set; }

        /// <summary>
        /// Constructor of Application Context
        /// </summary>
        /// <param name="options">Options for this context and for base</param>
        public FileContext(DbContextOptions<FileContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-C6F8D4Q\SQLEXPRESS;Database=MVCEntityMSSQLSignalR_Files_DB;Trusted_Connection=True;");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<File>().HasData(
        //        new File[]
        //        {
        //        new File { Id=1, Name = "", Path = "" },
        //        new File { Id=2, Name = "", Path = "" },
        //        new File { Id=3, Name = "", Path = "" }
        //        });
        //}
    }
}
