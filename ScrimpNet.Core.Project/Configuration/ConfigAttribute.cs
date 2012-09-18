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
    /// A setting for a specific environment (prod, dev, etc) or machine
    /// </summary>
    [DataContract]
	[Serializable]
    public class ConfigAttribute
    {
		/// <summary>
		/// Default constructor
		/// </summary>
		public ConfigAttribute()
		{
			AttributeId = Guid.Empty;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">textual identifier of this attribute</param>
		/// <param name="value">value of this attribute</param>
		public ConfigAttribute(string name, string value):this()
		{
			Name = name;
			Value = value;
		}

        /// <summary>
        /// Unique identifier of this version
        /// </summary>
        [DataMember]
        public Guid AttributeId { get; set; }

        /// <summary>
        /// All settings in ScrimpNet configuration subsystem has a common identifier.
        /// Often used for setting comparison and merging between environments
        /// </summary>
        [DataMember]
        public Guid ConfigNodeId { get; set; }

        /// <summary>
        /// Specifies specific instance of VariantName.  (e.g. 'localHost','production')
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Configuration value to return
        /// </summary>
        [DataMember]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("ConfigAttribute[{0}]='{1}'",Name, Value);
        }
    }
}
