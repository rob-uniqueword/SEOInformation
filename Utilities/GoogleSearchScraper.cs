using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SEOInformation.Utilities
{
    public class GoogleSearchScraper : ISearchScraper
    {
        private const string _googleSearchBaseURL = "https://www.google.co.uk/search?num=100&q=";

        private readonly IHTMLRequestor _htmlRequestor;

        public GoogleSearchScraper(IHTMLRequestor htmlRequestor)
        {
            _htmlRequestor = htmlRequestor;
        }

        public SEOResult GetSEOResults(string searchString, string targetURL)
        {
            var searchURL = MakeGoogleSearchURL(searchString);
            string html = _htmlRequestor.GetHtmlAsync(searchURL).Result;
            return ParseHtml(html, targetURL, searchString);
        }

        private static SEOResult ParseHtml(string html, string targetURL, string searchString)
        {
            var resultLinkRegex = @"<a href=""/url\?q=([^>]*)"">";
            var resultLinks = Regex.Matches(html, resultLinkRegex);

            var resultMatchIndices = resultLinks
                .Select((match, index) => new { match, index })
                .Where(p => p.match.Groups[1].Value.Contains(targetURL))
                .Select(p => p.index + 1)
                .ToList();

            return new SEOResult(SearchProvider.Google.ToString(), searchString, targetURL, resultMatchIndices);
        }

        private static string MakeGoogleSearchURL(string searchString)
        {
            var searchTerms = searchString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return _googleSearchBaseURL + String.Join('+', searchTerms);
        }
    }
}
