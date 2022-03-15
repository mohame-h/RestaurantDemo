using Domain.Entities;
using Service.DTOs;
using System.Collections.Generic;

namespace Service.UsersBO
{
    public class UsersCollectionDTO : PaginationDTO
    {
        public UsersCollectionDTO()
        {
            Users = new List<User>();
        }

        public IEnumerable<User> Users { get; set; }
    }
}
