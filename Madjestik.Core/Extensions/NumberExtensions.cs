using System;

namespace Madjestik.Core.Extensions
{
	/// <summary>
	/// The float extensions
	/// </summary>
	public static class IntExtensions
	{
		#region Public

		#region Percents

		/// <summary>
		/// Returns the percentage
		/// </summary>
		/// <param name="number">The number to convert</param>
		/// <returns>Returns the float number</returns>
		public static float GetFloatPercent(this int number)
		{
			// Return the rounded number
			return (float)number / 100f;
		}

		/// <summary>
		/// Returns the limit
		/// </summary>
		/// <param name="number">The number to limit</param>
		/// <param name="minValue">The min value</param>
		/// <param name="maxValue">The max value</param>
		/// <returns>Returns the rounded number</returns>
		public static int ApplyPercentLimits(this int number, int minValue = 0, int maxValue = 100)
		{
			// Return limit management
			return number.ApplyLimits(minValue, maxValue);
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
		public static int ApplyLimits(this int number, int minValue, int maxValue)
		{
			// Return limit management
			return Math.Min(Math.Max(number, minValue), maxValue);
		}

		#endregion

		#endregion
	}
}