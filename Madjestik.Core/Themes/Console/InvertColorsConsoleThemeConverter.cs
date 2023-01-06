using System;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	[Theme("Inverted")]
	public partial class InvertColorsConsoleThemeConverter : SimpleConsoleThemeConverter
	{
		#region Implementation

		/// <summary>
		/// Transform the given input into another
		/// </summary>
		/// <param name="value">The input value</param>
		/// <param name="contextObjects">The context objects</param>
		/// <returns>Returns the translated value</returns>
		public override ConsoleColor Transform(ConsoleColor value, params object[] contextObjects)
		{
			// Return the self value
			return (ConsoleColor)((SimpleConsoleThemeConverter.ConsoleColorsCount - 1) - (int)value);
		}

		#endregion
	}
}