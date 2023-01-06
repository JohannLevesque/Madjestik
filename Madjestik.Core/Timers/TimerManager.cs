using System;

namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The timer class for tests purpose
	/// </summary>
	public partial class TimerManager : ITimerManager
	{
		#region Constants

		/// <summary>
		/// The default is async
		/// </summary>
		public const bool IS_ASYNC_TIMER = ThreadTimer.DEFAULT_ASYNC;

		#endregion

		#region Implementation

		/// <summary>
		/// Create a new thread timer
		/// </summary>
		/// <param name="refreshRate">The refresh rate</param>
		/// <param name="callback">The callback handler for the timer</param>
		/// <param name="callbackException">The callback exception handler</param>
		/// <param name="exceptionMode">The exception mode</param>
		/// <param name="isAsync">The isAsync flag</param>
		public virtual ThreadTimer CreateThreadTimer(TimeSpan refreshRate, Action<ThreadTimer, bool, object> callback = null, Action<Exception, ThreadTimer> callbackException = null, ThreadTimerExceptionManagement exceptionMode = ThreadTimerExceptionManagement.THROW_EXCEPTION, bool isAsync = TimerManager.IS_ASYNC_TIMER)
		{
			// Check from entry
			return new ThreadTimer(refreshRate, callback, callbackException, exceptionMode, isAsync);
		}

		#endregion
	}
}