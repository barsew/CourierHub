using Courier.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IInquiryRepository Inquiry { get; }
        IOfferRepository Offer { get; }
        ICompanyRepository Company { get; }
        IOrderRepository Order { get; }
        IUserRepository User { get; }
        void Save();
    }
}
