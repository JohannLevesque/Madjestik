namespace Madjestik.Audio
{
	/// <summary>
	/// The audio service interface
	/// </summary>
	public interface IAudioService
	{
		#region Public

		/// <summary>
		/// Returns the current main volume
		/// </summary>
		/// <returns>Returns the float volume</returns>
		public float GetVolume();

		/// <summary>
		/// Set the current volume
		/// </summary>
		/// <returns>Returns a model object with all audio info</returns>
		public void SetVolume(float volume);

		/// <summary>
		/// Returns an audio model with all information
		/// </summary>
		/// <returns>Returns a model object with all audio info</returns>
		public AudioServiceModel GetData();

		#endregion
	}
}