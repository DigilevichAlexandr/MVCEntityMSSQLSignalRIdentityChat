using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.BLL.Services
{
    public class BotService : IBotService
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Bot class controller
        /// </summary>
        /// <param name="context">Database context</param>
        public BotService(ApplicationContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Clear messages table method
        /// </summary>
        public void ClearMessages()
        {
            _db.Messages.RemoveRange();
        }

        /// <summary>
        /// Resolving \commands method
        /// </summary>
        /// <param name="messageText"></param>
        /// <param name="userEmail">User Email</param>
        /// <returns>Collection of answer phrases</returns>
        public async Task<List<string>> HandleMessage(string messageText, string userEmail)
        {
            List<string> answer = new List<string>();
            var user = _db.Users.First(u => u.Email == userEmail);

            if (user == null)
            {
                answer.Add("You're user is missing in system");
            }

            switch (messageText.Split()[0])
            {
                case @"\get":
                    {
                        int.TryParse(messageText.Replace(@"\get", string.Empty), out int n);
                        var messages = await _db.Messages.Include(m => m.User).Take(n > 0 && n <= 100 ? n : 10).ToListAsync();

                        answer.AddRange(messages.Select(m => $"{m.User.Email}: {m.Text}"));
                    }
                    break;
                case @"\clear":
                    {
                        var messages = _db.Messages;
                        _db.Messages.RemoveRange(messages);
                        _db.SaveChanges();
                    }
                    break;
                case @"\remove":
                    {
                        var messages = _db.Messages.Where(m => m.Text == messageText.Replace(@"\remove ", ""));
                        _db.Messages.RemoveRange(messages);
                        _db.SaveChanges();
                    }
                    break;
                default:
                    {
                        Message message = new Message
                        {
                            Text = messageText,
                            User = user
                        };

                        _db.Messages.Add(message);
                        _db.SaveChanges();

                        answer.Add(messageText);
                    }
                    break;
            }

            return answer;
        }

        /// <summary>
        /// Removing messages by phrase method
        /// </summary>
        /// <param name="messageText">Message text to remove</param>
        public void RemoveMessage(string messageText)
        {
            throw new NotImplementedException();
        }
    }
}
