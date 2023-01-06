namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The after index log event indexed args
	/// </summary>
	public class AfterIndexLogEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after index log event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="model">The model</param>
		public AfterIndexLogEventArgs(ILogger logger, LogModel model) : base(logger)
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