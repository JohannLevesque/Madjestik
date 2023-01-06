using Madjestik.Core.Extensions;
using Madjestik.Core.Logger;
using Madjestik.Core.Timers;
using System;
using System.Collections.Generic;

namespace Madjestik.Audio
{
	/// <summary>
	/// The smart audio manager
	/// </summary>
	public partial class SmartAudioManager : ISmartAudioManager
	{
		#region Constants

		/// <summary>
		/// The refresh time rate in milliseconds
		/// </summary>
		public const int REFRESH_MILLISECONDS_RATE = 50;

		/// <summary>
		/// The refresh sound milliseconds rate
		/// </summary>
		public const int REFRESH_SOUND_MILLISECONDS_RATE = 1000;

		/// <summary>
		/// The searched sound seconds rate
		/// </summary>
		public const int SEARCHED_SOUND_SECONDS_RATE = 1;

		/// <summary>
		/// The min volume
		/// </summary>
		public const int MIN_VOLUME_PERCENT = 20;

		/// <summary>
		/// The max volume
		/// </summary>
		public const int MAX_VOLUME_PERCENT = 100;

		/// <summary>
		/// The max volume
		/// </summary>
		public const int BASE_VOLUME_PERCENT = 100;

		/// <summary>
		/// The check auto changes
		/// </summary>
		public const bool CHECK_AUTO_CHANGES = false;

		/// <summary>
		/// The default watch async flag
		/// </summary>
		public const bool DEFAULT_WATCH_ASYNC = false;

		#endregion

		#region Attributes

		/// <summary>
		/// The timer manager
		/// </summary>
		protected readonly ITimerManager timerManager = null;

		/// <summary>
		/// The audio service
		/// </summary>
		protected readonly IAudioService audioService = null;

		/// <summary>
		/// The logger
		/// </summary>
		protected readonly ILogger logger = null;

		/// <summary>
		/// The timers
		/// </summary>
		protected readonly List<ThreadTimer> timers = new List<ThreadTimer>();

		#endregion

		#region Constructors

		/// <summary>
		/// Simple constructor with logger
		/// </summary>
		/// <param name="timerManager">The timer manager</param>
		/// <param name="audioService">The audio service</param>
		/// <param name="logger">The logger facade</param>
		public SmartAudioManager(ITimerManager timerManager, IAudioService audioService, ILogger logger = null)
		{
			// Initialize internal propertiies. Default logger initialized with a disable state if logger is null
			this.timerManager = timerManager;
			this.audioService = audioService;
			this.logger = logger ?? new SimpleLogger
			{
				Disabled = true
			};
		}

		#endregion

		#region Properties

		#region Implementation

		/// <summary>
		/// The disposed flag
		/// </summary>
		public virtual bool Disposed { get; protected set; }

		#endregion

		#region Internal

		/// <summary>
		/// The thread manager
		/// </summary>
		protected virtual ITimerManager TimerManager
		{
			get
			{
				// Check initialized
				if (this.timerManager == null)
					throw new InvalidOperationException("Can not use a null timer manager");

				// Return the thread manager
				return this.timerManager;
			}
		}
		/// <summary>
		/// The thread manager
		/// </summary>
		protected virtual IAudioService AudioService
		{
			get
			{
				// Check initialized
				if (this.audioService == null)
					throw new InvalidOperationException("Can not use a null audio service");

				// Return the thread manager
				return this.audioService;
			}
		}

		/// <summary>
		/// The logger
		/// </summary>
		protected virtual ILogger Logger
		{
			get
			{
				// Check initialized
				if (this.logger == null)
					throw new InvalidOperationException("Can not log from a null logger");

				// Return the logger
				return this.logger;
			}
		}

		#endregion

		#region Audio service

		/// <summary>
		/// The current volume accesseur
		/// </summary>
		public virtual float Volume
		{
			get
			{
				return this.AudioService.GetVolume();
			}
			set
			{
				// Set the volume
				this.SetAudioVolume(value);
			}
		}

		#endregion

		#region Manager

		/// <summary>
		/// The refresh time rate
		/// </summary>
		public virtual TimeSpan RefreshRate { get; set; } = TimeSpan.FromMilliseconds(SmartAudioManager.REFRESH_MILLISECONDS_RATE);

		/// <summary>
		/// The refresh sound time rate
		/// </summary>
		public virtual TimeSpan RefreshSoundRate { get; set; } = TimeSpan.FromMilliseconds(SmartAudioManager.REFRESH_SOUND_MILLISECONDS_RATE);

