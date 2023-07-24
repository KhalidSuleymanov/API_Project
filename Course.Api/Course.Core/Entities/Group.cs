using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
