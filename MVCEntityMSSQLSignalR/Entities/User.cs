using System.Collections.Generic;

namespace MVCEntityMSSQLSignalR.Models
{
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string HashedPassword { get; set; }
        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Navigation property to Messages
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
