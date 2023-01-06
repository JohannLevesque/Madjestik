using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Madjestik.Core.Exports
{
	/// <summary>
	/// Interface for CSV builder manager
	/// </summary>
	public interface ICsvBuilderManager
	{
		#region Properties

		/// <summary>
		/// The current culture
		/// </summary>
		public CultureInfo Culture { get; set; }

		/// <summary>
		/// The current culture
		/// </summary>
		public IEnumerable<string> HeaderKeys { get; }

		/// <summary>
		/// The string builder content
		/// </summary>
		public StringBuilder Content { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Returns the CSV delimiter according to the related culture
		/// </summary>
		/// <returns>Returns the CSV delimiter from culture</returns>
		public string GetCsvDelimiter();

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="newLine">The new line flag</param>
		/// <returns>Returns the row content</returns>
		public string WriteHeader(bool newLine = true);

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="values">The values to write</param>
		/// <param name="newLine">The new line flag</param>
		/// <returns>Returns the row content</returns>
		public string WriteRow(IEnumerable<object> values, bool newLine = true);

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="newLine">The new line flag</param>
		/// <param name="values">The values to write</param>
		/// <returns>Returns the row content</returns>
		public string WriteRowValues(bool newLine, params object[] values);

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="values">The values to write</param>
		/// <returns>Returns the row content</returns>
		public string WriteRowValues(params object[] values);

		#endregion
	}
}