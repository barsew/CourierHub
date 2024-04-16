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
    public class OrderRepository : Repository<Domain.Models.Order>, IOrderRepository
    {
        private ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public override Order Get(Expression<Func<Order, bool>> filter)
        {
            var query = _db.Set<Order>()
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.Offer)
                .Include(o => o.Offer.PriceBreakdown)
                .Include(o => o.Offer.Inquiry)
                .Include(o => o.Offer.Inquiry.SourceAddress)
                .Include(o => o.Offer.Inquiry.DestinationAddress)
                .Where(filter);
            return query.FirstOrDefault();
        }

        public override IEnumerable<Order> GetAll(Expression<Func<Order, bool>> filter)
        {
            IQueryable<Order> query = _db.Set<Order>()
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.Offer)
                .Include(o => o.Offer.PriceBreakdown)
                .Include(o => o.Offer.Inquiry)
                .Include(o => o.Offer.Inquiry.SourceAddress)
                .Include(o => o.Offer.Inquiry.DestinationAddress);
            query = query.Where(filter);
            return query.ToList();
        }

        public override IEnumerable<Order> GetAll()
        {
            var query = _db.Set<Order>()
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.Offer)
                .Include(o => o.Offer.PriceBreakdown)
                .Include(o => o.Offer.Inquiry)
                .Include(o => o.Offer.Inquiry.SourceAddress)
                .Include(o => o.Offer.Inquiry.DestinationAddress);
            return query.ToList();
        }

        public void Update(Order order)
        {
            _db.Orders.Update(order);
        }
    }
}
