
using HubWally.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> opt) : IdentityDbContext(opt)
    {
        public DbSet<Wallet> Wallets { get; set; }
    }
}
