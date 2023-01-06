using Madjestik.Core.Timers;
using System;

namespace Madjestik.Audio
{
	/// <summary>
	/// The smart audio manager
	/// </summary>
	public interface ISmartAudioManager : ISmartAudioManagerEventHandlers, IDisposable
	{
		#region Properties

		/// <summary>
		/// The disposed flag
		/// </summary>
		public bool Disposed { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Initialize the OS sound level
		/// </summary>
		public void InitializeSoundLevel();

		/// <summary>
		/// Watch the current sound output to define master level
		/// </summary>
		/// <param name="exceptionManagementMode">The optional timer exception management mode</param>
		/// <param name="isAsync">The is async flag</param>
		public ThreadTimer WatchAudio(ThreadTimerExceptionManagement exceptionManagementMode = ThreadTimerExceptionManagement.THROW_EXCEPTION, bool isAsync = false);

		#endregion
	}
}