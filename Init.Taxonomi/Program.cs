using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Init.Taxonomi {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Doingsomething");
			args.ToList().ForEach(Console.WriteLine);
			Console.WriteLine("Done!");
			Console.ReadLine();
		}


		private static SecureString GetPassword() {
			char charBuffer;
			SecureString password = new SecureString();
			while ((charBuffer = Console.ReadKey(true).KeyChar) != (char)ConsoleKey.Enter)
				password.AppendChar(charBuffer);
			return password;
		}
	}
}
