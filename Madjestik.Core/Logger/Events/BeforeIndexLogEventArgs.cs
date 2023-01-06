namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The before index log event indexed args
	/// </summary>
	public class BeforeIndexLogEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before index log event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="model">The model</param>
		public BeforeIndexLogEventArgs(ILogger logger, LogModel model) : base(logger)
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