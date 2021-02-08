using MVCEntityMSSQLSignalR.BLL.DTO;
using MVCEntityMSSQLSignalR.BLL.Helpers;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _db;

        public AccountService(IUnitOfWork db)
        {
            _db = db;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">User login model</param>
        public async Task<bool> Login(LoginModel model)
        {
            var users = await _db.Users.Find(u => u.Email == model.Email);

            if (users.Any())
            {
                var user = users.First();
                string hashedPassword = HashHelper.HashWithSalt(model.Password, user.Salt);

                if (hashedPassword == user.HashedPassword)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Register(RegisterModel model)
        {
            var users = await _db.Users.Find(u => u.Email == model.Email);

            if (!users.Any())
            {
                (string hashedPassword, string salt) = HashHelper.Hash(model.Password);

                _db.Users.Create(new User
                {
                    Email = model.Email,
                    HashedPassword = hashedPassword,
                    Salt = salt,
                    UserGuid = Guid.NewGuid().ToString()
                });

                _db.Save();

                return true;
            }

            return false;
        }
    }
}
