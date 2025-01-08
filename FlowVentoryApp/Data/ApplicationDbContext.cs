using Microsoft.EntityFrameworkCore;
using FlowVentoryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlowVentoryApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}