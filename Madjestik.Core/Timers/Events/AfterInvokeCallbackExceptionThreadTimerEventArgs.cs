namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterInvokeExceptionCallback thread timer event args
	/// </summary>
	public class AfterInvokeCallbackExceptionThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterInvokeCallbackExceptionThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
			// Set property
		}

		#endregion
	}
}