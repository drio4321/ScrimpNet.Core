using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrimpNet;
using System.Collections;
using ScrimpNet.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using ScrimpNet.Collections;

namespace ScrimpNet
{

	/// <summary>
	/// A DTO that returns state (Error, Success) and data property bag back to caller.  Modeled after ASP.Net MVC
	/// ViewDataCollection.  Use this class as foundation for returning data/state back to caller (specifically
	/// code behind pages and web service calls)
	/// </summary>
	[DataContract]
	public partial class ActionReply : IEnumerable, IDataErrorInfo
	{
		List<Property> _dataItems = new List<Property>();

		/// <summary>
		/// Default constructor
		/// </summary>
		public ActionReply()
			: base()
		{
			Messages = new ActionReplyMessageList();
		}
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="result">Source to copy</param>
		public ActionReply(ActionReply result)
			: this()
		{
			//Payload = result.Payload;
		}
		/// <summary>
		/// Add a exception to model messages
		/// </summary>
		/// <param name="ex">Exception to add</param>
		public ActionReply(Exception ex):this()
		{
			Messages.Add(ex);
			this.Status = ActionStatus.InternalError;
		}
		/// <summary>
		/// Set the Model parameter with obj
		/// </summary>
		/// <param name="obj">Value to initialize Model parameter with</param>
		public ActionReply(object obj)
			: this()
		{
			//Payload = obj;
		}

		private List<ActionReply> _innerReplies;

		/// <summary>
		/// Get a list of all replies used to build up this reply.  Often used
		/// when aggregating operations before returning and caller wants 
		/// granular results.  
		/// </summary>
		[DataMember]
		public List<ActionReply> InnerReplies
		{
			get
			{
				if (_innerReplies == null)
				{
					_innerReplies = new List<ActionReply>();
				}
				return _innerReplies;
			}
			set
			{
				_innerReplies = value;
			}
		}


		private ActionStatus _status = ActionStatus.Unknown;
		/// <summary>
		/// General status of Request from the perspective of the
		/// services. Value is most severe between explicitly set and values associated with messages.  
		/// NOTE: Property can not be lowered in value once set to a higher value.
		/// </summary>
		[DataMember]
		public ActionStatus Status
		{
			get
			{
				if (_innerReplies != null)
				{
					foreach (var innerReply in _innerReplies) //child replies might have a more severe status
					{
						Status = innerReply.Status;
					}
				}
				return (ActionStatus)Math.Max((int)_status, (int)Messages.MaximumSeverity);
			}
			set
			{
				_status = (ActionStatus)(Math.Max((int)_status, (int)value));
			}
		}

		private Guid _correlationId = CoreConfig.ActivityId;
		/// <summary>
		/// System correlation context this message is created under
		/// </summary>
		[DataMember]
		public Guid CorrelationId
		{
			get
			{
				return _correlationId;
			}
			set
			{
				_correlationId = value;
			}
		}



		/// <summary>
		/// Messages or status (error, success), if any, called method wants to return.  ActionReply
		/// is considered to be in 'Valid/Success' state if there are no entries in Messages list or all
		/// entries are informational in nature
		/// </summary>
		[DataMember]
		public ActionReplyMessageList Messages { get; set; }

		/// <summary>
		/// True if there are no entries in Messages or all
		/// entries are informational in nature and ActionStatus has not been set to an error condition
		/// </summary>
		public bool IsValid
		{
			get
			{
				return Messages.IsValid && (int)Status < 400;
			}
		}

		/// <summary>
		/// Indexer to retrieve value from response values.  Note: this method returns any DATA items registered. this[key] returns any MESSAGES with key.
		/// </summary>
		/// <param name="key">Index of item to get</param>
		/// <returns>Object at key</returns>
		/// <exception cref="IndexOutOfRangeException">Thrown when key does not exist response</exception>
		public object DataItem(string key)
		{
			var item = _dataItems.FirstOrDefault(d => string.Compare(d.Key, key, true) == 0);
			object o;
			if (item == null)
			{
				throw new IndexOutOfRangeException(TextUtils.StringFormat("Unable to find key '{0}' in data item list", key));
			}
			return item;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dataItems.GetEnumerator();
		}

