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
					</OrderBy>
				</Query>";
	}
}