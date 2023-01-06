using Madjestik.Core.Extensions;
using System;

namespace Madjestik.Audio
{
	/// <summary>
	/// The smart audio manager model
	/// </summary>
	public class SmartAudioManagerWatchModel
	{
		public float? TOREMOVE { get; set; } // TODO: remove this temp property
		#region Properties

		/// <summary>
		/// The first capture date
		/// </summary>
		public DateTime StartWatchDate { get; set; } = DateTime.Now;

		/// <summary>
		/// The audio model
		/// </summary>
		public AudioServiceModel AudioModel { get; set; }

		/// <summary>
		/// The last model checked
		/// </summary>
		public SmartAudioManagerWatchModel LastModelChecked { get; set; }

		/// <summary>
		/// The volume set by user
		/// </summary>
		public float? VolumeSetByUser { get; set; }

		#endregion

		#region Extensions

		/// <summary>
		/// The volume set by user in percent
		/// </summary>
		public int? VolumeSetByUserPercent
		{
			get
			{
				// Small shortcut
				return this.VolumeSetByUser.HasValue ? (int?)this.VolumeSetByUser.Value.GetRoundedPercent() : (int?)null;
			}
		}

		#endregion
	}
}