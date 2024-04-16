using Courier.DataAccess.Data;
using Courier.DataAccess.Repository.IRepository;
using Courier.Domain.Models;
using Courier.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Courier.DataAccess.Repository
{
    public class InquiryRepository : Repository<Inquiry>, IInquiryRepository
    {
        private ApplicationDbContext _db;
        public InquiryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public override IEnumerable<Inquiry> GetAll()
        {
            var query = _db.Set<Inquiry>()
                .Include(i => i.SourceAddress)
                .Include(i => i.DestinationAddress);
            return query.ToList();
        }
        public override IEnumerable<Inquiry> GetAll(Expression<Func<Inquiry, bool>> filter)
        {
            var query = _db.Set<Inquiry>()
                .Include(i => i.SourceAddress)
                .Include(i => i.DestinationAddress)
                .Where(filter);
            return query.ToList();
        }

        public void Update(Inquiry inquiry)
        {
            _db.Inquiries.Update(inquiry);
        }
    }
}
