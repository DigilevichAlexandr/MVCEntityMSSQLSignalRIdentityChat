using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Services
{
    public interface IBotService
    {
        public Task<List<string>> HandleMessage(string messageText, string userEmail);
        public void RemoveMessage(string messageText);

        public void ClearMessages();

    }
}
