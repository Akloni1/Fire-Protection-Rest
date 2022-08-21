using System.Collections.Generic;
using System.Threading.Tasks;
using Fire.Models;
using Fire.ViewModels.User;

namespace Fire.Services.UserServices
{
    public interface IUserServices
    {
        Task<dynamic> GetUser(int id);
        Task<dynamic> GetAllUsers();
        Task<EditUserViewModels> UpdateUser(int id, EditUserViewModels userModel);
        Task<UserViewModels> AddUser(InputUserViewModels viewModel);
        Task<UserViewModels> DeleteUser(int id);
        Task<dynamic> GetUserByLogin(string login);
    }
}
