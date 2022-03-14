using Domain.Entities;
using Repository.Shared;
using System.Collections.Generic;

namespace Repository.Users
{
    public interface IUserRepository
    {
        bool AddUser(User user);

        IEnumerable<User> GetAllUsers(PaginationData pagination);
        int GetAllUsersCount();
        User GetUserBy(int id);
        User GetUserBy(string name);

        bool UpdateUser(User user);

        bool RemoveUserBy(int id);
        bool RemoveUserBy(string name);

    }
}