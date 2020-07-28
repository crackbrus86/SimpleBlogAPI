using Microsoft.EntityFrameworkCore;
using SimpleBlogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Context
{
    public class SimpleBlogContext : DbContext
    {
        public SimpleBlogContext(DbContextOptions<SimpleBlogContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
