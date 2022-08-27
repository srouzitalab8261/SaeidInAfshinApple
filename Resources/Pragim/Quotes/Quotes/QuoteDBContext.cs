using System.Data.Entity;

namespace Quotes
{
    public class QuoteDBContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
    }
}