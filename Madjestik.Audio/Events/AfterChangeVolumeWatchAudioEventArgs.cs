namespace Madjestik.Audio
{
	/// <summary>
	/// The on after change volume event
	/// </summary>
	public class AfterChangeVolumeWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="previousVolume">The previous volume</param>
		/// <param name="newVolume">The new volume</param>
		public AfterChangeVolumeWatchAudioEventArgs(ISmartAudioManager manager, float previousVolume, float newVolume) : base(manager)
		{
			// Set properties
			this.PreviousVolume = previousVolume;
			this.NewVolume = newVolume;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The previous volume
		/// </summary>
		public float PreviousVolume { get; }

		/// <summary>
		/// The new volume
		/// </summary>
		public float NewVolume { get; }

		#endregion
	}
}