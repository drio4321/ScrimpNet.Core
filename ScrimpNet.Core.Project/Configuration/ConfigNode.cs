using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScrimpNet.Configuration
{
	/// <summary>
	/// Common data for configuration element
	/// </summary>
	[DataContract]
	[Serializable]
	[KnownType(typeof(ConfigAttributeCollection))]
	public class ConfigNode
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ConfigNode()
		{
			Attributes = new ConfigAttributeCollection();
			NodeId = Guid.NewGuid();
		}

		/// <summary>
		/// Global unique identifier.  Used in conjunction with merge and rename operations. Value
		/// might not be set until after it is persisted to configruation store.  Implementations
		/// are free to set this value upon node creation but should be considered fixed. (ReadOnly)
		/// </summary>
		[DataMember]
		public Guid NodeId { get; set; }

		/// <summary>
		/// Node name (Required) (analogous to XML.Element.Name)
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Key value pairs of meta data associated with this node.  Keys must be unique for this node.
		/// Can be used to specific alternate values (e.g. applicationName, environment (e.g. dev, test, prod)) or provide additional
		/// context
		/// </summary>
		[DataMember]
		public ConfigAttributeCollection Attributes { get; set; }

		/// <summary>
		/// Textual description of the key:  what the key controls, valid values, etc.
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Full path from root to this node.  This value can be used to retrieve this node 
		/// from configuration store.  Most implementations will cacluclate this value at runtime
		/// and not store it in the configuration store. Value might not be set until it is persisted
		/// to configuration store.  Should be considered ReadOnly.
		/// </summary>
		[DataMember]
		public string FullPath { get; set; }

		/// <summary>
		/// Full path from root to the parent of this node.  If this node is top level node then path is null.  Most 
		/// implementations will caculate this value at run time and not store it in the configuration store.  Value
		/// might not be set until it is persisted to configruation store.
		/// </summary>
		[DataMember]
		public string ParentPath { get; set; }

		/// <summary>
		/// Gobal identifier of parent, if any.  If this node is top level node this ParentNodeId will be null
		/// </summary>
		[DataMember]
		public Guid? ParentNodeId { get; set; }

		/// <summary>
		/// Payload containing setting value
		/// </summary>
		[DataMember]
		public string Value { get; set; }

		/// <summary>
		/// .Net type of value.  Used in deserializing process.  If not specified framework will do 'best-guess' but usually 'System.String'
		/// </summary>
		[DataMember]
		public string ValueType { get; set; }

		/// <summary>
		/// Describes how Value should be serialized.  If not specified framework will do a 'best-guess'
		/// selection.
		/// </summary>
		[DataMember]
		public ConfigValueFormat ValueFormat { get; set; }

		public void SetValue(object value, ConfigValueFormat format = ConfigValueFormat.Default)
		{
			Type t = value.GetType();
			SetValue(t, value, format);
		}

		public void SetValue(Type valueType, object value, ConfigValueFormat format = ConfigValueFormat.Default)
		{
			this.ValueType  = valueType.AssemblyQualifiedName;
			this.ValueFormat = format;
			if (value == null)
			{
				this.Value = null;
			}
			else
			{
				this.Value = value.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetValue<T>()
		{
			return Transform.ConvertValue<T>(this.Value);
		}
		public T GetValue<T>(string attributeKey)
		{
			return Transform.ConvertValue<T>(this.Value);
		}
		public string GetValue()
		{
			return Value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="attributeKey"></param>
		/// <returns></returns>
		public string GetValue(string attributeKey)
		{
			return Value;
		}

	}
}
