using System;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The transform console color model
	/// </summary>
	public class TransformConsoleColorModel
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="transformColor">The transform color</param>
		/// <param name="source">The source</param>
		/// <param name="defaultColor">The default color</param>
		/// <param name="defaultBackground">The default background color</param>
		/// <param name="currentColor">The current color</param>
		/// <param name="currentBackground">The current background</param>
		/// <param name="isBackground">The is background flag</param>
		public TransformConsoleColorModel(ConsoleColor transformColor, object source, 
			ConsoleColor defaultColor, ConsoleColor defaultBackground, ConsoleColor currentColor, ConsoleColor currentBackground, 
			bool isBackground)
		{
			// Set properties
			this.TransformColor = transformColor;
			this.Source = source;
			this.DefaultForegroundColor = defaultColor;
			this.DefaultBackgroundColor = defaultBackground;
			this.CurrentForegroundColor = currentColor;
			this.CurrentBackgroundColor = currentBackground;
			this.IsBackground = isBackground;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The transform color
		/// </summary>
		public ConsoleColor TransformColor { get; set; }

		/// <summary>
		/// The source
		/// </summary>
		public object Source { get; set; }

		/// <summary>
		/// The transform color
		/// </summary>
		public ConsoleColor DefaultForegroundColor { get; set; }

		/// <summary>
		/// The default background color
		/// </summary>
		public ConsoleColor DefaultBackgroundColor { get; set; }

		/// <summary>
		/// The current color
		/// </summary>
		public ConsoleColor CurrentForegroundColor { get; set; }

		/// <summary>
		/// The current background color
		/// </summary>
		public ConsoleColor CurrentBackgroundColor { get; set; }

		/// <summary>
		/// The is background flag
		/// </summary>
		public bool IsBackground { get; set; }

		#endregion
	}
}