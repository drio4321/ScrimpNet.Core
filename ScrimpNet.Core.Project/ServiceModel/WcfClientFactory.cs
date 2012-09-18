/**
/// ScrimpNet.Core Library
/// Copyright © 2005-2012
///
/// This module is Copyright © 2005-2012 Steve Powell
/// All rights reserved.
///
/// This library is free software; you can redistribute it and/or
/// modify it under the terms of the Microsoft Public License (Ms-PL)
/// 
/// This library is distributed in the hope that it will be
/// useful, but WITHOUT ANY WARRANTY; without even the implied
/// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
/// PURPOSE.  See theMicrosoft Public License (Ms-PL) License for more
/// details.
///
/// You should have received a copy of the Microsoft Public License (Ms-PL)
/// License along with this library; if not you may 
/// find it here: http://www.opensource.org/licenses/ms-pl.html
///
/// Steve Powell, spowell@scrimpnet.com
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.ServiceModel.Channels;
using ScrimpNet.Reflection;

namespace ScrimpNet.ServiceModel
{
	/// <summary>
	/// Create WCF clients
	/// </summary>
	public static class WcfClientFactory
	{
		/// <summary>
		/// Create a new instance of a channel for a particular service interface
		/// </summary>
		/// <typeparam name="I">Service interface that is being instantiated</typeparam>
		/// <param name="ep">Hyrdrated paraemters to use while building client proxy</param>
		/// <returns>Client proxy to WCF service end point ready to be used by application</returns>
		public static I Create<I>(WcfEndPoint ep)
		{
			if (ep.Binding != WcfBindings.Direct)
			{
				Binding myBinding = ep.GetBinding();
				EndpointAddress myEndpoint = ep.WcfAddress;
				return new ChannelFactory<I>(myBinding, myEndpoint).CreateChannel();
			}

			return ProviderFactory<I>.GetInstance(ep.ProxyAddress);
		}
		/// <summary>
		/// Create a client proxy that does not need configuration.  Uses default parameters and BasicHttpBinding
		/// </summary>
		/// <typeparam name="I">Interface service contract that this connection will use</typeparam>
		/// <param name="uri">Host of service including any ports or paths URLs</param>
		/// <returns>Open client</returns>
		public static I Create<I>(string uri)
		{
			var ep = new WcfEndPoint()
			{
				EndPointAddressUrl = uri,
				Binding = WcfBindings.BasicHttp,
				ServiceContractTypeName = typeof(I).AssemblyQualifiedName
			};

			return Create<I>(ep);
		}

		/// <summary>
		/// Create a client proxy that uses an &lt;endpoint&gt; configuration section.
		/// </summary>
		/// <typeparam name="I">Interface of service contract that this connection will use</typeparam>
		/// <param name="uri">Host URL of service including any ports and paths</param>
		/// <param name="endPointConfigurationName">Name of end point configuration. NOTE: These parameters are in reverse of ChannelFactory constructor</param>
		/// <returns>Newly created open client</returns>
		public static I Create<I>(string uri, string endPointConfigurationName)
		{
			var ep = new WcfEndPoint()
			{
				EndPointAddressUrl = uri,
				ServiceContractTypeName = typeof(I).AssemblyQualifiedName,
				ConfigurationName = endPointConfigurationName
			};
			return Create<I>(ep);
		}

		/// <summary>
		/// Create a self-hosting instance
		/// </summary>
		/// <typeparam name="I">Interface of operation contracts</typeparam>
		/// <typeparam name="T">Implementation of operation contracts</typeparam>
		/// <returns></returns>
		public static I CreateSelfHost<T, I>()
		{
			WcfSelfHost<T, I> host = new WcfSelfHost<T, I>();
			return host.Client;
		}

		/// <summary>
		/// Close connection to service and dispose of it
		/// </summary>
		/// <param name="serviceProxy">Hydrated proxy</param>
		public static void CloseAndDispose(IClientChannel serviceProxy)
		{
			if (serviceProxy == null) return;
			if (serviceProxy.State == CommunicationState.Opened)
			{
				serviceProxy.Close();
			}
			serviceProxy.Dispose();
		}

	}
}
