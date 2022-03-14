using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : AuditableEntity
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int PaymentType { get; set; } // PaymentTypes Enum
        public string Notes { get; set; }
        public double DiscountPercentge { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
