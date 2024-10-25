using MDisasaterDampener.Models;

namespace MDisasaterDampener.Services.Interfaces
{
    public interface IUserServices
    {

        void Register(RegisterViewModel user);
        UserViewModel Login(LoginViewModel returningUser);
        void ChangeUsername(UserViewModel user, int id);
        void ChangeEmail(UserViewModel user, int id);
        void ChangePassword(UserViewModel user, int id);
    }
}
