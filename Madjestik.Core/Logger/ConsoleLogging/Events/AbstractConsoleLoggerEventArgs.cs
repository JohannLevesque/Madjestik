namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The abstract console log event args
	/// </summary>
	public abstract class AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before console log event arg
		/// </summary>
		/// <param name="model">The logger</param>
		public AbstractConsoleLoggerEventArgs(IConsoleLogger logger)
		{
			// Set property
			this.Logger = logger;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The log model
		/// </summary>
		public IConsoleLogger Logger
		{
			get;
		}

		#endregion
	}
}