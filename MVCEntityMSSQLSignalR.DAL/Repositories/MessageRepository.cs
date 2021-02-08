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
    public class MessageRepository : IRepository<Message>
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Message Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public MessageRepository(ApplicationContext context)
        {
            this._db = context;
        }

        /// <summary>
        /// Add Message
        /// </summary>
        /// <param name="item">Message created object</param>
        public void Create(Message item)
        {
            _db.Messages.Add(item);
        }

        /// <summary>
        /// Delete Message by id
        /// </summary>
        /// <param name="id">Id of Message</param>
        public void Delete(int id)
        {
            Message message = _db.Messages.Find(id);

            if (message != null)
                _db.Messages.Remove(message);
        }

        /// <summary>
        /// Get collection of Messages with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Collection of Messages</returns>
        public async Task<IEnumerable<Message>> Find(
            Func<Message, bool> predicate)
        {
            return (await _db.Messages.ToListAsync())
                .Where(predicate);
        }

        /// <summary>
        /// Get one Message by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Message</returns>
        public async Task<Message> Get(int id)
        {
            return await _db.Messages.FindAsync(id);
        }

        /// <summary>
        /// Method for getting all Messages
        /// </summary>
        /// <returns>Message</returns>
        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _db.Messages.ToListAsync();
        }

        /// <summary>
        /// Update exiting Message
        /// </summary>
        /// <param name="item">Found Message for updating</param>
        public void Update(Message item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
