namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeSetTickInterval thread timer event args
	/// </summary>
	public class BeforeSetTickIntervalThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeSetTickIntervalThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}