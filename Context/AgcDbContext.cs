using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGC_CHURCH.Context
{
    public class AgcDbContext:DbContext
    {
        public AgcDbContext(DbContextOptions<AgcDbContext> options) : base(options)
        {

        }

        public DbSet<AGC_CHURCH.Models.User> Users { get; set; }
        public DbSet<AGC_CHURCH.Models.Gender> Gender { get; set; }
        public DbSet<AGC_CHURCH.Models.AgpoCategory> agpoCategorie { get; set; }
        public DbSet<AGC_CHURCH.Models.Branches> Branches { get; set; }
        public DbSet<AGC_CHURCH.Models.Notifications> Notifications { get; set; }
    }
}
