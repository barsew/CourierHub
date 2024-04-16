using Courier.DataAccess.Data;
using Courier.DataAccess.Repository.IRepository;
using Courier.Domain.Models;
using Courier.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Courier.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _db = dbContext;
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
        }

        public override User Get(Expression<Func<User, bool>> filter)
        {
            IQueryable<User> query = _db.Set<User>()
                .Include(u => u.Subject);
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
    }
}
