using Microsoft.EntityFrameworkCore;
using Poc.Services.CouponAPI.Models;

namespace Poc.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupones { get; set; }
    }
}
