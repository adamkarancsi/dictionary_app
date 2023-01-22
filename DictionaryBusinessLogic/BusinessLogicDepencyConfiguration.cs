using DictionaryBusinessLogic.Localization.Services;
using DictionaryBusinessLogic.Localization.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DictionaryBusinessLogic
{
    public static class BusinessLogicDepencyConfiguration
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection collection)
        {
            collection.AddScoped<ILocalizationService, LocalizationService>();
            return collection;
        }
    }
}
