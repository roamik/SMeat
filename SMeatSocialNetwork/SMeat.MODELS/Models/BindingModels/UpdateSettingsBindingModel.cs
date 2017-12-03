﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
    }
}
