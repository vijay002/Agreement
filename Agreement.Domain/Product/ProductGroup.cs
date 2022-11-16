using Agreement.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Agreement.Domain.Product
{
    [Table("ProductGroup")]
    public class ProductGroup : TEntity<int>
    {
        [Required]
        public string GroupDescription { get; set; }

        [Required]
        public string GroupCode { get; set; }

        public bool IsActive { get; set; }



        //public ICollection<Product> Products { get; set; }
    }
}
