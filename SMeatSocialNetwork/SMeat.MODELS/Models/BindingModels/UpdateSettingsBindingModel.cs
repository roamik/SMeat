using System.ComponentModel.DataAnnotations;
using SMeat.MODELS.Models.Enums;

namespace SMeat.MODELS.Models.BindingModels
{
    public class UpdateSettingsBindingModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string About { get; set; }

        public string LocationId { get; set; }
        [Required]
        public GenderType Gender { get; set; }

        public RelationshipType Relationship { get; set; }

        public string WorkplaceId { get; set; }

        public string CustomGender { get; set; }
    }
}
