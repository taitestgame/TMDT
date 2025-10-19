namespace TMDT.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderItem")]
    public partial class OrderItem
    {
        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int VariantID { get; set; }

        [StringLength(100)]
        public string SKU { get; set; }

        [StringLength(300)]
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public virtual OrderTbl OrderTbl { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}
