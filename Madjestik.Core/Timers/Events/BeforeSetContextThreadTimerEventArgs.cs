namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeSetContext thread timer event args
	/// </summary>
	public class BeforeSetContextThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		/// <param name="oldContext">The old context</param>
		/// <param name="newContext">The new context</param>
		public BeforeSetContextThreadTimerEventArgs(ThreadTimer timer, object oldContext, object newContext) : base(timer)
		{
			// Set properties
			this.OldContext = oldContext;
			this.NewContext = newContext;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The old context
		/// </summary>
		public object OldContext { get; set; }

		/// <summary>
		/// The new context
		/// </summary>
		public object NewContext { get; set; }

		#endregion
	}
}