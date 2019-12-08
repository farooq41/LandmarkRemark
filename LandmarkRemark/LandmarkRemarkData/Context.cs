using LandmarkRemarkModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace LandmarkRemarkData
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> optionsBuilder)
        : base(optionsBuilder)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Remark> Remarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
