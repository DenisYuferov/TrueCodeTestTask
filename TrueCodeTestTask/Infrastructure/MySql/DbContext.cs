using Microsoft.EntityFrameworkCore;

using TrueCodeTestTask.Domain.Model;

namespace TrueCodeTestTask.Infrastructure.MySql
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<RequestLog>? RequestLogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(MySqlOptions.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RequestLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Url).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Response).IsRequired();
            });
        }
    }
}
