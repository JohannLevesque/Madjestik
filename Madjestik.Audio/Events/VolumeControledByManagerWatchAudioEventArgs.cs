namespace Madjestik.Audio
{
	/// <summary>
	/// The volume controlled by manager event
	/// </summary>
	public class VolumeControledByManagerWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public VolumeControledByManagerWatchAudioEventArgs(ISmartAudioManager manager) : base(manager)
		{
			// Nothing to do
		}

		#endregion
	}
}