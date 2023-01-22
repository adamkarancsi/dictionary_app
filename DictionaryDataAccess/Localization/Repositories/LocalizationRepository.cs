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
                .Where(i => i.Language.ToLower() == language.ToLower() && i.Phrase.ToLower().StartsWith(searchValue.ToLower()))
                .OrderBy(i => i.Phrase)
                .Select(i => i.Phrase)
                .Take(maxResultCount)
                .ToArrayAsync();
        }

        public async Task<string[]> GetLanguages()
        {
            return await dictionaryDbContext.LocalizationRecords.Select(i => i.Language).Distinct().ToArrayAsync();
        }

        public async Task<string?> GetTranslationAsync(string sourceLanguage, string targetLanguage, string searchValue)
        {
            var match = await dictionaryDbContext.LocalizationRecords
                .Where(i => i.Language.ToLower() == sourceLanguage.ToLower() && i.Phrase.ToLower() == searchValue.ToLower())
                .SingleOrDefaultAsync();

            if (match == null)
                return null;

            var result = await dictionaryDbContext.LocalizationRecords
                .SingleAsync(r => r.RowId == match.RowId && r.Language.ToLower() == targetLanguage.ToLower());

            return result.Phrase;
        }
    }
}