		/// <summary>
		/// The searched sound rate
		/// </summary>
		public virtual TimeSpan SearchedSoundRate { get; set; } = TimeSpan.FromSeconds(SmartAudioManager.SEARCHED_SOUND_SECONDS_RATE);

		/// <summary>
		/// The refresh time rate in milliseconds
		/// </summary>
		public virtual int MinVolumePercent { get; set; } = SmartAudioManager.MIN_VOLUME_PERCENT;

		/// <summary>
		/// The refresh time rate in milliseconds
		/// </summary>
		public virtual int MaxVolumePercent { get; set; } = SmartAudioManager.MAX_VOLUME_PERCENT;

		/// <summary>
		/// The check auto changes
		/// </summary>
		public virtual bool CheckAutoChanges { get; set; } = SmartAudioManager.CHECK_AUTO_CHANGES;

		/// <summary>
		/// The refresh time rate in milliseconds
		/// </summary>
		public virtual int BaseVolumePercent
		{
			get
			{
				return this.MaxVolumePercent;
			}
		}

		/// <summary>
		/// The current volume percent accesseur
		/// </summary>
		public virtual int VolumePercent
		{
			get
			{
				return this.Volume.GetRoundedPercent();
			}
			set
			{
				// Set the volume
				this.Volume = value.GetFloatPercent();
			}
		}

		#endregion

		#endregion

		#region Public

		/// <summary>
		/// Initialize the OS sound level
		/// </summary>
		public virtual void InitializeSoundLevel()
		{
			// Raise event
			int currentLevel = this.VolumePercent;
			int baseVolume = this.BaseVolumePercent;
			this.OnBeforeInitializeAudio?.Invoke(this, new BeforeInitializeAudioEventArgs(this, currentLevel, baseVolume));

			// Log
			this.Logger.Debug($"Sound initialization");

			// Extract the current sound level
			this.Logger.Debug($"Current sound level is: {currentLevel}%");

			// Check for not max
			if (currentLevel != baseVolume)
			{
				// Log
				this.Logger.Warning($"Sound level detected as different than the base {baseVolume}% ({currentLevel}%). Sound system will be automatically up to {baseVolume}%");

				// Raise event
				this.OnBeforeInitializeChangeAudio?.Invoke(this, new BeforeInitializeChangeAudioEventArgs(this, currentLevel, baseVolume));

				// Set volume
				this.Volume = baseVolume;

				// Raise event
				this.OnAfterInitializeChangeAudio?.Invoke(this, new AfterInitializeChangeAudioEventArgs(this, currentLevel, baseVolume));
			}

			// Raise event
			this.OnAfterInitializeAudio?.Invoke(this, new AfterInitializeAudioEventArgs(this, currentLevel, baseVolume));
		}

		/// <summary>
		/// Watch the current sound output to define master level
		/// </summary>
		/// <param name="exceptionManagementMode">The optional exception managemebt mode</param>
		/// <param name="isAsync">Optional async flag</param>
		public virtual ThreadTimer WatchAudio(ThreadTimerExceptionManagement exceptionManagementMode = ThreadTimerExceptionManagement.THROW_EXCEPTION, bool isAsync = SmartAudioManager.DEFAULT_WATCH_ASYNC)
		{
			// Log
			this.Logger.Info("Start to analyze audio output");

			// Initialize the first model
			var watchModel = new SmartAudioManagerWatchModel
			{
				AudioModel = AudioService.GetData()
			};

			// Initialize and start the timer
			var thManager = this.TimerManager;
			var timer = thManager.CreateThreadTimer(this.RefreshRate,
				(threadTimer, manualExecution, tickContext) =>
				{
					// Raise event
					this.OnBeforeRefresh?.Invoke(this, new BeforeRefreshWatchAudioEventArgs(this, watchModel));

					// Internal method
					var oldModel = watchModel;
					watchModel = this.WatchAudio(watchModel, threadTimer, manualExecution);

					// Raise event
					this.OnAfterRefresh?.Invoke(this, new AfterRefreshWatchAudioEventArgs(this, oldModel, watchModel));
				},
				(exception, timer) =>
				{
					// Check from entry
					this.OnWatchExceptionManagement(exception, timer);
				},
				exceptionManagementMode, isAsync);

			// Check returned timer
			if (timer == null)
				throw new InvalidOperationException("Can not start watch audio with a null returned timer");

			// Raise event
			this.OnBeforeWatch?.Invoke(this, new BeforeWatchAudioEventArgs(this));

			// Start the timer
			timer.Start();

			// Raise event
			this.OnAfterWatch?.Invoke(this, new AfterWatchAudioEventArgs(this, timer));

			// Add it to the list
			this.timers.Add(timer);

			// Return the timer
			return timer;
		}

