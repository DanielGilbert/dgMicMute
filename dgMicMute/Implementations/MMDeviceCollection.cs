using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Interfaces;

namespace dgMicMute.Implementations
{
    public class MMDeviceCollection : IEnumerable<MMDevice>
    {
        private readonly IMMDeviceCollection _deviceCollection;
        private readonly List<MMDevice> _devicesList; 

        public int Count
        {
            get
            {
                uint result;
                Marshal.ThrowExceptionForHR(_deviceCollection.GetCount(out result));
                return (int) result;
            }
        }

        public MMDevice this[int index]
        {
            get
            {
                IMMDevice result;
                _deviceCollection.Item((uint) index, out result);
                return new MMDevice(result);
            }
        }

        public MMDeviceCollection(IMMDeviceCollection deviceCollection)
        {
            _deviceCollection = deviceCollection;

            _devicesList = new List<MMDevice>();
            for (int i = 0; i < Count; i++)
            {
                _devicesList.Add(this[i]);
            }
        }

        public IEnumerator<MMDevice> GetEnumerator()
        {
            return _devicesList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}