using System;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	[Theme("ForegroundInverted")]
	public partial class InvertForegroundColorConsoleThemeConverter : InvertColorsConsoleThemeConverter
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
			// Extract context from objects
			TransformConsoleColorModel transformContext = this.GetTransformContext(contextObjects);

			// Check for transform
			if (transformContext != null && this.ShouldInvertColors(transformContext))
				return base.Transform(value, contextObjects);

			// Return the default value
			return value;
		}

		#endregion

		#region Protected

		/// <summary>
		/// Returns true if the context key is for color
		/// </summary>
		/// <param name="transformContext">The transform context</param>
		/// <returns>Returns true if should invert colors</returns>
		public virtual bool ShouldInvertColors(TransformConsoleColorModel transformContext)
		{
			// Check from context keys
			return transformContext != null && !transformContext.IsBackground;
		}

		#endregion
	}
}