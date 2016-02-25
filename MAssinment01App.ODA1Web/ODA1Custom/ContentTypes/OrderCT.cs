using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAssinment01App.ODA1Web.ODA1Custom.ContentTypes {
	public class OrderCT {

		//1. Title
		//2. Customer(Lookup to customer list)
		//3. Product(Taxonomy Field Multi - “Virus Protection”, ”Spam Protection”, “Internet Filtering” )
		//4. Amount(Number) – This is the amount of money made

		internal static void CreateOrderCT(ClientContext ctx) {

			//Looking for The Contetype by ID!
			if (!ctx.Web.ContentTypeExistsById(Constants.GUID.OrderCT.OrderCTGUID)) {
				ContentTypeCreationInformation ctci = new ContentTypeCreationInformation();
				ctci.Name = "ODA1_OrderCT";
				ctci.Group = "ODA1";
				ctci.Id = Constants.GUID.OrderCT.OrderCTGUID;
				ctci.Description = "This is the contentype used for Office Devlopment 1 Mandetory Assinment";


				ContentType orderCT = ctx.Web.ContentTypes.Add(ctci);
				ctx.ExecuteQueryRetry();

				try {
					ClearFields(ctx);
				}
				catch (Exception) {
					//yes im a noob
				}
				CreateFields(ctx, orderCT);
			}
		}

		private static void CreateFields(ClientContext ctx, ContentType orderCT) {
			#region Customer
			FieldCreationInformation customer = new FieldCreationInformation(FieldType.Lookup);
			customer.DisplayName = "Customer";
			customer.InternalName = "Customer";
			customer.Group = "ODA1";
			customer.Id = Constants.GUID.OrderCT.CUSTOMER.ToGuid();

			ctx.Web.AddFieldToContentType(orderCT, ctx.Web.CreateField<FieldUrl>(customer, false));
			#endregion
			#region Product
			TaxonomyFieldCreationInformation product = new TaxonomyFieldCreationInformation();

			product.DisplayName = "Product";
			product.InternalName = "Product_2";
			product.Group = "ODA1";
			product.TaxonomyItem = GetProductTermSet(ctx);
			
			product.Id = Constants.GUID.OrderCT.PRODUCT.ToGuid();
			var meh = ctx.Web.CreateTaxonomyField(product);
			ctx.ExecuteQuery();
			ctx.Web.WireUpTaxonomyField(meh, product.TaxonomyItem as TermSet);
			ctx.Web.AddFieldToContentType(orderCT, meh );
			#endregion
			#region Price
			FieldCreationInformation price = new FieldCreationInformation(FieldType.Currency);
			price.DisplayName = "Price";
			price.InternalName = "Price";
			price.Group = "ODA1";
			price.Id = Constants.GUID.OrderCT.PRICE.ToGuid();

			FieldUrl addedPrice = ctx.Web.CreateField<FieldUrl>(price, false);
			ctx.Web.AddFieldToContentType(orderCT, addedPrice);
			#endregion
		}

		private static TermSet GetProductTermSet(ClientContext ctx) {

			const string GUID_ODA1_TAXONOMI_TERMSET_PRODUCT = "b8e1b9a4-00e9-4c40-9726-63ecc1633e4e";

			var termSet = TaxonomySession.GetTaxonomySession(ctx)
				.GetDefaultSiteCollectionTermStore().GetTermSet(GUID_ODA1_TAXONOMI_TERMSET_PRODUCT.ToGuid());

			ctx.Load(termSet);
			ctx.ExecuteQuery();

			return termSet;
		}

		private static void ClearFields(ClientContext ctx) {
			ctx.Web.RemoveFieldById(Constants.GUID.OrderCT.CUSTOMER);
			ctx.Web.RemoveFieldById(Constants.GUID.OrderCT.PRICE);
			ctx.Web.RemoveFieldById(Constants.GUID.OrderCT.PRODUCT);
		}
	}
}