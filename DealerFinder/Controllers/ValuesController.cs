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
        string query = $@"SELECT DISTINCT ds.dealer_id, d.Name, d.City, d.State FROM Dealeron..Dealeron_Setup ds
            JOIN EDealer..Dealers d ON ds.dealer_id = d.dealerid
            JOIN Dealeron..CustomContentProviderDealers ccpd ON ds.dealer_id = ccpd.dealerid
            JOIN Dealeron..CustomizationsDealer cd ON ds.dealer_id = cd.dealer_id
            JOIN Dealeron..Section s ON ds.dealer_id = s.dealer_id
            JOIN Dealeron..Page p ON s.section_id = p.section_id
            JOIN Dealeron..PageBlock pb ON p.page_id = pb.page_id
            JOIN Dealeron..ChatProviderDealers cpd ON cpd.dealerId = ds.dealer_id
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

      var vdpTypes = products
        .Where(productType => Products.CategorizedProducts[CategoryType.VdpTypes].Products.Select(product => product.Type).Contains(productType));
      var vdpTypesQuery = GetVdpTypesQuery(vdpTypes);

      var widgets = products
        .Where(productType => Products.CategorizedProducts[CategoryType.Widgets].Products.Select(product => product.Type).Contains(productType));
      var widgetsQuery = GetWidgetsQuery(widgets);

      var chatProviders = products
        .Where(productType => Products.CategorizedProducts[CategoryType.ChatProviders].Products.Select(product => product.Type).Contains(productType));
      var chatProvidersQuery = GetChatProvidersQuery(chatProviders);

      var whereClause = tpiQuery + customizationsQuery + vdpTypesQuery + widgetsQuery + chatProvidersQuery;
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

    private string GetVdpTypesQuery(IEnumerable<ProductType> vdpTypes)
    {
      var query = "";
      foreach (var vdpType in vdpTypes)
      {
        var templateId = "";
        switch (vdpType)
        {
          case (ProductType.ClassicVdp):
            templateId = "NULL,40,41";
            break;
          case (ProductType.SrirachaVdp):
            templateId = "153,154";
            break;
        }
        if (templateId != "") { query += $"p.template_id IN ({templateId}) AND "; }
      }
      return query;
    }

    private string GetWidgetsQuery(IEnumerable<ProductType> widgets)
    {
      var query = "";
      foreach (var widget in widgets)
      {
        var assetId = 0;
        switch (widget)
        {
          case (ProductType.OpenSearch):
            assetId = 159;
            break;
        }
        if (assetId != 0) { query += $"pb.asset_id = {assetId} AND "; }
      }
      return query;
    }

    private string GetChatProvidersQuery(IEnumerable<ProductType> chatProviders)
    {
      var query = "";
      foreach (var chatProvider in chatProviders)
      {
        var id = 0;
        switch (chatProvider)
        {
          case (ProductType.ContactAtOnceLegacy):
            id = 3;
            break;
          case (ProductType.ContactAtOnceUnified):
            id = 10;
            break;
        }
        if (id != 0) { query += $"cpd.ChatProviderId = {id} AND "; }
      }
      return query;
    }
  }
}
