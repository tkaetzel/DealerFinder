using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DealerFinder.Model
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum CategoryType
  {
    Customizations,
    ChatProviders,
    VdpTypes,
    Tpis,
    Widgets
  }

  [JsonConverter(typeof(StringEnumConverter))]
  public enum ProductType
  {
    Bts,
    ContactAtOnce,
    ClassicVdp,
    SrirachaVdp,
    FlickFusion,
    OpenSearch
  }

  public class Products
  {
    [JsonConverter(typeof(StringEnumConverter))]
    public static readonly Dictionary<CategoryType, CategoryInfo> CategorizedProducts = new Dictionary<CategoryType, CategoryInfo>
    {
      {
        CategoryType.Customizations, new CategoryInfo(
          "Customizations",
          new List<Product>
          {
            new Product(ProductType.Bts, "BTS")
          }
        )
      },
      {
        CategoryType.ChatProviders, new CategoryInfo(
          "Chat Providers",
          new List<Product>
          {
            new Product(ProductType.ContactAtOnce, "Contact At Once!")
          } 
        )
      },
      {
        CategoryType.VdpTypes, new CategoryInfo(
          "VDP Type",
          new List<Product>
          {
            new Product(ProductType.ClassicVdp, "Classic VDP"),
            new Product(ProductType.SrirachaVdp, "Sriracha VDP")
          }
        )
      },
      { 
        CategoryType.Tpis, new CategoryInfo(
          "Third Party Integrations",
          new List<Product>
          {
            new Product(ProductType.FlickFusion, "FlickFusion")
          }
        )
      },
      {
        CategoryType.Widgets, new CategoryInfo(
          "Widgets",
          new List<Product>
          {
            new Product(ProductType.OpenSearch, "Open Search")
          }
        )
      }
    };
  }
}