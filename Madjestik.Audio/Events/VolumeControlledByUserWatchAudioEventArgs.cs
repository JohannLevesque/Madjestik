namespace Madjestik.Audio
{
	/// <summary>
	/// The volume controlled by user event
	/// </summary>
	public class VolumeControlledByUserWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public VolumeControlledByUserWatchAudioEventArgs(ISmartAudioManager manager) : base(manager)
		{
			// Nothing to do
		}

		#endregion
	}
}