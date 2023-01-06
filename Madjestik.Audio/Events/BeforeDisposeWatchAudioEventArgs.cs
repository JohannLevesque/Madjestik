namespace Madjestik.Audio
{
	/// <summary>
	/// The before dispose event
	/// </summary>
	public class BeforeDisposeWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public BeforeDisposeWatchAudioEventArgs(ISmartAudioManager manager) : base(manager)
		{
			// Nothing to do
		}

		#endregion
	}
}