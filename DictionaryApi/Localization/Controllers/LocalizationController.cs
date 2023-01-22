using DictionaryBusinessLogic.Localization.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Localization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationService localizationService;

        public LocalizationController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        [HttpGet]
        [Route("possible")]
        public async Task<string[]> GetPossible(string language, string searchValue)
        {
            var values = await localizationService.GetAutoCompleteAsync(language, searchValue, 5);
            return values.ToArray();
        }

        [HttpGet]
        public async Task<string?> GetExact(string searchValue, string sourceLanguage, string targetLanguage)
        {
            var value = await localizationService.GetTranslationAsync(sourceLanguage, targetLanguage, searchValue);
            return value;
        }
    }
}
