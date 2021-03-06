﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAssinment01App.ODA1Web.Models.Logic;
using MAssinment01App.ODA1Web.Extations;

namespace MAssinment01App.ODA1Web.Controllers {
	public class HomeController : Controller {
		[SharePointContextFilter]
		public ActionResult Index() {
			User spUser = null;

			var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

			using (var ctx = spContext.CreateUserClientContextForSPHost()) {
				if (ctx != null) {
					spUser = ctx.Web.CurrentUser;

					ctx.Load(spUser, user => user.Title);

					ctx.ExecuteQuery();
					var customers = Models.Logic.Get_Customers.GetCustomers(ctx);
					var orders = Models.Logic.Get_Orders.GetOrders(ctx);

					customers.GetOrders(orders);

					ViewBag.RandomCustomerWithNoOrders = customers.Where(c => !c.Orders.Any())
						.ToList().GetRand();


					var prods = ctx.GetProdcuts();
					//Need For _CreateOrder
					ViewBag.Customer = new SelectList(customers, "ID", "Title");
					ViewBag.Product = new SelectList(prods, "Guid", "Label");

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
