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

namespace ScrimpNet.Configuration
{
    /// <summary>
    /// Defines a single atomic key within the configuration settings space. 
    /// </summary>
    [DataContract]
	[Serializable]
    public partial class ConfigSetting:ConfigNode,IComparable,IEqualityComparer 
    {
		public ConfigSetting():base()
		{			
			ValueFormat = ConfigValueFormat.Default;
		}


		
        		
        public int CompareTo(object obj)
        {
            return (base.Equals(obj) == true) ? 0 : -1;
        }

        public bool Equals(object x, object y)
        {
            return (x as ConfigSetting).CompareTo(y) == 0;
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }

   
    }
}
