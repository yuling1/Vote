using System.Web;
using System.Web.Mvc;

namespace byssz.Wechat.VoteFavorite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }
    }
}