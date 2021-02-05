using System;
using System.Collections.Generic;

namespace MVCEntityMSSQLSignalR.DAL.Interfaces
{
    /// <summary>
    /// Interface of pattern Repository
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Method for getting all Items
        /// </summary>
        /// <returns>Collection of Items</returns>
        IEnumerable<T> GetAll(int n = 10);

        /// <summary>
        /// Get one Item by id
        /// </summary>
        /// <param name="id">Id of Item</param>
        /// <returns>Item</returns>
        T Get(int id);

        /// <summary>
        /// Get collection of Items with predicate
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <returns>Item</returns>
        IEnumerable<T> Find(Func<T, Boolean> predicate);

        /// <summary>
        /// Add Item
        /// </summary>
        /// <param name="item">Item created object</param>
        void Create(T item);

        /// <summary>
        /// Update exiting Item
        /// </summary>
        /// <param name="item">Found Item for updating</param>
        void Update(T item);

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id">Id of Item</param>
        void Delete(int id);
    }
}
