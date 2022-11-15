using System;
using System.Collections.Generic;
using System.Text;

namespace Agreement.Domain.Dto
{
    public class AgreementDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int ProductId { get; set; }
        public string Product { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductGroup { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal NewPrice { get; set; }

        public bool IsActive { get; set; }

        
        public int TotalRow { get; set; }
        
        public int RowNum { get; set; }

    }
}
