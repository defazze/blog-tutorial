using System;
using System.Collections.Generic;
using System.Text;

namespace BlogTutorial.Data.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Role Role { get; set; }
    }
}
