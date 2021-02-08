namespace MVCEntityMSSQLSignalR.DAL.Entities
{    
    /// <summary>
     /// Class for entity of Message
     /// </summary>
    public class Message
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property to user author
        /// </summary>
        public User User { get; set; }
    }
}
