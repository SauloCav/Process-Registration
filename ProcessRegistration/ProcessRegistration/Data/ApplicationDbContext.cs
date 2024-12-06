using Microsoft.EntityFrameworkCore;
using ProcessRegistration.Models;
using System.Collections.Generic;

namespace ProcessRegistration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<ProcessModel> Processes { get; set; }
    }
}
