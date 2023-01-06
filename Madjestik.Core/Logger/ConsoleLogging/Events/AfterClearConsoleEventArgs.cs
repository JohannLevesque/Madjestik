namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The after console clear event args
	/// </summary>
	public class AfterClearConsoleEventArgs : AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after console log event arg
		/// </summary>
		/// <param name="logger">The logger/param>
		public AfterClearConsoleEventArgs(IConsoleLogger logger) : base(logger)
		{
			// Nothing to do
		}

		#endregion
	}
}