using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Constants;
using dgMicMute.Enumerations;
using dgMicMute.Externals;
using dgMicMute.Interfaces;

namespace dgMicMute.Implementations
{
    /// <summary>
    /// Helper Implementation of the <see cref="IMMDevice"/>-Interface.
    /// </summary>
    public class MMDevice
    {
        private readonly IMMDevice _device;
        private AudioEndpointVolume _audioEndpointVolume;

        private static readonly Guid IID_IAudioEndpointVolume = typeof (IAudioEndpointVolume).GUID;

        /// <summary>
        /// Implementation of the <see cref="IAudioEndpointVolume"/>-Interface for this device.
        /// </summary>
        public AudioEndpointVolume AudioEndpointVolume
        {
            get
            {
                if (_audioEndpointVolume == null)
                    GetAudioEndpointVolume();

                return _audioEndpointVolume;
            }
        }

        /// <summary>
        /// The Id of this Device.
        /// </summary>
        public string Id
        {
            get
            {
                string result;
                Marshal.ThrowExceptionForHR(_device.GetId(out result));
                return result;
            }
        }

        /// <summary>
        /// Describes the DataFlow for this device.
        /// </summary>
        public EDataFlow DataFlow
        {
            get
            {
                EDataFlow result;
                IMMEndpoint endpoint = _device as IMMEndpoint;
                endpoint.GetDataFlow(out result);
                return result;
            }
        }

        /// <summary>
        /// Describes the State of this Device
        /// </summary>
        public EDeviceState DeviceState
        {
            get
            {
                EDeviceState result;
                Marshal.ThrowExceptionForHR(_device.GetState(out result));
                return result;
            }
        }

        public MMDevice(IMMDevice device)
        {
            _device = device;
        }

        private void GetAudioEndpointVolume()
        {
            object result;
            Marshal.ThrowExceptionForHR(_device.Activate(IID_IAudioEndpointVolume, ClsCtx.All, IntPtr.Zero, out result));
            _audioEndpointVolume = new AudioEndpointVolume(result as IAudioEndpointVolume);
        }
    }
}
