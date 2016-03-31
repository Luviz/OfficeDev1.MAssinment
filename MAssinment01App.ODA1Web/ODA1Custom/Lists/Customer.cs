using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAssinment01App.ODA1Web.ODA1Custom.Lists {
	public class Customer {
		public static void CreateCustomerList(ClientContext ctx) {
			ListCreationInformation lci = new ListCreationInformation();
			lci.Title = "Customer";
			lci.TemplateType = 100;
			//var CustomerList = ctx.Web.Lists.GetByTitle("Customer");
			//ctx.Load(CustomerList);
			//ctx.ExecuteQuery();
			
			if (!ctx.Web.ListExists("Customer")) {
				var CustomerList = ctx.Web.Lists.Add(lci);
				CustomerList.AddContentTypeToList(ctx.Web.GetContentTypeById(Constants.GUID.CustomerCT.CustomerCTGUID), true);
				CustomerList.Update();
				ctx.ExecuteQuery();
			}

			if (ctx.Web.ListExists("Customer")) {
				ConnectCustomerLookUpField(ctx);
			}

		}

		private static void ConnectCustomerLookUpField(ClientContext ctx) {
			var customerField = ctx.Web.GetFieldById<FieldLookup>(Constants.GUID.OrderCT.CUSTOMER.ToGuid());
			ctx.Load(customerField);
			ctx.ExecuteQuery();

			customerField.LookupList = ctx.Web.GetListByTitle("Customer").Id.ToString();
			customerField.LookupField = "Title";
			customerField.Update();
			ctx.ExecuteQuery();


		}

	}
}