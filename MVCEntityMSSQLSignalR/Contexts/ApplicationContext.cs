﻿using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Data
{
    /// <summary>
    /// Context for getting data from db
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-C6F8D4Q\SQLEXPRESS;Database=MVCEntityMSSQLSignalR_DB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(c => c.Messages)
                    .WithOne(s => s.User);
        }
    }
}
