namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterStop thread timer event args
	/// </summary>
	public class AfterStopThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterStopThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}