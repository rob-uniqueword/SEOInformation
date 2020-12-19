namespace SEOInformation.Utilities
{
    public interface ISearchScraper
    {
        SEOResult GetSEOResults(string searchString, string targetURL);
    }
}
