using Course.Core.Entities;
using Course.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = Course.Core.Entities.Group;

namespace Course.Data
{
    public class CourseDbContext:DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options):base(options) { } 
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroupConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
