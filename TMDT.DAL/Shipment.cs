namespace TMDT.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shipment")]
    public partial class Shipment
    {
        public int ShipmentID { get; set; }

        public int OrderID { get; set; }

        [StringLength(100)]
        public string Carrier { get; set; }

        [StringLength(200)]
        public string TrackingNumber { get; set; }

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public virtual OrderTbl OrderTbl { get; set; }
    }
}
