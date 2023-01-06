namespace Madjestik.Audio
{
	/// <summary>
	/// The on after index history event
	/// </summary>
	public class AfterIndexHistoryWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="removed">The removed model</param>
		public AfterIndexHistoryWatchAudioEventArgs(ISmartAudioManager manager, SmartAudioManagerWatchModel removed) : base(manager)
		{
			// Set property
			this.Removed = removed;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The model to set
		/// </summary>
		public SmartAudioManagerWatchModel Removed { get; }

		#endregion
	}
}