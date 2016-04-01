using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using MAssinment01App.ODA1Web.Models;
using System.Web;

namespace MAssinment01App.ODA1Web.Models.Logic {
	public static class Get_Customers {
		private const string CQ_XML = @"<Query>
					  <OrderBy>
						 <FieldRef Name='Title' Ascending='False' />
						 <FieldRef Name='DispLogo' Ascending='False' />
						 <FieldRef Name='Address' Ascending='False' />
						 <FieldRef Name='contactPerson' Ascending='False' />
						 <FieldRef Name='phoneOffice' Ascending='False' />
						 <FieldRef Name='phoneMobile' Ascending='False' />
						 <FieldRef Name='Email' Ascending='False' />
						 <FieldRef Name='Created' Ascending='False' />
						</OrderBy>
					</Query>";

		internal static Customer GetCustomer (this ClientContext ctx, int id) {
			if (ctx.Web.ListExists("Customer") && ctx.Web.ListExists("Order")) {
				//get Customer
				var customers = ctx.Web.Lists.GetByTitle("Customer");
				var cQuery = new CamlQuery();
				cQuery.ViewXml = $@"<View><Query><Where><Eq>
					<FieldRef Name='ID' /><Value Type='Counter'>{id}</Value>
				</Eq></Where></Query></View>";
				var customer = customers.GetItems(cQuery);

				//Get Orders
				var orders = ctx.Web.Lists.GetByTitle("Order");
				var oQuery = new CamlQuery();
				oQuery.ViewXml = $@"<View><Query><Where><Eq>
					<FieldRef Name='Customer' LookupId='TRUE'/><Value Type='Lookup'>{id}</Value>
				</Eq></Where></Query></View>";
				var customerOrders = orders.GetItems(oQuery);

				//Loading
				ctx.Load(customer);
				ctx.Load(customerOrders);

				ctx.ExecuteQuery();
				var ret = customer[0].ParseCustomer();
				ret.Orders = customerOrders.ParseOrders();
				return ret; 
			}
			return null;

		}

		internal static List<Customer> GetCustomers(this ClientContext ctx) {
			if (ctx.Web.ListExists("Customer")) {
				var List = ctx.Web.GetListByTitle("Customer");

				CamlQuery cq = new CamlQuery();
				cq.ViewXml = CQ_XML;
				var customers = List.GetItems(cq);
				ctx.Load(customers);

				ctx.ExecuteQuery();
				var a = customers[1]["phoneOffice"].ToString();
				Console.WriteLine(customers[0]["Title"]);
				List<Customer> ret = new List<Customer>();
				foreach (var c in customers.ToList()) {
					ret.Add(c.ParseCustomer());
				}
				return ret;
			}
			return null;
		}

		internal static void EditCustomer(this Customer customer, ClientContext ctx ) {
			var customers = ctx.Web.Lists.GetByTitle("Customer");
			var customerListItem = customers.GetItemById(customer.ID);
			
			customerListItem["Title"] = customer.Title;
			customerListItem["Address"] = customer.Address;
			customerListItem["contactPerson"] = customer.ContactPerson;
			customerListItem["phoneOffice"] = customer.OfficePhone;
			customerListItem["phoneMobile"] = customer.Mobile;
			customerListItem["Email"] = customer.Email;

			customerListItem.Update();

			ctx.ExecuteQuery();
		}
		private static Customer ParseCustomer(this ListItem li) {
			var logo = li["DispLogo"] as FieldUrlValue;

			Customer ret = new Customer();
			ret.ID = int.Parse(li["ID"].ToString());
			//sometimes Title dose not read
			ret.Title = string.IsNullOrEmpty(li["Title"].ToString())? "null" : li["Title"].ToString();
			ret.Logo = logo.Url;
			ret.Address = li["Address"].ToString();
			ret.ContactPerson = li["contactPerson"].ToString();
			ret.OfficePhone = li["phoneOffice"].ToString();
			ret.Mobile = li["phoneMobile"].ToString();
			ret.Email = li["Email"].ToString();
			ret.Created = (DateTime)li["Created"];
			return ret;
		}

		internal static void GetOrders(this List<Customer> customers, List<Order> orders) {
			foreach (var customer in customers) {
				customer.Orders = orders.Where(o => o.Customer.ID == customer.ID).ToList();
			}
		}
	}
}