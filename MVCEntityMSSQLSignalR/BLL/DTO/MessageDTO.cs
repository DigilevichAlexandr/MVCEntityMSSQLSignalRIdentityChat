using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.BLL.DTO
{
    public class MessageDTO
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User message
        /// </summary>
        public string MessageText { get; set; }
    }
}
