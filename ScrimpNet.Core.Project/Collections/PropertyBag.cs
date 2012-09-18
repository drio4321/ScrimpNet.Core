using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Serialization;

namespace ScrimpNet.Collections
{
	/// <summary>
	/// Serializable List of key-value properties (also called 'attributes' or 'metadata').  The list keys are non-unique
	/// </summary>
	[Serializable]
	[CollectionDataContract(Name = "Properties", ItemName = "Property")]
	public class PropertyBag : IList<Property>, ICollection<Property>, IEnumerable<Property>, IEnumerable,ICloneable 
	{
		private List<Property> _items = new List<Property>();

		/// <summary>
		/// List of properties associated with this class.  Used primarly for serialization purposes and as the backing store for interface implemenation
		/// </summary>
		[DataMember]
		public List<Property> Items
		{
			get
			{
				return _items;
			}
			set
			{
				_items = value;
			}
		}

		public int IndexOf(string key)
		{
			return _items.FindIndex(prop => string.Compare(prop.Key, key, CoreConfig.IGNORECASE_FLAG) == 0);
		}
		public int IndexOf(Property item)
		{
			return IndexOf(item.Key);
		}

		public void Insert(int index, Property item)
		{
			_items.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}

		[XmlIgnore, SoapIgnore, IgnoreDataMember]
		public Property this[int index]
		{
			get
			{
				return Items[index];
			}
			set
			{
				Items[index] = value;
			}
		}

		[XmlIgnore,SoapIgnore,IgnoreDataMember]
		public Property this[string key]
		{
			get
			{
				int index = IndexOf(key);
				if (index < 0)
				{
					_items.Add(new Property()
					{
						 Key = key
					});
					index = IndexOf(key);
				}
				return this[index];
			}
			set
			{

			}
		}

		public void Add(string key, string value)
		{
			Add(new Property(){ Key = key, Value = value});
		}

		public void Add(Property item)
		{
			//if (IndexOf(item.Key) >= 0)
			//{
			//    throw ExceptionFactory.New<InvalidOperationException>("Key '{0}' already exists in property collection", item.Key);
			//}
			_items.Add((Property)item.Clone());
		}

		public void Clear()
		{
			_items.Clear();
		}

		public bool Contains(Property item)
		{
			return _items.Contains(item);
		}
		public bool Contains(string propertyType)
		{
			return IndexOf(propertyType) > 0;
		}

		public void CopyTo(Property[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _items.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(Property item)
		{
			return _items.Remove(item);
		}

		public IEnumerator<Property> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		public object Clone()
		{
			PropertyBag newBag = new PropertyBag();
			for (int x = 0; x < Items.Count; x++)
			{
				newBag.Items.Add((Property)Items[x].Clone());
			}
			return (object) newBag;
		}
	}

}
