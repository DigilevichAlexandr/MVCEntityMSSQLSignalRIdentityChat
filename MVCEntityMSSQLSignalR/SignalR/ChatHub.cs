using Microsoft.AspNetCore.SignalR;
using MVCEntityMSSQLSignalR.BLL.DTO;
using MVCEntityMSSQLSignalR.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IBotService _bot;
        /// <summary>
        /// ChatHub constructor
        /// </summary>
        /// <param name="bot">Bot object</param>
        public ChatHub(IBotService bot)
        {
            _bot = bot;
        }
        /// <summary>
        /// Sending message method
        /// </summary>
        /// <param name="messageText">User's message</param>
        /// <returns></returns>
        public async Task Send(string messageText)
        {
            try
            {
                //var messageDTO = new MessageDTO() { UserName = };
                var answer = await _bot.HandleMessage(messageText);

                foreach (string message in answer)
                {
                    await this.Clients.All.SendAsync("Send", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await this.Clients.All.SendAsync("Send", "error!!!");
            }
        }
    }
}
