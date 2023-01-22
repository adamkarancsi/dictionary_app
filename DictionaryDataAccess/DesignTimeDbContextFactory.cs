using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DictionaryDataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DictionaryDbContext>
    {
        public DictionaryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DictionaryDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=DictionaryApp;Trusted_Connection=True");

            return new DictionaryDbContext(optionsBuilder.Options);
        }
    }
}
