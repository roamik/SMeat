using System.ComponentModel.DataAnnotations;

namespace SMeat.MODELS.Models.BindingModels
{
    public class LoginBindingModel
    {
        [Required(ErrorMessage = "ERROR_EMAIL_IS_REQUIRED")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ERROR_EMAIL_NOT_VALID")]
        [EmailAddress(ErrorMessage = "ERROR_EMAIL_NOT_VALID")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ERROR_PASSWORD_IS_REQUIRED")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])?(?=.*\d)(?=.*[$@$!%*?&-])?[A-Za-z\d$@$!%*?&-]{8,}", ErrorMessage = "ERROR_PASSWORD_NOT_VALID")] //regex to validate password with min 8 char at least 1 digit or 1 char
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
