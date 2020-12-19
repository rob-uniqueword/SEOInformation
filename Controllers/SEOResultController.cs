using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEOInformation.Utilities;
using System;

namespace SEOInformation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SEOResultController : ControllerBase
    {
        private readonly ILogger<SEOResultController> _logger;
        private readonly ISearchScraperFactory _searchScraperFactory;

        public SEOResultController(ILogger<SEOResultController> logger, ISearchScraperFactory searchScraperFactory)
        {
            _logger = logger;
            _searchScraperFactory = searchScraperFactory;
        }

        [HttpGet]
        public SEOResult Get([FromQuery]string searchProviderName, [FromQuery]string searchString, [FromQuery]string targetURL)
        {
            var searchProvider = (SearchProvider)Enum.Parse(typeof(SearchProvider), searchProviderName);
            var scraper = _searchScraperFactory.GetSearchScraper(searchProvider);
            var result = scraper.GetSEOResults(searchString, targetURL);
            return result;
        }
    }
}
