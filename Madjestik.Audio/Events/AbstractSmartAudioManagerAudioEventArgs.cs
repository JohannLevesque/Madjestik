namespace Madjestik.Audio
{
	/// <summary>
	/// The base event
	/// </summary>
	public abstract class AbstractSmartAudioManagerAudioEventArgs
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="manager">The manager</param>
		public AbstractSmartAudioManagerAudioEventArgs(ISmartAudioManager manager)
		{
			// Set property
			this.Manager = manager;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The model to set
		/// </summary>
		public ISmartAudioManager Manager { get; }

		#endregion
	}
}