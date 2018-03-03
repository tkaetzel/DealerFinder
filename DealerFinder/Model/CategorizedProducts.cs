using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DealerFinder.Model
{
  public enum Category
  {
    Customizations,
    MobileLeadDriver,
    PricingElements,
    ChatProviders,
    VdpTypes,
    Tpis,
    Widgets
  }

  [JsonConverter(typeof(StringEnumConverter))]
  public enum Product
  {
    Bts,
    PersonalizedOffer,
    ChromeIncentives,
    MobileLeadDriver,
    MobileLeadDriverWithGeofencing,
    ClassicVdp,
    SrirachaVdp,
    FlickFusion,
    OpenSearch
  }

  public class Products
  {
    [JsonConverter(typeof(StringEnumConverter))]
    public static readonly Dictionary<Category, List<Product>> CategorizedProducts = new Dictionary<Category, List<Product>>
    {
      {
        Category.Customizations, new List<Product>
        {
          Product.Bts
        }
      },
      {
        Category.MobileLeadDriver, new List<Product>
        {
          Product.MobileLeadDriver,
          Product.MobileLeadDriverWithGeofencing
        }
      },
      {
        Category.PricingElements, new List<Product>
        {
          Product.PersonalizedOffer,
          Product.ChromeIncentives
        }
      },
      {
        Category.ChatProviders, new List<Product>
        {
          Product.Bts
        }
      },
      {
        Category.VdpTypes, new List<Product>
        {
          Product.ClassicVdp,
          Product.SrirachaVdp
        }
      },
      {
        Category.Tpis, new List<Product>
        {
          Product.FlickFusion,
        }
      },
      {
        Category.Widgets, new List<Product>
        {
          Product.OpenSearch,
        }
      }
    };
  }
}