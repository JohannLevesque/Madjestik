namespace Madjestik.Audio
{
	/// <summary>
	/// The on capture event
	/// </summary>
	public class CaptureWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="model">The captured model</param>
		public CaptureWatchAudioEventArgs(ISmartAudioManager manager, SmartAudioManagerWatchModel model) : base(manager)
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