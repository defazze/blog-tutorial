using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogTutorial.Data.Models
{
    public class Blog
    {
        [Key]
        public Guid BlogId { get; set; }

        [Required]
        public string BlogName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual  ApplicationUser User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
