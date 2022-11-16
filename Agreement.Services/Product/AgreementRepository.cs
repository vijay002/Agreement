using Agreement.Domain;
using Agreement.Domain.Base;
using Agreement.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Agreement.Services.Product
{
    public class AgreementRepository : BaseRepository<Agreement.Domain.Product.Agreement>, IAgreementRepository
    {

        ApplicationDbContext _dbContext;
        public AgreementRepository(ApplicationDbContext respositoryContext) : base(respositoryContext)
        {
            _dbContext = respositoryContext;

            
        }
       
        public List<AgreementDto> GetAllAgreement(int start, int end, string orderby, string search, out int totalrecords)
        {
            
            int pagesize = (end - start) + 1;


            var query = (from t in _dbContext.Agreements.AsNoTracking().AsQueryable()
                          join p in _dbContext.Products.AsNoTracking() on t.ProductId equals p.Id into grp1
                          from subJoin1 in grp1.DefaultIfEmpty()
                          join pg in _dbContext.ProductGroups.AsNoTracking() on t.ProductGroupId equals pg.Id into grp2
                          from subJoin2 in grp2.DefaultIfEmpty()

                          select new AgreementDto
                          {
                              Id = t.Id,
                              ProductGroupId = t.ProductGroupId,
                              ProductGroup = subJoin2.GroupCode,
                              ProductId = t.ProductId,
                              Product = subJoin1.ProductNumber,
                              ProductPrice = t.ProductPrice,
                              EffectiveDate = t.EffectiveDate,
                              ExpirationDate = t.ExpirationDate,
                              IsActive = t.IsActive,
                              NewPrice = t.NewPrice,
                              UserId = t.UserId

                          });

            totalrecords = query.Count();
            var agrementInfo = query.Skip((start - 1)).Take(pagesize).ToList();
            return agrementInfo;
            
        }


    }
}
