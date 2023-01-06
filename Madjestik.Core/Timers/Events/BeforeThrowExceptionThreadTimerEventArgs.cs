namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeThrowException thread timer event args
	/// </summary>
	public class BeforeThrowExceptionThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeThrowExceptionThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}