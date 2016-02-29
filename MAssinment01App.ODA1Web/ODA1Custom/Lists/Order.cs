using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint.Client;

namespace MAssinment01App.ODA1Web.ODA1Custom.Lists {
	public class Order {
		internal static void CreateOrderList(ClientContext ctx) {
			ListCreationInformation lci = new ListCreationInformation();
			lci.Title = "Order";
			lci.TemplateType = 100;

			if (!ctx.Web.ListExists("Order")) {
				var OrderList = ctx.Web.Lists.Add(lci);
				OrderList.AddContentTypeToList(ctx.Web.GetContentTypeById(Constants.GUID.OrderCT.OrderCTGUID), true);
				OrderList.Update();
				ctx.ExecuteQuery();
			}

		}
	}
}