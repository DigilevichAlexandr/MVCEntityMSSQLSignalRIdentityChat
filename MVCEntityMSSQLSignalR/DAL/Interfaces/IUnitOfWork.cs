using MVCEntityMSSQLSignalR.DAL.Entities;
using System;

namespace MVCEntityMSSQLSignalR.DAL.Interfaces
{
    /// <summary>
    /// Interface of pattern Unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Users repository
        /// </summary>
        IRepository<User> Users { get; }

        /// <summary>
        /// Messages repository
        /// </summary>
        IRepository<Message> Messages { get; }

        /// <summary>
        /// Files repository
        /// </summary>
        IRepository<File> Files { get; }

        /// <summary>
        /// Save method
        /// </summary>
        void Save();
    }
}
