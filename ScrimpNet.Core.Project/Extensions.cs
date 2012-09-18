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
using System.Data;
using System.Reflection;
using System.IO;
using ScrimpNet.IO;

namespace ScrimpNet
{
    public static partial class Extensions
    {


        /// <summary>
        /// Convert a string into a target type
        /// </summary>
        /// <typeparam name="T">Type to covnert value into</typeparam>
        /// <param name="valueToConvert">Value being converted</param>
        /// <returns>Converted value</returns>
        public static T ConvertTo<T>(this string valueToConvert)
        {
            return Transform.ConvertValue<T>(valueToConvert);
        }

        /// <summary>
        /// Convert a string into a target type or return a default value if an error occurs
        /// </summary>
        /// <typeparam name="T">Type to convert value into</typeparam>
        /// <param name="valueToConvert">Value being converted</param>
        /// <param name="defaultValue">Returned value on conversion errors</param>
        /// <returns>Converted value or defaultValue on error</returns>
        public static T ConvertTo<T>(this string valueToConvert, T defaultValue)
        {
            try
            {
                return Transform.ConvertValue<T>(valueToConvert);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// (ScrimpNet.Core extension) Binary serialize an object to array of bytes.  This is an extension method so it will work on any object in your code after you have a 
        /// reference to ScrimpNet.Core
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Array of bytes of object or null if obj is null</returns>
        public static byte[] ToBytes(this object obj)
        {
            return IOUtils.ToBytes(obj);
        }

        /// <summary>
        /// (ScrimpNet.Core extension) Converts a DateTime to a value that is compatible with SQL Server 2005 and SQL Server 2008
        /// </summary>
        /// <param name="target">Value to constrain</param>
        /// <returns>A datetime value that is valid for storing in SQL Server 2005 and SQL Server 2008</returns>
        public static DateTime SqlDate(this DateTime target)
        {
            return Utils.Date.ToSqlDate(target);
        }

		/// <summary>
		/// Add a key,value pair to exception's data collection
		/// </summary>
		/// <param name="ex">Exception to whose data collection an item is being added</param>
		/// <param name="paramName">Name of parameter to add to exception's data collection</param>
		/// <param name="paramValue">Value of parameter being added</param>
		/// <param name="args">Any optional arguments to supply to parameter value text</param>
		/// <returns>Reference to target exception</returns>
		public static Exception AddParam(this Exception ex, string paramName, object paramValue, params object[] args)
		{
			ex.Data[paramName] = string.Format(paramValue.ToString(), args);
			return ex;
		}
        /// <summary>
        /// (ScrimpNet.Core extension) Creates a string of all exception properties
        /// </summary>
        /// <param name="ex">Exception to expand</param>
        /// <returns>Well defined string of all exception properties; includes all inner exceptions</returns>
        public static string Expand(this Exception ex)
        {
            return Utils.Expand(ex);
        }

        /// <summary>
        /// (ScrimpNet.Core extension) Creates a string of all exception properties
        /// </summary>
        /// <param name="ex">Exception to expand</param>
        /// <param name="offSet">Number of leading spacing characters to prepend to exception lines</param>
        /// <returns>Well defined string of all exception properties; includes all inner exceptions</returns>
        public static string Expand(this Exception ex, int offSet)
        {
            return Utils.Expand(ex,offSet);
        }

		/// <summary>
		/// (ScrimpNet.Core extension) Creates a string of all exception properties
		/// </summary>
		/// <param name="ex">Exception to expand</param>
		/// <returns>Well defined string of all exception properties; includes all inner exceptions</returns>
		public static string Expand(this LogException ex)
		{
			return Utils.Expand(ex);
		}

		/// <summary>
		/// (ScrimpNet.Core extension) Creates a string of all exception properties
		/// </summary>
		/// <param name="ex">Exception to expand</param>
		/// <param name="offSet">Number of leading spacing characters to prepend to exception lines</param>
		/// <returns>Well defined string of all exception properties; includes all inner exceptions</returns>
		public static string Expand(this LogException ex, int offSet)
		{
			return Utils.Expand(ex, offSet);
		}

    }
}
