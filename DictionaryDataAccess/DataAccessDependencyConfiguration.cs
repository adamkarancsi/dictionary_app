using DictionaryDataAccess.Localization.Abstractions;
using DictionaryDataAccess.Localization.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DictionaryDataAccess
{
    public static class DataAccessDependencyConfiguration
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection collection, IConfigurationRoot configuration)
        {
            collection.AddDbContext<DictionaryDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("DictionaryApp")!));
            collection.AddScoped<ILocalizationRepository, LocalizationRepository>();
            return collection;
        }
    }
}
