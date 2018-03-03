using System.Collections.Generic;

namespace DealerFinder.Model
{
  public class CategoryInfo
  {
    public string Value { get; }
    public List<Product> Products { get; }
    public CategoryInfo(string value, List<Product> products)
    {
      Value = value;
      Products = products;
    }
  }
}
