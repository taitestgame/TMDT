namespace TMDT.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public int PaymentID { get; set; }

        public int OrderID { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(200)]
        public string TransactionRef { get; set; }

        public DateTime? PaidAt { get; set; }

        public virtual OrderTbl OrderTbl { get; set; }
    }
}
