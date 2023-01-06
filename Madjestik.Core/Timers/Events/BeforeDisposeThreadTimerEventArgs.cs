namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeDipose thread timer event args
	/// </summary>
	public class BeforeDisposeThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeDisposeThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}