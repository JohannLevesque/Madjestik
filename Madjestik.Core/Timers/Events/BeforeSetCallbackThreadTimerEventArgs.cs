namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeSetCallback thread timer event args
	/// </summary>
	public class BeforeSetCallbackThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeSetCallbackThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}