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
    public class OfferRepository : Repository<Domain.Models.Offer>, IOfferRepository
    {
        private ApplicationDbContext _db;
        public OfferRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public override Offer Get(Expression<Func<Offer, bool>> filter)
        {
            var query = _db.Set<Offer>()
                .Include(o => o.Inquiry)
                .Where(filter);
            return query.FirstOrDefault();
        }

        public void Update(Offer offer)
        {
            _db.Offers.Update(offer);
        }
    }
}
