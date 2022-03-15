using Domain.Entities;

namespace Service.UsersBO
{
    public interface IUsersBO
    {
        bool AddUser(User user);

        UsersCollectionDTO GetAllUsers(PaginationData pagination);
        User GetUserByEmail(string email);
        //User GetUserBy(string name);

        bool UpdateUserRole(int userId, int role);
        bool UpdateUserPassword(int userId, string oldPassword, string newPassword);

        //bool RemoveUserBy(int id);
        //bool RemoveUserBy(string name);
    }
}