		/// <summary>
		/// IDataErrorInfo: Returns piple '|' delimited list of error messages or NULL if no error message
		/// </summary>
		public string Error
		{
			get
			{
				var retVal = Messages.Error;
				foreach (var reply in InnerReplies)
				{
					var innerMessage = reply.Error; 
					if (string.IsNullOrWhiteSpace(innerMessage) == false)
					{
						if (retVal != null && retVal.Length > 0)
						{
							retVal += "|";
						}
						retVal += innerMessage;
					}
				}
				return retVal;
			}
		}

		/// <summary>
		/// Returns any message associated with <paramref name="columnName"/>.  
		/// Note: this method returns any MESSAGES for key.  DataItem(key) returns DATA associated with this response
		/// </summary>
		/// <param name="columnName">Name of field or reference id for message to return</param>
		/// <returns>Message for <paramref name="columnName"/> or null if not found</returns>
		public string this[string columnName]
		{
			get
			{
				return Messages[columnName];
			}
		}

		/// <summary>
		/// Any data to return to caller if not included in Value.  Patterned from MVC.Net response object
		/// </summary>
		public List<Property> Data
		{
			get
			{
				return _dataItems;
			}
			set
			{
				_dataItems = value;
			}
		}

		/// <summary>
		/// Empty all data items (internal property bag)
		/// </summary>
		public void Clear()
		{
			_dataItems.Clear();
		}

		///// <summary>
		///// Checks to see if an item is a member of internal property bag
		///// </summary>
		///// <param name="item">Value to search for</param>
		///// <returns>True if found</returns>
		//public bool Contains(KeyValuePair<string, object> item)
		//{
		//    return _dataItems.Contains(item);
		//}

		/// <summary>
		/// Add/Replace a key/value pair in the data collection
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(string key, object value)
		{
			_dataItems.Add(new Property(key, value));
		}

		/// <summary>
		/// NOT implemented.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns number of items in internal property bag
		/// </summary>
		public int Count
		{
			get { return _dataItems.Count; }
		}

		/// <summary>
		/// Always returns FALSE
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}


		//-----------------------------------------------------------
		// convenience methods
		//-----------------------------------------------------------
		/// <summary>
		/// Merge source values (messages, exceptions, etc) into this reply setting status to most severe
		/// </summary>
		/// <param name="source">Values to be merged into this reply</param>
		/// <returns>Reference to this object with content merged</returns>
		public virtual ActionReply Merge(ActionReply source)
		{
			InnerReplies.Add(source);
			return this;
		}
		/// <summary>
		/// Captures exception and sets ActionStatus.InternalError
		/// </summary>
		/// <param name="ex">Exception to add to messages list</param>
		public virtual ActionReply SetError(Exception ex)
		{
			return SetError(ex, ActionStatus.InternalError);
		}

		/// <summary>
		/// Captures exception and sets action severity
		/// </summary>
		/// <param name="ex"></param>
		/// <param name="severity"></param>
		public virtual ActionReply SetError(Exception ex, ActionStatus severity)
		{
			Messages.Add(ex, severity);
			Status = severity;
			return this;
		}

		/// <summary>
		/// Set the status and return a reference to this 
		/// </summary>
		/// <param name="status">Status to set.  If this already has a higher reference then higher reference takes prescendence</param>
		/// <returns>Reference to this object</returns>
		public virtual ActionReply SetStatus(ActionStatus status)
		{
			Status = status;
			return this;
		}

		/// <summary>
		/// Set the status with a (optional) message and return the object (convenience method)
		/// </summary>
		/// <param name="status">Status to set response to</param>
		/// <param name="message">Text to be added to this response</param>
		/// <param name="args">List of arugments to supply to message</param>
		/// <returns>reference to this object</returns>
		public virtual ActionReply SetStatus(ActionStatus status, string message, params object[] args)
		{

			this.Messages.Add(status, message, args);
			return this;
		}

		/// <summary>
		/// Captures exception, sets severity, and writes reply to log.  (Convenience method)
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="severity"></param>
		/// <returns></returns>
		public virtual ActionReply SetErrorAndLog(Exception exception, ActionStatus severity)
		{
			Messages.Add(exception, severity);
			Status = severity;
			LogWrite();
			return this;
		}
		/// <summary>
		/// Captures exception, sets severity (ActionStatus.InternalError), and writes reply to log.  (Convenience method)
		/// </summary>
		/// <param name="exception"></param>
		/// <returns></returns>
		public virtual ActionReply SetErrorAndLog(Exception exception)
		{
			Messages.Add(exception, ActionStatus.InternalError);
			Status = ActionStatus.InternalError;
			LogWrite();
			return this;
		}

