using MVCEntityMSSQLSignalR.BLL.DTO;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.BLL.Services
{
    public interface IAccountService
    {
        public Task<bool> Login(LoginModel model);
        public Task<bool> Register(RegisterModel model);
    }
}
