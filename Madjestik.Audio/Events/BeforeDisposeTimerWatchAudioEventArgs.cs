using Madjestik.Core.Timers;

namespace Madjestik.Audio
{
	/// <summary>
	/// The before dispose timer event
	/// </summary>
	public class BeforeDisposeTimerWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="timer">The timer</param>
		public BeforeDisposeTimerWatchAudioEventArgs(ISmartAudioManager manager, ThreadTimer timer) : base(manager)
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