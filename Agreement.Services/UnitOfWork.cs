using Agreement.Domain;
using Agreement.Services.Account;
using Agreement.Services.Product;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agreement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _dbContext;

        #region MyRegion
        public IUserRepository _userRepository { get; set; }
        public IProductRepository _productRepository { get; set; }
        public IProductGroupRepository _productGroupRepository { get; set; }
        
        public IAgreementRepository _agreementRepository { get; set; }

        #endregion

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dbContext);
                }
                return _productRepository;
            }
        }

        public IProductGroupRepository ProductGroupRepository
        {
            get
            {
                if (_productGroupRepository == null)
                {
                    _productGroupRepository = new ProductGroupRepository(_dbContext);
                }
                return _productGroupRepository;
            }
        }

        public IAgreementRepository AgreementRepository
        {
            get
            {
                if (_agreementRepository == null)
                {
                    _agreementRepository = new AgreementRepository(_dbContext);
                }
                return _agreementRepository;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
