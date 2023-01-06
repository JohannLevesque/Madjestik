using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Madjestik.Core.Exports
{
	/// <summary>
	/// The CSV builder
	/// </summary>
	public class CsvBuilderManager : ICsvBuilderManager
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="headerKeys">The header keys</param>
		public CsvBuilderManager(params string[] headerKeys)
		{
			// Set properties
			this.HeaderKeys = headerKeys;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The culture of the manager
		/// </summary>
		protected CultureInfo culture = CultureInfo.CurrentCulture;
		public virtual CultureInfo Culture
		{
			get
			{
				// Check if the culture iqs defined
				if (culture == null)
					throw new InvalidOperationException("Can not access to the null culture (CsvBuilderManager)");

				// Return from attribute
				return this.culture;
			}
			set
			{
				// Set the culture
				this.culture = value ?? CultureInfo.CurrentCulture;
			}
		}

		/// <summary>
		/// The header keys
		/// </summary>
		public virtual IEnumerable<string> HeaderKeys { get; protected set; } = new string[0];

		/// <summary>
		/// The content
		/// </summary>
		public virtual StringBuilder Content { get; protected set; } = new StringBuilder();

		#endregion

		#region Public

		/// <summary>
		/// Returns the CSV delimiter according to the related culture
		/// </summary>
		/// <returns>Returns the CSV delimiter from culture</returns>
		public virtual string GetCsvDelimiter()
		{
			// Returns the list separator from culture
			return this.Culture.TextInfo.ListSeparator;
		}

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="newLine">The new line flag</param>
		/// <returns>Returns the row content</returns>
		public virtual string WriteHeader(bool newLine = true)
		{
			// Return the result
			return this.Write(this.HeaderKeys, newLine);
		}

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="values">The values to write</param>
		/// <param name="newLine">The new line flag</param>
		/// <returns>Returns the row content</returns>
		public virtual string WriteRow(IEnumerable<object> values, bool newLine = true)
		{
			// Return the result
			return this.Write(values?.Select(v => this.ConvertValueToString(v)), newLine);
		}

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="newLine">The new line flag</param>
		/// <param name="values">The values to write</param>
		/// <returns>Returns the row content</returns>
		public virtual string WriteRowValues(bool newLine, params object[] values)
		{
			// Write the given row
			return this.WriteRow(values, newLine);
		}

		/// <summary>
		/// Write the given header
		/// </summary>
		/// <param name="values">The values to write</param>
		/// <returns>Returns the row content</returns>
		public virtual string WriteRowValues(params object[] values)
		{
			// Write the given row
			return this.WriteRow(values);
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Returns the content as a string
		/// </summary>
		/// <returns>Returns the string content</returns>
		public override string ToString()
		{
			// Return the content string
			return this.Content?.ToString();
		}

		#endregion

		#region Protected

		/// <summary>
		/// Write the given row
		/// </summary>
		/// <param name="contents">The contents to write</param>
		/// <param name="newLine">The new line flag</param>
		/// <returns>Returns the row content</returns>
		protected virtual string Write(IEnumerable<string> contents, bool newLine)
		{
			// Initialize the string delimiter
			string delimiter = this.GetCsvDelimiter();

			// Build row
			var row = string.Join(delimiter, contents ?? new string[0]);

			// Check cases
			if (newLine)
				this.Content.AppendLine(row);
			else
				this.Content.Append(row);

			// Return the written row
			return row + (newLine ? Environment.NewLine : string.Empty);
		}

		/// <summary>
		/// Returns the string value according to the type
		/// </summary>
		/// <param name="value">The value</param>
		/// <returns>Returns the translated string</returns>
		protected virtual string ConvertValueToString(object value)
		{
			// Check cases
			if (value == null)
				return string.Empty;

			// Default
			return value.ToString();
		}

		#endregion
	}
}