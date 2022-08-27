using System.Data.Entity;

namespace Quotes
{
    public class QuoteDBContextSeeder : DropCreateDatabaseIfModelChanges<QuoteDBContext>
    {
        protected override void Seed(QuoteDBContext context)
        {
            Quote q1 = new Quote()
            {
                Id = 1,
                QuoteText = "Creativity is intelligence having fun"
            };

            Quote q2 = new Quote()
            {
                Id = 2,
                QuoteText = "Champions keep playing until they get it right"
            };

            Quote q3 = new Quote()
            {
                Id = 3,
                QuoteText = "The best time to plant a tree was 20 years ago. The second best time is now"
            };

            Quote q4 = new Quote()
            {
                Id = 4,
                QuoteText = "The only person you are destined to become is the person you decide to be"
            };

            Quote q5 = new Quote()
            {
                Id = 5,
                QuoteText = "Believe you can and you’re halfway there"
            };

            context.Quotes.Add(q1);
            context.Quotes.Add(q2);
            context.Quotes.Add(q3);
            context.Quotes.Add(q4);
            context.Quotes.Add(q5);

            base.Seed(context);
        }
    }
}