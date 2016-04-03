using MAssinment01App.ODA1Web.Models;
using MAssinment01App.ODA1Web.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAssinment01App.ODA1Web.Controllers {
	public class OrderController : Controller {
		// GET: Order
		[SharePointContextFilter]
		public ActionResult Index() {
			var spctx = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
			using (var ctx = spctx.CreateUserClientContextForSPHost()) {
				return View(ctx.GetOrders());
			}
		}

		// GET: Order/Details/5
		public ActionResult Details(int id) {
			return View();
		}

		// GET: Order/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Order/Create
		[HttpPost]
		public ActionResult CreateOrder(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		// GET: Order/Edit/5
		public ActionResult Edit(int id) {
			return View();
		}

		// POST: Order/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		// GET: Order/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Order/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}
	}
}
