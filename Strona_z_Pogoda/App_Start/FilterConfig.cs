using System.Web;
using System.Web.Mvc;

namespace Strona_z_Pogoda
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
