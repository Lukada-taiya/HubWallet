using System.ComponentModel.DataAnnotations; 

namespace HubWally.Application.DTOs.Accounts
{
    public class LoginDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
