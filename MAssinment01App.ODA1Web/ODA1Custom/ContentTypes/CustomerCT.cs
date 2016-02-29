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

			//Looking for The Contetype by ID!
			if (!ctx.Web.ContentTypeExistsById(Constants.GUID.CustomerCT.CustomerCTGUID)) {
				ContentTypeCreationInformation ctci = new ContentTypeCreationInformation();
				ctci.Name = "ODA1_CustomerCT";
				ctci.Group = "ODA1";
				ctci.Id = Constants.GUID.CustomerCT.CustomerCTGUID;
				ctci.Description = "This is the contentype used for Office Devlopment 1 Mandetory Assinment";


				ContentType customerCT = ctx.Web.ContentTypes.Add(ctci);
				ctx.ExecuteQueryRetry();
				try {
					ClearFields(ctx);

				}
				catch (Exception) {

				}
				CreateFields(ctx, customerCT);
			}
		}

		private static void CreateFields(ClientContext ctx, ContentType customerCT) {
			#region Customer logo
			FieldCreationInformation customerLogo = new FieldCreationInformation(FieldType.URL);
			customerLogo.DisplayName = "Logo";
			customerLogo.InternalName = "DispLogo";
			customerLogo.Group = "ODA1";
			customerLogo.Id = Constants.GUID.CustomerCT.CUSTOMER_LOGO.ToGuid();

			FieldUrl AddedCLOGO = ctx.Web.CreateField<FieldUrl>(customerLogo, false);
			//AddedCLOGO.DisplayFormat = UrlFieldFormatType.Image;
			//AddedCLOGO.Update();
			ctx.ExecuteQuery();
			ctx.Web.AddFieldToContentType(customerCT, AddedCLOGO);
			#endregion
			#region Address
			FieldCreationInformation address = new FieldCreationInformation(FieldType.Text);
			address.DisplayName = "Address";
			address.InternalName = "Address";
			address.Group = "ODA1";
			address.Id = Constants.GUID.CustomerCT.ADDRESS.ToGuid();
			ctx.Web.AddFieldToContentType(customerCT, ctx.Web.CreateField(address));
			#endregion
			#region Main Contact Person
			FieldCreationInformation contactPerson = new FieldCreationInformation(FieldType.Text);
			contactPerson.DisplayName = "Contact Person";
			contactPerson.InternalName = "contactPerson";
			contactPerson.Group = "ODA1";
			contactPerson.Id = Constants.GUID.CustomerCT.MAIN_CONTACT_PERSON.ToGuid();

			ctx.Web.AddFieldToContentType(customerCT, ctx.Web.CreateField(contactPerson));
			#endregion
			#region Office Phone
			FieldCreationInformation phoneOffice = new FieldCreationInformation(FieldType.Text);
			phoneOffice.DisplayName = "Office Phone";
			phoneOffice.InternalName = "phoneOffice";
			phoneOffice.Group = "ODA1";
			phoneOffice.Id = Constants.GUID.CustomerCT.PHONE_OFFICE.ToGuid();

			ctx.Web.AddFieldToContentType(customerCT, ctx.Web.CreateField(phoneOffice));
			#endregion
			#region Mobile Phone
			FieldCreationInformation phoneMobile = new FieldCreationInformation(FieldType.Text);
			phoneMobile.DisplayName = "Mobile";
			phoneMobile.InternalName = "phoneMobile";
			phoneMobile.Group = "ODA1";
			phoneMobile.Id = Constants.GUID.CustomerCT.PHONE_MOBILE.ToGuid();

			ctx.Web.AddFieldToContentType(customerCT, ctx.Web.CreateField(phoneMobile));
			#endregion
			#region Email
			FieldCreationInformation email = new FieldCreationInformation(FieldType.Text);
			email.DisplayName = "E-Mail";
			email.InternalName = "Email";
			email.Group = "ODA1";
			email.Id = Constants.GUID.CustomerCT.EMAIL.ToGuid();

			ctx.Web.AddFieldToContentType(customerCT, ctx.Web.CreateField(email));
			#endregion
			#region Last Contacted (Date)
			FieldCreationInformation lastContacted = new FieldCreationInformation(FieldType.DateTime);
			lastContacted.DisplayName = "Last Contacted";
			lastContacted.InternalName = "LastContacted";
			lastContacted.Group = "ODA1";
			lastContacted.Id = Constants.GUID.CustomerCT.LAST_CONTACTED.ToGuid();

			ctx.Web.AddFieldToContentType(customerCT, ctx.Web.CreateField(lastContacted));
			#endregion
			#region Last Order Made(Date, Read Only)
			FieldCreationInformation lastOrderMade = new FieldCreationInformation(FieldType.DateTime);
			lastOrderMade.DisplayName = "Last order made";
			lastOrderMade.InternalName = "LastOrderMade";
			lastOrderMade.Group = "ODA1";
			lastOrderMade.Id = Constants.GUID.CustomerCT.LAST_ORDER_MADE.ToGuid();

			FieldDateTime addedLastOderMade = ctx.Web.CreateField<FieldDateTime>(lastOrderMade, false);
			addedLastOderMade.ReadOnlyField = true;
			addedLastOderMade.Update();
			ctx.ExecuteQuery();

			ctx.Web.AddFieldToContentType(customerCT, addedLastOderMade);
			#endregion
		}

		private static void ClearFields(ClientContext ctx) {
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.CUSTOMER_LOGO);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.ADDRESS);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.MAIN_CONTACT_PERSON);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.PHONE_OFFICE);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.PHONE_MOBILE);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.EMAIL);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.LAST_CONTACTED);
			ctx.Web.RemoveFieldById(Constants.GUID.CustomerCT.LAST_ORDER_MADE);
		}


	}
}