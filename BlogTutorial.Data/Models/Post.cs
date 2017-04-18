using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogTutorial.Data.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        
        [Required]
        public string PostName { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey(nameof(Blog))]
        public Guid BlogId { get; set; }

        public virtual Blog Blog { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
