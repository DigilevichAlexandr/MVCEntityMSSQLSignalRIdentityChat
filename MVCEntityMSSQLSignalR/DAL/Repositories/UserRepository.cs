using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCEntityMSSQLSignalR.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext db;

        /// <summary>
        /// User Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="item">User created object</param>
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id">Id of User</param>
        public void Delete(int id)
        {
            User User = db.Users.Find(id);

            if (User != null)
                db.Users.Remove(User);
        }

        /// <summary>
        /// Get collection of Users with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Collection of Users</returns>
        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        /// <summary>
        /// Get one User by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>User</returns>
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// Method for getting all Users
        /// </summary>
        /// <returns>User</returns>
        public IEnumerable<User> GetAll(int n = 10)
        {
            return db.Users;
        }

        /// <summary>
        /// Update exiting User
        /// </summary>
        /// <param name="item">Found User for updating</param>
        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
