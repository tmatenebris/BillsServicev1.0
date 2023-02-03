using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BillsServiceLibrary.Models
{
    public partial class UsersXBills
    {
        public int RelId { get; set; }
        public int? UserId { get; set; }
        public int? BillId { get; set; }
        public bool? IsOwner { get; set; }

        public virtual Bills Bill { get; set; }
        public virtual Users User { get; set; }
    }
}
