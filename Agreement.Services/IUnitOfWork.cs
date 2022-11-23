using Agreement.Services.Account;
using Agreement.Services.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agreement.Services
{
    public interface IUnitOfWork: IDisposable
    {
        public DbContext Db { get; }

        public IUserRepository UserRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IProductGroupRepository ProductGroupRepository { get; }

        public IAgreementRepository AgreementRepository { get;  }

        
    }
}
