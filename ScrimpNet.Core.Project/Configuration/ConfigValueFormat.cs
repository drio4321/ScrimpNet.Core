using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScrimpNet.Configuration
{
	/// <summary>
	/// Specifies how the value in a configuration setting is serialized
	/// </summary>
	[DataContract]
	public enum ConfigValueFormat
	{
		/// <summary>
		/// Value is stored in default format, usually a value.ToString()
		/// </summary>
		[EnumMember]
		Default=0,
		
		/// <summary>
		/// Value is serialized XML
		/// </summary>
		[EnumMember]
		Xml=1,

		/// <summary>
		/// Value is serialized using DataContract serializer
		/// </summary>
		[EnumMember]
		DataContract=2,

		/// <summary>
		/// Value is serialized using JSON serializer
		/// </summary>
		[EnumMember]
		Json=3,

		/// <summary>
		/// Value is serialized to binary then converted to Base64
		/// </summary>
		[EnumMember]
		Binary=4
	}
}
