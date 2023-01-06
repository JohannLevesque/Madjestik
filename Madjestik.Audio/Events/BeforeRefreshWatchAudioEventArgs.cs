namespace Madjestik.Audio
{
	/// <summary>
	/// The on before refresh event
	/// </summary>
	public class BeforeRefreshWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="model">The model</param>
		public BeforeRefreshWatchAudioEventArgs(ISmartAudioManager manager, SmartAudioManagerWatchModel model) : base(manager)
		{
			// Set property
			this.Model = model;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The captured model
		/// </summary>
		public SmartAudioManagerWatchModel Model { get; }

		#endregion
	}
}