namespace TMDT.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventory")]
    public partial class Inventory
    {
        public int InventoryID { get; set; }

        public int VariantID { get; set; }

        public int QuantityAvailable { get; set; }

        public int QuantityReserved { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}
