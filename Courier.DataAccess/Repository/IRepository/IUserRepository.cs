using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;

namespace Courier.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}
