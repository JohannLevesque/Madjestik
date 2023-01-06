using System;

namespace Madjestik.Audio
{
	/// <summary>
	/// The manager events
	/// </summary>
	public partial class SmartAudioManager
	{
		#region Events

		/// <summary>
		/// The before initialize audio event
		/// </summary>
		public event EventHandler<BeforeInitializeAudioEventArgs> OnBeforeInitializeAudio;

		/// <summary>
		/// The after initialize audio event
		/// </summary>
		public event EventHandler<AfterInitializeAudioEventArgs> OnAfterInitializeAudio;

		/// <summary>
		/// The before initialize change audio event
		/// </summary>
		public event EventHandler<BeforeInitializeChangeAudioEventArgs> OnBeforeInitializeChangeAudio;

		/// <summary>
		/// The after initialize change audio event
		/// </summary>
		public event EventHandler<AfterInitializeChangeAudioEventArgs> OnAfterInitializeChangeAudio;

		/// <summary>
		/// The before watch audio event
		/// </summary>
		public event EventHandler<BeforeWatchAudioEventArgs> OnBeforeWatch;

		/// <summary>
		/// The after watch audio event
		/// </summary>
		public event EventHandler<AfterWatchAudioEventArgs> OnAfterWatch;

		/// <summary>
		/// The on before refresh event
		/// </summary>
		public event EventHandler<BeforeRefreshWatchAudioEventArgs> OnBeforeRefresh;

		/// <summary>
		/// The on after refresh event
		/// </summary>
		public event EventHandler<AfterRefreshWatchAudioEventArgs> OnAfterRefresh;

		/// <summary>
		/// The on capture event
		/// </summary>
		public event EventHandler<CaptureWatchAudioEventArgs> OnCapture;

		/// <summary>
		/// The on before change volume event
		/// </summary>
		public event EventHandler<BeforeChangeVolumeWatchAudioEventArgs> OnBeforeChangeVolume;

		/// <summary>
		/// The on after change volume event
		/// </summary>
		public event EventHandler<AfterChangeVolumeWatchAudioEventArgs> OnAfterChangeVolume;

		/// <summary>
		/// The on exception event
		/// </summary>
		public event EventHandler<ExceptionWatchAudioEventArgs> OnWatchException;

		/// <summary>
		/// The on before index history event
		/// </summary>
		public event EventHandler<BeforeIndexHistoryWatchAudioEventArgs> OnBeforeIndexHistory;

		/// <summary>
		/// The on after index history event
		/// </summary>
		public event EventHandler<AfterIndexHistoryWatchAudioEventArgs> OnAfterIndexHistory;

		/// <summary>
		/// The on before add history event
		/// </summary>
		public event EventHandler<BeforeAddHistoryWatchAudioEventArgs> OnBeforeAddHistory;

		/// <summary>
		/// The on after add history event
		/// </summary>
		public event EventHandler<AfterAddHistoryWatchAudioEventArgs> OnAfterAddHistory;

		/// <summary>
		/// The volume changed by user event
		/// </summary>
		public event EventHandler<VolumeChangedByUserWatchAudioEventArgs> OnVolumeChangedByUser;

		/// <summary>
		/// The volume controlled by user event
		/// </summary>
		public event EventHandler<VolumeControlledByUserWatchAudioEventArgs> OnVolumeControlledByUser;

		/// <summary>
		/// The volume controlled by manager event
		/// </summary>
		public event EventHandler<VolumeControledByManagerWatchAudioEventArgs> OnVolumeControlledByManager;

		/// <summary>
		/// The before dispose event
		/// </summary>
		public event EventHandler<BeforeDisposeWatchAudioEventArgs> OnBeforeDispose;

		/// <summary>
		/// The after dispose event
		/// </summary>
		public event EventHandler<AfterDisposeWatchAudioEventArgs> OnAfterDispose;

		/// <summary>
		/// The before dispose timer event
		/// </summary>
		public event EventHandler<BeforeDisposeTimerWatchAudioEventArgs> OnBeforeDisposeTimer;

		/// <summary>
		/// The after dispose timer event
		/// </summary>
		public event EventHandler<AfterDisposeTimerWatchAudioEventArgs> OnAfterDisposeTimer;

		#endregion
	}
}