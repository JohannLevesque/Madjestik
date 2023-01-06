using System;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	public abstract partial class DynamicThemeConverter<T> : AbstractThemeConverter<T>
	{
		#region Constructor

		/// <summary>
		/// Simple consutrctor
		/// </summary>
		/// <param name="handler">The dynamic handler</param>
		public DynamicThemeConverter(Func<T, object, T> handler)
		{
			// Set property
			this.Handler = handler;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Return the expected handler
		/// </summary>
		public Func<T, object, T> Handler { get; set; }

		#endregion

		#region Implementation

		/// <summary>
		/// Transform the given input into another
		/// </summary>
		/// <param name="value">The input value</param>
		/// <param name="contextObjects">The context objects</param>
		/// <returns>Returns the translated value</returns>
		public override T Transform(T value, params object[] contextObjects)
		{
			// Check null case
			if (this.Handler == null)
				throw new InvalidOperationException("Can not transform ConsoleColor from a null dynamic handler");

			// Return the self value
			return this.Handler.Invoke(value, contextObjects);
		}

		#endregion
	}
}