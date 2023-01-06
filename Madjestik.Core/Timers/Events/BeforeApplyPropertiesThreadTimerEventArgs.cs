namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The BeforeApplyProperties thread timer event args
	/// </summary>
	public class BeforeApplyPropertiesThreadTimerEventArgs : AbstractThreadTimerEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="timer">The thread timer</param>
		public BeforeApplyPropertiesThreadTimerEventArgs(ThreadTimer timer) : base(timer)
		{
		}

		#endregion
	}
}