using Domain.Entities;
using Repository;

namespace Service.UsersBO
{
    public class UsersBO : IUsersBO
    {
        private IUserRepository _userRepo;
        private IUserFeaturesRepository _userFeaturesRepo;
        private IFeatureRepository _featuresRepo;

        public UsersBO(IUserRepository userRepo, IUserFeaturesRepository userFeaturesRepo, IFeatureRepository featuresRepo)
        {
            _userRepo = userRepo;
            _userFeaturesRepo = userFeaturesRepo;
            _featuresRepo = featuresRepo;
        }

        public bool AddUser(User user)
        {
            var userExistance = _userRepo.GetByEmail(user.Email);
            if (userExistance != null)
                return false;

            var userAdded = _userRepo.Add(user);
            if (!userAdded)
                return false;

            var roleFeatures = _featuresRepo.GetIdsByDefaultAssociatedRole(user.Role);
            var userFeaturesAdded = _userFeaturesRepo.AddRange(user.Id, roleFeatures);
            if (!userFeaturesAdded)
                return false;

            return true;
        }
        public UsersCollectionDTO GetAllUsers(PaginationData pagination)
        {
            var result = new UsersCollectionDTO();

            var users = _userRepo.GetAll(pagination);
            var usersCount = _userRepo.GetAllCount();

            result.Users = users;
            result.RequiredItemsCount = pagination.RequiredItemsCount;
            result.PageNumber = pagination.PageNumber;
            result.TotalItemsCount = usersCount;

            return result;
        }
        public User GetUserByEmail(string email)
        {
            var user = _userRepo.GetByEmail(email);
            return user;
        }
        public bool UpdateUserRole(int userId, int role)
        {
            var user = _userRepo.GetById(userId);
            if (user == null)
                return false;

            user.Role = role;
            var userUpdated = _userRepo.Update(user);
            if (!userUpdated)
                return false;

            return true;
        }
        public bool UpdateUserPassword(int userId, string oldPassword, string newPassword)
        {
            var user = _userRepo.GetById(userId);
            if (user == null || user.Password != oldPassword)
                return false;

            user.Password = newPassword;
            var userUpdated = _userRepo.Update(user);
            if (!userUpdated)
                return false;

            return true;
        }


    }
}
