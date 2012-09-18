using System;
namespace ScrimpNet.Diagnostics

{
	public interface ILog
	{
		void SetDispatcher(ILogDispatcher dispatcher);
		ScrimpNet.Diagnostics.LogMessage Critical(ScrimpNet.Diagnostics.LogMessage message);
		ScrimpNet.Diagnostics.LogMessage Critical(Exception exception);
		ScrimpNet.Diagnostics.LogMessage Critical(Exception exception, string messageNumber);
		ScrimpNet.Diagnostics.LogMessage Critical(Exception exception, string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Critical(Exception exception, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Critical(string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Critical(string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Debug(ScrimpNet.Diagnostics.LogMessage message);
		ScrimpNet.Diagnostics.LogMessage Debug(Exception exception);
		ScrimpNet.Diagnostics.LogMessage Debug(Exception exception, string messageNumber);
		ScrimpNet.Diagnostics.LogMessage Debug(Exception exception, string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Debug(Exception exception, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Debug(string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Debug(string messageText, params object[] args);
		void Dispose();
		void Dispose(bool isDisposing);
		ScrimpNet.Diagnostics.LogMessage Error(ScrimpNet.Diagnostics.LogMessage message);
		ScrimpNet.Diagnostics.LogMessage Error(Exception exception);
		ScrimpNet.Diagnostics.LogMessage Error(Exception exception, string messageNumber);
		ScrimpNet.Diagnostics.LogMessage Error(Exception exception, string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Error(Exception exception, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Error(string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Error(string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Information(ScrimpNet.Diagnostics.LogMessage message);
		ScrimpNet.Diagnostics.LogMessage Information(Exception exception);
		ScrimpNet.Diagnostics.LogMessage Information(Exception exception, string messageNumber);
		ScrimpNet.Diagnostics.LogMessage Information(Exception exception, string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Information(Exception exception, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Information(string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Information(string messageText, params object[] args);
		bool IsCriticalEnabled { get; set; }
		bool IsDebugEnabled { get; set; }
		bool IsErrorEnabled { get; set; }
		bool IsInformationEnabled { get; set; }
		bool IsLevelEnabled(ScrimpNet.Diagnostics.MessageLevel level);
		bool IsTraceEnabled { get; set; }
		bool IsWarningEnabled { get; set; }
		ScrimpNet.Diagnostics.LoggerLevels LogLevels { get; set; }
		string LogName { get; set; }
		T NewMessage<T>() where T : ScrimpNet.Diagnostics.LogMessage, new();
		T NewMessage<T>(string message, params object[] args) where T : ScrimpNet.Diagnostics.LogMessage, new();
		ScrimpNet.Diagnostics.TraceProbe NewTrace();
		ScrimpNet.Diagnostics.TraceProbe NewTrace(string traceCategory);
		event ScrimpNet.Diagnostics.Log.LogCritical OnLogCriticalEventHandler;
		event ScrimpNet.Diagnostics.Log.LogDebug OnLogDebugEventHandler;
		event ScrimpNet.Diagnostics.Log.LogError OnLogErrorEventHandler;
		event ScrimpNet.Diagnostics.Log.LogInformation OnLogInformationEventHandler;
		event ScrimpNet.Diagnostics.Log.LogTrace OnLogTraceEventHandler;
		event ScrimpNet.Diagnostics.Log.LogWarning OnLogWarningEventHandler;
		ScrimpNet.Diagnostics.LoggerLevels SetLogLevel(ScrimpNet.Diagnostics.LoggerLevels logLevel, bool value);
		ScrimpNet.Diagnostics.LogMessage Trace(ScrimpNet.Diagnostics.LogMessage message);
		ScrimpNet.Diagnostics.LogMessage Trace(System.Diagnostics.TraceEventType direction, System.Reflection.MemberInfo method);
		ScrimpNet.Diagnostics.LogMessage Trace(Exception exception);
		ScrimpNet.Diagnostics.LogMessage Trace(Exception exception, string messageNumber);
		ScrimpNet.Diagnostics.LogMessage Trace(Exception exception, string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Trace(Exception exception, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Trace(string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Trace(string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Warning(ScrimpNet.Diagnostics.LogMessage message);
		ScrimpNet.Diagnostics.LogMessage Warning(Exception exception);
		ScrimpNet.Diagnostics.LogMessage Warning(Exception exception, string messageNumber);
		ScrimpNet.Diagnostics.LogMessage Warning(Exception exception, string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Warning(Exception exception, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Warning(string messageNumber, string messageText, params object[] args);
		ScrimpNet.Diagnostics.LogMessage Warning(string messageText, params object[] args);
		void Write(ScrimpNet.Diagnostics.LogMessage message);
		void Write(ScrimpNet.Diagnostics.MessageLevel level, ScrimpNet.Diagnostics.MessagePriority priority, Exception ex);
		void Write(ScrimpNet.Diagnostics.MessageLevel level, ScrimpNet.Diagnostics.MessagePriority priority, string message, params object[] args);
		void Write(Exception ex);
		void Write(Exception ex, string message, params object[] args);
		void Write(object message);
	}
}
