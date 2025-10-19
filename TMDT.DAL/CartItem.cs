namespace TMDT.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartItem")]
    public partial class CartItem
    {
        public int CartItemID { get; set; }

        public int CartID { get; set; }

        public int VariantID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}
