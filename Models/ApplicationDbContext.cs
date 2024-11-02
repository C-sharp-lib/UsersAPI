using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;
using System.Collections.Generic;

namespace PracticeAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
