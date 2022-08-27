using System;
using System.Web.UI;

namespace Quotes
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QuoteRepository quoteRepository = new QuoteRepository();
            
            repeaterQuotes.DataSource = quoteRepository.GetQuotes();
            repeaterQuotes.DataBind();
        }
    }
}