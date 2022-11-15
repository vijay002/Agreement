using Agreement.Domain;
using Agreement.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agreement.Services.Product
{
    public class ProductGroupRepository : BaseRepository<Agreement.Domain.Product.ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(ApplicationDbContext respositoryContext)
           : base(respositoryContext)
        {
        }

    }
}
