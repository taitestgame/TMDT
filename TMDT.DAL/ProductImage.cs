namespace TMDT.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        [Key]
        public int ImageID { get; set; }

        public int? ProductID { get; set; }

        public int? VariantID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Url { get; set; }

        [StringLength(300)]
        public string AltText { get; set; }

        public int? SortOrder { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}
