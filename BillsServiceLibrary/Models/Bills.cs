using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BillsServiceLibrary.Models
{
    public partial class Bills
    {
        public Bills()
        {
            UsersXBills = new HashSet<UsersXBills>();
        }

        public int BillId { get; set; }
        public string Product { get; set; }
        public int? ProductPrice { get; set; }
        public string Currency { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerAccountNumber { get; set; }
        public string SellerEmail { get; set; }
        public string SellerNip { get; set; }
        public string SellerPhone { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerEmail { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsersXBills> UsersXBills { get; set; }
    }
}
