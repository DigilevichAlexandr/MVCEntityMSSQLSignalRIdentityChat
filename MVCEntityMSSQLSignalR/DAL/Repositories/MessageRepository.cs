using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCEntityMSSQLSignalR.DAL.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly ApplicationContext db;

        /// <summary>
        /// Message Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public MessageRepository(ApplicationContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Add Message
        /// </summary>
        /// <param name="item">Message created object</param>
        public void Create(Message item)
        {
            db.Messages.Add(item);
        }

        /// <summary>
        /// Delete Message by id
        /// </summary>
        /// <param name="id">Id of Message</param>
        public void Delete(int id)
        {
            Message message = db.Messages.Find(id);

            if (message != null)
                db.Messages.Remove(message);
        }

        /// <summary>
        /// Get collection of Messages with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Collection of Messages</returns>
        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return db.Messages.Where(predicate).ToList();
        }

        /// <summary>
        /// Get one Message by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Message</returns>
        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }

        /// <summary>
        /// Method for getting all Messages
        /// </summary>
        /// <returns>Message</returns>
        public IEnumerable<Message> GetAll(int n = 10)
        {
            return db.Messages;
        }

        /// <summary>
        /// Update exiting Message
        /// </summary>
        /// <param name="item">Found Message for updating</param>
        public void Update(Message item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
