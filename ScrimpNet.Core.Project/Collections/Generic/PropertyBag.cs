using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
namespace ScrimpNet.Collections.Generic
{
	[Serializable()]
	[CollectionDataContract]
	[Citation("http://ianfnelson.com/archives/2006/09/17/a-serializeable-keyvaluepair-class/")]
	public class PropertyBag<TKey, TValue> :
	Collection<Property<TKey, TValue>>,ICloneable
	{
		public PropertyBag():base()
		{
			//for serialization purposes only
		}
		public void Add(TKey key, TValue value)
		{
			this.Add(new Property<TKey,
			TValue>(key, value));
		}

		public object Clone()
		{
			var newBag = new PropertyBag<TKey, TValue>();
			foreach (var prop in this)
			{
				newBag.Add((Property<TKey,TValue>)prop.Clone());
			}
			return newBag;
		}
	}
}
