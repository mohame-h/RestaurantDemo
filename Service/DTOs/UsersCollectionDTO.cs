using Domain.Entities;
using System.Collections.Generic;

namespace Service.DTOs
{
    public class UsersCollectionDTO : PaginationDTO
    {
        public IEnumerable<User> Users { get; set; }
    }
}
