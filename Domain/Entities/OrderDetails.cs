using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderDetails : AuditableEntity
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

    }
}
