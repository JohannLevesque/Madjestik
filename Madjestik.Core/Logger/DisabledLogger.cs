namespace Madjestik.Core.Logger
{
	/// <summary>
	/// Disabled logger
	/// </summary>
	public class DisabledLogger : AbstractLogger
	{
		#region Override

		/// <summary>
		/// Override to never log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <returns>Returns always false</returns>
		public override bool ShouldLog(LogLevel logLevel)
		{
			return false;
		}

		#endregion
	}
}