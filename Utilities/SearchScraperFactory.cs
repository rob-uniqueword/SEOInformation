using System;

namespace SEOInformation.Utilities
{
    public class SearchScraperFactory : ISearchScraperFactory
    {
        private readonly IHTMLRequestor _htmlRequestor;

        public SearchScraperFactory(IHTMLRequestor htmlRequestor)
        {
            _htmlRequestor = htmlRequestor;
        }

        public ISearchScraper GetSearchScraper(SearchProvider searchProvider)
        {
            switch (searchProvider)
            {
                case SearchProvider.Google:
                    return new GoogleSearchScraper(_htmlRequestor);
                default:
                    throw new NotImplementedException("No search scraper specified for search provider " + searchProvider);
            }
        }
    }
}
