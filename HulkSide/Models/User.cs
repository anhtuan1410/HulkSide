using System;
using System.Collections.Generic;

#nullable disable

namespace HulkSide.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public long IdUser { get; set; }
        public long IdUserGroup { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string Note { get; set; }

        public virtual Usergroup IdUserGroupNavigation { get; set; }
    }
}
