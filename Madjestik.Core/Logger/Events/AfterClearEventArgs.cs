namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The before clear event args
	/// </summary>
	public class AfterClearEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before clear event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		public AfterClearEventArgs(ILogger logger) : base(logger)
		{
			// Nothing to do
		}

		#endregion
	}
}