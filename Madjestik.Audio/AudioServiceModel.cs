using Madjestik.Core.Extensions;
using System;

namespace Madjestik.Audio
{
	/// <summary>
	/// The audio service model
	/// </summary>
	public class AudioServiceModel
	{
		#region Properties

		/// <summary>
		/// The current capture date
		/// </summary>
		public DateTime CaptureDate { get; set; } = DateTime.Now;

		/// <summary>
		/// The current volume
		/// </summary>
		public float Volume { get; set; }

		/// <summary>
		/// The current peak value
		/// </summary>
		public float PeakValue { get; set; }

		/// <summary>
		/// The mute status
		/// </summary>
		public bool IsMute { get; set; }

		/// <summary>
		/// The is playing status
		/// </summary>
		public bool IsPlaying { get; set; }

		#endregion

		#region Extension

		/// <summary>
		/// The current volume percent
		/// </summary>
		public int VolumePercent
		{
			get
			{
				// Round using extension
				return this.Volume.GetRoundedPercent();
			}
		}

		/// <summary>
		/// The audible status
		/// </summary>
		public bool IsAudible
		{
			get
			{
				// Check from model properties
				return this.IsPlaying && !this.IsMute && this.Volume > 0f;
			}
		}

		#endregion
	}
}