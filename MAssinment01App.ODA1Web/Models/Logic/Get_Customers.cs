using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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

		private static Customer ParseCustomer(this ListItem li) {
			var logo = li["DispLogo"] as FieldUrlValue;

			Customer ret = new Customer();
			ret.ID = int.Parse(li["ID"].ToString());
			ret.Title = li["Title"].ToString();
			ret.Logo = logo.Url;
			ret.Address = li["Address"].ToString();
			ret.ContactPerson = li["contactPerson"].ToString();
			ret.OfficePhone = li["phoneOffice"].ToString();
			ret.Mobile = li["phoneMobile"].ToString();
			ret.Email = li["Email"].ToString();
			ret.Created = (DateTime)li["Created"];
			return ret;
		}

	}
}