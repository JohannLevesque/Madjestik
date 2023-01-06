using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The after console log write event args
	/// </summary>
	public class AfterWriteConsoleEventArgs : AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after console log event arg
		/// </summary>
		/// <param name="logger">The console logger</param>
		/// <param name="message">The message</param>
		/// <param name="logColor">The log color</param>
		/// <param name="logBackgroundColor">The log background color</param>
		/// <param name="newLine">The new line flag</param>
		public AfterWriteConsoleEventArgs(IConsoleLogger logger, string message, ConsoleColor logColor, ConsoleColor logBackgroundColor, bool newLine) : base(logger)
		{
			// Set properties
			this.Message = message;
			this.Color = logColor;
			this.BackgroundColor = logBackgroundColor;
			this.NewLine = newLine;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The log message
		/// </summary>
		public string Message
		{
			get;
		}

		/// <summary>
		/// The log color
		/// </summary>
		public ConsoleColor Color
		{
			get;
		}

		/// <summary>
		/// The log background color
		/// </summary>
		public ConsoleColor BackgroundColor
		{
			get;
		}

		/// <summary>
		/// The log new line flag
		/// </summary>
		public bool NewLine
		{
			get;
		}

		#endregion
	}
}