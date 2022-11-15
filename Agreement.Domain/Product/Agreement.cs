using Agreement.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Agreement.Domain.Product
{
    [Table("Agreement")]
    public class Agreement : TEntity<int>
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }
        public int ProductGroupId { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        [DisplayName("Product Price")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal ProductPrice { get; set; }

        [DisplayName("Product Price")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal NewPrice { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("ProductId")]
        public ICollection<Product> Products { get; set; }

        [ForeignKey("ProductGroupId")]
        public ICollection<ProductGroup> ProductGroups { get; set; }

        [NotMapped]
        public int TotalRow { get; set; }
        [NotMapped]
        public int RowNum { get; set; }
        
        [NotMapped]
        public string Product { get; set; }

        [NotMapped]
        public string ProductGroup { get; set; }

    }
}
