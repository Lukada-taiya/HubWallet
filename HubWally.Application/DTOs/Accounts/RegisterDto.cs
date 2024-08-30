using System.ComponentModel.DataAnnotations; 

namespace HubWally.Application.DTOs.Accounts
{
    public class RegisterDto
    {
        [Required]
        [Phone]
        [MinLength(6)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
