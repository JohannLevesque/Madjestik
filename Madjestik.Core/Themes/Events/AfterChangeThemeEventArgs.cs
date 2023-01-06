namespace Madjestik.Core.Themes
{
	/// <summary>
	/// The after change theme event arg
	/// </summary>
	public class AfterChangeThemeEventArgs<T>
	{
		#region Constructor

		/// <summary>
		/// The change theme event args
		/// </summary>
		/// <param name="sender">The event sender</param>
		/// <param name="oldConverter">The old converter</param>
		/// <param name="newConverter">The new converter</param>
		public AfterChangeThemeEventArgs(object sender, IThemeConverter<T> oldConverter, IThemeConverter<T> newConverter)
		{
			// Set properties
			this.Sender = sender;
			this.OldConverter = oldConverter;
			this.NewConverter = newConverter;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The sender
		/// </summary>
		public object Sender { get; set; }

		/// <summary>
		/// The old converter
		/// </summary>
		public IThemeConverter<T> OldConverter { get; set; }

		/// <summary>
		/// The new converter
		/// </summary>
		public IThemeConverter<T> NewConverter { get; set; }

		#endregion
	}
}