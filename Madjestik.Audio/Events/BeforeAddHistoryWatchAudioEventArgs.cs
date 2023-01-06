namespace Madjestik.Audio
{
	/// <summary>
	/// The on before add history event
	/// </summary>
	public class BeforeAddHistoryWatchAudioEventArgs : AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		/// <param name="model">The model</param>
		public BeforeAddHistoryWatchAudioEventArgs(ISmartAudioManager manager, SmartAudioManagerWatchModel model) : base(manager)
		{
			// Set property
			this.Model = model;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The model to set
		/// </summary>
		public SmartAudioManagerWatchModel Model { get; }

		#endregion
	}
}