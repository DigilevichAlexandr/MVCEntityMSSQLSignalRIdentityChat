using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Services
{
    public interface IBotService
    {
        public Task<List<string>> HandleMessage(string messageText);
        public void RemoveMessage(string messageText);

        public void ClearMessages();

    }
}
