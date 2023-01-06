using System;
using System.Runtime.InteropServices;

namespace Madjestik.Audio
{
	/// <summary>
	/// The audio service class
	/// </summary>
	public class AudioService : IAudioService
	{
		#region Constants

		/// <summary>
		/// The minimal audible peak value
		/// </summary>
		public const float MINIMAL_AUDIBLE_PEAK_VALUE = 1E-08f;

		#endregion

		#region Static

		/// <summary>
		/// Volume accesseur
		/// </summary>
		protected static float Volume
		{
			get
			{
				// Set new device endpoint
				using var device = new DeviceEndpointRequest();
				Marshal.ThrowExceptionForHR(device.GetEndPoint<IAudioEndpointVolume>().GetMasterVolumeLevelScalar(out float v));

				// Return the output
				return v;
			}
			set
			{
				// Set new device endpoint
				using var device = new DeviceEndpointRequest();
				Marshal.ThrowExceptionForHR(device.GetEndPoint<IAudioEndpointVolume>().SetMasterVolumeLevelScalar(value, Guid.Empty));
			}
		}

		/// <summary>
		/// Returns the peak value
		/// </summary>
		protected static float PeakValue
		{
			get
			{
				// Set new device endpoint
				using var device = new DeviceEndpointRequest();

				// Return the peak value
				return device.GetEndPoint<IAudioEndpointMeterInformation>().GetPeakValue();
			}
		}

		/// <summary>
		/// Mute accesseur
		/// </summary>
		protected static bool IsMute
		{
			get
			{
				// Set new device endpoint
				using var device = new DeviceEndpointRequest();
				Marshal.ThrowExceptionForHR(device.GetEndPoint<IAudioEndpointVolume>().GetMute(out bool mute));

				// Return output
				return mute;
			}
			set
			{
				// Set new device endpoint
				using var device = new DeviceEndpointRequest();
				Marshal.ThrowExceptionForHR(device.GetEndPoint<IAudioEndpointVolume>().SetMute(value, Guid.Empty));
			}
		}

		/// <summary>
		/// Returns true if sound is played
		/// </summary>
		protected static bool IsPlaying
		{
			get
			{
				// This is a bit tricky. 0 is the official "no sound" value
				// but for example, if you open a video and plays/stops with it (w/o killing the app/window/stream),
				// the value will not be zero, but something really small (around 1E-09)
				// so, depending on your context, it is up to you to decide
				// if you want to test for 0 or for a small value
				return AudioService.PeakValue > AudioService.MINIMAL_AUDIBLE_PEAK_VALUE;
			}
		}

		/// <summary>
		/// Returns an audio model with all information
		/// </summary>
		/// <returns>Returns a model object with all audio info</returns>
		protected static AudioServiceModel StaticGetData()
		{
			// Build the new model
			return new AudioServiceModel
			{
				Volume = AudioService.Volume,
				PeakValue = AudioService.PeakValue,
				IsMute = AudioService.IsMute,
				IsPlaying = AudioService.IsPlaying
			};
		}

		#endregion

		#region Public

		/// <summary>
		/// Returns the current main volume
		/// </summary>
		/// <returns>Returns the float volume</returns>
		public virtual float GetVolume()
		{
			// Return the current volume
			return AudioService.Volume;
		}

		/// <summary>
		/// Set the current volume
		/// </summary>
		/// <returns>Returns a model object with all audio info</returns>
		public virtual void SetVolume(float volume)
		{
			// Return the current volume
			AudioService.Volume = volume;
		}

		/// <summary>
		/// Returns an audio model with all information
		/// </summary>
		/// <returns>Returns a model object with all audio info</returns>
		public virtual AudioServiceModel GetData()
		{
			// Build the new model
			return AudioService.StaticGetData();
		}

		#endregion
	}
}