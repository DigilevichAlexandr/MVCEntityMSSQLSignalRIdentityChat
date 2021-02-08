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
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// User Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="item">User created object</param>
        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id">Id of User</param>
        public void Delete(int id)
        {
            User User = _db.Users.Find(id);

            if (User != null)
                _db.Users.Remove(User);
        }

        /// <summary>
        /// Get collection of Users with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Collection of Users</returns>
        public async Task<IEnumerable<User>> Find(Func<User, bool> predicate)
        {
            return (await _db.Users.ToListAsync().ConfigureAwait(false)).Where(predicate);
        }

        /// <summary>
        /// Get one User by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>User</returns>
        public async Task<User> Get(int id)
        {
            return await _db.Users.FindAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Method for getting all Users
        /// </summary>
        /// <returns>User</returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update exiting User
        /// </summary>
        /// <param name="item">Found User for updating</param>
        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
