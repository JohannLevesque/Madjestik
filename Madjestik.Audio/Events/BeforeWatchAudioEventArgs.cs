namespace Madjestik.Audio
{
	/// <summary>
	/// The before watch audio event
	/// </summary>
	public class BeforeWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public BeforeWatchAudioEventArgs(ISmartAudioManager manager) : base(manager)
		{
			// Nothing to do
		}

		#endregion
	}
}