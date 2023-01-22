﻿using DictionaryApi.Localization.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Localization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationService localizationService;

        public LocalizationController(ILocalizationService localizationRepository)
        {
            this.localizationService = localizationRepository;
        }

        [HttpGet]
        [Route("possible")]
        public async Task<string[]> GetPossible(string language, string searchValue)
        {
            var values = await localizationService.GetAutoCompleteAsync(language, searchValue, 5);
            return values.ToArray();
        }

        [HttpGet]
        public async Task<string?> GetExact(string searchValue)
        {
            var value = await localizationService.GetTranslationAsync("", "", searchValue);
            return value;
        }
    }
}