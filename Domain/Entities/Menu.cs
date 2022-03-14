using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Menu : AuditableEntity
    {
        public string Name { get; set; }
        public int Season { get; set; } //SeasonTypes Enum
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
    }
}
