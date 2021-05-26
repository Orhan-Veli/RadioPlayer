using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Radio_Player.Authentication;
using Radio_Player.Dal.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Data.Identity
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);           
          
        }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            var connection = @"Server=ORHAN\TEW_SQLEXPRESS;Database=IdentityApi;Trusted_Connection=True;";
            option.UseSqlServer(connection);
        }

       
    }
}
