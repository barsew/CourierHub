using Courier.DataAccess.Data;
using Courier.DataAccess.Repository;
using Courier.DataAccess.Repository.IRepository;
using Courier.Domain.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IInquiryRepository Inquiry { get; private set; }
        public IOfferRepository Offer { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IUserRepository User { get; private set; }

        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Inquiry = new InquiryRepository(_db);
            Offer = new OfferRepository(_db);
            Company = new CompanyRepository(_db);
            Order = new OrderRepository(_db);
            User = new UserRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
