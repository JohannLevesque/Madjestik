namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The before clear event args
	/// </summary>
	public abstract class AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before clear event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		public AbstractClearEventArgs(ILogger logger)
		{
			// Set property
			this.Logger = logger;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The log model
		/// </summary>
		public ILogger Logger
		{
			get;
		}

		#endregion
	}
}