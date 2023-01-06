using System;

namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The thread interface for tests purpose
	/// </summary>
	public interface ITimerManager
	{
		#region Methods

		/// <summary>
		/// Create a new thread timer
		/// </summary>
		/// <param name="refreshRate">The refresh rate</param>
		/// <param name="callback">The callback handler for the timer</param>
		/// <param name="callbackException">The callback exception handler</param>
		/// <param name="exceptionMode">The exception mode</param>
		/// <param name="isAsync">The is async flag</param>
		public ThreadTimer CreateThreadTimer(TimeSpan refreshRate, Action<ThreadTimer, bool, object> callback = null, Action<Exception, ThreadTimer> callbackException = null, ThreadTimerExceptionManagement exceptionMode = ThreadTimerExceptionManagement.THROW_EXCEPTION, bool isAsync = ThreadTimer.DEFAULT_ASYNC);

		#endregion
	}
}