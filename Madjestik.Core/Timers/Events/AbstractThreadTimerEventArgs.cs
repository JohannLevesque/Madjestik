namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The base thread timer event args
	/// </summary>
	public abstract class AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AbstractThreadTimerEventArgs(ThreadTimer timer)
		{
			// Set property
			this.Timer = timer;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The model to set
		/// </summary>
		public ThreadTimer Timer { get; }

		#endregion
	}
}