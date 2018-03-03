using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DealerFinder.Model
{
  public enum Category
  {
    AddOns,
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
    public static readonly Dictionary<Category, Product[]> CategorizedProducts = new Dictionary<Category, Product[]>
    {
      {
        Category.AddOns, new[]
        {
          Product.Bts,
          Product.MobileLeadDriver,
          Product.MobileLeadDriverWithGeofencing,
        }
      },
      {
        Category.PricingElements, new[]
        {
          Product.PersonalizedOffer,
          Product.ChromeIncentives
        }
      },
      {
        Category.ChatProviders, new[]
        {
          Product.Bts
        }
      },
      {
        Category.VdpTypes, new[]
        {
          Product.ClassicVdp,
          Product.SrirachaVdp
        }
      },
      {
        Category.Tpis, new[]
        {
          Product.FlickFusion,
        }
      },
      {
        Category.Widgets, new[]
        {
          Product.OpenSearch,
        }
      }
    };
  }
}