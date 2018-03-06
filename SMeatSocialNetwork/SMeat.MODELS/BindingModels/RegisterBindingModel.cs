using System.ComponentModel.DataAnnotations;
using SMeat.MODELS.Enums;

namespace SMeat.MODELS.BindingModels
{
    public class RegisterBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(10)]
        public string Name { get; set; }

        [MinLength(4)]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ERROR_EMAIL_IS_REQUIRED")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ERROR_EMAIL_NOT_VALID")]
        [EmailAddress(ErrorMessage = "ERROR_EMAIL_NOT_VALID")]
        public string Email { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "ERROR_PASSWORD_IS_REQUIRED")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])?(?=.*\d)(?=.*[$@$!%*?&-])?[A-Za-z\d$@$!%*?&-]{8,}", ErrorMessage = "ERROR_PASSWORD_NOT_VALID")] //regex to validate password with min 8 char at least 1 digit or 1 char
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
