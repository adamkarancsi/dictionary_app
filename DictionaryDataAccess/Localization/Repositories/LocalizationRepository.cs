using DictionaryDataAccess.Localization.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DictionaryDataAccess.Localization.Repositories
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly DictionaryDbContext dictionaryDbContext;
        public LocalizationRepository(DictionaryDbContext dictionaryDbContext)
        {
            this.dictionaryDbContext = dictionaryDbContext;
        }

        public async Task<IReadOnlyCollection<string>> GetAutoCompleteAsync(string language, string searchValue, int maxResultCount)
        {
            return await dictionaryDbContext.LocalizationRecords
                .Where(i => i.English.ToLower().StartsWith(searchValue.ToLower()))
                .OrderBy(i => i.English)
                .Select(i => i.English)
                .Take(maxResultCount)
                .ToArrayAsync();
        }

        public async Task<string?> GetTranslationAsync(string sourceLanguage, string targetLanguage, string searchValue)
        {
            return await dictionaryDbContext.LocalizationRecords
                .Where(i => i.English.ToLower() == searchValue.ToLower())
                .Select(i => i.Hungarian)
                .SingleOrDefaultAsync();
        }
    }
}
