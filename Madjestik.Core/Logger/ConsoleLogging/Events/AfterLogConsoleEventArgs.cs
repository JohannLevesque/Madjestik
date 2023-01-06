namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The after console log event args
	/// </summary>
	public class AfterLogConsoleEventArgs : AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after console log event arg
		/// </summary>
		/// <param name="logger">The logger/param>
		/// <param name="model">The model</param>
		public AfterLogConsoleEventArgs(IConsoleLogger logger, ConsoleLogModel model) : base(logger)
		{
			// Set property
			this.Model = model;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The console log model
		/// </summary>
		public ConsoleLogModel Model { get; }

		#endregion
	}
}