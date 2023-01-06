namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterSetTickInterval thread timer event args
	/// </summary>
	public class AfterSetTickIntervalThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterSetTickIntervalThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
			// Set property
		}

		#endregion
	}
}