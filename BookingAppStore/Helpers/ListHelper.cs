using System.Web.Mvc;

namespace BookingAppStore.Helpers
{
    public static class ListHelper
    {
        public static MvcHtmlString CreateListExt(this HtmlHelper html, string[] items)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach (string item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(item);
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }
    }
}