using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Init.Taxonomi.Extensions;

namespace Init.Taxonomi {
	class Program {

		/// <summary>
		/// ********************
		/// ***Coments about slight lack of Secureity***
		/// I had to try it this way toget a handel 
		/// on diffrent ways handleing passwords
		/// *******************
		/// initalli I had made use of the ConsoleReadKey(true)
		/// ** password.AppendChar(Console.ReadKey(true)); **
		/// 
		/// </summary>
		/// <param name="args">
		/// args[0] [username] 
		/// args[1] [unsafePassword]
		/// args[2] [url]
		/// </param>
		static void Main(string[] args) {

			//Presets
			//Update this!
			string url = "https://Luviz.sharepoint.com";
			string username = "bardiajedi@luviz.onmicrosoft.com";

			//input form powershell 
			if (args.Length > 2) {
				SecureString securePassword = new SecureString();
				args[1].ToCharArray().ToList().ForEach(c => securePassword.AppendChar(c));
				ConnectSPO(args[2], args[0], securePassword);
				Console.WriteLine("Done!");
			}
			//login with presets
			else {
				Console.WriteLine("Logging in to:");
				Console.WriteLine(url);
				Console.WriteLine("Username: " + username);
				Console.Write("Passowrd: ");
				ConnectSPO(url, username, GetPassword());
				Console.WriteLine("Done!");
				Console.ReadLine();
			}

		}


		private static void InstorctionsToBeSend(ClientContext ctx) {
			TaxonomySession tSession = TaxonomySession.GetTaxonomySession(ctx);
			var defaultTermStore = tSession.GetDefaultSiteCollectionTermStore();
			ctx.Load(defaultTermStore, ts => ts.Groups);
			ctx.ExecuteQuery();
			Taxonomi.CustomTaxonomi.GenODA1Terms(defaultTermStore);
			Console.WriteLine("Groups in defaultTermStore:");
			defaultTermStore.Groups.Select(g => g.Name).ToList().ForEach(Console.WriteLine);
		}


		private static void ConnectSPO(string url, string username, SecureString password) {
			using (var ctx = new ClientContext(url)) {
				ctx.Credentials = new SharePointOnlineCredentials(username, password);
				Console.WriteLine("Executing Instroctions");
				InstorctionsToBeSend(ctx);

			}
		}


		private static SecureString GetPassword() {
			char charBuffer;
			SecureString password = new SecureString();
			while ((charBuffer = Console.ReadKey(true).KeyChar) != (char)ConsoleKey.Enter)
				password.AppendChar(charBuffer);
			Console.WriteLine();
			return password;
		}
	}
}
