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

        public bool AddUser(User user)
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

        public IEnumerable<User> GetAllUsers(PaginationData pagination)
        {
            var getAllUsersSQL = @"SELECT Name, Age, Email 
                                    FROM User
                                    OFFSET @Skip Rows
                                    FETCH NEXT @Take Rows ONLY";
            var users = _dbQueries.Query<User>(getAllUsersSQL, new { Skip = ((pagination.PageNumber - 1) * pagination.RequiredItemsCount), Take = pagination.RequiredItemsCount });
            return users;
        }

        public int GetAllUsersCount()
        {
            var totalUsersSQL = @"SELECT COUNT(Id) FROM User";
            var totalUsers = _dbQueries.ExecuteScalar<int>(totalUsersSQL);
            return totalUsers;
        }

        public User GetUserBy(int id)
        {
            var getUserSQL = @"SELECT Name, Age, Email 
                                    FROM User
                                    WHERE Id = @id";
            var result = _dbQueries.QueryFirstOrDefault<User>(getUserSQL, new { id });
            return result;
        }

        public User GetUserBy(string name)
        {
            var getUserSQL = @"SELECT Name, Age, Email 
                                    FROM User
                                    WHERE Name = @name";
            var result = _dbQueries.QueryFirstOrDefault<User>(getUserSQL, new { name });
            return result;
        }

        public bool RemoveUserBy(int id)
        {
            var user = _dbCommands.Users.FirstOrDefault(z => z.Id == id);
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

        public bool RemoveUserBy(string name)
        {
            var user = _dbCommands.Users.FirstOrDefault(z => z.Name == name);
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

        public bool UpdateUser(User user)
        {
            var oldUser = _dbCommands.Users.FirstOrDefault(z => z.Id == user.Id);
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
