namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The before change level event args
	/// </summary>
	public class BeforeChangeLevelLogEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after index log event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="model">The model</param>
		public BeforeChangeLevelLogEventArgs(ILogger logger, LogLevel oldLevel, LogLevel newLevel) : base(logger)
		{
			// Set properties
			this.OldLevel = oldLevel;
			this.NewLevel = newLevel;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The old level
		/// </summary>
		public LogLevel OldLevel
		{
			get;
		}

		/// <summary>
		/// The new level
		/// </summary>
		public LogLevel NewLevel
		{
			get;
		}

		#endregion
	}
}