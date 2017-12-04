using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
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
    }
}
