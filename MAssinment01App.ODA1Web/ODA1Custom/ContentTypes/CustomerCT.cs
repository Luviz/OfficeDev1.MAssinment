using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeDevPnP.Core;
using OfficeDevPnP.Core.Entities;

namespace MAssinment01App.ODA1Web.ODA1Custom.ContentTypes {
	public class CustomerCT {
		internal static void CreateCustomerCT(ClientContext ctx) {
			//[x]3.Title of customer (Using existing title)
			//[ ]4.Customer Logo(Picture)
			//[ ]5.Address
			//[ ]6.Main Contact Person
			//[ ]7.Office Phone
			//[ ]8.Mobile Phone
			//[ ]9.Email
			//[ ]10.Last Contacted(Date)
			//[ ]11.Last Order Made(Date, Read Only)V

			//Looking for The Contetype by ID!
			if (ctx.Web.ContentTypeExistsById(Constants.GUID.CustomerCT.CustomerCTGUID)) {
				ContentTypeCreationInformation ctci = new ContentTypeCreationInformation();
				ctci.Name = "ODA1_CustomerCT";
				ctci.Group = "ODA1";
				ctci.Id = Constants.GUID.CustomerCT.CustomerCTGUID;
				ctci.Description = "This is the contentype used for Office Devlopment 1 Mandetory Assinment";

				
				ContentType CustomerCT = ctx.Web.ContentTypes.Add(ctci);
				ctx.ExecuteQueryRetry();

				//FieldCreationInformation CustomerLogo = new FieldCreationInformation(???); //wip



				//ctx.Web.AddFieldToContentType(CustomerLogo);

			}
		}
	}
}