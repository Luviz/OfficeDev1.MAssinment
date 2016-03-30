using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAssinment01App.ODA1Web.Models {
	public class Customer {
		public int ID { get; set; }
		public string Title { get; set; }
		public string Logo { get; set; }
		public string Address { get; set; }
		public string ContactPerson { get; set; }
		public string OfficePhone { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public DateTime Created { get; set; }

		public List<Order> Orders { get; set; }
	}
}