		/// <summary>
		/// Explicitly add message to reply that is considered by caller to be of a specific importance
		/// </summary>
		/// <param name="status">Callers preception on how important this message is</param>
		/// <param name="message">Textual portion of message</param>
		/// <param name="args">Arguments to supply to <paramref name="message"/></param>
		/// <returns>Reference to this object</returns>
		public ActionReply SetStatusAndLog(ActionStatus status, string message, params object[] args)
		{
			Status = status;
			Messages.Add(status, message, args);
			return this;
		}

		/// <summary>
		/// Convenience method to add formatted parameters values to reply.
		/// Default severity: information
		/// </summary>
		/// <param name="paramName">Name of parameter</param>
		/// <param name="paramValue">Value of parameter</param>
		public void AddParam(string paramName, object paramValue)
		{
			AddParam(ActionStatus.Information, paramName, paramValue);
		}

		/// <summary>
		/// Convenience method to add formatted parameter values to reply.  Default severity: information
		/// </summary>
		/// <param name="severity">Describes how important this parameter note is to be considered</param>
		/// <param name="paramName">Name of property or parameter</param>
		/// <param name="paramValue">Current value of parameter</param>
		public void AddParam(ActionStatus severity, string paramName, object paramValue)
		{
			ActionReplyMessageItem item = new ActionReplyMessageItem(paramName, severity, "Param: {0}: {1}", paramName, paramValue);
			item.Severity = severity;
			Messages.Add(item);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			GetDetails(sb);
			if (this.IsValid == false)
			{
				sb.AppendLine("Error(s): {0}", this.Error);
			}

			sb.AppendLine("Status: {0}", this.Status.ToString());
			sb.AppendLine("IsValid: {0}", this.IsValid.ToString());
			sb.AppendLine("CorrelationId: {0}", this.CorrelationId.ToString());
			sb.AppendLine("Messages: Total: {0}", this.Messages.Count());
			for (int x = 0; x < Messages.Count; x++)
			{
				var msg = Messages[x];
				sb.AppendLine("  Message[{0}] {2} {1}", x, msg.MessageText, msg.Severity.ToString());
				if (msg.Exception != null)
				{
					sb.Append("     {0}", msg.Exception.Expand(5));
				}
			}

			return sb.ToString();
		}

