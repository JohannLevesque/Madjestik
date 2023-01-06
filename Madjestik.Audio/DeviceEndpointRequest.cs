using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Madjestik.Audio
{
	/// <summary>
	/// The device endpoint
	/// </summary>
	public class DeviceEndpointRequest : IDisposable
	{
		#region Attributes

		/// <summary>
		/// The device input
		/// </summary>
		public readonly IMMDevice DeviceOutput = null;

		/// <summary>
		/// List of activated objects
		/// </summary>
		public readonly List<object> ActivatedEndpoints = new List<object>();

		#endregion

		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		public DeviceEndpointRequest(EDataFlow dataFlow = EDataFlow.eRender, ERole role = ERole.eMultimedia)
		{
			// Extract device from device enumerator
			this.DeviceOutput = (new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator).GetDefaultAudioEndpoint(dataFlow, role);
		}

		#endregion

		#region Public

		/// <summary>
		/// Initialize the end points
		/// </summary>
		public T GetEndPoint<T>()
		{
			// Build audio enpoints from interface
			try
			{
				// Activate
				var activated = this.DeviceOutput.Activate<T>();

				// Store it to dispose it later
				this.ActivatedEndpoints.Add(activated);

				// Return the activated object
				return activated;
			}
			catch (Exception ex)
			{
				// Throw custom error
				throw new InvalidOperationException("Can not build the sound controller", ex);
			}
		}

		#endregion

		#region Dispose

		/// <summary>
		/// Dispose method
		/// </summary>
		public void Dispose()
		{
			// Release all endpoints
			foreach (var activated in this.ActivatedEndpoints)
				Marshal.ReleaseComObject(activated);

			// Release the COM object
			if (this.DeviceOutput != null)
				Marshal.ReleaseComObject(this.DeviceOutput);
		}

		#endregion
	}
}