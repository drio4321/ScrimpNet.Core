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

namespace ScrimpNet.Configuration
{
    /// <summary>
    /// List of all variants for a specific setting (dev, prod, etc)
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ConfigAttribute))]
	[Serializable]
    public class ConfigAttributeCollection : List<ConfigAttribute>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
		public ConfigAttributeCollection()
		{

		}

		/// <summary>
		/// Add a new node to existing collection
		/// </summary>
		/// <param name="newAttribute">Existing attribute.</param>
		public new void Add(ConfigAttribute newAttribute)
		{
			base.Add(newAttribute);
		}

		/// <summary>
		/// Add an attribute to this collection
		/// </summary>
		/// <param name="name">Textual key of this attribute</param>
		/// <param name="value">Content value of this attribute</param>
		public new void Add(string name, string value)
		{
			this.Add(new ConfigAttribute(name, value));
		}

		/// <summary>
		/// Set/reset config node all these attributes belong to.  NOTE: overwrites any existing value
		/// </summary>
		/// <param name="ownerId">Owner these attributes belong to</param>
		public void SetOwnerId(Guid ownerId)
		{
			foreach (var attr in this)
			{
				attr.ConfigNodeId = ownerId;
			}
		}
    }
}
