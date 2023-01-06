namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeStop thread timer event args
	/// </summary>
	public class BeforeStopThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeStopThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}