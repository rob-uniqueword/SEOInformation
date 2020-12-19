namespace SEOInformation.Utilities
{
    public interface ISearchScraperFactory
    {
        public ISearchScraper GetSearchScraper(SearchProvider searchProvider);
    }
}
