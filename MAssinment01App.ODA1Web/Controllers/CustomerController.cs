﻿using MAssinment01App.ODA1Web.Models;
using MAssinment01App.ODA1Web.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAssinment01App.ODA1Web.Controllers {
	public class CustomerController : Controller {
		// GET: Customer
		[SharePointContextFilter]
		public ActionResult Index() {
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
		public ActionResult Details(int id) {
			var spCtx = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
			using (var ctx = spCtx.CreateUserClientContextForSPHost()) {
				var customer = ctx.GetCustomer(id);

				//Need For _CreateOrder
				var list = new List<Customer>();
				list.Add(customer);
				var prods = ctx.GetProdcuts();
				ViewBag.Customer = new SelectList(list, "ID", "Title");
				ViewBag.Product = new SelectList(prods, "Guid", "Label");

				return View(customer);
			}
		}

		// GET: Customer/Create
		[SharePointContextFilter]
		public ActionResult Create() {
			return View();
		}

		// POST: Customer/Create
		[HttpPost]
		[SharePointContextFilter]
		public ActionResult CreateCustomer(Customer customer) {
			try {
				var spCtx = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
				using (var ctx = spCtx.CreateUserClientContextForSPHost()) {
					customer.CreateCustomer(ctx);
					return RedirectToAction("Index", new { SPHostUrl = Request.QueryString.Get("SPHostUrl") });
				}
			}
			catch {
				return RedirectToAction("Create");
			}
		}

		// GET: Customer/Edit/5
		[SharePointContextFilter]
		public ActionResult Edit(int id) {
			var spCtx = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
			using (var ctx = spCtx.CreateUserClientContextForSPHost()) {
				return View(ctx.GetCustomer(id));
			}
		}

		// POST: Customer/Edit/5
		//[HttpPost]
		[SharePointContextFilter]
		public ActionResult EditList(int id, Customer customer) {
			try {
				// TODO: Add update logic here
				var spCtx = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
				using (var ctx = spCtx.CreateUserClientContextForSPHost()) {
					customer.EditCustomer(ctx);
				}
				return RedirectToAction("Index", new { SPHostUrl = Request.QueryString.Get("SPHostUrl") });
			}
			catch (Exception e) {
				ViewBag.e = e;
				throw new HttpException("bad", e);
			}
		}

		// GET: Customer/Delete/5
		public ActionResult Delete(int id) {
			return View();
		}

		// POST: Customer/Delete/5
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
