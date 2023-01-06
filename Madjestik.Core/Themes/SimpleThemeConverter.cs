namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	public partial class SimpleThemeConverter<T> : AbstractThemeConverter<T>
	{
		#region Implementation

		/// <summary>
		/// Transform the given input into another
		/// </summary>
		/// <param name="value">The input value</param>
		/// <param name="contextObjects">The context objects</param>
		/// <returns>Returns the translated value</returns>
		public override T Transform(T value, params object[] contextObjects)
		{
			// Return the self value
			return value;
		}

		#endregion
	}
}