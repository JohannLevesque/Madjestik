namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterStart thread timer event args
	/// </summary>
	public class AfterStartThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterStartThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}