		/// <summary>
		/// The dispose implementation
		/// </summary>
		public virtual void Dispose()
		{
			// Check already disposed
			if (this.Disposed)
				return;

			// Raise on after change volume event
			this.OnBeforeDispose?.Invoke(this, new BeforeDisposeWatchAudioEventArgs(this));

			// Browse all timers
			var allTimers = timers.ToArray();
			foreach (var timer in allTimers)
			{
				// Check for dispose
				if (timer != null && timer.ShouldBeDisposed)
				{
					// Raise on after change volume event
					this.OnBeforeDisposeTimer?.Invoke(this, new BeforeDisposeTimerWatchAudioEventArgs(this, timer));

					// Dispose current timer
					timer.Dispose();

					// Raise on after dispose timer event
					this.OnAfterDisposeTimer?.Invoke(this, new AfterDisposeTimerWatchAudioEventArgs(this, timer));
				}

				// Remove from the timer list
				this.timers.Remove(timer);
			}

			// Raise on after dispose event
			this.OnAfterDispose?.Invoke(this, new AfterDisposeWatchAudioEventArgs(this));

			// Set flag as disposed
			this.Disposed = true;
		}

		#endregion

		#region Protected

		/// <summary>
		/// Returns the audio data
		/// </summary>
		/// <returns>Returns the model object</returns>
		protected virtual AudioServiceModel GetAudioData()
		{
			// Extract audio data
			return AudioService.GetData();
		}

		/// <summary>
		/// Watch the current sound output to define master level
		/// </summary>
		/// <param name="previousModel">The smart audio manager model</param>
		/// <returns>Returns the watch audio model</returns>
		protected virtual SmartAudioManagerWatchModel CreateWatchAudioModel(SmartAudioManagerWatchModel previousModel)
		{
			// Check from entry
			if (previousModel == null)
				throw new InvalidOperationException("Can not watch the sound output with a null smart audio manager model");

			// Initialize model
			var currentModel = new SmartAudioManagerWatchModel
			{
				VolumeSetByUser = previousModel.VolumeSetByUser,
				LastModelChecked = previousModel.LastModelChecked,
				StartWatchDate = previousModel.StartWatchDate
			};

			// Extract audio data
			var currentAudioModel = this.GetAudioData();

			// Store into result model
			currentModel.AudioModel = currentAudioModel ?? throw new InvalidOperationException("Can not watch audio data from a null audio object model");

			// Check changes
			this.WatchAudioChanges(previousModel, currentModel);

			// Raise event
			this.OnCapture?.Invoke(this, new CaptureWatchAudioEventArgs(this, currentModel));

			// Return the created model
			return currentModel;
		}

		/// <summary>
		/// Watch the current sound output to define master level
		/// </summary>
		/// <param name="previousModel">The previous audio model</param>
		/// <param name="timer">The trhead timer</param>
		/// <param name="manualExecution">The manual execution</param>
		/// <returns>Returns the new model</returns>
		protected virtual SmartAudioManagerWatchModel WatchAudio(SmartAudioManagerWatchModel previousModel, ThreadTimer timer, bool manualExecution)
		{
			// Log
			this.Logger.Log(LogLevel.DEBUG, string.Empty);
			this.Logger.Debug(" Analyzing current output audio trame");

			// Check from entry
			if (previousModel == null)
				throw new InvalidOperationException("Can not watch the sound output with a null smart audio manager model");

			// Initialize model
			var currentModel = this.CreateWatchAudioModel(previousModel);
			if (currentModel == null)
				throw new InvalidOperationException("Can not watch the sound output with a null created smart audio manager model");

			// Return the created model
			return this.WatchAudioCheck(currentModel);
		}

