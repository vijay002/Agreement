using Agreement.Domain.Base;
using Agreement.Domain.Dto;
using Agreement.Domain.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agreement.Services.Product
{
    public interface IAgreementRepository : IRepository<Agreement.Domain.Product.Agreement>
    {
        public List<AgreementDto> GetAllAgreement(int pageSize, int pageNumber, string orderby, string search, out int totalrecords);

    }
}
