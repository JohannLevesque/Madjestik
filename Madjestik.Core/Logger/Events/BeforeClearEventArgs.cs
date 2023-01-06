namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The before clear event args
	/// </summary>
	public class BeforeClearEventArgs : AbstractClearEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before clear event arg
		/// </summary>
		/// <param name="logger">The logger</param>
		public BeforeClearEventArgs(ILogger logger) : base(logger)
		{
			// Nothing to do
		}

		#endregion
	}
}