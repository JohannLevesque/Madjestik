using Madjestik.Core.Timers;

namespace Madjestik.Audio
{
	/// <summary>
	/// The after dispose timer event
	/// </summary>
	public class AfterDisposeTimerWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="timer">The timer</param>
		public AfterDisposeTimerWatchAudioEventArgs(ISmartAudioManager manager, ThreadTimer timer) : base(manager)
		{
			// Set property
			this.Timer = timer;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The timer
		/// </summary>
		public ThreadTimer Timer { get; set; }

		#endregion
	}
}