using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCEntityMSSQLSignalR.DAL.Repositories
{
    public class FileRepository : IRepository<File>
    {
        private readonly FileContext db;

        /// <summary>
        /// File Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public FileRepository(FileContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Add File
        /// </summary>
        /// <param name="item">File created object</param>
        public void Create(File item)
        {
            db.Files.Add(item);
        }

        /// <summary>
        /// Delete File by id
        /// </summary>
        /// <param name="id">Id of File</param>
        public void Delete(int id)
        {
            File file = db.Files.Find(id);

            if (file != null)
                db.Files.Remove(file);
        }

        /// <summary>
        /// Get collection of Files with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Collection of Files</returns>
        public IEnumerable<File> Find(Func<File, bool> predicate)
        {
            return db.Files.Where(predicate).ToList();
        }

        /// <summary>
        /// Get one File by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>File</returns>
        public File Get(int id)
        {
            return db.Files.Find(id);
        }

        /// <summary>
        /// Method for getting all Files
        /// </summary>
        /// <returns>File</returns>
        public IEnumerable<File> GetAll(int n = 10)
        {
            return db.Files.Take(n);
        }

        /// <summary>
        /// Update exiting File
        /// </summary>
        /// <param name="item">Found File for updating</param>
        public void Update(File item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
