using Courier.DataAccess.Data;
using Courier.DataAccess.Repository.IRepository;
using Courier.Domain.Models;
using Courier.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Company company)
        {
            _db.Companies.Update(company);
        }
    }
}
