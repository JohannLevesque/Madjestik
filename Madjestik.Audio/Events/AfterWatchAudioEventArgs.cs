using Madjestik.Core.Timers;

namespace Madjestik.Audio
{
	/// <summary>
	/// The after watch audio event
	/// </summary>
	public class AfterWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public AfterWatchAudioEventArgs(ISmartAudioManager manager, ThreadTimer timer) : base(manager)
		{
			// Set property
			this.Timer = timer;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The thread timer
		/// </summary>
		public ThreadTimer Timer { get; }

		#endregion
	}
}