using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAssinment01App.ODA1Web.Controllers {
	public class HomeController : Controller {
		[SharePointContextFilter]
		public ActionResult Index() {
			User spUser = null;

			var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

			using (var clientContext = spContext.CreateUserClientContextForSPHost()) {
				if (clientContext != null) {
					spUser = clientContext.Web.CurrentUser;

					clientContext.Load(spUser, user => user.Title);

					clientContext.ExecuteQuery();
					var customers = Models.Logic.Get_Customers.GetCustomers(clientContext);
					var orders = Models.Logic.Get_Orders.GetOrders(clientContext);

					ViewBag.NewstCustomers = customers.OrderByDescending(c => c.Created).Take(3) ;
					ViewBag.NewstOrders = orders.OrderByDescending(o => o.Created).Take(5);
					ViewBag.UserName = spUser.Title;
				}
			}

			return View();
		}

		public ActionResult About() {
			ViewBag.Message = "Your application description page.";
			
			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
