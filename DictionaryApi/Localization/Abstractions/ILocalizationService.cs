namespace DictionaryApi.Localization.Abstractions
{
    public interface ILocalizationService
    {
        /// <summary>
        /// Gets a list of words starting with the searchValue.
        /// </summary>
        public Task<IReadOnlyCollection<string>> GetAutoCompleteAsync(string language, string searchValue, int maxResultCount);

        /// <summary>
        /// Gets the exact translation of a word.
        /// </summary>
        public Task<string?> GetTranslationAsync(string sourceLanguage, string targetLanguage, string searchValue);
    }
}
