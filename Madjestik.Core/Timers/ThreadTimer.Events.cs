using System;

namespace Madjestik.Core.Timers
{
	/// <summary>
	/// Partial class for events
	/// </summary>
	public partial class ThreadTimer
	{
		#region Events

		/// <summary>
		/// The BeforeApplyProperties event
		/// </summary>
		public event EventHandler<BeforeApplyPropertiesThreadTimerEventArgs> OnBeforeApplyProperties;

		/// <summary>
		/// The AfterApplyProperties event
		/// </summary>
		public event EventHandler<AfterApplyPropertiesThreadTimerEventArgs> OnAfterApplyProperties;

		/// <summary>
		/// The BeforeSetTickIntervalProperty event
		/// </summary>
		public event EventHandler<BeforeSetTickIntervalThreadTimerEventArgs> OnBeforeSetTickInterval;

		/// <summary>
		/// The AfterSetTickIntervalProperty event
		/// </summary>
		public event EventHandler<AfterSetTickIntervalThreadTimerEventArgs> OnAfterSetTickInterval;

		/// <summary>
		/// The BeforeApplyProperties event
		/// </summary>
		public event EventHandler<BeforeSetContextThreadTimerEventArgs> OnBeforeSetContext;

		/// <summary>
		/// The AfterApplyProperties event
		/// </summary>
		public event EventHandler<AfterSetContextThreadTimerEventArgs> OnAfterSetContext;

		/// <summary>
		/// The BeforeSetCallback event
		/// </summary>
		public event EventHandler<BeforeSetCallbackThreadTimerEventArgs> OnBeforeSetCallback;

		/// <summary>
		/// The AfterSetCallback event
		/// </summary>
		public event EventHandler<AfterSetCallbackThreadTimerEventArgs> OnAfterSetCallback;

		/// <summary>
		/// The BeforeInvokeCallback event
		/// </summary>
		public event EventHandler<BeforeInvokeCallbackThreadTimerEventArgs> OnBeforeInvokeCallback;

		/// <summary>
		/// The AfterInvokeCallback event
		/// </summary>
		public event EventHandler<AfterInvokeCallbackThreadTimerEventArgs> OnAfterInvokeCallback;

		/// <summary>
		/// The BeforeInvokeCallbackException event
		/// </summary>
		public event EventHandler<BeforeInvokeCallbackExceptionThreadTimerEventArgs> OnBeforeInvokeCallbackException;

		/// <summary>
		/// The AfterInvokeCallbackException event
		/// </summary>
		public event EventHandler<AfterInvokeCallbackExceptionThreadTimerEventArgs> OnAfterInvokeCallbackException;

		/// <summary>
		/// The BeforeStart event
		/// </summary>
		public event EventHandler<BeforeStartThreadTimerEventArgs> OnBeforeStart;

		/// <summary>
		/// The AfterStart event
		/// </summary>
		public event EventHandler<AfterStartThreadTimerEventArgs> OnAfterStart;

		/// <summary>
		/// The BeforeStop event
		/// </summary>
		public event EventHandler<BeforeStopThreadTimerEventArgs> OnBeforeStop;

		/// <summary>
		/// The AfterStop event
		/// </summary>
		public event EventHandler<AfterStopThreadTimerEventArgs> OnAfterStop;

		/// <summary>
		/// The BeforeThrowException event
		/// </summary>
		public event EventHandler<BeforeThrowExceptionThreadTimerEventArgs> OnBeforeThrowException;

		/// <summary>
		/// The BeforeExitc event
		/// </summary>
		public event EventHandler<BeforeExitThreadTimerEventArgs> OnBeforeExit;

		/// <summary>
		/// The BeforeDispose event
		/// </summary>
		public event EventHandler<BeforeDisposeThreadTimerEventArgs> OnBeforeDispose;

		/// <summary>
		/// The AfterDispose event
		/// </summary>
		public event EventHandler<AfterDisposeThreadTimerEventArgs> OnAfterDispose;

		#endregion
	}
}