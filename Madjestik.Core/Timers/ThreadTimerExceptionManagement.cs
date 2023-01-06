using System;
using System.Collections.Generic;
using System.Text;

namespace Madjestik.Core.Timers
{
	/// <summary>
	/// The enum for thread timer exception managemebt
	/// </summary>
	public enum ThreadTimerExceptionManagement
	{
		/// <summary>
		/// The timer will throw the exception
		/// </summary>
		THROW_EXCEPTION = 0,

		/// <summary>
		/// The do nothing option
		/// </summary>
		DO_NOTHING = 1,

		/// <summary>
		/// The application will exit on exception
		/// </summary>
		EXIT = 2
	}
}