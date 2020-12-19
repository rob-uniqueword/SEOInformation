using System.Collections.Generic;

namespace SEOInformation
{
    public class SEOResult
    {
        public string SearchProvider { get; }
        public string SearchString { get; }
        public string TargetURL { get; }
        public List<int> Occurrences { get; }

        public SEOResult(string searchProvider, string searchString, string targetURL, List<int> occurrences)
        {
            SearchProvider = searchProvider;
            SearchString = searchString;
            TargetURL = targetURL;
            Occurrences = occurrences;
        }
    }
}
