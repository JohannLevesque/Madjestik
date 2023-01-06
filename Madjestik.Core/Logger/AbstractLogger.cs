using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The abstract logger
	/// </summary>
	public abstract class AbstractLogger : ILogger
	{
		#region Constants

		/// <summary>
		/// The max kept entries
		/// </summary>
		public const int MAX_ENTRIES = 1024;

		/// <summary>
		/// The default log level
		/// </summary>
		private const LogLevel DEFAULT_LOG_LEVEL = LogLevel.INFO;

		/// <summary>
		/// The null log
		/// </summary>
		private const string LOG_NULL = "{null}";

		/// <summary>
		/// The empty log
		/// </summary>
		private const string LOG_EMPTY = "{empty}";

		/// <summary>
		/// The error object serialization log
		/// </summary>
		private const string LOG_OBJECT_ERROR = "{error:object serialization}";

		/// <summary>
		/// The delimiter count
		/// </summary>
		private const char DELIMITER_CHAR = '#';

		/// <summary>
		/// The delimiter count
		/// </summary>
		private const int DELIMITER_COUNT = 100;

		/// <summary>
		/// The program arg log prefixes
		/// </summary>
		public static readonly string[] INIT_ARG_LOG_LEVEL_PREFIXES = new string[] { "/l", "/level" };

		/// <summary>
		/// The program arg debug
		/// </summary>
		public static readonly string[] INIT_ARG_DEBUG_VALUES = new string[] { "/d", "/debug", "--debug" };

		/// <summary>
		/// Return the log delimiter
		/// </summary>
		private static string LOG_DELIMITER { get { return new string(AbstractLogger.DELIMITER_CHAR, AbstractLogger.DELIMITER_COUNT); } }

		/// <summary>
		/// The format for log exception
		/// </summary>
		private static readonly string FORMAT_LOG_EXCEPTION = @"
" + AbstractLogger.LOG_DELIMITER + @"
" + AbstractLogger.DELIMITER_CHAR + @" EXCEPTION: {0} 
" + AbstractLogger.LOG_DELIMITER;

		/// <summary>
		/// The format for log inner exception
		/// </summary>
		private static readonly string FORMAT_LOG_INNER_EXCEPTION = @"
INNER EXCEPTION: {0} 
" + AbstractLogger.LOG_DELIMITER;

		/// <summary>
		/// The format for stack trace
		/// </summary>
		private static readonly string FORMAT_LOG_STACK_TRACE = @"

STACK TRACE{1}: {0} 

" + AbstractLogger.LOG_DELIMITER + @"
";
		#endregion

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

		#region Attributes

		/// <summary>
		/// The log object options
		/// </summary>
		public static readonly JsonSerializerOptions LogObjectOptions = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		/// <summary>
		/// The log object
		/// </summary>
		public readonly List<LogModel> Logs = new List<LogModel>();

		#endregion

		#region Properties

		/// <summary>
		/// The current log level
		/// </summary>
		protected LogLevel currentLevel = AbstractLogger.DEFAULT_LOG_LEVEL;
		public virtual LogLevel CurrentLevel
		{
			get
			{
				// Return the current level
				return this.currentLevel;
			}
			set
			{
				// Set new log level on change
				if (this.CurrentLevel != value)
					this.SetCurrentLevel(value);
			}
		}

		/// <summary>
		/// The max entries
		/// </summary>
		public virtual int MaxEntries { get; set; } = AbstractLogger.MAX_ENTRIES;

		/// <summary>
		/// Flag to disable the logger
		/// </summary>
		public virtual bool Disabled { get; set; }

		/// <summary>
		/// The serialize options
		/// </summary>
		public virtual JsonSerializerOptions JsonSerializerOptions { get; set; } = AbstractLogger.LogObjectOptions;

		#endregion

		#region Static methods

		/// <summary>
		/// Initialize a logger from arguments
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="args">The arguments</param>
		public static void StaticInitializeLogLevelFromArguments(ILogger logger, string[] args)
		{
			// Check logger
			if (logger == null)
				throw new InvalidOperationException("Can not initialize from arguments a null logger");

			// Check for optional arguments
			if (args != null && args.Length > 0)
			{
				// Browse all args
				foreach (var arg in args)
				{
					// Check from arg
					if (string.IsNullOrEmpty(arg))
						continue;

					// Set as lowered
					var compared = arg.ToLower();

					// Check for log level
					var logLevelPrefixes = AbstractLogger.INIT_ARG_LOG_LEVEL_PREFIXES.Where(p => compared.StartsWith(p?.ToLower() + "="))
						.ToArray();
					foreach (var prefix in logLevelPrefixes)
					{
						// Check states
						if (string.IsNullOrEmpty(prefix))
							continue;

						// Check for parse value
						var value = compared[(prefix.Length + 1)..].ToString();
						if (!string.IsNullOrEmpty(value) && Enum.TryParse<LogLevel>(value.ToUpper(), out LogLevel parsedLogLevel))
						{
							// Set the logger level
							logger.CurrentLevel = parsedLogLevel;
							logger.Debug($"Console logger level set as {logger.CurrentLevel} (/l)");
						}
					}

					// Check for debug
					if (AbstractLogger.INIT_ARG_DEBUG_VALUES.Where(p => p?.ToLower() == compared).Any())
					{
						// Set the logger as debug
						logger.CurrentLevel = LogLevel.DEBUG;
						logger.Debug("Console logger level set as DEBUG (/d)");
					}
				}
			}
		}

		/// <summary>
		/// Check if should log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="currentLogLevel">The current log level</param>
		/// <returns>Returns true if should be logged</returns>
		public static bool StaticShouldLog(LogLevel logLevel, LogLevel currentLogLevel)
		{
			// Cast as integer
			var level = (int)currentLogLevel;
			var targetLevel = (int)logLevel;

			// Check cases
			return targetLevel >= level;
		}

		/// <summary>
		/// Build the log message
		/// </summary>
		/// <param name="message">The main message</param>
		/// <param name="title">The optional title</param>
		/// <param name="noFormat">The optional flag for no format</param>
		/// <returns>Returns the built log message</returns>
		public static string StaticBuildLogMessage(string message, string title = null, bool noFormat = false)
		{
			// Check for message
			if (string.IsNullOrEmpty(title))
				return noFormat ? message : AbstractLogger.StaticFormatLogMessage(message);

			// Return built message
			return $"{(noFormat ? title : AbstractLogger.StaticFormatLogMessage(title))}\n\n{(noFormat ? message : AbstractLogger.StaticFormatLogMessage(message))}";
		}

		/// <summary>
		/// Format the log
		/// </summary>
		/// <param name="message">The message to format</param>
		/// <returns>Returns the formated log message</returns>
		public static string StaticFormatLogMessage(string message)
		{
			// Check cases
			message ??= AbstractLogger.LOG_NULL;
			if (message == string.Empty)
				message = AbstractLogger.LOG_EMPTY;

			// Return the message
			return message;
		}

		/// <summary>
		/// The exception to string converter
		/// </summary>
		/// <param name="exception">The exception</param>
		/// <returns>Returns the exception message</returns>
		public static string StaticGetExceptionContent(Exception exception)
		{
			// Check from entry
			if (exception == null)
				return null;

			// Start build
			var message = string.Format(AbstractLogger.FORMAT_LOG_EXCEPTION, exception.Message);

			// Check from internal
			if (exception.InnerException != null)
			{
				// Append inner exception message
				message += string.Format(AbstractLogger.FORMAT_LOG_INNER_EXCEPTION, exception.InnerException.Message);

				// Append inner exception message
				message += string.Format(AbstractLogger.FORMAT_LOG_STACK_TRACE, " (INNER)", exception.InnerException.StackTrace);
			}

			// Append inner exception message
			message += string.Format(AbstractLogger.FORMAT_LOG_STACK_TRACE, string.Empty, exception.StackTrace);

			// Return the built message
			return message;
		}

		/// <summary>
		/// The object to string converter
		/// </summary>
		/// <param name="target">The target object</param>
		/// <param name="options">The options</param>
		/// <returns>Returns the object content</returns>
		public static string StaticGetObjectContent(object target, JsonSerializerOptions options = null)
		{
			// Check from entry
			if (target == null)
				return null;

			// Return the built message
			try
			{
				// Try to serialize
				return JsonSerializer.Serialize(target, options);
			}
			catch
			{
				// Default
				return AbstractLogger.LOG_OBJECT_ERROR;
			}
		}

		#endregion

		#region Static endpoints

		/// <summary>
		/// Check if should log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="currentLogLevel">The current log level</param>
		/// <returns>Returns true if should be logged</returns>
		public virtual bool ShouldLog(LogLevel logLevel)
		{
			// Internal
			return !this.Disabled && AbstractLogger.StaticShouldLog(logLevel, this.CurrentLevel);
		}

		/// <summary>
		/// Build the log message
		/// </summary>
		/// <param name="message">The main message</param>
		/// <param name="title">The optional title</param>
		/// <param name="noFormat">The optional flag for no format</param>
		/// <returns>Returns the built log message</returns>
		public virtual string BuildLogMessage(string message, string title = null, bool noFormat = false)
		{
			// Internal
			return AbstractLogger.StaticBuildLogMessage(message, title, noFormat);
		}

		/// <summary>
		/// Format the log
		/// </summary>
		/// <param name="message">The message to format</param>
		/// <returns>Returns the formated log message</returns>
		public virtual string FormatLogMessage(string message)
		{
			// Internal
			return AbstractLogger.StaticFormatLogMessage(message);
		}

		/// <summary>
		/// The exception to string converter
		/// </summary>
		/// <param name="exception">The exception</param>
		/// <returns>Returns the exception message</returns>
		public virtual string GetExceptionContent(Exception exception)
		{
			// Internal
			return AbstractLogger.StaticGetExceptionContent(exception);
		}

		/// <summary>
		/// The object to string converter
		/// </summary>
		/// <param name="target">The target object</param>
		/// <param name="options">The optional JSON serialization options</param>
		/// <returns>Returns the object content</returns>
		public virtual string GetObjectContent(object target, JsonSerializerOptions options = null)
		{
			// Internal
			return AbstractLogger.StaticGetObjectContent(target, options ?? this.JsonSerializerOptions);
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Initialize from arguments
		/// </summary>
		/// <param name="currentModel">The current model</param>
		public virtual void InitializeLogLevelFromArguments(string[] args)
		{
			// Register the last model checked
			AbstractLogger.StaticInitializeLogLevelFromArguments(this, args);
		}

		/// <summary>
		/// Clear all entries
		/// </summary>
		public virtual void Clear()
		{
			// Raise event
			this.OnBeforeClear?.Invoke(this, new BeforeClearEventArgs(this));

			// Clear all logs
			this.Logs.Clear();

			// Raise event
			this.OnAfterClear?.Invoke(this, new AfterClearEventArgs(this));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="message">The message to log</param>
		/// <param name="logDate">The optional log date</param>
		/// <returns>Returns true if logged</returns>
		public virtual bool Log(LogModel model)
		{
			// Check
			if (model == null)
				throw new InvalidOperationException("Can not log with a null log model");

			// Check if should log
			if (!this.ShouldLog(model.Level))
				return false;

			// Manage event
			this.OnBeforeLog?.Invoke(this, new BeforeLogEventArgs(this, model));

			// Manage max values here and remove first entry if max reach
			var logs = this.Logs;
			if (logs != null && logs.Count > 0 && logs.Count == this.MaxEntries)
			{
				// Extract
				LogModel removed = logs.First();

				// Manage events
				this.OnBeforeIndexLog?.Invoke(this, new BeforeIndexLogEventArgs(this, removed));

				// Remove
				logs.Remove(removed);

				// Manage events
				this.OnAfterIndexLog?.Invoke(this, new AfterIndexLogEventArgs(this, removed));
			}

			// Add into list
			logs.Add(model);

			// Manage event
			this.OnAfterLog?.Invoke(this, new AfterLogEventArgs(this, model));

			// Returns true
			return true;
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="message">The message to log</param>
		/// <param name="logDate">The optional log date</param>
		/// <returns>Returns true if logged</returns>
		public virtual bool Log(LogLevel logLevel, string message, DateTime? logDate = null)
		{
			// Internal
			return this.Log(new LogModel
			{
				DateTime = logDate ?? DateTime.Now,
				Level = logLevel,
				LogEntries = new LogEntryModel[] { new LogEntryModel(message) }
			});
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public virtual void Debug(string message)
		{
			// Internal
			this.Log(LogLevel.DEBUG, this.FormatLogMessage(message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Debug(Exception exception, string message = null)
		{
			// Internal
			this.Debug(this.BuildLogMessage(this.GetExceptionContent(exception), message));
		}

		/// <summary>
		/// Debug log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Debug(object target, string message = null)
		{
			// Internal
			this.Debug(this.BuildLogMessage(this.GetObjectContent(target), message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public virtual void Info(string message)
		{
			// Internal
			this.Log(LogLevel.INFO, this.FormatLogMessage(message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Info(Exception exception, string message = null)
		{
			// Internal
			this.Info(this.BuildLogMessage(this.GetExceptionContent(exception), message));
		}

		/// <summary>
		/// Info log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Info(object target, string message = null)
		{
			// Internal
			this.Info(this.BuildLogMessage(this.GetObjectContent(target), message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public virtual void Warning(string message)
		{
			// Internal
			this.Log(LogLevel.WARN, this.FormatLogMessage(message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Warning(Exception exception, string message = null)
		{
			// Internal
			this.Warning(this.BuildLogMessage(this.GetExceptionContent(exception), message));
		}

		/// <summary>
		/// Warning log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Warning(object target, string message = null)
		{
			// Internal
			this.Warning(this.BuildLogMessage(this.GetObjectContent(target), message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public virtual void Error(string message)
		{
			// Internal
			this.Log(LogLevel.ERROR, this.FormatLogMessage(message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Error(Exception exception, string message = null)
		{
			// Internal
			this.Error(this.BuildLogMessage(this.GetExceptionContent(exception), message));
		}

		/// <summary>
		/// Error log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Error(object target, string message = null)
		{
			// Internal
			this.Error(this.BuildLogMessage(this.GetObjectContent(target), message));
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="message">The message to log</param>
		public virtual void Fatal(string message)
		{
			// Internal
			this.Log(LogLevel.FATAL, this.FormatLogMessage(message));
		}

		/// <summary>
		/// Fatal log
		/// </summary>
		/// <param name="exception">The exception to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Fatal(Exception exception, string message = null)
		{
			// Internal
			this.Fatal(this.BuildLogMessage(this.GetExceptionContent(exception), message));
		}

		/// <summary>
		/// Fatal log
		/// </summary>
		/// <param name="target">The object to log</param>
		/// <param name="message">The optional message to log</param>
		public virtual void Fatal(object target, string message = null)
		{
			// Internal
			this.Fatal(this.BuildLogMessage(this.GetObjectContent(target), message));
		}

		#endregion

		#region Protected

		/// <summary>
		/// Set the current level
		/// </summary>
		/// <param name="newLevel">The new level to set</param>
		protected virtual void SetCurrentLevel(LogLevel newLevel)
		{
			// Raise before event
			var oldLevel = this.CurrentLevel;
			this.OnBeforeChangeLevel?.Invoke(this, new BeforeChangeLevelLogEventArgs(this, oldLevel, newLevel));

			// Set property with value
			this.currentLevel = newLevel;

			// Raise after event
			this.OnAfterChangeLevel?.Invoke(this, new AfterChangeLevelLogEventArgs(this, oldLevel, newLevel));
		}

		#endregion
	}
}