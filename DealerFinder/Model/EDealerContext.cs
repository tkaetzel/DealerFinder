using Microsoft.EntityFrameworkCore;

namespace DealerFinder.Model
{
  public class EDealerContext : DbContext
  {
    public EDealerContext(DbContextOptions<DealeronContext> options) : base(options)
    {
    }
  }
}