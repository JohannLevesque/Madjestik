using System;
using System.Runtime.InteropServices;

namespace Madjestik.Audio
{
	#region Enums

	/// <summary>
	/// Device State
	/// </summary>
	[Flags]
	public enum DeviceState
	{
		/// <summary>
		/// DEVICE_STATE_ACTIVE
		/// </summary>
		Active = 0x00000001,

		/// <summary>
		/// DEVICE_STATE_DISABLED
		/// </summary>
		Disabled = 0x00000002,

		/// <summary>
		/// DEVICE_STATE_NOTPRESENT 
		/// </summary>
		NotPresent = 0x00000004,

		/// <summary>
		/// DEVICE_STATE_UNPLUGGED
		/// </summary>
		Unplugged = 0x00000008,

		/// <summary>
		/// DEVICE_STATEMASK_ALL
		/// </summary>
		All = 0x0000000F
	}

	/// <summary>
	/// Private data flow enum
	/// </summary>
	public enum EDataFlow
	{
		eRender,
		eCapture,
		eAll,
	}

	/// <summary>
	/// Private role enum
	/// </summary>
	public enum ERole
	{
		eConsole,
		eMultimedia,
		eCommunications,
	}

	#endregion

	#region COM

	#region COM Implementations

	[ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
	public class MMDeviceEnumeratorComObject { }

	[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMMDeviceEnumerator
	{
		/// <summary>
		/// Do not remove
		/// </summary>
		void NotNeeded();

		/// <summary>
		/// Returns the default audio endpoints
		/// </summary>
		/// <param name="dataFlow">The dataflow</param>
		/// <param name="role">The role</param>
		/// <returns>Returns the audio endpoint</returns>
		IMMDevice GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role);
	}

	[Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMMDevice
	{
		#region Implementation

		/// <summary>
		/// Activate for custom types
		/// </summary>
		/// <param name="iid">The ID to activate</param>
		/// <param name="dwClsCtx">The CLS context</param>
		/// <param name="pActivationParams">The activation parameters</param>
		/// <returns>Returns the endpoint handler</returns>
		[return: MarshalAs(UnmanagedType.IUnknown)]
		public object Activate([MarshalAs(UnmanagedType.LPStruct)] Guid iid, int dwClsCtx, IntPtr pActivationParams);

		#endregion

		#region Utils

		/// <summary>
		/// Activate for custom types
		/// </summary>
		/// <returns>Returns the endpoint handler</returns>
		public T Activate<T>()
		{
			// Internal
			return (T)Activate(typeof(T).GUID, 0, IntPtr.Zero);
		}

		#endregion
	}

	#endregion

	#region COM Endpoints

	[Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAudioEndpointVolume
	{
		// f(), g(), ... are unused COM method slots. Define these if you care
		protected int F(); protected int G(); protected int Hh(); protected int I();

		/// <summary>
		/// Set the master volume
		/// </summary>
		/// <param name="fLevel">The float volume level</param>
		/// <param name="pguidEventContext">The pointer id events context</param>
		/// <returns>Returns the execution code</returns>
		public int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

		/// <summary>
		/// Unused but necessary for override...
		/// </summary>
		protected int J();

		/// <summary>
		/// Returns the current volume
		/// </summary>
		/// <param name="pfLevel">The output sound level</param>
		/// <returns>Returns the output level</returns>
		public int GetMasterVolumeLevelScalar(out float pfLevel);

		/// <summary>
		/// Unused but necessary for override...
		/// </summary>
		protected int K(); protected int L(); protected int M(); protected int N();

		/// <summary>
		/// Set the current sound system as mute
		/// </summary>
		/// <param name="bMute">The output flag</param>
		/// <param name="pguidEventContext">The pointer id events context</param>
		/// <returns>Returns the execution code</returns>
		public int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);

		/// <summary>
		/// Returns true into output if mute
		/// </summary>
		/// <param name="pbMute">The output mute</param>
		/// <returns>Returns the execution code</returns>
		public int GetMute(out bool pbMute);
	}

	[Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAudioEndpointMeterInformation
	{
		/// <summary>
		/// Returns the peak value
		/// </summary>
		/// <pa>Returns the peak value</returns>
		/// <returns>Returns the memory result</returns>
		public float GetPeakValue();
	}

	#endregion

	#endregion
}