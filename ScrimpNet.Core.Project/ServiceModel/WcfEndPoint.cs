using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml.Serialization;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace ScrimpNet.ServiceModel
{

	/// <summary>
	/// Defines an client side end-point that will be used to connect to a WCF web service
	/// </summary>
	/// <remarks>
	/// Not XML serializable since TimeSpan is not XML serializable
	/// </remarks>
	[DataContract]
	[Serializable]
	public class WcfEndPoint
	{
		/// <summary>
		/// True to bypass the proxy server for local addresses (Default: true).  Note: when using traffic capture utilities (e.g. Fiddler, SOAP UI)
		/// this value might need to be 'True'
		/// </summary>
		[DefaultValue(true)]
		[DataMember]
		public bool ByPassProxyOnLocal { get; set; }

		/// <summary>
		/// True to use proxy as defined on operating system (e.g. via Internet Explorer). (Default: true).   Note: when using traffic capture utilities (e.g. Fiddler, SOAP UI)
		/// this value might need to be 'True'
		/// </summary>
		[DefaultValue(true)]
		[DataMember]
		public bool UseDefaultProxy { get; set; }

		/// <summary>
		/// Gets the interval of time after which the close method, invoked by a communication object, times out. Default ("00:01:00")
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:01:00")]
		[DataMember]
		public TimeSpan CloseTimeout { get; set; }

		/// <summary>
		/// Gets the interval of time after which the open method, invoked by a communication object, times out.
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:01:00")]
		[DataMember]
		[XmlElement(Type=typeof(SoapDuration))] 
		public TimeSpan OpenTimeout { get; set; }

		/// <summary>
		/// Gets the interval of time after which the receive method, invoked by a communication object, times out. NOTE: Might need to extend this
		/// value during debugging or for large data sets
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:10:00")]
		[DataMember]
		public TimeSpan ReceiveTimeout { get; set; }

		///<summary>
		/// Gets the interval of time after which the send method, invoked by a communication object, times out.
		/// </summary>
		[DefaultValue(typeof(TimeSpan),"00:01:00")]
		[DataMember]
		public TimeSpan SendTimeout { get; set; }

		/// <summary>
		///  Gets or sets the maximum amount of memory that is allocated for use by the
		///     manager of the message buffers that receive messages from the channel. (Default: 524288, 0x8000)
		/// </summary>
		[DefaultValue(524288)]
		[DataMember]
		public long MaxBufferPoolSizeBytes { get; set; }

		/// <summary>
		/// The maximum size, in bytes, of a buffer that stores messages while they are
		///     processed for an endpoint configured with this binding. Default: int.MaxValue bytes)
		/// </summary>
		[DefaultValue(int.MaxValue)]
		[DataMember]
		public int MaxBufferSize { get; set; }

		/// <summary>
		/// Gets or sets the maximum size for a message that can be received on a channel
		///     configured with this binding. (Default:int.Max)
		/// </summary>
		[DefaultValue(int.MaxValue)]
		[DataMember]
		public long MaxReceivedMessageSize { get; set; }

		/// <summary>
		/// Determines what kind of binding this end point will use to connect to service with. (Default: BasicHttp).  Note: If WcfBinding.Direct then ProxyAddress will contain fully qualified name of assembly implementing ServiceContractTypeName
		/// </summary>
		[DefaultValue(typeof(WcfBindings), "BasicHttp")]		
		[DataMember]
		public WcfBindings Binding { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public WcfEndPoint()
		{
			MaxArrayLength = ushort.MaxValue;
			MaxBytesPerRead = ushort.MaxValue;
			MaxDepth = ushort.MaxValue;
			MaxNameTableCharCount = ushort.MaxValue;
			MaxStringContentLength = int.MaxValue;

			CloseTimeout = new TimeSpan(0, 1, 0);
			OpenTimeout = new TimeSpan(0, 1, 0);
			ReceiveTimeout = new TimeSpan(0, 10, 0);
			SendTimeout = new TimeSpan(0, 1, 0);

			ByPassProxyOnLocal = true;
			UseDefaultProxy = true;

			MaxBufferPoolSizeBytes = 524288;
			MaxBufferSize = ushort.MaxValue;
			MaxReceivedMessageSize = int.MaxValue;

			Binding = WcfBindings.BasicHttp;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="addressUrl">Fully qualified URL to reach the intended web service protocol://domain/service.svc </param>
		/// <param name="serviceContractType">Fully qualified namespace.type,assembly of service contract</param>
		public WcfEndPoint(string addressUrl, Type serviceContractType)
			: this()
		{
			EndPointAddressUrl = addressUrl;
			ServiceContractTypeName = serviceContractType.AssemblyQualifiedName;
		}

		/// <summary>
		/// Fully qualified end-point url protocol://domain/service.svc
		/// </summary>
		[DataMember]
		public string EndPointAddressUrl { get; set; }

		/// <summary>
		/// Fully qualified service interface (namespace.class,assemblyName.dll)
		/// </summary>
		[DataMember]
		public string ServiceContractTypeName { get; set; }

		/// <summary>
		/// WCF endpoint configuration to use.  If non-null then load client using the end-point as defined in configuration file instead of using parameters defined here
		/// </summary>
		[DataMember]
		public string ConfigurationName { get; set; }

		/// <summary>
		/// A positive integer that specifies the maximum allowed array length of data being received by Windows Communication Foundation (WCF) from a client. (Default: 
		/// </summary>
		[DataMember, DefaultValue(ushort.MaxValue)]
		public int MaxArrayLength { get; set; }

		/// <summary>
		/// A positive integer that specifies the maximum allowed bytes returned per read.
		/// </summary>
		[DataMember, DefaultValue(ushort.MaxValue)]
		public int MaxBytesPerRead { get; set; }

		/// <summary>
		/// A positive integer that specifies the maximum nested node depth per read.
		/// </summary>
		[DataMember, DefaultValue(ushort.MaxValue)]
		public int MaxDepth { get; set; }

		/// <summary>
		/// A positive integer that specifies the maximum characters allowed in a table name.
		/// </summary>
		[DataMember, DefaultValue(ushort.MaxValue)]
		public int MaxNameTableCharCount { get; set; }

		/// <summary>
		/// A positive integer that specifies the maximum characters allowed in XML element content
		/// </summary>
		[DataMember, DefaultValue(ushort.MaxValue)]
		public int MaxStringContentLength { get; set; }

		/// <summary>
		/// If specified, tells channel which proxy to use.  If Binding == WcfBinding.Direct then this value will contain fully qualified type,assembly name of class containing service implementation.
		/// </summary>
		[DataMember]
		public string ProxyAddress { get; set; }

		/// <summary>
		/// Get a binding based on values of this class
		/// </summary>
		/// <returns>Hydrated binding</returns>
		/// <exception cref="InvalidOperationException">Thrown if requested binding doesn't match one of the implemented bindings</exception>
		public Binding GetBinding()
		{
			System.ServiceModel.Channels.Binding binder = null;
			switch (this.Binding)
			{
				case WcfBindings.BasicHttp:
					if (!string.IsNullOrWhiteSpace(ConfigurationName))
					{
						binder = new BasicHttpBinding(ConfigurationName);
					}
					else
					{
						binder = new BasicHttpBinding()
						{
							BypassProxyOnLocal = ByPassProxyOnLocal,
							MaxBufferPoolSize = MaxBufferSize,
							MaxReceivedMessageSize = MaxReceivedMessageSize,
							//UseDefaultWebProxy = UseDefaultProxy,							
							ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
							{
								MaxArrayLength = MaxArrayLength,
								MaxBytesPerRead = MaxBytesPerRead,
								MaxDepth = MaxDepth,
								MaxNameTableCharCount = MaxNameTableCharCount,
								MaxStringContentLength = MaxStringContentLength
							}
						};
						
					}
					break;
				case WcfBindings.BasicContext:
					if (!string.IsNullOrWhiteSpace(ConfigurationName))
					{
						binder = new BasicHttpContextBinding(ConfigurationName);
					}
					else
					{
						binder = new BasicHttpContextBinding()
						{
							BypassProxyOnLocal = ByPassProxyOnLocal,
							MaxBufferPoolSize = MaxBufferSize,
							MaxReceivedMessageSize = MaxReceivedMessageSize,
							UseDefaultWebProxy = UseDefaultProxy,
							ProxyAddress = new Uri(ProxyAddress),
							ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
							{
								MaxArrayLength = MaxArrayLength,
								MaxBytesPerRead = MaxBytesPerRead,
								MaxDepth = MaxDepth,
								MaxNameTableCharCount = MaxNameTableCharCount,
								MaxStringContentLength = MaxStringContentLength
							}
						};
					}
					break;
				case WcfBindings.NetTcp:
					if (!string.IsNullOrWhiteSpace(ConfigurationName))
					{
						binder = new NetTcpBinding(ConfigurationName);
					}
					else
					{
						binder = new NetTcpBinding()
							{
								MaxBufferPoolSize = MaxBufferSize,
								MaxReceivedMessageSize = MaxReceivedMessageSize,
								ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
								{
									MaxArrayLength = MaxArrayLength,
									MaxBytesPerRead = MaxBytesPerRead,
									MaxDepth = MaxDepth,
									MaxNameTableCharCount = MaxNameTableCharCount,
									MaxStringContentLength = MaxStringContentLength
								}
							};
					}
					break;
				case WcfBindings.WebHttp:
					if (!string.IsNullOrWhiteSpace(ConfigurationName))
					{
						binder = new WebHttpBinding(ConfigurationName);
					}
					else
					{
						binder = new WebHttpBinding()
						{
							BypassProxyOnLocal = ByPassProxyOnLocal,
							MaxBufferPoolSize = MaxBufferSize,
							MaxReceivedMessageSize = MaxReceivedMessageSize,
							UseDefaultWebProxy = UseDefaultProxy,
							ProxyAddress = new Uri(ProxyAddress),
							ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
							{
								MaxArrayLength = MaxArrayLength,
								MaxBytesPerRead = MaxBytesPerRead,
								MaxDepth = MaxDepth,
								MaxNameTableCharCount = MaxNameTableCharCount,
								MaxStringContentLength = MaxStringContentLength
							}
						};
					}
					break;
				case WcfBindings.WsHttpBinding:
					if (!string.IsNullOrWhiteSpace(ConfigurationName))
					{
						binder = new WSHttpBinding(ConfigurationName);
					}
					else
					{
						binder = new WSHttpBinding()
							{
								BypassProxyOnLocal = ByPassProxyOnLocal,
								MaxBufferPoolSize = MaxBufferSize,
								MaxReceivedMessageSize = MaxReceivedMessageSize,
								UseDefaultWebProxy = UseDefaultProxy,
								ProxyAddress = new Uri(ProxyAddress),
								ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
								{
									MaxArrayLength = MaxArrayLength,
									MaxBytesPerRead = MaxBytesPerRead,
									MaxDepth = MaxDepth,
									MaxNameTableCharCount = MaxNameTableCharCount,
									MaxStringContentLength = MaxStringContentLength
								}
							};
					}
					break;
				case WcfBindings.WsHttpContext:
					if (!string.IsNullOrWhiteSpace(ConfigurationName))
					{
						binder = new WSHttpContextBinding(ConfigurationName);
					}
					else
					{
						binder = new WSHttpContextBinding()
						{
							BypassProxyOnLocal = ByPassProxyOnLocal,
							MaxBufferPoolSize = MaxBufferSize,
							MaxReceivedMessageSize = MaxReceivedMessageSize,
							UseDefaultWebProxy = UseDefaultProxy,
							ProxyAddress = new Uri(ProxyAddress),
							ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
							{
								MaxArrayLength = MaxArrayLength,
								MaxBytesPerRead = MaxBytesPerRead,
								MaxDepth = MaxDepth,
								MaxNameTableCharCount = MaxNameTableCharCount,
								MaxStringContentLength = MaxStringContentLength
							}
						};
					}
					break;
				default:
					throw ExceptionFactory.New<InvalidOperationException>("Unable to instantiante a binding of '{0}'", this.Binding.ToString());
			}

			binder.CloseTimeout = CloseTimeout;
			binder.OpenTimeout = OpenTimeout;
			binder.ReceiveTimeout = ReceiveTimeout;
			binder.SendTimeout = SendTimeout;
			return binder;
		}

		/// <summary>
		/// Returns an address based on values in this class and is suitable for creating an WCF client channel
		/// </summary>
		/// <returns>Hyrdated WCF endpoint address of server/host for client to connect to</returns>
		public EndpointAddress WcfAddress
		{
			get
			{
				return new EndpointAddress(this.EndPointAddressUrl);
			}
		}
	}
}
