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
    }
}
