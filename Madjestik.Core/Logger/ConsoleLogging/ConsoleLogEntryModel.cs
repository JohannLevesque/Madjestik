using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The console log model class
	/// </summary>
	public class ConsoleLogEntryModel : LogEntryModel
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="logMessage">The log message</param>
		/// <param name="logColor">The log color</param>
		/// <param name="logBackgroundColor">The log background color</param>
		public ConsoleLogEntryModel(string logMessage, ConsoleColor? logColor = null, ConsoleColor? logBackgroundColor = null) : base(logMessage)
		{
			// Set attributes
			this.color = logColor;
			this.backgroundColor = logBackgroundColor;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The console color to display
		/// </summary>
		protected readonly ConsoleColor? color;
		public ConsoleColor? Color
		{
			get
			{
				// Internal
				return this.color;
			}
		}

		/// <summary>
		/// The console background color to display
		/// </summary>
		protected readonly ConsoleColor? backgroundColor;
		public ConsoleColor? BackgroundColor
		{
			get
			{
				// Internal
				return this.backgroundColor;
			}
		}

		#endregion
	}
}