namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeInvokeExceptionCallback thread timer event args
	/// </summary>
	public class BeforeInvokeCallbackExceptionThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeInvokeCallbackExceptionThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}