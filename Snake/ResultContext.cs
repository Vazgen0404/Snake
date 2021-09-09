using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class ResultContext : DbContext
    {
        public DbSet<Result> Results { get; set; }

        public ResultContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=db1.cy0cli55ts1y.eu-west-3.rds.amazonaws.com;Database=Snake;User ID=admin;Password=Kokosiyux1;");
        }
    }
}
