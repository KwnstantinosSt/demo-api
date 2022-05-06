using System;
using demo_api.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_api.Context
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;

    }
}