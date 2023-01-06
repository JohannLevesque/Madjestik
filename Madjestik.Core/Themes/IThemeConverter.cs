using System;

namespace Madjestik.Core.Themes
{
	/// <summary>
	/// The theme converter
	/// </summary>
	/// <Type of the converter
	public interface IThemeConverter<T>
	{
		#region Properties

		/// <summary>
		/// The theme value type
		/// </summary>
		public Type ThemeValueType { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Translate the given theme
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <param name="contextObjects">The optional context objects</param>
		/// <returns>Returns the converted value</returns>
		public T Transform(T value, params object[] contextObjects);

		#endregion
	}
}