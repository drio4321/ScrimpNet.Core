﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ScrimpNet.Text;

namespace ScrimpNet
{
    /// <summary>
    /// Describes a single state item for an action response.  A response might have more than one message returning to the caller
    /// </summary>
    [DataContract]
    public class ActionReplyMessageItem : ICloneable
    {

        /// <summary>
        /// Level of importance this state is to the creator of the state.
        /// </summary>
        [DataMember]
        public ActionStatus Severity { get; set; }

        /// <summary>
        /// Any text application wants to associate with this state
        /// </summary>
        [DataMember]
        public string MessageText { get; set; }

        /// <summary>
        /// Used to store field/property names when implementing IDataErrorInfo.  Can be any user provided value
        /// </summary>
        [DataMember]
        public string ReferenceKey { get; set; }


        /// <summary>
        /// Optional .Net exception that should be associated with this state.
        /// </summary>
        [DataMember]
        public LogException Exception { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ActionReplyMessageItem()
        {
            Severity = ActionStatus.Unknown;
        }

        /// <summary>
        /// Creates an state message.  Default state: Information
        /// </summary>
        /// <param name="messageText">Text to associate with this state</param>
        /// <param name="args">Arguments to supply ot this text</param>
        public ActionReplyMessageItem(string messageText, params object[] args)
            : this()
        {
            Severity = ActionStatus.Information;
            MessageText = TextUtils.StringFormat(messageText, args);
        }

        /// <summary>
        /// Creates a state message
        /// </summary>
        /// <param name="severity">The type of message (info, error, etc)</param>
        /// <param name="messageText">Text to associate with this state</param>
        /// <param name="args">Arguments to supply ot this text</param>
        public ActionReplyMessageItem(ActionStatus severity, string messageText, params object[] args)
        {
            Severity = severity;
            MessageText = TextUtils.StringFormat(messageText, args);
        }

        /// <summary>
        /// Creates a state message
        /// </summary>
        /// <param name="referenceKey">Field/Property level name (used for IDataErrorInfo bindings)</param>
        /// <param name="severity">The type of message (info, error, etc)</param>
        /// <param name="messageText">Text to associate with this state</param>
        /// <param name="args">Arguments to supply ot this text</param>
        public ActionReplyMessageItem( string referenceKey, ActionStatus severity, string messageText, params object[] args)
        {
            Severity = severity;
            ReferenceKey = referenceKey;
            MessageText = TextUtils.StringFormat(messageText, args);
        }

        /// <summary>
        /// Creates an ERROR level message.  MessageText = ex.Message
        /// </summary>
        /// <param name="ex">Exception to associate with this message.  Should be serializable if state is going to be sent across application boundries</param>
        public ActionReplyMessageItem(Exception ex):this(ex, ActionStatus.InternalError)
        {
            
        }

        public ActionReplyMessageItem(Exception ex, ActionStatus severity)
        {
            Exception = new LogException(ex);
			if (ex != null)
			{
				MessageText = ex.Message;
			}
            
            Severity = severity;
        }
        /// <summary>
        /// Creates an ERROR level message.  MessageText = ex.Message
        /// </summary>
        /// <param name="referenceKey">Field/Property level name (used for IDataErrorInfo bindings)</param>
        /// <param name="ex">Exception to associate with this message.  Should be serializable if state is going to be sent across application boundries</param>
        public ActionReplyMessageItem(string referenceKey, Exception ex)
        {
            Exception = new LogException(ex);
			if (ex != null)
			{
				MessageText = ex.Message;
			}
            Severity = ActionStatus.InternalError;
            ReferenceKey = referenceKey;
        }


        /// <summary>
        /// Creates an ERROR level message
        /// </summary>
        /// <param name="ex">Exception to associate with this message.  Should be serializable if state is going to be sent across application boundries</param>
        /// <param name="messageText">Text to associate with this state</param>
        /// <param name="args">Arguments to supply ot this text</param>
        public ActionReplyMessageItem(Exception ex, string messageText, params object[] args)
        {
			Exception = new LogException(ex);
            MessageText = TextUtils.StringFormat(messageText, args);            
            Severity = ActionStatus.InternalError;
        }

        /// <summary>
        /// Creates an ERROR level message
        /// </summary>
        /// <param name="referenceKey">Field/Property level name (used for IDataErrorInfo bindings)</param>
        /// <param name="ex">Exception to associate with this message.  Should be serializable if state is going to be sent across application boundries</param>
        /// <param name="messageText">Text to associate with this state</param>
        /// <param name="args">Arguments to supply ot this text</param>
        public ActionReplyMessageItem(string referenceKey, Exception ex, string messageText, params object[] args)
        {
            Exception = new LogException(ex);
            MessageText = TextUtils.StringFormat(messageText, args);
            Severity = ActionStatus.InternalError;
            ReferenceKey = referenceKey;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="item"></param>
        public ActionReplyMessageItem(ActionReplyMessageItem item)
        {
            Exception = item.Exception;
            Severity = item.Severity;
            MessageText = item.MessageText;
            ReferenceKey = item.ReferenceKey;
        }

        /// <summary>
        /// Any error code greater than 400
        /// </summary>
        public bool IsError
        {
            get
            {
                return (int)Severity >= 400; //any severity greater than 400 are errors (see HTTP standard response codes)
            }
        }

        /// <summary>
        /// Provides a copy of this object.  
        /// </summary>
        /// <returns>Copy of this object (except for any attached exceptions.</returns>
        public object Clone()
        {
            return new ActionReplyMessageItem(
                 Severity = this.Severity,
                 MessageText = this.MessageText,
                 ReferenceKey = this.ReferenceKey,
                 Exception = (LogException)this.Exception.Clone()
            );
        }
    }

}
