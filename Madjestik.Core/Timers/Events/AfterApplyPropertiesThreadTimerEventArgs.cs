namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The AfterApplyProperties thread timer event args
	/// </summary>
	public class AfterApplyPropertiesThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public AfterApplyPropertiesThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
			// Set property
		}

		#endregion
	}
}