using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Madjestik.Audio.Helpers
{
	/// <summary>
	/// Helper class for programs
	/// </summary>
	public static class ProgramHelper
	{
		#region Constants

		/// <summary>
		/// The mutex program key
		/// </summary>
		public const string MUTEX_PROGRAM_KEY_FORMAT = "Global\\{0}";

		#endregion

		#region Public

		/// <summary>
		/// Returns the program ID from assembly attribute
		/// </summary>
		/// <returns>Returns the GUID of the entry assembly</returns>
		public static string GetProgramId()
		{
			// Extract the GUID of the current program
			var programID = Assembly.GetEntryAssembly().GetCustomAttribute<GuidAttribute>()?.Value?.ToLower();
			if (string.IsNullOrEmpty(programID))
				throw new InvalidOperationException("Can not determine the GUID of the program");

			// Return the program ID
			return programID;
		}

		/// <summary>
		/// Returns the mutex key
		/// </summary>
		/// <returns>Returns the mutex key</returns>
		public static string GetMutexKeyFromProgramId()
		{
			// Return the program ID key
			return string.Format(ProgramHelper.MUTEX_PROGRAM_KEY_FORMAT, ProgramHelper.GetProgramId());
		}

		/// <summary>
		/// Returns an instance of the mutex from the program key
		/// </summary>
		/// <returns>Returns the mutex instance</returns>
		public static Mutex GetMutexFromProgramId()
		{
			// Return a new instance of the mutex from the program key
			return new Mutex(false, ProgramHelper.GetMutexKeyFromProgramId());
		}

		/// <summary>
		/// Returns an instance of the mutex from the program key
		/// </summary>
		/// <returns>Returns the mutex</returns>
		public static Mutex AssertMutexFromProgramId(string exceptionToRaise)
		{
			// Check existing
			if (string.IsNullOrEmpty(exceptionToRaise))
				return ProgramHelper.AssertMutexFromProgramId((Exception)null);
			else
				return ProgramHelper.AssertMutexFromProgramId(new InvalidOperationException(exceptionToRaise));
		}

		/// <summary>
		/// Returns an instance of the mutex from the program key
		/// </summary>
		/// <returns>Returns the mutex</returns>
		public static Mutex AssertMutexFromProgramId(Exception exceptionToRaise)
		{
			// Check existing
			Mutex mutex = ProgramHelper.GetMutexFromProgramId();

			// Check already existing
			if (!mutex.WaitOne(0, false))
			{
				// Throw exception
				throw exceptionToRaise ?? new InvalidOperationException($"The program with the same ID ({ProgramHelper.GetProgramId()}) is already running");
			}

			// Return the mutex
			return mutex;
		}

		#endregion
	}
}