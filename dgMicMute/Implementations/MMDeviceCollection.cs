using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using dgMicMute.Interfaces;

namespace dgMicMute.Implementations
{
    /// <summary>
    /// Helper Class for the <see cref="IMMDeviceCollection"/>-Interface.
    /// Implements the IEnumerable-Interface, so it can be used in LINQ (Yeah!).
    /// </summary>
    public class MMDeviceCollection : IEnumerable<MMDevice>
    {
        private readonly IMMDeviceCollection _deviceCollection;
        private readonly List<MMDevice> _devicesList; 

        /// <summary>
        /// Number of devices in this Collection.
        /// </summary>
        public int Count
        {
            get
            {
                uint result;
                Marshal.ThrowExceptionForHR(_deviceCollection.GetCount(out result));
                return (int) result;
            }
        }

        /// <summary>
        /// Grants access to a certain device, based on the supplied index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MMDevice this[int index]
        {
            get
            {
                if (index - 1 > Count)
                    throw new IndexOutOfRangeException("");
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