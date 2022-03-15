using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserFeatures
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int FeatureId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }
    }
}
