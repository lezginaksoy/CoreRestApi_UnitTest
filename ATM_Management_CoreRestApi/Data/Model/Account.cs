using System;
using System.Collections.Generic;

namespace ATM_Management_CoreRestApi.Data.Model
{
    public partial class Account
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public decimal Balance { get; set; }
    }
}
