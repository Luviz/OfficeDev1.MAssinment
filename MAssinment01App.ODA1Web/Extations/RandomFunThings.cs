using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAssinment01App.ODA1Web.Extations {
	public static class RandomFunThings {
		public static T GetRand<T>(this List<T> list) {
			Random r = new Random();
			return list.Any() ? list[r.Next(list.Count)] : default(T);
		}
	}
}