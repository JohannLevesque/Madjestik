using System;

namespace Madjestik.Core.Themes
{
	/// <summary>
	/// Themable interface
	/// </summary>
	/// <typeparam name="T">The type of the theme</typeparam>
	public interface IThemable<T>
	{
		#region Events

		/// <summary>
		/// The before change theme event arg
		/// </summary>
		public event EventHandler<BeforeChangeThemeEventArgs<T>> OnBeforeChangeTheme;

		/// <summary>
		/// The after change theme event args
		/// </summary>

		public event EventHandler<AfterChangeThemeEventArgs<T>> OnAfterChangeTheme;

		#endregion

		#region Properties

		/// <summary>
		/// The theme converter
		/// </summary>
		public IThemeConverter<T> ThemeConverter { get; set; }

		#endregion
	}
}