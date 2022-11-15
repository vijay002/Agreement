using Agreement.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Agreement.Domain.Product
{
    [Table("Product")]
    public class Product : TEntity<int>
    {
        public int ProductGroupId { get; set; }

        public string ProductDescription { get; set; }

        [Required]
        public string ProductNumber { get; set; }

        [DisplayName("Product Price")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("ProductGroupId")]
        public ICollection<ProductGroup> ProductGroups { get; set; }




    }
}
