using LearnDI.EntityDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDI.Connection
{
    public class DiDbContext : DbContext
    {
        public DiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<DiEntity> DiEntity { get; set; }
        public DbSet<LoginEntity> LoginEntities { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiEntity>(b =>
            {
                b.HasIndex(e => e.id);
            });

            modelBuilder.Entity<LoginEntity>(b =>
            {
                b.HasIndex(e => e.id);
            });
        }

        public override int SaveChanges()
        {
            //ApplyAuditConcepts();
            var result = base.SaveChanges();
            return result;
        }

        public virtual Task<int> SaveChangesAsync()
        {
            var result = base.SaveChangesAsync();
            return result;
        }
    }
}
