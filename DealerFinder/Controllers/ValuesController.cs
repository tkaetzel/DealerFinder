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

    // POST api/values
    [HttpPost]
    public void Post(string[] products)
    {
      using (IDbConnection db = new SqlConnection(Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "Dealeron")))
      {
        return db.Query<Author>
        (“Select * From Author”).ToList();
      }
    }
  }
}
