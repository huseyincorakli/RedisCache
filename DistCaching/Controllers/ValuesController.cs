using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace DistCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SetCache()
        {


            await _distributedCache.SetStringAsync("name", "hüseyin", options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(100),
                SlidingExpiration=TimeSpan.FromSeconds(5)
            });
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes("çoraklı"),options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(100),
                SlidingExpiration = TimeSpan.FromSeconds(5)

            });
            return Ok(); 

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCache()
        {
            var name = await _distributedCache.GetStringAsync("name");
            var surnameBinary =  await _distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(surnameBinary);
            return Ok(new
            {
                Name=name,
                Surname=surname
            });
        }
    }
}
