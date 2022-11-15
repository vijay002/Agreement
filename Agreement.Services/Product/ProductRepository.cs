using Agreement.Domain;
using Agreement.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agreement.Services.Product
{
    public class ProductRepository : BaseRepository<Agreement.Domain.Product.Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext respositoryContext)
           : base(respositoryContext)
        {
        }
    }
}
