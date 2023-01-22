using DictionaryBusinessLogic.Localization.Services;
using DictionaryDataAccess.Localization.Abstractions;
using DictionaryDataAccess.Localization.Model;
using Moq;

namespace DictionaryBusinessLogicTests
{
    public class LocalizationServiceTests
    {
        private Mock<ILocalizationRepository> localizationRepositoryMock = new Mock<ILocalizationRepository>();
        private List<LocalizationRecord> records = new List<LocalizationRecord>();

        private LocalizationService localizationService;

        public LocalizationServiceTests()
        {
            records = new List<LocalizationRecord>
            {
                new LocalizationRecord(hungarian: "alma", english: "apple"),
                new LocalizationRecord(hungarian: "körte", english: "pear"),
                new LocalizationRecord(hungarian: "app", english: "app")
            };

            localizationRepositoryMock
                .Setup(i => i.GetTranslationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string sourceLanguage, string targetLanguage, string searchValue) => Task.FromResult(records.SingleOrDefault(r => r.English == searchValue)?.Hungarian));

            localizationRepositoryMock
                .Setup(i => i.GetAutoCompleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns((string language, string searchValue, int maxResultCount) =>
                    Task.FromResult(
                        !string.IsNullOrEmpty(searchValue)
                        ? (IReadOnlyCollection<string>)records.Where(r => r.English.StartsWith(searchValue)).Select(r => r.English).OrderBy(r => r).ToArray()
                        : (IReadOnlyCollection<string>)Array.Empty<string>()));

            localizationService = new LocalizationService(localizationRepositoryMock.Object);
        }

        [Theory]
        [InlineData("English", "app", new[] {"app","apple"})]
        [InlineData("English", "pe", new[] { "pear" })]
        [InlineData("English", "", new string[0])]
        public async Task AutoCompleteTest(string language, string searchValue, string[] expectedResults)
        {
            var results = await localizationService.GetAutoCompleteAsync(language, searchValue, 5);
            Assert.Equal(expectedResults, results);
        }

        [Theory]
        [InlineData("English", "Hungarian", "pear", "körte")]
        [InlineData("English", "Hungarian", "apple", "alma")]
        public async Task GetTranslationTest(string sourceLanguage, string targetLanguage, string searchValue, string expectedResult)
        {
            var result = await localizationService.GetTranslationAsync(sourceLanguage, targetLanguage, searchValue);
            Assert.Equal(expectedResult, result);
        }
    }
}