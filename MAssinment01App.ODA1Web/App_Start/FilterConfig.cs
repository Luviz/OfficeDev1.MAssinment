using System.Web;
using System.Web.Mvc;

namespace MAssinment01App.ODA1Web {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
