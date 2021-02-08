using Microsoft.EntityFrameworkCore;

namespace MVCEntityMSSQLSignalR.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Extension method for clearing table
        /// </summary>
        /// <typeparam name="T">Table represented class</typeparam>
        /// <param name="dbSet">Database table rows set</param>
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
