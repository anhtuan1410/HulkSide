using System;
using System.Collections.Generic;

#nullable disable

namespace HulkSide.Models
{
    public partial class Usergroup
    {
        public Usergroup()
        {
            Users = new HashSet<User>();
        }

        public long IdUserGroup { get; set; }
        public string Groupname { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string Note { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
