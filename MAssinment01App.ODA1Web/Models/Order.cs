
using System;

namespace MAssinment01App.ODA1Web.Models {
	public class Order {
		public int ID { get; set; }
		public string Title { get; set; }
		public miniCustomer Customer { get; set; }
		public string Product { get; set; }
		public double Price { get; set; }
		public DateTime Created { get; set; }
	}
	public class miniCustomer {
		public int ID { get; set; }
		public string Title { get; set; }
	}
}