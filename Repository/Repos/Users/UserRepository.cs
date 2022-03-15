using Dapper;
using Domain.Entities;
using Repository.QueriesConnectionFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private DBEntities _dbCommands;
        private IQueryConnection _dbQueriesConnection;
        private IDbConnection _dbQueries;

        public UserRepository(DBEntities dbCommands, IQueryConnection dbQueriesConnection)
        {
            _dbCommands = dbCommands;
            _dbQueriesConnection = dbQueriesConnection;
            _dbQueries = _dbQueriesConnection.OpenConnection();
        }

        public bool Add(User user)
        {
            try
            {
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;
                user.IsDeleted = false;

                _dbCommands.Users.Add(user);
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<User> GetAll(PaginationData pagination)
        {
            var getAllUsersSQL = @"SELECT Name, Age, Email 
                                    FROM User
                                    WHERE IsDeleted= 0 AND IsActive= 1
                                    OFFSET @Skip Rows
                                    FETCH NEXT @Take Rows ONLY";
            var users = _dbQueries.Query<User>(getAllUsersSQL, new { Skip = ((pagination.PageNumber - 1) * pagination.RequiredItemsCount), Take = pagination.RequiredItemsCount });
            return users;
        }

        public int GetAllCount()
        {
            var totalUsersSQL = @"SELECT COUNT(Id)
                                FROM User 
                                WHERE IsDeleted= 0 AND IsActive= 1";
            var totalUsers = _dbQueries.ExecuteScalar<int>(totalUsersSQL);
            return totalUsers;
        }

        public User GetById(int id)
        {
            var getUserSQL = @"SELECT * 
                             FROM User
                             WHERE Id = @id AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.QueryFirstOrDefault<User>(getUserSQL, new { id });
            return result;
        }

        public User GetByName(string name)
        {
            var getUserSQL = @"SELECT Name, Age, Email 
                             FROM User
                             WHERE Name = @name AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.QueryFirstOrDefault<User>(getUserSQL, new { name });
            return result;
        }
        public User GetByEmail(string email)
        {
            var getUserSQL = @"SELECT Name, Age, Email 
                             FROM User
                             WHERE Email = @email AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.QueryFirstOrDefault<User>(getUserSQL, new { email });
            return result;
        }

        public bool RemoveBy(int id)
        {
            var user = _dbCommands.Users.FirstOrDefault(z => z.Id == id && z.IsActive == true && z.IsDeleted == false);
            if (user == null)
                return false;

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;
            try
            {
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveBy(string name)
        {
            var user = _dbCommands.Users.FirstOrDefault(z => z.Name == name && z.IsActive == true && z.IsDeleted == false);
            if (user == null)
                return false;

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;
            try
            {
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            var oldUser = _dbCommands.Users.FirstOrDefault(z => z.Id == user.Id && z.IsActive == true && z.IsDeleted == false);
            if (oldUser == null)
                return false;

            oldUser.Name = user.Name;
            oldUser.Age = user.Age;
            oldUser.Email = user.Email;
            oldUser.Role = user.Role;

            oldUser.UpdatedAt = DateTime.Now;
            try
            {
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
