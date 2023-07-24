using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Entities
{
    public class Student:BaseEntity
    {
        public string FullName { get; set; }
        public byte Point { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
