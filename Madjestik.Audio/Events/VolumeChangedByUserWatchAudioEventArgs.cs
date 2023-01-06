namespace Madjestik.Audio
{
	/// <summary>
	/// The volume changed by user event
	/// </summary>
	public class VolumeChangedByUserWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public VolumeChangedByUserWatchAudioEventArgs(ISmartAudioManager manager) : base(manager)
		{
			// Nothing to do
		}

		#endregion
	}
}