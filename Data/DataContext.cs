using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo2.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
    }
}
