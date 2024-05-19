using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("[action]")]
        public void SetCache()
        {
            _memoryCache.Set<string>("Name", "Hüseyin");
        }

        [HttpGet("[action]")]
        public string GetCache()
        {
            if (_memoryCache.TryGetValue<string>("Name", out string name))
            {
                return name.Substring(3);
            }
            else
                return "";
        }

        [HttpGet("[action]")]
        public void SetCacheWithOptions()
        {
            _memoryCache.Set<string>("date", DateTime.Now.ToString(), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            }) ;
        }

        [HttpGet("[action]")]
        public string GetCacheWithOptions()
        {
            return _memoryCache.Get<string>("date");
        }

    }
}