		/// <summary>
		/// Watch the current sound output to define master level
		/// </summary>
		/// <param name="previousModel">The previous smart audio manager model</param>
		/// <param name="newModel">The new smart audio manager model</param>
		/// <param name="noLog">The no log flag</param>
		/// <returns>Returns true on detected change</returns>
		protected virtual bool WatchAudioChanges(SmartAudioManagerWatchModel previousModel, SmartAudioManagerWatchModel newModel, bool noLog = false)
		{
			// Result
			var result = false;

			// Check changes
			if (this.CheckAutoChanges && previousModel.AudioModel != null)
			{
				// Check to disable the smart sound system
				if (previousModel.AudioModel.Volume != newModel.AudioModel.Volume)
				{
					// Store change into model
					newModel.VolumeSetByUser = newModel.AudioModel.Volume;

					// Store change
					result = true;

					// Raise event
					this.OnVolumeChangedByUser?.Invoke(this, new VolumeChangedByUserWatchAudioEventArgs(this));
					this.OnVolumeControlledByUser?.Invoke(this, new VolumeControlledByUserWatchAudioEventArgs(this));

					// Log
					if (!noLog)
						this.Logger.Debug($"User has changed the sound volume (from {previousModel.AudioModel.VolumePercent}% to {newModel.AudioModel.VolumePercent}%)");
				}

				// Check for enable the smart system
				if (newModel.VolumeSetByUser.HasValue)
				{
					// Check for full management
					if (newModel.AudioModel.VolumePercent == SmartAudioManager.MAX_VOLUME_PERCENT)
					{
						// Store change into model
						newModel.VolumeSetByUser = null;

						// Store change
						result = true;

						// Raise event
						this.OnVolumeControlledByManager?.Invoke(this, new VolumeControledByManagerWatchAudioEventArgs(this));

						// Log
						if (!noLog)
							this.Logger.Debug($"Smart sound system enabled");
					}
				}
			}

			// Return the result
			return result;
		}

		/// <summary>
		/// Watch the current sound output to define master level
		/// </summary>
		/// <param name="currentModel">The current model</param>
		/// <returns>Returns the new model</returns>
		protected virtual SmartAudioManagerWatchModel WatchAudioCheck(SmartAudioManagerWatchModel currentModel)
		{
			// Log
			if (currentModel == null)
				throw new InvalidOperationException("Can not check the watched sound output with a null created smart audio manager model");

			// Extract audio data
			var currentAudioModel = currentModel.AudioModel;
			if (currentAudioModel == null)
				throw new InvalidOperationException("Can not watch audio data from a null audio model");

			// Calculate checks
			var currentDate = DateTime.Now;
			var lastModelChecked = currentModel.LastModelChecked;
			if (lastModelChecked == null)
				currentModel.LastModelChecked = currentModel;
			var hadCheck = lastModelChecked != null;
			var diffCheckDates = hadCheck ? currentDate - lastModelChecked.AudioModel.CaptureDate : TimeSpan.Zero;

			// Check for refresh count and refresh if required
			if (hadCheck && diffCheckDates.TotalMilliseconds >= this.RefreshSoundRate.TotalMilliseconds)
			{
				// Register the last model checked
				this.StoreLastCheckDate(currentModel);

				// Refresh sound
				this.WatchAudioUpdateVolume(currentModel);
			}

			// Return the created model
			return currentModel;
		}

		/// <summary>
		/// Store the last check date
		/// </summary>
		/// <param name="currentModel">The current model</param>
		protected virtual void StoreLastCheckDate(SmartAudioManagerWatchModel currentModel)
		{
			// Register the last model checked
			currentModel.LastModelChecked = currentModel ?? throw new InvalidOperationException("Can not store the last check date with a null or empty model");
		}

