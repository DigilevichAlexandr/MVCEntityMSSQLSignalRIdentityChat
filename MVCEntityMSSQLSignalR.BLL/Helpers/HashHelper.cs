using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace MVCEntityMSSQLSignalR.BLL.Helpers
{
    public static class HashHelper
    {
        /// <summary>
        /// Hash text and generate the salt
        /// </summary>
        /// <param name="text">Text to hash</param>
        /// <returns> hashed text, salt</returns>
        public static (string, string) Hash(string text)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: text,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 8000,
                numBytesRequested: 256 / 8));

            return (hashed, Convert.ToBase64String(salt));
        }
        /// <summary>
        /// Hash with given salt method
        /// </summary>
        /// <param name="text">Text to hash</param>
        /// <param name="salt">Salt</param>
        /// <returns>Hashed text</returns>
        public static string HashWithSalt(string text, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: text,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 8000,
                numBytesRequested: 256 / 8));
        }
    }
}
