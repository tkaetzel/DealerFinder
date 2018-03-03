using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DealerFinder.Controllers
{
    [Route("api/[controller]")]
    public class OptionsController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
          return Json(Model.Products.CategorizedProducts);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
