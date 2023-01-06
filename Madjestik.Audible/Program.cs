using Madjestik.Audio;
using Madjestik.Audio.Helpers;
using Madjestik.Core.Helpers;
using Madjestik.Core.Logger.ConsoleLogging;
using Madjestik.Core.Timers;
using System;
using System.IO;
using System.Threading;

namespace Madjestik.Audible
{
	/// <summary>
	/// Main code
	/// </summary>
	internal class Program
	{
		#region Constants

		/// <summary>
		/// The name of the program
		/// </summary>
		public static readonly string PROGRAM_NAME = $"{nameof(Madjestik)}.{nameof(Madjestik.Audible)}";

		#endregion

		#region Main

		/// <summary>
		/// Entry method
		/// </summary>
		/// <param name="args">Program arguments</param>
		[STAThread]
		static void Main(string[] args)
		{
			// Console logger init
			var consoleLogger = ConsoleLogger.InitializeLoggerFromArgs(args);

			try
			{
				// Run
				Program.WatchAudioAndReport(consoleLogger);
			}
			catch (Exception exception)
			{
				// Store and write error
				if (consoleLogger != null && exception != null)
					consoleLogger.Fatal(exception);

				// Final log and exit
				Environment.Exit(1);
			}
		}

		#endregion

		#region Internal

		/// <summary>
		/// Run the program manager
		/// </summary>
		/// <param name="consoleLogger">The consoleLogger</param>
		internal static void WatchAudioAndReport(IConsoleLogger consoleLogger)
		{
			// Check existing
			using Mutex mutex = ProgramHelper.AssertMutexFromProgramId($"The {Program.PROGRAM_NAME} program is already running");

			// Initialize the audio managers
			using var smartAudioManager = new SmartAudioManager(new TimerManager(), new AudioService(), consoleLogger)
			{
				CheckAutoChanges = true
			};

			// Watch the audio from handler and write the report when report is complete
			var handler = new WatchAudioAndReportHandler();
			using var timer = handler.WatchAudioAndReport(consoleLogger, smartAudioManager, 
				Program.WriteReport, ThreadTimerExceptionManagement.EXIT);

			// Keep process alive with mutex and smart audio manager
			TimerHelper.InfiniteSleep(timer, null, new object[] { mutex, smartAudioManager, handler });
		}

		#endregion

		#region Private

		/// <summary>
		/// Write the given report
		/// </summary>
		/// <param name="consoleLogger">The logger</param>
		/// <param name="reportFileName">The report file name</param>
		/// <param name="reportContent">The report content</param>
		private static void WriteReport(IConsoleLogger consoleLogger, string reportFileName, string reportContent)
		{
			// Check for file name
			if (string.IsNullOrEmpty(reportFileName))
				throw new InvalidOperationException("Can not write repor with null or empty file name");
			if (string.IsNullOrEmpty(reportContent))
				throw new InvalidOperationException("Can not write repor with null or empty content");

			// Manage report handler
			if (!File.Exists(reportFileName))
				File.WriteAllText(reportFileName, reportContent);
			else if (consoleLogger != null)
				consoleLogger.Warning($"Report file '{reportFileName}' already existing...");
		}

		#endregion
	}
}