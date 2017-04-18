using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTutorial.Data.Models
{
    public class ApplicationUser
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Comment> AsCommentator { get; set; }
        public virtual ICollection<Blog> AsBlogger { get; set; }

    }
}
