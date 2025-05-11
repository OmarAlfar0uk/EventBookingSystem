using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTo>> GetAllUsersAsync();
        Task<UserDTo?> GetUserByIdAsync(int id);
        Task<UserDTo?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserDTo userDto);
        Task UpdateUserAsync(int id, UserDTo userDto);
        Task DeleteUserAsync(int id);
    }
}
