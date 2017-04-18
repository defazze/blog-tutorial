using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogTutorial.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey(nameof(ParentComment))]
        public Guid? ParentCommentId { get; set; }

        public virtual Post Post { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Comment ParentComment { get; set; }
    }
}
