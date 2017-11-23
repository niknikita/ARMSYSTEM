using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ARMSYSTEM.Models
{
    public class PhonesContext:DbContext
    {
        public DbSet<Phones> Phones { get; set; }
        public DbSet<BlackList> BlackList { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<DefCode> DefCode { get; set; }
        public DbSet<BasesProject> BasesProject { get; set; }
        public DbSet<PhoneBases> PhoneBases { get; set; }
        public DbSet<FileModel> File { get; set; }
        public DbSet<Filter> Filter { get; set; }
        public PhonesContext(DbContextOptions<PhonesContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phones>().HasIndex(u => u.Phone).IsUnique();
            modelBuilder.Entity<BlackList>().HasIndex(u => u.Phone).IsUnique();
            modelBuilder.Entity<Projects>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
