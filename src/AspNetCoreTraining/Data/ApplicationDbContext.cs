using AspNetCoreTraining.Models.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTraining.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ToDoItem> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}