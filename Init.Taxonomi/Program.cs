using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Init.Taxonomi {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Doingsomething");
			args.ToList().ForEach(Console.WriteLine);
		}
	}
}
