namespace Madjestik.Audio
{
	/// <summary>
	/// The on before index history event
	/// </summary>
	public class BeforeIndexHistoryWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="removed">The indexed</param>
		public BeforeIndexHistoryWatchAudioEventArgs(ISmartAudioManager manager, SmartAudioManagerWatchModel removed) : base(manager)
		{
			// Set property
			this.Removed = removed;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The indexed model to set
		/// </summary>
		public SmartAudioManagerWatchModel Removed { get; }

		#endregion
	}
}