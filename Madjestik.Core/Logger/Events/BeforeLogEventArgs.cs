namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The before log event args
	/// </summary>
	public class BeforeLogEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before log event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="model">The model</param>
		public BeforeLogEventArgs(ILogger logger, LogModel model) : base(logger)
		{
			// Set property
			this.Log = model;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The log model
		/// </summary>
		public LogModel Log
		{
			get;
		}

		#endregion
	}
}