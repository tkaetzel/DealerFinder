using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration.Assemblies;

namespace DealerFinder.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
    // GET api/values
    [HttpGet]
    public ActionResult Get()
    {
      return Json(Model.Products.CategorizedProducts);
    }
  }
}
