using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Entities;
using Init.Taxonomi.Extensions;

namespace Init.Taxonomi.Taxonomi {
	class CustomTaxonomi {

		internal static void GenODA1Terms(TermStore defaultTermStore) {
			var ProductTS = defaultTermStore.GenerateGroup("ODA1", ConstGUIDs.GUID_G_ODA1)
				.GenerateTermSet("Product", ConstGUIDs.GUID_TS_PRODUCT);

			ProductTS.AddTerm("Virus Protection", ConstGUIDs.GUID_T_VP);
			ProductTS.AddTerm("Spam Protection", ConstGUIDs.GUID_T_SP);
			ProductTS.AddTerm("Internet Filtering", ConstGUIDs.GUID_T_IF).Context.ExecuteQuery(); 
		}
	}
}
