using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.DAL.Repositories
{
    public class FileRepository : IRepository<File>
    {
        private readonly FileContext _db;

        /// <summary>
        /// File Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public FileRepository(FileContext context)
        {
            this._db = context;
        }

        /// <summary>
        /// Add File
        /// </summary>
        /// <param name="item">File created object</param>
        public void Create(File item)
        {
            _db.Files.Add(item);
        }

        /// <summary>
        /// Delete File by id
        /// </summary>
        /// <param name="id">Id of File</param>
        public void Delete(int id)
        {
            File file = _db.Files.Find(id);

            if (file != null)
                _db.Files.Remove(file);
        }

        /// <summary>
        /// Get collection of Files with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Collection of Files</returns>
        public async Task<IEnumerable<File>> Find(
            Func<File, bool> predicate)
        {
            var files = (await _db.Files.ToListAsync().ConfigureAwait(false))
                .Where(predicate);

            return files;
        }

        /// <summary>
        /// Get one File by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>File</returns>
        public async Task<File> Get(int id)
        {
            return await _db.Files.FindAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Method for getting all Files
        /// </summary>
        /// <returns>File</returns>
        public async Task<IEnumerable<File>> GetAll()
        {
            return await _db.Files.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update exiting File
        /// </summary>
        /// <param name="item">Found File for updating</param>
        public void Update(File item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
