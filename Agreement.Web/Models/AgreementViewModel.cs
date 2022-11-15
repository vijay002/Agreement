using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agreement.Web.Models
{
    public class AgreementViewModel
    {
        public int Id { get; set; }


        [Required]
        public int ProductGroupId { get; set; }
        public string ProductGroup { get; set; }


        [Required]
        public int ProductId { get; set; }
        
        public string Product { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        ///[BindProperty, DisplayFormat(DataFormatString = "{0:MM/dd/yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Product Price")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal ProductPrice { get; set; }

        [DisplayName("Product Price")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal NewPrice { get; set; }


        public bool IsActive { get; set; }

        public  List<SelectListItem> ProductList { set; get; }
        public  List<SelectListItem> ProductGroupList { get; set; }

    }
}
