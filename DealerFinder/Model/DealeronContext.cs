using Microsoft.EntityFrameworkCore;

namespace DealerFinder.Model
{
  public class DealeronContext : DbContext
  {
    public DealeronContext(DbContextOptions<DealeronContext> options) : base(options)
    {
    }
  }
}