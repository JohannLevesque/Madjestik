using Madjestik.Core.Timers;
using System;
using System.Threading;

namespace Madjestik.Core.Helpers
{
	/// <summary>
	/// Timer utility class
	/// </summary>
	public static class TimerHelper
	{
		#region Public

		/// <summary>
		/// While true with thread sleep of 1ms
		/// </summary>
		/// <param name="timer">The watch audio timer</param>
		/// <param name="tick">The optional timespan tick</param>
		/// <param name="context">The optional context to keep alive objects for example</param>
		public static void InfiniteSleep(ThreadTimer timer, TimeSpan? tick = null, object context = null)
		{
			// Check from timer
			if (timer == null)
				throw new InvalidOperationException("Can not infinite sleep with a null timer");

			// Infinite loop until the child timer is disposed
			while (timer.Disposed == false)
			{
				// Small process sleep
				Thread.Sleep(tick ?? TimeSpan.FromSeconds(60));
			};
		}

		#endregion
	}
}