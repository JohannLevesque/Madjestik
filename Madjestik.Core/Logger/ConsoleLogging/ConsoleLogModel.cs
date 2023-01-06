using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The console log model
	/// </summary>
	public class ConsoleLogModel : LogModel
	{
		#region Properties

		/// <summary>
		/// The show date time flag
		/// </summary>
		public bool ShowDateTime { get; set; } = true;

		/// <summary>
		/// The console color
		/// </summary>
		public ConsoleColor? Color { get; set; }

		/// <summary>
		/// The console background color
		/// </summary>
		public ConsoleColor? BackgroundColor { get; set; }

		/// <summary>
		/// The console date time color
		/// </summary>
		public ConsoleColor? DateTimeColor { get; set; }

		/// <summary>
		/// The console date time background color
		/// </summary>
		public ConsoleColor? DateTimeBackgroundColor { get; set; }

		/// <summary>
		/// The console message color
		/// </summary>
		public ConsoleColor? MessageColor { get; set; }

		/// <summary>
		/// The console message background color
		/// </summary>
		public ConsoleColor? MessageBackgroundColor { get; set; }

		#endregion
	}
}