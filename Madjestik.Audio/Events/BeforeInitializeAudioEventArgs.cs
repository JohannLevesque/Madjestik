namespace Madjestik.Audio
{
	/// <summary>
	/// The before initialize audio event
	/// </summary>
	public class BeforeInitializeAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="currentVolume">The current volume</param>
		/// <param name="baseVolume">The base volume</param>
		public BeforeInitializeAudioEventArgs(ISmartAudioManager manager, int currentVolume, int baseVolume) : base(manager)
		{
			// Set properties
			this.CurrentVolume = currentVolume;
			this.BaseVolume = baseVolume;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The volume
		/// </summary>
		public int CurrentVolume { get; }

		/// <summary>
		/// The base volume
		/// </summary>
		public int BaseVolume { get; }

		#endregion
	}
}