		protected virtual void GetDetails(StringBuilder sb)
		{

		}
	}

	/// <summary>
	/// A DTO that returns state (Error, Success) and data property bag back to caller.  Modeled after ASP.Net MVC
	/// ViewDataCollection.  Use this class as foundation for returning data/state back to caller (specifically
	/// code behind pages).  Use this class if returning a strongly typed Model contained in 'Value' property
	/// </summary>
	/// <typeparam name="T">Type the 'Value' property will take.</typeparam>
	[DataContract]
	public partial class ActionReply<T> : ActionReply
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ActionReply()			
		{
			if (typeof(T).IsPrimitive == true || typeof(T).FullName == "System.String")
			{
				Payload = default(T);
			}
			else
			{
				try
				{
					Payload = Activator.CreateInstance<T>();
				}
				catch (Exception ex)
				{
					//in these cases caller must initialize field;
					//throw new InvalidOperationException(string.Format("Unable to create an instance of '{0}'. {1}", typeof(T).FullName, ex.Expand()));
				}
			}
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="result"></param>
		public ActionReply(ActionReply<T> result)
			: base(result)
		{
			Payload = Activator.CreateInstance<T>();
		}

		/// <summary>
		/// Create new reply with ActionStatus.InternalError
		/// </summary>
		/// <param name="ex"></param>
		public ActionReply(Exception ex)
			: base(ex)
		{
			Payload = Activator.CreateInstance<T>();
		}

		/// <summary>
		/// Captures exception and sets ActionStatus.InternalError
		/// </summary>
		/// <param name="exception">Exception to add to messages list</param>
		public new ActionReply<T> SetError(Exception exception)
		{
			return this.SetError(exception, ActionStatus.InternalError);
		}

		/// <summary>
		/// Sets message status to InternalError, associates exception with this message, and writes entire message to log file
		/// </summary>
		/// <param name="exception">Hydrated, non-null exception</param>
		/// <returns>Reference to this hydrated class.  Useful for single line returns</returns>
		public new ActionReply<T> SetErrorAndLog(Exception exception)
		{
			return this.SetErrorAndLog(exception, ActionStatus.InternalError);
		}
		/// <summary>
		/// Captures exception details.  Use when wanting to set severity level to something other than default: ActionStatus.InternalError
		/// </summary>
		/// <param name="exception">Exception to add to messages list</param>
		/// <param name="severity">Message severity level.</param>
		public new ActionReply<T> SetError(Exception exception, ActionStatus severity)
		{
			base.SetError(exception, severity);
			return this;
		}

		/// <summary>
		/// Sets reply status and text (Convience method)
		/// </summary>
		/// <param name="status">Status to set message to</param>
		/// <param name="message">Text to associate with this reply</param>
		/// <param name="args">Any arguments to supply to text.</param>
		/// <returns>A reference to this object. Useful for single line returns</returns>
		public new ActionReply<T> SetStatusAndLog(ActionStatus status, string message, params object[] args)
		{
			base.SetStatusAndLog(status, message, args);
			return this;
		}

		/// <summary>
		/// Sets reply status (Convience method)
		/// </summary>
		/// <returns>A reference to this object. Useful for single line returns</returns>
		public new ActionReply<T> SetStatus(ActionStatus status)
		{
			base.SetStatus(status);
			return this;
		}

		/// <summary>
		/// Merge source values (messages, exceptions, etc) into this reply setting status to most severe
		/// </summary>
		/// <param name="source">Values to be merged into this reply</param>
		/// <returns>Reference to this object with content merged</returns>
		public ActionReply<T> Merge(ActionReply<T> source)
		{
			base.Merge((ActionReply)source);
			return this;
		}

		/// <summary>
		/// Merge all elements from source into this reply.  Does not merge Payload
		/// </summary>
		/// <param name="source">Hydrated reply that is being merged</param>
		/// <returns>Reference to this object with <paramref name="source"/> merged.</returns>
		public new ActionReply<T> Merge(ActionReply source)
		{
			base.Merge(source);
			return this;
		}

		/// <summary>
		/// Set severity to ActionStatus.Error and automatically log reply to configured ScrimpNet.Log (Convenience method)
		/// </summary>
		/// <param name="exception">Exception that will be associated with this reply.  Will append to any previously caught exceptions</param>
		/// <param name="severity">How important caller considers this exception</param>
		/// <returns>Reference to this object with exception and severity level set</returns>
		public new ActionReply<T> SetErrorAndLog(Exception exception, ActionStatus severity)
		{
			base.SetErrorAndLog(exception, severity);
			return this;
		}

		private T _payload;
		/// <summary>
		/// Gets/sets strongly typed object to use as the 'Model' property
		/// </summary>
		[DataMember]
		public new T Payload
		{
			get
			{
				return _payload;
			}
			set
			{
				_payload = value;
			}
		}

		public override string ToString()
		{
			return base.ToString();
		}

		protected override void GetDetails(StringBuilder sb)
		{
			if (Payload != null)
			{
				try
				{
					string s = Serialize.To.DataContract(Payload);
					sb.AppendLine("Payload ({0}): {1}", Payload.GetType().Name, s);
				}
				catch (Exception ex)
				{
					try
					{
						string s = Serialize.To.DataContract(Payload);
						sb.AppendLine("Error trying to serialize {0}. {1}", Payload.GetType().Name, ex.ToString());
					}
					catch
					{
						try
						{
							string s = Serialize.To.Soap(Payload);
							sb.AppendLine("Error trying to serialize {0}. {1}", Payload.GetType().Name, ex.ToString());
						}
						catch (Exception ex2)
						{
							string s = Payload.ToString();
							sb.AppendLine("Payload<{0}>.ToString(): ", Payload.GetType().Name, Payload.ToString());
							sb.AppendLine("Error trying to serialize {0}. {1}", Payload.GetType().Name, ex2.ToString());
						}
					}
				}
			}
		}
	}
}
