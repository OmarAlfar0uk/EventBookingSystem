using DomainLayer.Contract;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService(IUnitOfWork _unitOfWork) : IUserService
    {
        public async Task AddUserAsync(UserDTo userDto)
        {
            var repo = _unitOfWork.GetRepository<User, int>();

            var existing = (await repo.GetAllAsync())
                .FirstOrDefault(u => u.Email == userDto.Email);

            if (existing is not null)
                throw new Exception("User already exists with this email.");

            var newUser = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email
            };

            await repo.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<User, int>();
            var user = await repo.GetByIdAsync(id);

            if (user is null)
                throw new Exception("User not found.");

            repo.Remove(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDTo>> GetAllUsersAsync()
        {
            var repo = _unitOfWork.GetRepository<User, int>();
            var users = await repo.GetAllAsync();

            return users.Select(u => new UserDTo
            {
                FullName = u.FullName,
                Email = u.Email
            });
        }

        public async Task<UserDTo?> GetUserByEmailAsync(string email)
        {
            var repo = _unitOfWork.GetRepository<User, int>();
            var users = await repo.GetAllAsync();

            var user = users.FirstOrDefault(u => u.Email == email);
            if (user is null) return null;

            return new UserDTo
            {
                FullName = user.FullName,
                Email = user.Email
            };
        }

        public async Task<UserDTo?> GetUserByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<User, int>();
            var user = await repo.GetByIdAsync(id);

            if (user is null) return null;

            return new UserDTo
            {
                FullName = user.FullName,
                Email = user.Email
            };
        }

        public async Task UpdateUserAsync(int id, UserDTo userDto)
        {
            var repo = _unitOfWork.GetRepository<User, int>();
            var user = await repo.GetByIdAsync(id);

            if (user is null)
                throw new Exception("User not found.");

            user.FullName = userDto.FullName;
            user.Email = userDto.Email;

            repo.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}