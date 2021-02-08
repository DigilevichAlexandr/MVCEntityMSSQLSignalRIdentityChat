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
                        answer = await Get(answer, messageText).ConfigureAwait(false);
                    }
                    break;
                case @"\clear":
                    {
                        Clear();
                    }
                    break;
                case @"\remove":
                    {
                        RemoveMessage(messageText);
                    }
                    break;
                case @"\info":
                    {
                        Info(answer);
                    }
                    break;
                default:
                    {
                        Add(answer, messageText, user);
                    }
                    break;
            }

            return answer;
        }

        /// <summary>
        /// Removing messages by phrase method
        /// </summary>
        /// <param name="messageText"></param>
        public void RemoveMessage(string messageText)
        {
            var messages = _db.Messages.Where(m => m.Text == messageText.Replace(@"\remove ", ""));
            _db.Messages.RemoveRange(messages);
            _db.SaveChanges();
        }

        /// <summary>
        /// Clear messages table method
        /// </summary>
        public void Clear()
        {
            var messages = _db.Messages;
            _db.Messages.RemoveRange(messages);
            _db.SaveChanges();
        }

        /// <summary>
        /// Gets all previous messages
        /// </summary>
        /// <param name="answer">answers collection</param>
        /// <param name="messageText">Message-query text</param>
        /// <returns>Updated answers collection</returns>
        public async Task<List<string>> Get(List<string> answer, string messageText)
        {
            if (!int.TryParse(messageText.Replace(@"\get", string.Empty), out int n))
            {
                n = 100;
            }

            var messages = await _db.Messages.Include(m => m.User)
                    .Take(n > 0 && n <= 100 ? n : 10).ToListAsync().ConfigureAwait(false);
            answer.AddRange(messages.Select(m => $"{m.User.Email}: {m.Text}"));

            return answer;
        }

        /// <summary>
        /// Add message to db
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="messageText"></param>
        /// <param name="user"></param>
        public void Add(List<string> answer, string messageText, User user)
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

        public void Info(List<string> answer)
        {
            var comandList = new []{
                "\\get {number of messages} - get previous messages",
                "\\remove {message text} - remove message by it's text",
                "\\clear - clear stored messages",
                "\\info - get list of commands"
            };

            answer.AddRange(comandList);
        }
    }
}
