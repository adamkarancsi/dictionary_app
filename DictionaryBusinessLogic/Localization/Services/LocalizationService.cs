using DictionaryBusinessLogic.Localization.Abstractions;
using DictionaryDataAccess.Localization.Abstractions;

namespace DictionaryBusinessLogic.Localization.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationRepository localizationRepository;

        public LocalizationService(ILocalizationRepository localizationRepository)
        {
            this.localizationRepository = localizationRepository;
        }

        public Task<IReadOnlyCollection<string>> GetAutoCompleteAsync(string language, string searchValue, int maxResultCount)
            => localizationRepository.GetAutoCompleteAsync(language, searchValue, maxResultCount);

        public Task<string?> GetTranslationAsync(string sourceLanguage, string targetLanguage, string searchValue)
            => localizationRepository.GetTranslationAsync(sourceLanguage, targetLanguage, searchValue);
    }
}
