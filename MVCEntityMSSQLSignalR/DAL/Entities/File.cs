namespace MVCEntityMSSQLSignalR.DAL.Entities
{
    /// <summary>
    /// Class for entity of File
    /// </summary>
    public class File
    {
        /// <summary>
        /// File id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Unique user indicator
        /// </summary>
        public string UserGuid { get; set; }
    }
}
