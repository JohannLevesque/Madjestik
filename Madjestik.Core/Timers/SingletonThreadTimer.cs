using System;
using System.Collections.Generic;

namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The singleton thread timer
	/// </summary>
	public class SingletonThreadTimer : ThreadTimer
	{
		#region Attributes

		/// <summary>
		/// Default instance
		/// </summary>
		protected static readonly Dictionary<object, SingletonThreadTimer> Instances = new Dictionary<object, SingletonThreadTimer>();

		/// <summary>
		/// The lock object
		/// </summary>
		protected static object LockObject = new object();

		#endregion

		#region Private constructor

		/// <summary>
		/// Private singleton thread timer
		/// </summary>
		private SingletonThreadTimer() : base()
		{
			// Set property
			this.ShouldBeDisposed = false;
		}

		#endregion

		#region Static

		/// <summary>
		/// Returns the instance ferom key
		/// </summary>
		/// <param name="key">The timer key</param>
		/// <returns>Returns the related timer</returns>
		public static SingletonThreadTimer GetInstance(object key)
		{
			// Check entry
			if (key == null)
				throw new InvalidOperationException("Can not get a timer from a null key");

			// First check
			if (!SingletonThreadTimer.Instances.ContainsKey(key))
			{
				// Check from lock
				lock (SingletonThreadTimer.LockObject)
				{
					// Check from instance
					if (!SingletonThreadTimer.Instances.ContainsKey(key))
					{
						// Add the instance
						SingletonThreadTimer.Instances.Add(key, new SingletonThreadTimer());
					}
				}
			}

			// Return from key
			return SingletonThreadTimer.Instances[key];
		}

		#endregion

		#region Override

		/// <summary>
		/// Override the dispose method to raise an exception on dispose
		/// </summary>
		public override void Dispose()
		{
			// Throw dispose exception
			throw new InvalidOperationException("Can not dispose the singleton thread timer");
		}

		#endregion
	}
}