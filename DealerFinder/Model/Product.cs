namespace DealerFinder.Model
{
  public class Product
  {
    public ProductType Type { get; }
    public string Value { get; }
    public Product(ProductType type, string value)
    {
      Type = type;
      Value = value;
    }
  }
}
