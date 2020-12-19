using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEOInformation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchProvidersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enum.GetNames(typeof(SearchProvider));
        }
    }
}
