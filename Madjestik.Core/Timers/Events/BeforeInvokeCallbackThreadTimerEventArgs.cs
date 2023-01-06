namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeInvokeCallback thread timer event args
	/// </summary>
	public class BeforeInvokeCallbackThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		/// <param name="isManual">The is manual flag</param>
		public BeforeInvokeCallbackThreadTimerEventArgs(ThreadTimer timer, bool isManual) : base(timer)
		{
			// Set property
			this.IsManual = isManual;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The his manual flag
		/// </summary>
		public bool IsManual { get; set; }

		#endregion
	}
}