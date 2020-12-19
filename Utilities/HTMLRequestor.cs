using System.Net.Http;
using System.Threading.Tasks;

namespace SEOInformation.Utilities
{
    public class HTMLRequestor : IHTMLRequestor
    {
        public async Task<string> GetHtmlAsync(string URL)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync(URL);
            return await response;
        }
    }
}
