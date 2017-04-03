﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogTutorial.Data.Models
{
    public class ApplicationUser
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public string NormalizedUserName { get; set; }

        [NotMapped]
        public string NormalizedEmail { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}