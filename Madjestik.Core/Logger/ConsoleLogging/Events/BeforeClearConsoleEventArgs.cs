namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The before console clear event args
	/// </summary>
	public class BeforeClearConsoleEventArgs : AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The after console log event arg
		/// </summary>
		/// <param name="logger">The logger/param>
		public BeforeClearConsoleEventArgs(IConsoleLogger logger) : base(logger)
		{
			// Nothing to do
		}

		#endregion
	}
}