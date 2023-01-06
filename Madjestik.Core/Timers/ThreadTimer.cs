using System;
using System.Threading;

namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The thread timer
	/// </summary>
	public partial class ThreadTimer : IDisposable
	{
		#region Constants

		/// <summary>
		/// Update timer after a change
		/// </summary>
		public const bool DEFAULT_UPDATE_TIMER = true;

		/// <summary>
		/// Reset timer after a change
		/// </summary>
		public const bool DEFAULT_RESET_TIMER = true;

		/// <summary>
		/// Async flag
		/// </summary>
		public const bool DEFAULT_ASYNC = true;

		#endregion

		#region Attributes

		/// <summary>
		/// The internal lock object
		/// </summary>
		protected readonly object lockObject = new object();

		/// <summary>
		/// The internal timer
		/// </summary>
		protected readonly Timer timer;

		#endregion

		#region Constructor

		/// <summary>
		/// The simple thread timer
		/// </summary>
		public ThreadTimer() : this(TimeSpan.Zero)
		{
		}

		/// <summary>
		/// The thread timer
		/// </summary>
		/// <param name="refreshRate">The refresh rate</param>
		/// <param name="callback">The callback handler</param>
		/// <param name="callbackException">The callback exception handler</param>
		/// <param name="exceptionMode">The exception mode</param>
		/// <param name="isAsync">The is async flag</param>
		public ThreadTimer(TimeSpan refreshRate, Action<ThreadTimer, bool, object> callback = null, Action<Exception, ThreadTimer> callbackException = null, ThreadTimerExceptionManagement exceptionMode = ThreadTimerExceptionManagement.THROW_EXCEPTION, bool isAsync = ThreadTimer.DEFAULT_ASYNC)
		{
			// Set properties
			this.TickInterval = refreshRate;
			this.Callback = callback;
			this.ExceptionCallback = callbackException;
			this.ExceptionManagementMode = exceptionMode;
			this.IsAsync = isAsync;

			// Initialize timer

			this.timer = new Timer(e => this.HandleTimer(), null, -1, -1);
		}

		#endregion

		#region Properties

		/// <summary>
		/// The async flag
		/// </summary>
		public virtual bool IsAsync { get; set; }

		/// <summary>
		/// The exception management mode
		/// </summary>
		public virtual ThreadTimerExceptionManagement ExceptionManagementMode { get; set; }

		/// <summary>
		/// The exception callback
		/// </summary>
		public virtual Action<Exception, ThreadTimer> ExceptionCallback { get; set; }

		/// <summary>
		/// The callback
		/// </summary>
		public virtual Action<ThreadTimer, bool, object> Callback { get; protected set; }

		/// <summary>
		/// The refresh ratetick interval
		/// </summary>
		public virtual TimeSpan TickInterval { get; protected set; }

		/// <summary>
		/// The is playing flag
		/// </summary>
		public virtual bool Started { get; protected set; }

		/// <summary>
		/// The timer is failed
		/// </summary>
		public virtual bool Failed { get; protected set; }

		/// <summary>
		/// The context
		/// </summary>
		public virtual object Context { get; protected set; }

		/// <summary>
		/// The dispose property
		/// </summary>
		public bool Disposed { get; protected set; }

		/// <summary>
		/// The flag for should dispose timer flag
		/// </summary>
		public virtual bool ShouldBeDisposed { get; protected set; } = true;

		/// <summary>
		/// The refresh rate
		/// </summary>
		protected virtual Timer Timer
		{
			get
			{
				// Check for state
				if (this.Disposed)
					throw new InvalidOperationException("Can not access to the internal timer: the ThreadTimer object is disposed");

				// Return the created timer
				return this.timer;
			}
		}

		#endregion

		#region Public

		/// <summary>
		/// Set the given context
		/// </summary>
		/// <param name="context">The context to set</param>
		public virtual void SetContext(object context)
		{
			// Raise the BeforeSetContext event
			var oldContext = this.Context;
			this.OnBeforeSetContext?.Invoke(this, new BeforeSetContextThreadTimerEventArgs(this, oldContext, context));

			// Apply changes
			this.Context = context;

			// Raise the AfterSetContext event
			this.OnAfterSetContext?.Invoke(this, new AfterSetContextThreadTimerEventArgs(this, oldContext, context));
		}

		/// <summary>
		/// Clear the current context
		/// </summary>
		public virtual void ClearContext()
		{
			// Apply changes
			this.SetContext(null);
		}

		/// <summary>
		/// Set timer with current properties
		/// </summary>
		public virtual void ApplyTimerProperties()
		{
			// Apply changes
			this.ApplyTimerProperties(this.TickInterval);
		}

		/// <summary>
		/// Start the timer
		/// </summary>
		/// <returns>Returns true if started</returns>
		public virtual bool Start()
		{
			// Check if already started
			if (this.Started)
				return false;

			// Raise the BeforeStart event
			this.OnBeforeStart?.Invoke(this, new BeforeStartThreadTimerEventArgs(this));

			// Set change
			this.ApplyTimerProperties();

			// Raise the AfterStart event
			this.OnAfterStart?.Invoke(this, new AfterStartThreadTimerEventArgs(this));

			// Set flag
			this.Started = true;

			// Default
			return true;
		}

		/// <summary>
		/// Stop the timer
		/// </summary>
		/// <returns>Returns true on change</returns>
		public virtual bool Stop()
		{
			// Check if already stopped
			if (!this.Started)
				return false;

			// Raise the BeforeStop event
			this.OnBeforeStop?.Invoke(this, new BeforeStopThreadTimerEventArgs(this));

			// Set change
			this.ApplyTimerProperties(TimeSpan.FromMilliseconds(-1));

			// Raise the AfterStop event
			this.OnAfterStop?.Invoke(this, new AfterStopThreadTimerEventArgs(this));

			// Set flag
			this.Started = false;

			// Default
			return true;
		}

		/// <summary>
		/// Reset the current ticker
		/// </summary>
		/// <param name="updateTimer">The update timer flag</param>
		public virtual void Reset(bool updateTimer = ThreadTimer.DEFAULT_UPDATE_TIMER)
		{
			// Internal
			this.ApplyTimerProperties();
		}

		/// <summary>
		/// Toggle the play state
		/// </summary>
		/// <returns>Returns the result of the toogle status</returns>
		public virtual bool TogglePlayState()
		{
			// Check for stop
			if (this.Started)
				return this.Stop();

			// Start
			return this.Start();
		}

		/// <summary>
		/// Run manually the handler
		/// </summary>
		public virtual void RunHandler()
		{
			// Internal call
			this.HandleTimerCallback(true);
		}

		/// <summary>
		/// Set the timer with given properties
		/// </summary>
		/// <param name="newInterval">The new tick interval</param>
		/// <param name="updateTimer">The update timer</param>
		/// <returns>Returns true on changes</returns>
		public virtual bool SetTimerProperties(TimeSpan newInterval, bool updateTimer = ThreadTimer.DEFAULT_UPDATE_TIMER)
		{
			// Check for changes
			var isIntervalChanged = this.TickInterval.TotalMilliseconds != newInterval.TotalMilliseconds;

			// Set properties
			if (isIntervalChanged)
			{
				// Raise before event
				this.OnBeforeSetTickInterval?.Invoke(this, new BeforeSetTickIntervalThreadTimerEventArgs(this));

				// Set property
				this.TickInterval = newInterval;

				// Raise afetr event
				this.OnAfterSetTickInterval?.Invoke(this, new AfterSetTickIntervalThreadTimerEventArgs(this));
			}

			// Apply changes
			if (updateTimer)
				this.ApplyTimerProperties();

			// Return the update flag
			return isIntervalChanged;
		}

		/// <summary>
		/// Set the tick interval
		/// </summary>
		/// <param name="newInterval">The new interval</param>
		/// <param name="updateTimer">The update timer flag</param>
		/// <returns>Returns true on changes</returns>
		public virtual bool SetTickInterval(TimeSpan newInterval, bool updateTimer = ThreadTimer.DEFAULT_UPDATE_TIMER)
		{
			// Internal
			return this.SetTimerProperties(newInterval, true);
		}

		/// <summary>
		/// Set the current ticker
		/// </summary>
		/// <param name="newCallback">The new callback</param>
		/// <param name="resetTimer">The reset timer flag</param>
		public virtual void SetCallback(Action<ThreadTimer, bool, object> newCallback, bool resetTimer = ThreadTimer.DEFAULT_UPDATE_TIMER)
		{
			// Pause
			var wasStopped = resetTimer && this.Stop();

			// Raise OnBeforeSetCallback event
			this.OnBeforeSetCallback?.Invoke(this, new BeforeSetCallbackThreadTimerEventArgs(this));

			// Set callback
			this.Callback = newCallback;

			// Raise OnAfterSetCallback event
			this.OnAfterSetCallback?.Invoke(this, new AfterSetCallbackThreadTimerEventArgs(this));

			// Restart
			if (wasStopped)
				this.Start();
		}

		/// <summary>
		/// Set the current ticker
		/// </summary>
		/// <param name="context">The context</param>
		/// <param name="resetTimer">The reset timer flag</param>
		public virtual void SetContext(object context, bool resetTimer = ThreadTimer.DEFAULT_UPDATE_TIMER)
		{
			// Pause
			var wasStopped = resetTimer && this.Stop();

			// Raise OnBeforeSetCallback event
			var currentContext = this.Context;
			this.OnBeforeSetContext?.Invoke(this, new BeforeSetContextThreadTimerEventArgs(this, currentContext, context));

			// Set callback
			this.Context = context;

			// Raise OnAfterSetCallback event
			this.OnAfterSetContext?.Invoke(this, new AfterSetContextThreadTimerEventArgs(this, currentContext, context));

			// Restart
			if (wasStopped)
				this.Start();
		}

		#endregion

		#region Implementation

		/// <summary>
		/// The dispose method
		/// </summary>
		public virtual void Dispose()
		{
			// Check for already disposed
			if (this.Disposed)
				return;

			// Raise OnBeforeDispose event
			this.OnBeforeDispose?.Invoke(this, new BeforeDisposeThreadTimerEventArgs(this));

			// Check for timer
			this.timer?.Dispose();

			// Raise OnAfterDispose event
			this.OnAfterDispose?.Invoke(this, new AfterDisposeThreadTimerEventArgs(this));

			// Set as disposed
			this.Disposed = true;
		}

		#endregion

		#region Protected

		/// <summary>
		/// Set timer with current properties
		/// </summary>
		/// <param name="newInterval">The new tick interval</param>
		protected virtual void ApplyTimerProperties(TimeSpan newInterval)
		{
			// Raise the BeforeApplyProperties event
			this.OnBeforeApplyProperties?.Invoke(this, new BeforeApplyPropertiesThreadTimerEventArgs(this));

			// Apply changes
			this.Timer.Change(TimeSpan.Zero, newInterval);

			// Raise the AfterApplyProperties event
			this.OnAfterApplyProperties?.Invoke(this, new AfterApplyPropertiesThreadTimerEventArgs(this));
		}

		/// <summary>
		/// The callback invoke of the timer
		/// </summary>
		/// <param name="tickContext">The current tickContext</param>
		protected virtual void InvokeCallback(bool isManual)
		{
			// Check for callback
			if (this.Callback == null)
				return;

			// Raise the OnBeforeInvokeCallback event
			this.OnBeforeInvokeCallback?.Invoke(this, new BeforeInvokeCallbackThreadTimerEventArgs(this, isManual));

			// Invoke
			this.Callback.Invoke(this, isManual, this.Context);

			// Raise the OnAfterInvokeCallback event
			this.OnAfterInvokeCallback?.Invoke(this, new AfterInvokeCallbackThreadTimerEventArgs(this, isManual));
		}

		/// <summary>
		/// The callback handler of the timer
		/// </summary>
		/// <param name="isManual">The manual flag</param>
		protected virtual void HandleTimerCallback(bool isManual)
		{
			// Check for had failed flag
			if (this.Failed)
				return;

			// Callback
			try
			{
				this.InvokeCallback(isManual);
			}
			catch (Exception ex)
			{
				// Check for flag
				if (this.ExceptionManagementMode == ThreadTimerExceptionManagement.THROW_EXCEPTION)
					this.Failed = true;

				// Check for callback
				this.InvokeExceptionCallback(ex);

				// Throw the original error
				if (this.ExceptionManagementMode == ThreadTimerExceptionManagement.THROW_EXCEPTION)
				{
					// Raise the BeforeThrowException event
					this.OnBeforeThrowException?.Invoke(this, new BeforeThrowExceptionThreadTimerEventArgs(this));

					// Throw the current exception without lose the stacktrace
					throw;
				}
				else if (this.ExceptionManagementMode == ThreadTimerExceptionManagement.EXIT)
				{
					// Raise the BeforeExitOnException event
					this.OnBeforeExit?.Invoke(this, new BeforeExitThreadTimerEventArgs(this));

					// Exit the environment
					Environment.Exit(this.GetExitCode(ex));
				}
			}
		}

		/// <summary>
		/// The on thread tick
		/// </summary>
		/// <param name="handlerContext">The optional handler context</param>
		protected virtual void HandleTimer()
		{
			// Lock for thread access
			if (this.IsAsync)
			{
				// Check for had failed flag
				this.HandleTimerCallback(false);

				// Stop execution
				return;
			}

			// Lock process
			lock (this.lockObject)
			{
				// Check for had failed flag
				this.HandleTimerCallback(false);
			}
		}

		/// <summary>
		/// The exception managemebt
		/// </summary>
		/// <param name="exception">The exception to manage</param>
		protected virtual void InvokeExceptionCallback(Exception exception)
		{
			// Check for callback exception
			if (this.ExceptionCallback == null)
				return;

			// Raise BeforeInvokeExceptionCallback event
			this.OnBeforeInvokeCallbackException?.Invoke(this, new BeforeInvokeCallbackExceptionThreadTimerEventArgs(this));

			// Internal
			this.ExceptionCallback?.Invoke(exception, this);

			// Raise AfterInvokeExceptionCallback event
			this.OnAfterInvokeCallbackException?.Invoke(this, new AfterInvokeCallbackExceptionThreadTimerEventArgs(this));
		}

		/// <summary>
		/// Returns the exit code
		/// </summary>
		/// <param name="ex">The entry exception</param>
		/// <returns>Returns the exit code</returns>
		protected virtual int GetExitCode(Exception ex)
		{
			// Always return default value;
			return 1;
		}

		#endregion
	}
}