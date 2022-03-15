using Domain.Entities;
using System.Collections.Generic;

namespace Repository
{
    public interface IUserRepository
    {
        bool Add(User obj);

        IEnumerable<User> GetAll(PaginationData p);
        int GetAllCount();
        User GetById(int id);
        User GetByName(string name);
        User GetByEmail(string email);

        bool Update(User obj);

        bool RemoveBy(int id);
        bool RemoveBy(string name);
    }
}
