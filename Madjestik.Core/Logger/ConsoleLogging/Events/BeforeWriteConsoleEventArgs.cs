using Madjestik.Core.Logger.ConsoleLogging;
using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The before console log event args
	/// </summary>
	public class BeforeWriteConsoleEventArgs : AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before console log write event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="message">The message</param>
		/// <param name="logColor">The log color</param>
		/// <param name="logBackgroundColor">The log background color</param>
		/// <param name="newLine">The new line flag</param>
		public BeforeWriteConsoleEventArgs(IConsoleLogger logger, string message, ConsoleColor logColor, ConsoleColor logBackgroundColor, bool newLine) : base(logger)
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