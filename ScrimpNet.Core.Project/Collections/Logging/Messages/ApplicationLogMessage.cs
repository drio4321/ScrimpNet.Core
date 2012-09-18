/*
// ScrimpNet.Core Library
/// Copyright © 2005-2011
///
/// This module is Copyright © 2005-2011 Steve Powell
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
using ScrimpNet.Text;
using System.Runtime.Serialization;

namespace ScrimpNet.Diagnostics
{
    /// <summary>
    /// Defines a single message to be persisted into log.  This class is used in application logging scenarios
    /// </summary>
    [DataContract(Namespace = CoreConfig.WcfNamespace)]
    public partial class ApplicationLogMessage : LogMessage
    {
        #region [Constructor(s)]

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationLogMessage()
            : base()
        {
           
        }

 
   

        #endregion


		RuntimeContext _runtimeContext = null;
        /// <summary>
        /// Place holder for gathering run time information.  Note:  This method is very heavy and should be
        /// used cautiously.  Use <see cref="T:RuntimeContext"/> constructors for information
        /// on capturing contexts.  If this field is populated, logging providers will generally persist
        /// values.
        /// </summary>
        [System.Xml.Serialization.XmlElement]
        [DataMember]
        public RuntimeContext RuntimeContext
        {
            get { return _runtimeContext; }
            set { _runtimeContext = value; }
        }

        /// <summary>
        /// Generates a standard output string.  EXTENSION:  Modify this class to change the format of the message being persisted
        /// </summary>
        /// <returns>A standard format of this message type</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(base.ToString());
      
				//if (this.RuntimeContext != null)
				//{
				//    if (this.Exception == null)
				//    {                 
				//        sb.AppendLine("   Machine:Stack: {0}", indentRow(this.RuntimeContext.MachineContext["StackTrace"],8));
				//    }
				//    else
				//    {
				//        sb.AppendLine("   Machine:Stack: {0}", "(see exception)");
				//    }

				//    for (int x = 0; x < RuntimeContext.MachineContext.Count; x++)
				//    {
				//        string key = RuntimeContext.MachineContext.Keys[x];
				//        if (key == "StackTrace") continue;  //this will be logged further down
				//        string value = RuntimeContext.MachineContext.Get(x);
				//        sb.AppendLine("   Machine[{0}]={1}", key, indentRow(value,8));
				//    }

				//}
               
				//if (this.RuntimeContext != null && RuntimeContext.HttpRequest.Count > 0)
				//{
				//    for (int x = 0; x < RuntimeContext.HttpRequest.Count; x++)
				//    {
				//        string key = RuntimeContext.HttpRequest.Keys[x];
				//        string value = RuntimeContext.HttpRequest.Get(x);
				//        sb.AppendLine("   HttpRequest[{0}]={1}", key, indentRow(value,8));
				//    }                   
				//}  			
            }
            catch (Exception ex)
            {
                Log.LastChanceLog(Utils.Expand(ex));
                Log.LastChanceLog(sb.ToString());
            }
            return sb.ToString();
        }
   
    }
}
