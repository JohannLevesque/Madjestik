using Madjestik.Core.Themes;
using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The logger console interface
	/// </summary>
	public interface IConsoleLogger : ILogger, IThemable<ConsoleColor>
	{
		#region Events

		/// <summary>
		/// The before log console event
		/// </summary>
		public event EventHandler<BeforeLogConsoleEventArgs> OnBeforeLogConsole;

		/// <summary>
		/// The after log console event
		/// </summary>
		public event EventHandler<AfterLogConsoleEventArgs> OnAfterLogConsole;

		/// <summary>
		/// The before write console event
		/// </summary>
		public event EventHandler<BeforeWriteConsoleEventArgs> OnBeforeWriteConsole;

		/// <summary>
		/// The after write console event
		/// </summary>
		public event EventHandler<AfterWriteConsoleEventArgs> OnAfterWriteConsole;

		/// <summary>
		/// The before console clear event
		/// </summary>
		public event EventHandler<BeforeClearConsoleEventArgs> OnBeforeClearConsole;

		/// <summary>
		/// The after console clear event
		/// </summary>
		public event EventHandler<AfterClearConsoleEventArgs> OnAfterClearConsole;

		#endregion

		#region Properties

		/// <summary>
		/// The initial log color
		/// </summary>
		public ConsoleColor InitialColor { get; }

		/// <summary>
		/// The initial background log color
		/// </summary>
		public ConsoleColor InitialBackgroundColor { get; }

		/// <summary>
		/// The debug log color
		/// </summary>
		public ConsoleColor DebugColor { get; set; }

		/// <summary>
		/// The info log color
		/// </summary>
		public ConsoleColor InfoColor { get; set; }

		/// <summary>
		/// The warn log color
		/// </summary>
		public ConsoleColor WarnColor { get; set; }

		/// <summary>
		/// The error log color
		/// </summary>
		public ConsoleColor ErrorColor { get; set; }

		/// <summary>
		/// The fatal log color
		/// </summary>
		public ConsoleColor FatalColor { get; set; }

		/// <summary>
		/// The default space
		/// </summary>
		public string DefaultSpace { get; set; }

		/// <summary>
		/// The datetime format
		/// </summary>
		public string DateTimeFormat { get; set; }

		/// <summary>
		/// The show date time flag for new log
		/// </summary>
		public bool ShowDateTime { get; set; }

		/// <summary>
		/// The color for new log
		/// </summary>
		public ConsoleColor ForegroundColor { get; }

		/// <summary>
		/// The background color for new log
		/// </summary>
		public ConsoleColor BackgroundColor { get; }

		/// <summary>
		/// The date time color for new log
		/// </summary>
		public ConsoleColor? DateTimeColor { get; set; }

		/// <summary>
		/// The date time background color for new log
		/// </summary>
		public ConsoleColor? DateTimeBackgroundColor { get; set; }

		/// <summary>
		/// The date time color for new log
		/// </summary>
		public ConsoleColor? MessageColor { get; set; }

		/// <summary>
		/// The message background color for new log
		/// </summary>
		public ConsoleColor? MessageBackgroundColor { get; set; }

		#endregion

		#region Public

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="entries">The entries to log</param>
		/// <returns>Returns true if logged</returns>
		public bool Log(LogLevel logLevel, params ConsoleLogEntryModel[] entries);

		#endregion
	}
}