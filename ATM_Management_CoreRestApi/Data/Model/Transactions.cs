using System;
using System.Collections.Generic;

namespace ATM_Management_CoreRestApi.Data.Model
{
    public partial class Transactions
    {
        public Guid Guid { get; set; }
        public string Lastupdate { get; set; }
        public string CardBrand { get; set; }
        public int? AccountNo { get; set; }
        public int? TxnCode { get; set; }
        public int? TxnSubCode { get; set; }
        public string Rrn { get; set; }
        public string ReqDateTime { get; set; }
        public string TermId { get; set; }
    }
}
