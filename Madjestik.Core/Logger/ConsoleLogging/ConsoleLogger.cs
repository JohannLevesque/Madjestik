using Madjestik.Core.Themes;
using Madjestik.Core.Themes.Console;
using System;

namespace Madjestik.Core.Logger.ConsoleLogging
{
	/// <summary>
	/// The default logger console implementation
	/// </summary>
	public class ConsoleLogger : AbstractLogger, IConsoleLogger
	{
		#region Constants

		/// <summary>
		/// The default show date time flag
		/// </summary>
		public const bool DEFAULT_SHOW_DATETIME = true;

		/// <summary>
		/// The default show date time flag
		/// </summary>
		public const bool DEFAULT_IS_ASYNC = true;

		/// <summary>
		/// The debug console color
		/// </summary>
		private const ConsoleColor DEBUG_COLOR = ConsoleColor.Green;

		/// <summary>
		/// The info console color
		/// </summary>
		private const ConsoleColor INFO_COLOR = ConsoleColor.Cyan;

		/// <summary>
		/// The warn console color
		/// </summary>
		private const ConsoleColor WARN_COLOR = ConsoleColor.Yellow;

		/// <summary>
		/// The error console color
		/// </summary>
		private const ConsoleColor ERROR_COLOR = ConsoleColor.Red;

		/// <summary>
		/// The fatal console color
		/// </summary>
		private const ConsoleColor FATAL_COLOR = ConsoleColor.Red;

		/// <summary>
		/// The default space
		/// </summary>
		private const string DEFAULT_SPACE = " ";

		/// <summary>
		/// The datetime format
		/// </summary>
		private const string DATETIME_FORMAT = "MM/dd/yyyy hh:mm:ss tt";

		#endregion

		#region Events

		/// <summary>
		/// The before log console event
		/// </summary>
		public event EventHandler<BeforeLogConsoleEventArgs> OnBeforeLogConsole;

		/// <summary>
		/// The after log console event
		/// </summary>
		public event EventHandler<AfterLogConsoleEventArgs> OnAfterLogConsole;

		/// <summary>
		/// The before write console event
		/// </summary>
		public event EventHandler<BeforeWriteConsoleEventArgs> OnBeforeWriteConsole;

		/// <summary>
		/// The after write console event
		/// </summary>
		public event EventHandler<AfterWriteConsoleEventArgs> OnAfterWriteConsole;

		/// <summary>
		/// The before console clear event
		/// </summary>
		public event EventHandler<BeforeClearConsoleEventArgs> OnBeforeClearConsole;

		/// <summary>
		/// The after console clear event
		/// </summary>
		public event EventHandler<AfterClearConsoleEventArgs> OnAfterClearConsole;

		/// <summary>
		/// The before change theme event arg
		/// </summary>
		public event EventHandler<BeforeChangeThemeEventArgs<ConsoleColor>> OnBeforeChangeTheme;

		/// <summary>
		/// The after change theme event args
		/// </summary>

		public event EventHandler<AfterChangeThemeEventArgs<ConsoleColor>> OnAfterChangeTheme;

		#endregion

		#region Attributes

		/// <summary>
		/// The lock object
		/// </summary>
		protected static object lockObject = new object();

		#endregion

		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="initialColor">The optional initial color. Console properties will be used as fallback</param>
		/// <param name="initialBackgroundColor">The optional initial background color. Console properties will be used as fallback</param>
		/// <param name="noInit">Flag to prevent to set the console </param>
		public ConsoleLogger(ConsoleColor? initialColor = null, ConsoleColor? initialBackgroundColor = null, bool noInit = false)
		{
			// Initialize properties
			this.InitialColor = this.ForegroundColor = initialColor ?? this.GetCurrentConsoleForegroundColor();
			this.InitialBackgroundColor = this.BackgroundColor = initialBackgroundColor ?? this.GetCurrentConsoleBackgroundColor();

			// Check for init
			if (!noInit)
				this.ApplyConsoleColors();
		}

		#endregion

		#region Properties

		#region Implementation

