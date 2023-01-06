using System;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	[Theme("BlackAndWhiteInverted")]
	public partial class InvertBlackAndWhiteColorsConsoleThemeConverter : SimpleConsoleThemeConverter
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
			// Check from colors
			return value switch
			{
				// Manage white
				ConsoleColor.White => ConsoleColor.Black,

				// Manage black
				ConsoleColor.Black => ConsoleColor.White,

				// Default
				_ => value,
			};
		}

		#endregion
	}
}