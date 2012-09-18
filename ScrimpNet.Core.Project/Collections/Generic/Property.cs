using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.Serialization;
namespace ScrimpNet.Collections.Generic
{
	/// <summary>
	/// It's just like a System.Collections.Generic.KeyValuePair,
	/// but the XmlSerializer will serialize the
	/// Key and Value properties!
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	[DataContract]
	[Citation("http://ianfnelson.com/archives/2006/09/17/a-serializeable-keyvaluepair-class/")]
	public class Property<TKey, TValue>:ICloneable
	{
		private TKey key;
		private TValue value;

		public Property()
		{

		}

		public Property(TKey key,
		TValue value)
		{
			this.key = key;
			this.value = value;
		}
		public override string ToString()
		{
			StringBuilder builder1 = new StringBuilder();
			builder1.Append('[');
			if (this.Key != null)
			{
				builder1.Append(this.Key.ToString());
			}
			builder1.Append(", ");
			if (this.Value != null)
			{
				builder1.Append(this.Value.ToString());
			}
			builder1.Append(']');
			return builder1.ToString();
		}
		/// <summary>
		/// Gets the Value in the Key/Value Pair
		/// </summary>
		[DataMember]
		public TValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}
		/// <summary>
		/// Gets the Key in the Key/Value pair
		/// </summary>
		[DataMember]
		public TKey Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		public object Clone()
		{
			return new Property<TKey,TValue>(this.key, this.value);
		}
	}
}
