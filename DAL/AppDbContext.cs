using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThinkingHome.Migrator;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<MyDay> MyDays { get; set; }
        public DbSet<LogRecord> Logs { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Download> Downloads { get; set; }

        public void Migrate(string connectionString)
        {
            
            using (var loggerFactory = new LoggerFactory())
            {
                var logger = loggerFactory.CreateLogger("Migration");

                using (var migrator = new Migrator("postgres", connectionString, Assembly.GetAssembly(typeof(AppDbContext)), logger))
                {
                    HashSet<long> applied = new HashSet<long>(migrator.GetAppliedMigrations());
                    foreach (var migrationInfo in migrator.AvailableMigrations.OrderBy(m => m.Version))
                    {
                        if (!applied.Contains(migrationInfo.Version))
                        {
                            migrator.ExecuteMigration(migrationInfo.Version, migrationInfo.Version - 1);
                        }
                    }
                }
            }
        }
    }
}