using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The before console log event args
	/// </summary>
	public class BeforeLogConsoleEventArgs : AbstractConsoleLoggerEventArgs
	{
		#region Constructor

		/// <summary>
		/// The before console log event arg
		/// </summary>
		/// <param name="model">The model</param>
		public BeforeLogConsoleEventArgs(IConsoleLogger logger, ConsoleLogModel model) : base(logger)
		{
			// Set property
			this.Model = model;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The console log model
		/// </summary>
		public ConsoleLogModel Model { get; }

		#endregion
	}
}