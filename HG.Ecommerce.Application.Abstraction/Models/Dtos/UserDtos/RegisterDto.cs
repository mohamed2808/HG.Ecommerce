using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HG.Ecommerce.Application.Abstraction.Models.Dtos.UserDtos
{
    public record RegisterDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passward not equal.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
