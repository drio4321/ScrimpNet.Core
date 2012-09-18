using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using ScrimpNet.Reflection;
using System.Collections;
using ScrimpNet.Collections;

namespace ScrimpNet
{
	/// <summary>
	/// Serializable version of a .Net exception including all inner exceptions and public properties
	/// </summary>
	[DataContract(Namespace = CoreConfig.WcfNamespace)]
	[Serializable]
	public class LogException : ICloneable
	{
		static string[] _filterList = Reflector.GetPublicPropertyNames(typeof(Exception));

		public LogException()
		{
			Properties = new List<Property>();
			Data = new List<Property>();
		}

		public LogException(Exception ex)
			: this()
		{
			Message = ex.Message;
			Source = ex.Source;
			StackTrace = ex.StackTrace;
			HelpLink = ex.HelpLink;
			ExceptionType = ex.GetType().FullName;
			if (ex.InnerException != null)
			{
				InnerException = new LogException(ex.InnerException );
			}

			foreach (object key in ex.Data.Keys)
			{
				string keyString = key.ToString();
				Data.Add(new Property(keyString, ex.Data[key]));
			}

			//-------------------------------------------------------
			// use reflection to get all public properties on the
			//	exception being examined except those defined in 
			//	_filterList (generally just those in base Exception class)
			//-------------------------------------------------------			
			this.Properties = Reflector.GetPublicProperties(ex, _filterList);
			
			TargetSite = (ex.TargetSite == null)?"(null)":ex.TargetSite.ToString();
			ErrorCode = Reflector.GetProtectedProperty<int>("HResult", ex, default(int)).ToString();
		}

		[DataMember]
		public List<Property> Properties { get; set; }

		[DataMember]
		public LogException InnerException { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public string Source { get; set; }

		[DataMember]
		public string StackTrace { get; set; }

		[DataMember]
		public string HelpLink { get; set; }

		[DataMember]
		public string TargetSite { get; set; }

		[DataMember]
		public List<Property> Data { get; set; }

		/// <summary>
		///  A coded value that is assigned to a specific exception. Often the HRESULT from the attached exception 
		/// </summary>
		[DataMember]
		public string ErrorCode { get; set; }

		/// <summary>
		/// .Net data type of exception
		/// </summary>
		[DataMember]
		public string ExceptionType { get; set; }

		/// <summary>
		/// Get's the innermost exception or a reference to this instance if there are no inner exceptions
		/// </summary>
		/// <returns></returns>
		public LogException GetBaseException()
		{
			var retEx = this;
			while (retEx.InnerException != null)
			{
				retEx = retEx.InnerException;
			}
			return retEx;
		}

		public object Clone()
		{
			var retEx = new LogException()
			{
				ErrorCode = this.ErrorCode,
				ExceptionType = this.ExceptionType,
				HelpLink = this.HelpLink,
				Message = this.Message,
				Source = this.Source,
				StackTrace = this.StackTrace,
				TargetSite = this.TargetSite
			};
			if (InnerException != null)
			{
				retEx.InnerException = (LogException)InnerException.Clone();
			}
			if (Data != null && Data.Count > 0)
			{
				foreach (var item in Data)
				{
					retEx.Data.Add(item.Clone() as Property);
				}
			}

			if (Properties != null && Properties.Count > 0)
			{
				foreach (var prop in Properties)
				{
					retEx.Properties.Add(prop.Clone() as Property);
				}
			}
			return retEx;
		}
		
		public override string ToString()
		{
			return this.Expand();
		}
	}
}
