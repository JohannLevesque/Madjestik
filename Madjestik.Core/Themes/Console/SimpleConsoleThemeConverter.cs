using System;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	[Theme("Simple")]
	public partial class SimpleConsoleThemeConverter : AbstractThemeConverter<ConsoleColor>
	{
		#region Constants

		/// <summary>
		/// The colors count
		/// </summary>
		public static int ConsoleColorsCount = Enum.GetValues(typeof(ConsoleColor)).Length;

		#endregion

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
			return value;
		}

		#endregion

		#region Protected

		/// <summary>
		/// Returns true if the context key is for color
		/// </summary>
		/// <param name="contextObjects">The context objects</param>
		/// <returns>Returns true if should invert colors</returns>
		protected virtual TransformConsoleColorModel GetTransformContext(object[] contextObjects)
		{
			// Check from context keys
			if (contextObjects != null && contextObjects.Length > 0 && contextObjects[0] is TransformConsoleColorModel transformContext)
				return transformContext;

			// Default
			return null;
		}

		#endregion
	}
}