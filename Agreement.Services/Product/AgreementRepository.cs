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
        //Agreement.Domain.Product.Agreement
        public List<Agreement.Domain.Product.Agreement> GetAllAgreement(int start, int end, string orderby, string search)
        {
            ////var param = new SqlParameter("@PageSize", pageSize);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartIndex", start),
                new SqlParameter("@EndIndex", end),
                new SqlParameter("@OrderBy", orderby),
                new SqlParameter("@Search", search),
            };

            var result = _dbContext.Agreements.
                FromSqlRaw("EXEC SP_GET_AGREEMENTS @StartIndex, @EndIndex, @OrderBy, @Search", parameters)
                .AsNoTracking()
                .AsEnumerable<Agreement.Domain.Product.Agreement>()
                .ToList();

            return result;

            //var result = _dbContext.Agreements.FromSql("EXEC SP_GET_AGREEMENTS @PageSize, @PageNumber, @OrderBy, @Search", parameters).ToList();


            //string sqlQuery = String.Format("EXEC  SP_GET_AGREEMENTS @PageSize = {0} , @PageNumber = {1}, @OrderBy = '{2}', @Search = '{3}'", pageSize, pageNumber, orderby, search);
            ////var query = _dbContext.Agreements.FromSql<List<AgreementDto>>(param).ToList();
            ////var query = this.Table.FromSql(param).ToList();
            //    //_dbContext.Agreements.FromSqlRaw(param).ToList<AgreementDto>();

            //var a = this.Table.FromSqlRaw(sqlQuery).ToList<AgreementDto>();


            //var account = _dbContext.Agreements.get.GetAllIncluding(a => a.GeneralLedgerLines,
            //   a => a.AccountClass,
            //   a => a.ChildAccounts,
            //   a => a.ParentAccount,
            //   a => a.Company)
            //   .Where(a => a.Id == id)
            //   .FirstOrDefault();

            //int numberOfObjectsPerPage = pageSize;
            // .Skip((pageNumber - 1) * pageSize)

            /*
            var query1 = from t in _dbContext.Agreements.AsNoTracking().AsQueryable()
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
                             
                        };
            //query.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize).ToArray();
            return query1.ToList();
            */
        }


    }
}
