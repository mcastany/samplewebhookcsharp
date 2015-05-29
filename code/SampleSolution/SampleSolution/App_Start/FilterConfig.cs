using SampleSolution.Models;
using System.Web;
using System.Web.Mvc;

namespace SampleSolution
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
