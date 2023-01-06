namespace Madjestik.Audio
{
	/// <summary>
	/// The on after refresh event
	/// </summary>
	public class AfterRefreshWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="previousModel">The previous model</param>
		/// <param name="newModel">The new model</param>
		public AfterRefreshWatchAudioEventArgs(ISmartAudioManager manager, SmartAudioManagerWatchModel previousModel, SmartAudioManagerWatchModel newModel) : base(manager)
		{
			// Set property
			this.PreviousModel = previousModel;
			this.NewModel = newModel;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The previous model
		/// </summary>
		public SmartAudioManagerWatchModel PreviousModel { get; }

		/// <summary>
		/// The new model
		/// </summary>
		public SmartAudioManagerWatchModel NewModel { get; }

		#endregion
	}
}