		/// <summary>
		/// Manage the audio refresh sound
		/// </summary>
		/// <param name="currentModel">The current model</param>
		/// <returns>Returns true if volume was updated</returns>
		protected virtual bool WatchAudioUpdateVolume(SmartAudioManagerWatchModel currentModel)
		{
			// Check from entry
			if (currentModel == null)
				throw new InvalidOperationException("Can not watch audio updating volume with a null model");

			// Extract audio model
			var currentAudioModel = currentModel.AudioModel;
			if (currentAudioModel == null)
				throw new InvalidOperationException("Can not watch audio updating volume with a null audio model");

			// Check for playing status
			if (currentAudioModel.IsAudible)
			{
				// Log. TODO: check radian
				this.Logger.Debug($"   ?  PeakValue: {currentAudioModel.PeakValue} - Volume: {currentAudioModel.VolumePercent}%");

				// Check for non disabled by user
				if (!currentModel.VolumeSetByUser.HasValue)
				{
					// Check from volume
					if (currentModel.TOREMOVE.HasValue && currentModel.TOREMOVE.Value != currentModel.AudioModel.Volume)
					{
						// Log
						this.Logger.Debug($"   #  Smart audio is targeting the new volume: {currentModel.TOREMOVE.Value.GetRoundedPercent()}%");

						// Set the audio volume
						var setLevel = this.SetAudioVolume(currentModel.TOREMOVE.Value);

						// Detect changes
						if (currentAudioModel.Volume != setLevel)
						{
							// Store the new volume
							currentAudioModel.Volume = setLevel;

							// Log
							this.Logger.Debug($"  /!\\ New volume calculated: from {currentModel.AudioModel.VolumePercent}% to: {setLevel.GetRoundedPercent()}%");

							// Return true
							return true;
						}
					}

					// Log
					this.Logger.Debug($"  /!\\ No audio update ({currentModel.AudioModel.VolumePercent}%)");
				}
				else
				{
					// Log
					this.Logger.Debug($"  /!\\ Volume managed by user ({currentModel.VolumeSetByUserPercent}%) - Smart sound system disabled");
				}
			}
			else
			{
				// Log
				this.Logger.Debug($"  /!\\ {(currentAudioModel.IsMute || currentAudioModel.VolumePercent == 0 ? "No audible audio / muted" : "No audio detected")}");
			}

			// Default return
			return false;
		}

		/// <summary>
		/// Set the sound system level
		/// </summary>
		/// <param name="level">The sound level to set</param>
		/// <param name="force">Optional flag to force value</param>
		/// <returns>Returns the set volume</returns>
		protected virtual float SetAudioVolume(float level, bool force = false)
		{
			// Calculate range
			var newLevel = force ? level : this.ApplyVolumeLimits(level);

			// Check from entry
			if (newLevel < 0f)
				throw new InvalidOperationException($"Can not set a sound level less than 0 ({newLevel})");
			if (newLevel > 1f)
				throw new InvalidOperationException($"Can not set a sound level more than 1 ({newLevel})");

			// Check for changes
			if (newLevel == this.Volume)
				return newLevel;

			// Raise event
			this.OnBeforeChangeVolume?.Invoke(this, new BeforeChangeVolumeWatchAudioEventArgs(this, level, newLevel));

			// Log
			int levelPercent = newLevel.GetRoundedPercent();
			this.Logger.Debug($"   => Try to set the sound system level to {levelPercent}% ({newLevel})");

			// Try to execute
			try
			{
				// Try to set the audio command reconverting for rounded values
				this.AudioService.SetVolume(newLevel);
			}
			catch (Exception ex)
			{
				// Throw a custom exception
				throw new InvalidOperationException($"Can not set the system sound level", ex);
			}

			// Raise on after change volume event
			this.OnAfterChangeVolume?.Invoke(this, new AfterChangeVolumeWatchAudioEventArgs(this, level, newLevel));

			// Check for log
			this.Logger.Debug($"   +  Sound level set to {levelPercent}%");

			// Return the new level
			return newLevel;
		}

		/// <summary>
		/// Apply the limits on a float number
		/// </summary>
		/// <param name="number">The number to limit</param>
		/// <returns>Returns the limit number</returns>
		protected virtual float ApplyVolumeLimits(float number)
		{
			// Limits management
			var minValue = this.MinVolumePercent.GetFloatPercent();
			var maxValue = this.MaxVolumePercent.GetFloatPercent();

			// Return limit management
			return number.ApplyLimitsPercent(minValue, maxValue);
		}

		/// <summary>
		/// Watch exception management
		/// </summary>
		/// <param name="exception">The exception</param>
		/// <param name="timer">The thread timer</param>
		/// <returns>Returns the new model</returns>
		protected virtual void OnWatchExceptionManagement(Exception exception, ThreadTimer timer)
		{
			// Check from entry
			if (exception == null)
				return;

			// Log
			var exceptionMode = timer?.ExceptionManagementMode;
			if (exceptionMode.HasValue && exceptionMode.Value != ThreadTimerExceptionManagement.THROW_EXCEPTION)
			{
				// Check cases
				if (exceptionMode.Value == ThreadTimerExceptionManagement.EXIT)
					this.Logger.Fatal(exception);
				else
					this.Logger.Error(exception);
			}

			// Raise event
			this.OnWatchException?.Invoke(this, new ExceptionWatchAudioEventArgs(this, exception, timer));
		}

		#endregion
	}
}