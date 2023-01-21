using DictionaryDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizationController : ControllerBase
    {
        private readonly DictionaryDbContext dictionaryDbContext;

        public LocalizationController(DictionaryDbContext dictionaryDbContext)
        {
            this.dictionaryDbContext = dictionaryDbContext;
        }

        [HttpGet]
        public IActionResult Get(string searchValue)
        {
            var values = dictionaryDbContext.LocalizationRecords.Where(l => l.Hungarian.StartsWith(searchValue));
            return Ok(values);
        }
    }
}
