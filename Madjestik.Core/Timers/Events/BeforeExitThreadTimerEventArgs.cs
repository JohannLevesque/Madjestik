namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeExit thread timer event args
	/// </summary>
	public class BeforeExitThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeExitThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}