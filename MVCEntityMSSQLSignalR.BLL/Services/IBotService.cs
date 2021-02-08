using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.BLL.Services
{
    public interface IBotService
    {
        /// <summary>
        /// Bot class controller
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="userEmail">User Email</param>
        /// <returns>Collection of answer phrases</returns>
        public Task<List<string>> HandleMessage(string messageText, string userEmail);
        /// <summary>
        /// Removing messages by phrase method
        /// </summary>
        /// <param name="messageText"></param>
        public void RemoveMessage(string messageText);
        /// <summary>
        /// Clear messages table method
        /// </summary>
        public void ClearMessages();

    }
}
