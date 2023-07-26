using Course.Core.Entities;
using Course.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Course.Data.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly CourseDbContext _context;
        public GroupRepository(CourseDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
