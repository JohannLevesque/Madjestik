namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeStart thread timer event args
	/// </summary>
	public class BeforeStartThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeStartThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}