namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterCalbbackSet thread timer event args
	/// </summary>
	public class AfterSetCallbackThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterSetCallbackThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
			// Set property
		}

		#endregion
	}
}