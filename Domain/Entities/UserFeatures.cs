using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserFeatures : AuditableEntity
    {
        public int UserId { get; set; }
        public int FeatureId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }
    }
}