		/// <summary>
		/// The console converter
		/// </summary>
		protected IThemeConverter<ConsoleColor> themeConverter = null;
		public IThemeConverter<ConsoleColor> ThemeConverter
		{
			get
			{
				// The theme converter
				return this.themeConverter;
			}
			set
			{
				// Set the theme
				this.SetThemeConverter(value);
			}
		}

		#endregion

		#region Log

		/// <summary>
		/// The initial log color
		/// </summary>
		public virtual ConsoleColor InitialColor { get; protected set; }

		/// <summary>
		/// The initial background log color
		/// </summary>
		public virtual ConsoleColor InitialBackgroundColor { get; protected set; }

		/// <summary>
		/// The debug log color
		/// </summary>
		public virtual ConsoleColor DebugColor { get; set; } = ConsoleLogger.DEBUG_COLOR;

		/// <summary>
		/// The info log color
		/// </summary>
		public virtual ConsoleColor InfoColor { get; set; } = ConsoleLogger.INFO_COLOR;

		/// <summary>
		/// The warn log color
		/// </summary>
		public virtual ConsoleColor WarnColor { get; set; } = ConsoleLogger.WARN_COLOR;

		/// <summary>
		/// The error log color
		/// </summary>
		public virtual ConsoleColor ErrorColor { get; set; } = ConsoleLogger.ERROR_COLOR;

		/// <summary>
		/// The fatal log color
		/// </summary>
		public virtual ConsoleColor FatalColor { get; set; } = ConsoleLogger.FATAL_COLOR;

		/// <summary>
		/// The default is async flag
		/// </summary>
		public virtual bool IsAsync { get; set; } = ConsoleLogger.DEFAULT_IS_ASYNC;

		/// <summary>
		/// The show date time flag for new log
		/// </summary>
		public virtual bool ShowDateTime { get; set; } = ConsoleLogger.DEFAULT_SHOW_DATETIME;

		/// <summary>
		/// The default space
		/// </summary>
		public virtual string DefaultSpace { get; set; } = ConsoleLogger.DEFAULT_SPACE;

		/// <summary>
		/// The datetime format
		/// </summary>
		public virtual string DateTimeFormat { get; set; } = ConsoleLogger.DATETIME_FORMAT;

		/// <summary>
		/// The color for new log
		/// </summary>
		public virtual ConsoleColor ForegroundColor { get; set; }

		/// <summary>
		/// The background color for new log
		/// </summary>
		public virtual ConsoleColor BackgroundColor { get; set; }

		/// <summary>
		/// The date time color for new log
		/// </summary>
		public virtual ConsoleColor? DateTimeColor { get; set; } = null;

		/// <summary>
		/// The date time background color for new log
		/// </summary>
		public virtual ConsoleColor? DateTimeBackgroundColor { get; set; } = null;

		/// <summary>
		/// The default message color
		/// </summary>
		public virtual ConsoleColor? MessageColor { get; set; } = null;

		/// <summary>
		/// The default message background color for new log
		/// </summary>
		public virtual ConsoleColor? MessageBackgroundColor { get; set; } = null;

		#endregion

		#endregion

		#region Static

