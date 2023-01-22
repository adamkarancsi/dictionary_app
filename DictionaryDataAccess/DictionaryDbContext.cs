using DictionaryDataAccess.Localization.Model;
using Microsoft.EntityFrameworkCore;

namespace DictionaryDataAccess
{
    public class DictionaryDbContext : DbContext
    {
        public DbSet<LocalizationRecord> LocalizationRecords { get; set; }

        public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureLocalization(modelBuilder);
        }

        private static void ConfigureLocalization(ModelBuilder modelBuilder)
        {
            var localizationRecord = modelBuilder.Entity<LocalizationRecord>();
        }
    }
}