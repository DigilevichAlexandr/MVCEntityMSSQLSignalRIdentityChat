﻿using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.DAL.Repositories
{
    /// <summary>
    /// Entity framework Unit of work
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext db;
        private readonly FileContext filesDb;
        private UserRepository userRepository;
        private MessageRepository messageRepository;
        private FileRepository fileRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationContext">Application context object</param>
        /// <param name="fileContext">File context object</param>
        public EFUnitOfWork(ApplicationContext applicationContext,
            FileContext fileContext)
        {
            db = applicationContext;
            filesDb = fileContext;
        }

        /// <summary>
        /// Users Repository
        /// </summary>
        public IRepository<User> Users
        {
            get
            {
                return userRepository ??= new UserRepository(db);
            }
        }

        /// <summary>
        /// Messages Repository
        /// </summary>
        public IRepository<Message> Messages
        {
            get
            {
                return messageRepository ??= new MessageRepository(db);
            }
        }

        /// <summary>
        /// Files Repository
        /// </summary>
        public IRepository<File> Files
        {
            get
            {
                return fileRepository ??= new FileRepository(filesDb);
            }
        }

        /// <summary>
        /// Save method
        /// </summary>
        public async void Save()
        {
            await db.SaveChangesAsync().ConfigureAwait(false);
            filesDb.SaveChanges();
        }

        private bool disposed = false;

        /// <summary>
        /// Overridden Dispose method
        /// </summary>
        /// <param name="disposing">flag if dispose db context objects</param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                    filesDb.Dispose();
                }
                this.disposed = true;
            }
        }

        /// <summary>
        /// Original dispose method with suppress finalize
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
