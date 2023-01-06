namespace Madjestik.Core.Logger
{
	/// <summary>
	/// The log entry model class
	/// </summary>
	public class LogEntryModel
	{
		#region Attributes

		/// <summary>
		/// The console message
		/// </summary>
		protected readonly string message;

		#endregion

		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="logColor">The log color</param>
		/// <param name="logMessage">The log message</param>
		public LogEntryModel(string logMessage)
		{
			// Set attributes
			message = logMessage;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The console message to display
		/// </summary>
		public string Message
		{
			get
			{
				// Internal
				return message;
			}
		}

		#endregion
	}
}