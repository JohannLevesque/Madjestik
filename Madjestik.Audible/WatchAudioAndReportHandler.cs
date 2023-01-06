using Madjestik.Audio;
using Madjestik.Core.Exports;
using Madjestik.Core.Logger.ConsoleLogging;
using Madjestik.Core.Timers;
using System;
using System.IO;
using System.Reflection;

namespace Madjestik.Audible
{
	/// <summary>
	/// The program manager class (to be testable)
	/// </summary>
	public class WatchAudioAndReportHandler
	{
		#region Constants

		/// <summary>
		/// The report audio stat interval
		/// </summary>
		public static readonly TimeSpan REPORT_AUDIO_STATS_INTERVAL = TimeSpan.FromSeconds(10);

		#endregion

		#region Properties

		/// <summary>
		/// The report interval
		/// </summary>
		public virtual TimeSpan ReportInterval { get; protected set; } = WatchAudioAndReportHandler.REPORT_AUDIO_STATS_INTERVAL;

		#endregion

		#region Public

		/// <summary>
		/// Watch the audio
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="smartAudioManager">The audio manager</param>
		/// <param name="onReport">The on report method</param>
		/// <param name="exceptionManagement">The exception management</param>
		/// <returns>Returns the timer</returns>
		public virtual ThreadTimer WatchAudioAndReport(IConsoleLogger logger, ISmartAudioManager smartAudioManager, Action<IConsoleLogger, string, string> onReport, ThreadTimerExceptionManagement exceptionManagement)
		{
			// Check from entry
			if (smartAudioManager == null)
				throw new InvalidOperationException("Can not watch audio with a null smartAudioManager (ProgramManager)");

			// Initialize the sound system level
			smartAudioManager.InitializeSoundLevel();

			// TODO: cleanup. Do a CSV report for stats
			this.SubscribeReportAudioStatsEvent(logger, smartAudioManager, onReport);

			// Watch sound
			return smartAudioManager.WatchAudio(exceptionManagement, false);
		}

		#endregion

		#region Report stats

		/// <summary>
		/// Subscribe to the report audio stats event
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="smartAudioManager">The smart audio manager</param>
		/// <param name="onReport">The on report</param>
		protected virtual void SubscribeReportAudioStatsEvent(IConsoleLogger logger, ISmartAudioManager smartAudioManager, Action<IConsoleLogger, string, string> onReport)
		{
			// Check for reporting
			if (this.ReportInterval.TotalMilliseconds <= 0)
				return;

			// Log
			logger?.Debug($"Audio stats (peak values, ...) will be reported each {Math.Round(this.ReportInterval.TotalSeconds, 3)} seconds into CSV file");

			// Initialize CSV builder for export
			var csvBuilder = new CsvBuilderManager("Time (seconds)", "Peak values (volts)", "Volume");

			// Append header
			csvBuilder.WriteHeader();

			// Check from timer
			var lastCheckDate = DateTime.Now;
			var reportDate = DateTime.Now;
			var id = 0;
			smartAudioManager.OnCapture += (s, e) =>
			{
				// Check for output file
				lastCheckDate = this.WriteReportAudioStatsCallback(logger, e?.Model, csvBuilder, reportDate, lastCheckDate, () => { return ++id; }, onReport);
			};
		}

		/// <summary>
		/// Subscribe to the report audio stats event
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="model">The smart audio manager model</param>
		/// <param name="csvBuilder">The CSV builder manager</param>
		/// <param name="reportDate">The report date</param>
		/// <param name="lastCheckDate">The last check date</param>
		/// <param name="getId">The handler to get ID</param>
		/// <param name="onReport">The on report callback</param>
		protected virtual DateTime WriteReportAudioStatsCallback(IConsoleLogger logger, SmartAudioManagerWatchModel model, ICsvBuilderManager csvBuilder, DateTime reportDate, DateTime lastCheckDate, Func<int> getId, Action<IConsoleLogger, string, string> onReport)
		{
			// Check entry
			if (model?.AudioModel == null)
				throw new InvalidOperationException("Can not write report with a null model");
			if (csvBuilder == null)
				throw new InvalidOperationException("Can not write report with a null csv builder");

			// Write entry into the string builder
			var audioModel = model.AudioModel;
			csvBuilder.WriteRow(new object[]
				{
					(audioModel.CaptureDate - model.StartWatchDate).TotalSeconds.ToString() + "s",
					audioModel.PeakValue,
					audioModel.Volume
				});

			// Check from event
			if ((DateTime.Now - lastCheckDate).TotalSeconds <= this.ReportInterval.TotalSeconds)
				return lastCheckDate;

			// Check on report
			int id = 0;
			if (getId != null)
				id = getId.Invoke();

			// Store the check date
			lastCheckDate = DateTime.Now;

			// Check for output file
			this.WriteReportAudioStatsContent(logger, csvBuilder, id, reportDate, onReport);

			// Return the last check date
			return lastCheckDate;
		}

		/// <summary>
		/// Subscribe to the report audio stats event
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="csvBuilder">The CSV builder manager</param>
		/// <param name="id">The ID of the report</param>
		/// <param name="reportDate">The report date</param>
		/// <param name="onReport">The on report callback</param>
		protected virtual void WriteReportAudioStatsContent(IConsoleLogger logger, ICsvBuilderManager csvBuilder, int id, DateTime reportDate, Action<IConsoleLogger, string, string> onReport = null)
		{
			// Check entry
			if (csvBuilder == null)
				throw new InvalidOperationException("Can not write report with a null csv builder");

			// Check for output file
			var assemblyPath = Assembly.GetExecutingAssembly().Location;
			if (!string.IsNullOrEmpty(assemblyPath))
			{
				// Initialize report variables
				var assemblyInfo = new FileInfo(assemblyPath);
				var reportPath = assemblyPath;

#if DEBUG
				// Custom path for debug only
				reportPath = assemblyInfo.Directory.Parent.Parent.Parent.Parent.FullName;
#endif

				// Write report
				var reportFileName = $"audio_{reportDate:hh_mm_ss}_part_{id}.csv";
				if (onReport != null)
					onReport.Invoke(logger, Path.Combine(reportPath, reportFileName), csvBuilder.ToString());

				// Log
				logger?.Info($"Report file '{reportFileName}' created");
			}

			// Reset the string builder
			csvBuilder.Content.Clear();

			// Append header
			csvBuilder.WriteHeader();
		}

		#endregion
	}
}