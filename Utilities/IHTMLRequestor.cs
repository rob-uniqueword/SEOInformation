using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEOInformation.Utilities
{
    public interface IHTMLRequestor
    {
        Task<string> GetHtmlAsync(String URL);
    }
}
