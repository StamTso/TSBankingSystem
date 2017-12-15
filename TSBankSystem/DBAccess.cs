using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TSBankSystem
{
    public class DBAccess : DbContext
    {
        private readonly string cs = @"Data Source=TSO8\SQLEXPRESS;Initial Catalog=afdemp_csharp_1;Integrated Security=true;";

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(cs);
        }
    }
}
