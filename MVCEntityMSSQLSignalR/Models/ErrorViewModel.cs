namespace MVCEntityMSSQLSignalR.Models
{
    public class ErrorViewModel
    {
        /// <summary>
        /// Request Id property
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Boolean property if Request Ids is empty
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
