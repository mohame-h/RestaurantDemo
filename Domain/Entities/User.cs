using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        [MinLength(3, ErrorMessage = "At least 3 Digit")]
        public string Name { get; set; }
        [Range(12, 100, ErrorMessage = "Invalid Age")]
        public int Age { get; set; }
        [MinLength(5, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [MinLength(5, ErrorMessage = "At least 5 Digits")]
        public string Password { get; set; }
        public int Role { get; set; } //UserRoleTypes Enum
        [DefaultValue(true)]
        public bool AcceptTerms { get; set; }
    }
}
