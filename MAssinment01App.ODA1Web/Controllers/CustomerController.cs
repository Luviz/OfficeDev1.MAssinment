using MAssinment01App.ODA1Web.Models;
using MAssinment01App.ODA1Web.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAssinment01App.ODA1Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
		[SharePointContextFilter]
        public ActionResult Index()
        {
			var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
			using (var clientContext = spContext.CreateUserClientContextForSPHost()) {
				List<Customer> customers = clientContext.GetCustomers();
				List<Order> orders = clientContext.GetOrders();
				customers.GetOrders(orders);

				return View(customers);
			}
        }

        // GET: Customer/Details/5
		[SharePointContextFilter]
        public ActionResult Details(int id)
        {
			var spCtx = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
			using (var ctx = spCtx.CreateUserClientContextForSPHost()) {
				return View(ctx.GetCustomer(id));
			}
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
