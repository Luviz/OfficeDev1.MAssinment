using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace MAssinment01App.ODA1Web.Services {
	public class AppEventReceiver : IRemoteEventService {
		/// <summary>
		/// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
		/// </summary>
		/// <param name="properties">Holds information about the app event.</param>
		/// <returns>Holds information returned from the app event.</returns>
		public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties) {
			SPRemoteEventResult result = new SPRemoteEventResult();

			using (ClientContext ctx = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: false)) {
				if (ctx != null) {
					switch (properties.EventType) {
						case SPRemoteEventType.AppInstalled:
							Install(ctx);
							break;
						case SPRemoteEventType.AppUninstalling:
							break;
						default:
							break;
					}
				}
			}

			return result;
		}


		[SharePointContextFilter]
		private void Install(ClientContext ctx) {
			ODA1Custom.ContentTypes.CustomerCT.CreateCustomerCT(ctx);
			ODA1Custom.ContentTypes.OrderCT.CreateOrderCT(ctx);
		}




		/// <summary>
		/// This method is a required placeholder, but is not used by app events.
		/// </summary>
		/// <param name="properties">Unused.</param>
		public void ProcessOneWayEvent(SPRemoteEventProperties properties) {

		}

	}
}
