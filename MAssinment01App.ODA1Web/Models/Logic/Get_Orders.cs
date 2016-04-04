using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAssinment01App.ODA1Web.Models.Logic {
	public static class Get_Orders {
		private const string CQ_XML = @"
				<Query>
					<OrderBy>
						<FieldRef Name='Customer' Ascending='False' />
						<FieldRef Name='Title' Ascending='False' />
						<FieldRef Name='ID' Ascending='False' />
						<FieldRef Name='Price' Ascending='False' />
						<FieldRef Name='Created' Ascending='False' />
						<FieldRef Name='Product_2' Ascending='False' />
					</OrderBy>
				</Query>";

		internal static List<Order> GetOrders(this ClientContext ctx) {
			if (ctx.Web.ListExists("Order")) {
				var List = ctx.Web.GetListByTitle("Order");

				CamlQuery cq = new CamlQuery();
				cq.ViewXml = CQ_XML;
				var orders = List.GetItems(cq);
				ctx.Load(orders);

				ctx.ExecuteQuery();
				List<Order> ret = orders.ParseOrders();
				//foreach (var c in customers.ToList()) {
				//	ret.Add(c.ParseOrder());
				//}
				return ret;
			}
			return null;
		}
		internal static void AddOrder
					(this ClientContext ctx, string title, string customerId, string ProductId, string price) {
			var orders = ctx.Web.Lists.GetByTitle("Order");
			ListItem li = orders.AddItem(new ListItemCreationInformation());

			li["Title"] = title;
			li["Customer"] = new FieldLookupValue() { LookupId = customerId.ToInt32()};
			li["Product_2"] = new TaxonomyFieldValue() { TermGuid = ProductId, Label="", WssId=-1};
			li["Price"] = price;

			li.Update();
			ctx.ExecuteQuery();
		}

		internal static List<Order> ParseOrders(this ListItemCollection lc) {
			var ret = new List<Order>();
			foreach (var il in lc.ToList()) {
				ret.Add(il.ParseOrder());
			}
			return ret;
		}


		private static Order ParseOrder(this ListItem li) {

			var prod = li["Product_2"] as TaxonomyFieldValue;
			var cust = li["Customer"] as FieldLookupValue;

			Order ret = new Order();
			ret.ID = int.Parse(li["ID"].ToString());
			ret.Title = li["Title"].ToString();
			ret.Product = prod.Label;
			ret.Price = (double)li["Price"];
			ret.Created = (DateTime)li["Created"];
			if (cust != null) {
				ret.Customer = new miniCustomer { ID = cust.LookupId, Title = cust.LookupValue };
			}
			else {
				ret.Customer = new miniCustomer { ID = -1, Title = "null" };
			}
			return ret;
		}
	}
}