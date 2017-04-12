using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTutorial2017.Models
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
