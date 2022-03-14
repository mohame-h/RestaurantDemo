using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ItemIngredient : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public DateTime ExpireDate { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

    }
}
