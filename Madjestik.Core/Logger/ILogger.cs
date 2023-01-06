using System;

namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The logger interface
	/// </summary>
	public interface ILogger
	{
		#region Events

		/// <summary>
		/// The before log event
		/// </summary>
		public event EventHandler<BeforeLogEventArgs> OnBeforeLog;

		/// <summary>
		/// The after log event
		/// </summary>
		public event EventHandler<AfterLogEventArgs> OnAfterLog;

		/// <summary>
		/// The before index log event
		/// </summary>
		public event EventHandler<BeforeIndexLogEventArgs> OnBeforeIndexLog;

		/// <summary>
		/// The after index log event
		/// </summary>
		public event EventHandler<AfterIndexLogEventArgs> OnAfterIndexLog;

		/// <summary>
		/// The before change level event
		/// </summary>
		public event EventHandler<BeforeChangeLevelLogEventArgs> OnBeforeChangeLevel;

		/// <summary>
		/// The after change level event
		/// </summary>
		public event EventHandler<AfterChangeLevelLogEventArgs> OnAfterChangeLevel;

		/// <summary>
		/// The before clear event
		/// </summary>
		public event EventHandler<BeforeClearEventArgs> OnBeforeClear;

		/// <summary>
		/// The after clear event
		/// </summary>
		public event EventHandler<AfterClearEventArgs> OnAfterClear;

		#endregion

		#region Properties

		/// <summary>
		/// The current log level
		/// </summary>
		public LogLevel CurrentLevel { get; set; }

		/// <summary>
		/// The disabled flag
		/// </summary>
		public bool Disabled { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Initialize logger from arguments
		/// </summary>
		/// <param name="args">The arguments</param>
		public void InitializeLogLevelFromArguments(string[] args);

		/// <summary>
		/// Clear all entries
		/// </summary>
		public void Clear();

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logModel">The log model</param>
		/// <returns>Returns true if logged</returns>
		public bool Log(LogModel logModel);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="message">The message to log</param>
		/// <returns>Returns true if logged</returns>
		public bool Log(LogLevel logLevel, string message, DateTime? logDate = null);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Debug(string message);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public void Debug(Exception exception, string message = null);

		/// <summary>
		/// Debug log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public void Debug(object target, string message = null);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Info(string message);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public void Info(Exception exception, string message = null);

		/// <summary>
		/// Info log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public void Info(object target, string message = null);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Warning(string message);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public void Warning(Exception exception, string message = null);

		/// <summary>
		/// Warning log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public void Warning(object target, string message = null);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Error(string message);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public void Error(Exception exception, string message = null);

		/// <summary>
		/// Error log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public void Error(object target, string message = null);

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public void Fatal(string message);

		/// <summary>
		/// Fatal log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public void Fatal(Exception exception, string message = null);

		/// <summary>
		/// Fatal log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public void Fatal(object target, string message = null);

		#endregion
	}
}