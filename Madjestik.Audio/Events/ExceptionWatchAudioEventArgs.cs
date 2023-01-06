using Madjestik.Core.Timers;
using System;

namespace Madjestik.Audio
{
	/// <summary>
	/// The on exception management event
	/// </summary>
	public class ExceptionWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="exception">The exception</param>
		/// <param name="timer">The timer</param>
		public ExceptionWatchAudioEventArgs(ISmartAudioManager manager, Exception exception, ThreadTimer timer) : base(manager)
		{
			// Set property
			this.Exception = exception;
			this.Timer = timer;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The public exception
		/// </summary>
		public Exception Exception { get; }

		/// <summary>
		/// The thread timer
		/// </summary>
		public ThreadTimer Timer { get; }

		#endregion
	}
}