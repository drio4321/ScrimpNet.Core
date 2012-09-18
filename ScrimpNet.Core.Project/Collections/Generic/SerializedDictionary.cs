using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace ScrimpNet.Collections.Generic
{
	[Serializable()]
	[Citation("http://ianfnelson.com/archives/2006/09/17/a-serializeable-keyvaluepair-class/")]
	public class SerializedDictionary<TKey, TValue> :
	KeyedCollection<TKey, Property
	<TKey, TValue>>
	{
		protected override TKey GetKeyForItem(
		Property<TKey, TValue> item)
		{
			return item.Key;
		}
		public void Add(TKey key, TValue value)
		{
			this.Add(new Property<TKey,
			TValue>(key, value));
		}
	}
}
