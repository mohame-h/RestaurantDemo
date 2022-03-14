using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "At least 3 Digit")]
        public string Name { get; set; }
        [MinLength(3, ErrorMessage = "At least 3 Digit")]
        public string Address { get; set; }
        public string Contact { get; set; }
        public int CousineType { get; set; } //CousineTypes Enum
        [MinLength(10, ErrorMessage = "At least 10 Digit")]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool AcceptScheduledOrders { get; set; }
        public int OwnerId { get; set; } // UserId

        [ForeignKey("OwnerId")]
        public User User { get; set; }

    }
}
