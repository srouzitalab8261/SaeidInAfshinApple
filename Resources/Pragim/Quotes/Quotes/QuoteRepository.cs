using System.Collections.Generic;
using System.Linq;

namespace Quotes
{
    public class QuoteRepository
    {
        public List<Quote> GetQuotes()
        {
            QuoteDBContext quoteDBContext = new QuoteDBContext();
            return quoteDBContext.Quotes.ToList();
        }
    }
}