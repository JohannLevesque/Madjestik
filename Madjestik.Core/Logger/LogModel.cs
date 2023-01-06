using System;
using System.Linq;

namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The log model class
	/// </summary>
	public class LogModel
	{
		#region Constants

		/// <summary>
		/// The space character
		/// </summary>
		public const string SPACE_CHARACTER = " ";

		#endregion

		#region Properties

		/// <summary>
		/// The log date time
		/// </summary>
		public DateTime DateTime { get; set; } = DateTime.Now;

		/// <summary>
		/// The log level
		/// </summary>
		public LogLevel Level { get; set; }

		/// <summary>
		/// The console logs
		/// </summary>
		public LogEntryModel[] LogEntries { get; set; } = new LogEntryModel[0];

		#endregion

		#region Overrides

		/// <summary>
		/// The to string implementation
		/// </summary>
		/// <returns>Returns the chained log content</returns>
		public override string ToString()
		{
			// Check null
			if (this.LogEntries == null)
				return string.Empty;

			// Return the built string
			return string.Join(LogModel.SPACE_CHARACTER, this.LogEntries.Select(l => l.Message));
		}

		#endregion
	}
}