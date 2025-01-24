using System.ComponentModel.DataAnnotations;

namespace GamerWeb.Dto.Dtos.IdentityDtos
{
    public class UpdateProfileDto
    {
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz email formatı")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
        public string? ConfirmPassword { get; set; }
    }
}
