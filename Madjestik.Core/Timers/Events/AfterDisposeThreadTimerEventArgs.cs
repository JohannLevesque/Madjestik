namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterDispose thread timer event args
	/// </summary>
	public class AfterDisposeThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterDisposeThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
			// Set property
		}

		#endregion
	}
}