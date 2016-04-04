using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAssinment01App.ODA1Web.Models.Logic {
	public static class Get_Product {
		internal static List<Product> GetProdcuts(this ClientContext ctx) {
			//guid to Product termSet -- from Init.Taxonomi.Taxonomi.ConstGuid --
			const string GUID_ODA1_TAXONOMI_TERMSET_PRODUCT = "b8e1b9a4-00e9-4c40-9726-63ecc1633e4e";
			var termSet = TaxonomySession.GetTaxonomySession(ctx)
							.GetDefaultSiteCollectionTermStore()
							.GetTermSet(GUID_ODA1_TAXONOMI_TERMSET_PRODUCT.ToGuid());

			ctx.Load(termSet);
			ctx.Load(termSet.Terms);
			ctx.ExecuteQuery();

			return ParceProducts(termSet.Terms);
		}

		

		private static List<Product> ParceProducts(TermCollection tc) {
			List<Product> ret = new List<Product>();
			foreach(Term term in tc) {
				Product p = new Product() {
					Guid = term.Id.ToString(),
					Label = term.Name
				};
				ret.Add(p);
			}
			return ret;
		}
	}
}