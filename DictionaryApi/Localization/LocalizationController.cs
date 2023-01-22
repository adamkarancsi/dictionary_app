using DictionaryBusinessLogic.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Localization
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationRepository localizationRepository;

        public LocalizationController(ILocalizationRepository localizationRepository)
        {
            this.localizationRepository = localizationRepository;
        }

        [HttpGet]
        [Route("possible")]
        public async Task<string[]> GetPossible(string language, string searchValue)
        {
            var values = await localizationRepository.GetAutoCompleteAsync(language, searchValue, 5);
            return values.ToArray();
        }

        [HttpGet]
        public async Task<string?> GetExact(string searchValue)
        {
            var value = await localizationRepository.GetTranslationAsync("", "", searchValue);
            return value;
        }
    }
}
