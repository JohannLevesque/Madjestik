using System;

namespace Madjestik.Core.Extensions
{
	/// <summary>
	/// The float extensions
	/// </summary>
	public static class FloatExtensions
	{
		#region Constants

		/// <summary>
		/// The default percent digits
		/// </summary>
		public const int DEFAULT_PERCENT_DIGITS = 2;

		#endregion

		#region Public

		#region Percents

		/// <summary>
		/// Returns the percentage
		/// </summary>
		/// <param name="number">The number to round</param>
		/// <param name="digit">The optional digit to round</param>
		/// <returns>Returns the rounded number</returns>
		public static int GetRoundedPercent(this float number, int digit = FloatExtensions.DEFAULT_PERCENT_DIGITS)
		{
			// Return the rounded number
			return (int)Math.Round(number * 100f, digit);
		}

		/// <summary>
		/// Returns the percentage
		/// </summary>
		/// <param name="number">The number to limit</param>
		/// <param name="minValue">The min value</param>
		/// <param name="maxValue">The max value</param>
		/// <param name="digit">The optional digit to round</param>
		/// <returns>Returns the rounded number</returns>
		public static float ApplyLimitsPercent(this float number, float minValue = 0f, float maxValue = 100f, int digit = FloatExtensions.DEFAULT_PERCENT_DIGITS)
		{
			// Return the rounded number
			return (float)Math.Round(number.ApplyLimits(minValue, maxValue), digit);
		}

		#endregion

		#region Limits

		/// <summary>
		/// Returns the limit
		/// </summary>
		/// <param name="number">The number to limit</param>
		/// <param name="minValue">The min value</param>
		/// <param name="maxValue">The max value</param>
		/// <returns>Returns the rounded number</returns>
		public static float ApplyLimits(this float number, float minValue, float maxValue)
		{
			// Return limit management
			return Math.Min(Math.Max(number, minValue), maxValue);
		}

		#endregion

		#endregion
	}
}