using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Service.Dtos.StudentDtos
{
    public class PaginatedListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}
