namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The after log event args
	/// </summary>
	public class AfterLogEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after log event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="model">The model</param>
		public AfterLogEventArgs(ILogger logger, LogModel model) : base(logger)
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