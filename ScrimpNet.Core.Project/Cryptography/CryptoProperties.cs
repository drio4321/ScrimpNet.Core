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
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.ObjectModel;

namespace ScrimpNet.Cryptography
{
    /// <summary>
    /// List of key-value properties (also called 'attributes' or 'metadata') associated with an object.  Property keys may be any value the implementation desires but Crypto.PropertyTypes define those used in the library
    /// </summary>
    [Serializable]
    [CollectionDataContract(Name = "Properties", ItemName = "Property", Namespace = CoreConfig.WcfNamespace)]
    public class CryptoProperties : List<CryptoProperty>
    {
        public int IndexOf(string key)
        {
            return base.FindIndex(prop => string.Compare(prop.Name, key, false) == 0);
        }
        public CryptoProperty this[string key]
        {
            get
            {
                int index = IndexOf(key);
                if (index < 0)
                {
                    throw ExceptionFactory.New<IndexOutOfRangeException>("Unable to find property key '{0}'", key);
                }
                return this[index];
            }
            set
            {
            }
        }

        public void Add(string propertyName, string propertyValue, string propertyType = Crypto.PropertyValueTypes.String)
        {
            Add(new CryptoProperty()
            {
                Name = propertyName,
                ValueType = propertyType,
                Value = propertyValue
            });
        }

        public void Add(CryptoProperty item)
        {
            if (IndexOf(item.Name) >= 0)
            {
                throw ExceptionFactory.New<InvalidOperationException>("Key '{0}' already exists in property collection", item.Name);
            }
            base.Add(item);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(CryptoProperty item)
        {
            return this.Contains(item);
        }
        public bool Contains(string propertyType)
        {
            return IndexOf(propertyType) > 0;
        }

        public void CopyTo(CryptoProperty[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }


        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CryptoProperty item)
        {
            return this.Remove(item);
        }
    }



}