		/// <summary>
		/// Initialize and returns the logger
		/// </summary>
		/// <param name="args">The program arguments</param>
		/// <returns>Returns the initialized logger</returns>
		public static IConsoleLogger InitializeLoggerFromArgs(string[] args)
		{
			// Console logger init
			var consoleLogger = new ConsoleLogger()
			{
				DateTimeColor = ConsoleColor.DarkYellow,
				// You also can use theme uncommenting the line below:
				//ThemeConverter = new Madjestik.Core.Themes.Console.InvertColorsConsoleThemeConverter()
			};

			// Initialize the theme from arguments if no theme was set. Command arg should be (/t=[convertername]) from the launchSettings.json file.
			consoleLogger.ThemeConverter ??= AbstractThemeConverter<ConsoleColor>.GetThemeConverterFromArguments(args);

#if DEBUG
			// Small hack to avoid to have issue with background console color on debug
			if (consoleLogger?.ThemeConverter != null)
				consoleLogger.Clear();
#endif

			// Initialize log level from args
			consoleLogger.InitializeLogLevelFromArguments(args);

			// Return the initialized logger
			return consoleLogger;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Clear all entries
		/// </summary>
		public override void Clear()
		{
			// Raise event
			this.OnBeforeClearConsole?.Invoke(this, new BeforeClearConsoleEventArgs(this));

			// Clear all logs
			this.ClearConsole();

			// Raise event
			this.OnAfterClearConsole?.Invoke(this, new AfterClearConsoleEventArgs(this));
		}

		/// <summary>
		/// Log into console according to the issue
		/// </summary>
		/// <param name="model">The log model</param>
		/// <returns>Returns true if logged</returns>
		public override bool Log(LogModel model)
		{
			// Check async case
			if (this.IsAsync)
				return this.LogToConsole(model);

			// Use lock to avoid issues
			lock (ConsoleLogger.lockObject)
			{
				// Log to console
				return this.LogToConsole(model);
			}
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="message">The message to log</param>
		/// <param name="logDate">The optional log date</param>
		/// <returns>Returns true if logged</returns>
		public override bool Log(LogLevel logLevel, string message, DateTime? logDate = null)
		{
			// Internal
			return this.Log(new ConsoleLogModel
			{
				DateTime = logDate ?? DateTime.Now,
				Level = logLevel,
				LogEntries = new ConsoleLogEntryModel[] { new ConsoleLogEntryModel(message) },
				ShowDateTime = this.ShowDateTime,
				Color = this.GetColorFromLogLevel(logLevel),
				DateTimeColor = this.DateTimeColor,
				DateTimeBackgroundColor = this.DateTimeBackgroundColor,
				MessageColor = this.MessageColor,
				MessageBackgroundColor = this.MessageBackgroundColor
			});
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logLevel">The log level</param>
		/// <param name="entries">The entries to log</param>
		/// <returns>Returns true if logged</returns>
		public virtual bool Log(LogLevel logLevel, params ConsoleLogEntryModel[] entries)
		{
			// Internal
			return this.Log(new ConsoleLogModel
			{
				DateTime = DateTime.Now,
				Level = logLevel,
				LogEntries = entries,
				ShowDateTime = this.ShowDateTime,
				Color = this.GetColorFromLogLevel(logLevel),
				DateTimeColor = this.DateTimeColor,
				DateTimeBackgroundColor = this.DateTimeBackgroundColor,
				MessageColor = this.MessageColor,
				MessageBackgroundColor = this.MessageBackgroundColor
			});
		}

		#endregion

		#region Callbacks

		/// <summary>
		/// Manage the theme change
		/// </summary>
		/// <param name="context">Optional context</param>
		/// <param name="converter">The theme converter</param>
		public virtual void SetThemeConverter(IThemeConverter<ConsoleColor> converter, object context = null)
		{
			// Raise event
			var current = this.themeConverter;
			this.OnBeforeChangeTheme?.Invoke(this, new BeforeChangeThemeEventArgs<ConsoleColor>(this, current, converter));

			// Set the theme
			this.SetThemeConverterAndApply(converter, context);

			// Raise event
			this.OnAfterChangeTheme?.Invoke(this, new AfterChangeThemeEventArgs<ConsoleColor>(this, current, converter));
		}

		#endregion

		#region Protected

		#region Console

		/// <summary>
		/// Clear the console
		/// </summary>
		protected void ClearConsole()
		{
			// Clear all logs
			Console.Clear();
		}

		/// <summary>
		/// Write the log
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="newLine">The new line flag</param>
		protected virtual void WriteTextToConsole(string text, bool newLine = false)
		{
			// Check cases
			if (newLine)
				Console.WriteLine(text);
			else
				Console.Write(text);
		}

		/// <summary>
		/// Get the current console color property
		/// </summary>
		/// <returns>Returns the console color value</returns>
		protected virtual ConsoleColor GetCurrentConsoleForegroundColor()
		{
			// Return the console color
			return Console.ForegroundColor;
		}

		/// <summary>
		/// Get the current console background color property
		/// </summary>
		/// <returns>Returns the console background color value</returns>
		protected virtual ConsoleColor GetCurrentConsoleBackgroundColor()
		{
			// Return the console color
			return Console.BackgroundColor;
		}

		#endregion

		#region Log

		/// <summary>
		/// Log to console
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>Returns true if logged into console</returns>
		protected virtual bool LogToConsole(LogModel model)
		{
			// Cast as integer
			if (!base.Log(model) || model.LogEntries == null || model.LogEntries.Length == 0)
				return false;

			// Check for model
			if (!(model is ConsoleLogModel consoleModel))
				throw new InvalidOperationException("Can not cast the log model into a console log model");

			// Reset the default colors applying current configuration
			this.ApplyConsoleColors(true);

			// Manage color
			ConsoleColor consoleDefaultColor = consoleModel.Color ?? this.ForegroundColor;
			ConsoleColor consoleDefaultBackgroundColor = consoleModel.BackgroundColor ?? this.BackgroundColor;

			// Raise event
			this.OnBeforeLogConsole?.Invoke(this, new BeforeLogConsoleEventArgs(this, consoleModel));

			// Write the given log
			this.WriteLog(model, consoleDefaultColor, consoleDefaultBackgroundColor);

			// Raise event
			this.OnAfterLogConsole?.Invoke(this, new AfterLogConsoleEventArgs(this, consoleModel));

			// Reset the default colors applying current configuration
			this.ApplyConsoleColors(true);

			// Default true
			return true;
		}

		/// <summary>
		/// Write the given log
		/// </summary>
		/// <param name="model">The model</param>
		/// <param name="defaultColor">The default color</param>
		/// <param name="defaultBackgroundColor">The default background color</param>
		protected virtual void WriteLog(LogModel model, ConsoleColor defaultColor, ConsoleColor defaultBackgroundColor)
		{
			// Check for model
			if (!(model is ConsoleLogModel consoleModel))
				throw new InvalidOperationException("Can not cast the log model into a console log model");

			// Extract flag
			var dateShown = this.WriteLogDate(consoleModel, defaultColor, defaultBackgroundColor);

			// Log
			var logCount = model.LogEntries.Length;
			for (var i = 0; i < logCount; i++)
			{
				// Extract
				var entryModel = model.LogEntries[i];

				// Check from entry
				if (entryModel == null || !(entryModel is ConsoleLogEntryModel consoleEntryModel))
					throw new InvalidOperationException("Console logger can not write other entries than ConsoleLogEntryModel implementations");

				// Write entry
				this.WriteLogEntry(consoleModel, consoleEntryModel, i > 0, dateShown && i < logCount,
					consoleEntryModel.Color ?? consoleModel.MessageColor ?? defaultColor,
					consoleEntryModel.BackgroundColor ?? consoleModel.MessageBackgroundColor ?? defaultBackgroundColor);
			}
		}

		/// <summary>
		/// Write the log date
		/// </summary>
		/// <param name="consoleModel">The console model</param>
		/// <param name="defaultColor">The default color</param>
		/// <param name="defaultBackgroundColor">The default background color</param>
		/// <returns>Returns true if log date written</returns>
		protected virtual bool WriteLogDate(ConsoleLogModel consoleModel, ConsoleColor defaultColor, ConsoleColor defaultBackgroundColor)
		{
			// Check show date time
			if (consoleModel != null && consoleModel.ShowDateTime)
			{
				// Write date
				this.WriteToConsole(consoleModel.DateTime.ToString(this.DateTimeFormat),
					consoleModel.DateTimeColor ?? this.DateTimeColor ?? defaultColor,
					consoleModel.DateTimeBackgroundColor ?? this.DateTimeBackgroundColor ?? defaultBackgroundColor,
					false, 
					ConsoleLoggerTransformSourceKind.DATE_TIME);

				// Return true
				return true;
			}

			// Write the current date
			return false;
		}

		/// <summary>
		/// Write the given log entry
		/// </summary>
		/// <param name="consoleModel">The console model</param>
		/// <param name="consoleEntryModel">The entry model</param>
		/// <param name="addSpace">The add space flag</param>
		/// <param name="newLine">The new line flag</param>
		/// <param name="defaultColor">The default console color</param>
		/// <param name="defaultBackgroundColor">The default background console color</param>
		protected virtual void WriteLogEntry(ConsoleLogModel consoleModel, ConsoleLogEntryModel consoleEntryModel, bool addSpace, bool newLine, ConsoleColor defaultColor, ConsoleColor defaultBackgroundColor)
		{
			// Check from entry
			if (consoleModel == null || consoleEntryModel == null)
				return;

			// Check content
			if (consoleModel.ShowDateTime || !addSpace)
			{
				// Write additional space
				this.WriteToConsole(this.DefaultSpace, defaultColor, defaultBackgroundColor, false, ConsoleLoggerTransformSourceKind.ENTRY);
			}

			// Write log
			this.WriteToConsole(consoleEntryModel.Message, defaultColor, defaultBackgroundColor, newLine, ConsoleLoggerTransformSourceKind.ENTRY);
		}

		/// <summary>
		/// Write the log
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="logColor">The log color</param>
		/// <param name="logBackgroundColor">The log background color</param>
		/// <param name="newLine">The new line flag</param>
		/// <param name="sourceKind">The source kind</param>
		protected virtual void WriteToConsole(string text, ConsoleColor? logColor = null, ConsoleColor? logBackgroundColor = null, bool newLine = false, ConsoleLoggerTransformSourceKind sourceKind = ConsoleLoggerTransformSourceKind.UNDEFINED)
		{
			// Extract
			var color = logColor ?? this.GetCurrentConsoleForegroundColor();
			var backgroundColor = logBackgroundColor ?? this.GetCurrentConsoleBackgroundColor();

			// Initialize color
			var currentColor = this.GetCurrentConsoleForegroundColor();
			if (logColor.HasValue && logColor.Value != currentColor)
				this.SetConsoleForegroundColor(logColor.Value, sourceKind);

			// Initialize background color
			var currentBackgroundColor = this.GetCurrentConsoleBackgroundColor();
			if (logBackgroundColor.HasValue && logBackgroundColor.Value != currentBackgroundColor)
				this.SetConsoleBackgroundColor(logBackgroundColor.Value, sourceKind);

			// Raise event
			this.OnBeforeWriteConsole?.Invoke(this, new BeforeWriteConsoleEventArgs(this, text, color, backgroundColor, newLine));

			// Write text
			this.WriteTextToConsole(text, newLine);

			// Raise event
			this.OnAfterWriteConsole?.Invoke(this, new AfterWriteConsoleEventArgs(this, text, color, backgroundColor, newLine));
		}

		#endregion

		#region Colors

		/// <summary>
		/// Set the color property
		/// </summary>
		/// <param name="color">The color to set</param>
		/// <param name="sourceKind">The source kind</param>
		/// <returns>Returns the converted value</returns>
		protected virtual ConsoleColor SetConsoleForegroundColor(ConsoleColor color, ConsoleLoggerTransformSourceKind sourceKind = ConsoleLoggerTransformSourceKind.UNDEFINED)
		{
			// Transform value
			var transformedColor = this.TransformConsoleColor(color, false, sourceKind);

			// Set the given value for the console
			Console.ForegroundColor = transformedColor;

			// Return the converted value
			return transformedColor;
		}

		/// <summary>
		/// Set the color property
		/// </summary>
		/// <param name="color">The color to set</param>
		/// <param name="sourceKind">The source kind</param>
		/// <returns>Returns the converted value</returns>
		protected virtual ConsoleColor SetConsoleBackgroundColor(ConsoleColor color, ConsoleLoggerTransformSourceKind sourceKind = ConsoleLoggerTransformSourceKind.UNDEFINED)
		{
			// Transform value
			var transformedColor = this.TransformConsoleColor(color, true, sourceKind);

			// Set the given value for the console
			Console.BackgroundColor = transformedColor;

			// Return the converted value
			return transformedColor;
		}

		/// <summary>
		/// Transform the given color with converter
		/// </summary>
		/// <param name="color">The initial color</param>
		/// <param name="isBackground">The is background flag</param>
		/// <param name="sourceKind">The source kind</param>
		/// <returns>Returns the translated colors</returns>
		protected virtual ConsoleColor TransformConsoleColor(ConsoleColor color, bool isBackground, ConsoleLoggerTransformSourceKind sourceKind = ConsoleLoggerTransformSourceKind.UNDEFINED)
		{
			// Check converter
			if (this.ThemeConverter == null)
				return color;

			// Get model
			var transformModel = this.GetTransformModel(color, isBackground);

			// Return 
			return this.ThemeConverter.Transform(color, transformModel, sourceKind);
		}

		/// <summary>
		/// Returns the context model
		/// </summary>
		/// <param name="color">The initial color to transform</param>
		/// <param name="isBackground">The is background flag</param>
		/// <returns>Returns the final contextvarray</returns>
		protected virtual TransformConsoleColorModel GetTransformModel(ConsoleColor color, bool isBackground)
		{
			// Build new context array
			return new TransformConsoleColorModel(color, this, this.ForegroundColor, this.BackgroundColor,
				this.GetCurrentConsoleForegroundColor(), this.GetCurrentConsoleBackgroundColor(),
				isBackground);
		}

		/// <summary>
		/// Returns the console color
		/// </summary>
		/// <param name="level">The log level</param>
		/// <returns>Returns the console color</returns>
		protected virtual ConsoleColor? GetColorFromLogLevel(LogLevel level)
		{
			// Log level cases
			return level switch
			{
				// Debug
				LogLevel.DEBUG => (ConsoleColor?)this.DebugColor,

				// Info
				LogLevel.INFO => (ConsoleColor?)this.InfoColor,

				// Warning
				LogLevel.WARN => (ConsoleColor?)this.WarnColor,

				// Error
				LogLevel.ERROR => (ConsoleColor?)this.ErrorColor,

				// Fatal
				LogLevel.FATAL => (ConsoleColor?)this.FatalColor,

				// Default
				_ => null,
			};
		}

		/// <summary>
		/// Manage the theme change
		/// </summary>
		/// <returns>Returns true on change</returns>
		protected virtual bool ApplyConsoleColors(bool force = false)
		{
			// Check changes
			var isNewColor = this.GetCurrentConsoleForegroundColor() != this.ForegroundColor;
			var isNewBackground = this.GetCurrentConsoleBackgroundColor() != this.BackgroundColor;

			// Set colors
			if (isNewColor || force)
				this.SetConsoleForegroundColor(this.ForegroundColor, ConsoleLoggerTransformSourceKind.UNDEFINED);
			if (isNewBackground || force)
				this.SetConsoleBackgroundColor(this.BackgroundColor, ConsoleLoggerTransformSourceKind.UNDEFINED);

			// Returns true on change
			return isNewColor || isNewBackground || force;
		}

		/// <summary>
		/// Manage the theme change
		/// </summary>
		/// <param name="force">The force flag to reset console properties even if no change detected</param>
		protected virtual void ResetInitialConsoleColors(bool force = true)
		{
			// Set properties
			this.ForegroundColor = this.InitialColor;
			this.BackgroundColor = this.InitialBackgroundColor;

			// Apply properties
			this.ApplyConsoleColors(force);
		}

		/// <summary>
		/// Set the theme converter and apply
		/// </summary>
		/// <param name="converter">The theme converter</param>
		/// <param name="context">The context</param>
		protected virtual void SetThemeConverterAndApply(IThemeConverter<ConsoleColor> converter, object context = null)
		{
			// Set the theme
			this.themeConverter = converter;

			// Set colors and clear console on change
			this.ApplyConsoleColors(true);
		}

		#endregion

		#endregion
	}
}