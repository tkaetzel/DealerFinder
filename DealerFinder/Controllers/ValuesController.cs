using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using DealerFinder.Model;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DealerFinder.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
    private string _connectionString;

    public ValuesController(IConfiguration config)
    {
      _connectionString = "Server=dev-sql01; Database=Dealeron; Integrated Security=False; UID=cmsuser; PWD=cmsuser1;";
    }

    private IDbConnection DbConnection => new SqlConnection(_connectionString);

    // GET api/values
    [HttpGet]
    public ActionResult Get()
    {
      return Json(Products.CategorizedProducts);
    }

    // POST api/values
    [HttpPost]
    public ActionResult Post([FromBody] string[] stringProducts)
    {
      var products = new List<ProductType>();
      foreach (var stringProduct in stringProducts)
      {
        if (Enum.TryParse(stringProduct, out ProductType product))
        {
          products.Add(product);
        }
      }

      var whereClause = GetWhereClause(products);
      if (string.IsNullOrWhiteSpace(whereClause)) return Json("");

      using (var dbConnection = DbConnection)
      {
        string query = $@"SELECT ds.dealer_id, d.Name, d.City, d.State FROM Dealeron..Dealeron_Setup ds
            JOIN EDealer..Dealers d ON ds.dealer_id = d.dealerid
            JOIN Dealeron..CustomContentProviderDealers ccpd ON ds.dealer_id = ccpd.dealerid
            JOIN Dealeron..CustomizationsDealer cd ON ds.dealer_id = cd.dealer_id
            WHERE {whereClause}";

        dbConnection.Open();
        var response = Json(dbConnection.Query<dynamic>(query));
        dbConnection.Close();
        return response;
      }
    }

    private string GetWhereClause(IEnumerable<ProductType> products)
    {
      var tpis = products
        .Where(productType => Products.CategorizedProducts[CategoryType.Tpis].Products.Select(product => product.Type).Contains(productType));
      var tpiQuery = GetTpiQuery(tpis);

      var customizations = products
        .Where(productType => Products.CategorizedProducts[CategoryType.Customizations].Products.Select(product => product.Type).Contains(productType));
      var customizationsQuery = GetCustomizationsQuery(customizations);

      /*var vdpTypes = products
        .Where(productType => Products.CategorizedProducts[CategoryType.VdpTypes].Products.Select(product => product.Type).Equals(productType));
      var vdpTypesQuery = GetCustomizationsQuery(vdpTypes);*/

      var whereClause = tpiQuery + customizationsQuery;
      if (!string.IsNullOrWhiteSpace(whereClause))
      {
        whereClause = whereClause.Substring(0, whereClause.LastIndexOf(" AND"));
      }

      return whereClause;
    }

    private string GetTpiQuery(IEnumerable<ProductType> tpis)
    {
      var query = "";
      foreach (var tpi in tpis)
      {
        var providerId = 0;
        switch (tpi)
        {
          case (ProductType.FlickFusion):
            providerId = 50;
            break;
        }
        if (providerId != 0) { query += $"ccpd.ProviderId = {providerId} AND "; }
      }
      return query;
    }

    private string GetCustomizationsQuery(IEnumerable<ProductType> customizations)
    {
      var query = "";
      foreach(var customization in customizations)
      {
        var custId = 0;
        switch(customization)
        {
          case (ProductType.Bts):
            custId = 356;
            break;
        }
        if (custId != 0) { query += $"cd.cust_id = {custId} AND "; }
      }
      return query;
    }
  }
}
