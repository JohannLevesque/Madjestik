namespace Madjestik.Audio
{
	/// <summary>
	/// The after dispose event
	/// </summary>
	public class AfterDisposeWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public AfterDisposeWatchAudioEventArgs(ISmartAudioManager manager) : base(manager)
		{
			// Nothing to do
		}

		#endregion
	}
}