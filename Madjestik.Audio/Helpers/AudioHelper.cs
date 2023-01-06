using System.Linq;

namespace Madjestik.Audio.Helpers
{
	/// <summary>
	/// The audio helper class
	/// </summary>
	public static class AudioHelper
	{
		#region Public

		/// <summary>
		/// Returns the RMS
		/// </summary>
		/// <param name="peakValues">The RMS</param>
		/// <returns>Returns the RMS average</returns>
		public static float? GetRMS(float[] peakValues)
		{
			// Check from entry
			if (peakValues == null)
				return null;

			// Return average
			return peakValues.Any() ? peakValues.Average() : (float?)null;
		}

		/// <summary>
		/// Returns the dynamic range
		/// </summary>
		/// <param name="peakValues">The RMS</param>
		/// <returns>Returns the RMS average</returns>
		public static float? GetDynamicRange(float[] peakValues)
		{
			// Check from entry
			if (peakValues == null)
				return null;

			// Return average
			return peakValues.Any() ? peakValues.Max() : (float?)null;
		}

		/// <summary>
		/// Returns the peak average
		/// </summary>
		/// <param name="rms">The RMS</param>
		/// <param name="dynamicRange">The range</param>
		/// <returns>Returns the peak average</returns>
		public static float? GetPeakAverage(float? rms, float? dynamicRange)
		{
			// Check for RMS
			if (!rms.HasValue || !dynamicRange.HasValue)
				return null;

			// Return the average between rms and dynamic range
			return (rms.Value + dynamicRange.Value) / 2;
		}

		/// <summary>
		/// Returns the peak average
		/// </summary>
		/// <param name="peakValues">The peak values</param>
		/// <returns>Returns the peak average</returns>
		public static float? GetPeakAverage(float[] peakValues)
		{
			// Check for RMS
			if (peakValues == null)
				return null;

			// Extract values
			var rms = AudioHelper.GetRMS(peakValues);
			var dynamicRange = AudioHelper.GetDynamicRange(peakValues);

			// Return the average between RMS and dynamic range
			return AudioHelper.GetPeakAverage(rms, dynamicRange);
		}

		#endregion
	}
}