using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace dgMicMute
{
    public delegate void SettingsChanged(string propertyName);

    [Serializable]
    public static class Settings
    {
        /// <summary>
        /// Ugly hack.
        /// </summary>
        public static bool _startWithWindows;
        public static bool _isMuted;
        public static bool _isForced;

        public static event SettingsChanged OnSettingsChanged = delegate { };

        public static bool StartWithWindows
        {
            get
            {
                return _startWithWindows;
            }
            set
            {
                _startWithWindows = value;
                ToggleAutostart();
                OnSettingsChanged("StartWithWindows");
            }
        }

        public static bool IsMuted
        {
            get
            {
                return _isMuted;
            }
            set
            {
                _isMuted = value;
            }
        }

        public static bool IsForced
        {
            get
            {
                return _isForced;
            }
            set
            {
                _isForced = value;
            }
        }

        private static bool IsStartupItem()
        {
            // The path to the key where Windows looks for startup applications
            using (RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (rkApp.GetValue("dgMicMute") == null)
                    // The value doesn't exist, the application is not set to run at startup
                    return false;
                else
                    // The value exists, the application is set to run at startup
                    return true;
            }
        }

        private static void ToggleAutostart()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (StartWithWindows)
            {
                try
                {
                    // Add the value in the registry so that the application runs at startup
                    rkApp.SetValue("dgMicMute", System.Windows.Forms.Application.ExecutablePath.ToString());
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    // Remove the value from the registry so that the application doesn't start
                    rkApp.DeleteValue("dgMicMute", false);
                }
                catch
                {

                }
            }
        }
    }

    public class SerializeStatic
    {
        public static bool Save(Type static_class, string path = "")
        {
            try
            {
                if (String.IsNullOrWhiteSpace(path))
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dgMicMute\",
                        "settings.xml");

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                FieldInfo[] fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);
                object[,] a = new object[fields.Length, 2];
                int i = 0;
                foreach (FieldInfo field in fields)
                {
                    a[i, 0] = field.Name;
                    a[i, 1] = field.GetValue(null);
                    i++;
                };
                Stream f = File.Open(path, FileMode.Create);
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(f, a);
                f.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Load(Type static_class, string path = "")
        {
            try
            {
                if (String.IsNullOrWhiteSpace(path))
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dgMicMute\",
                        "settings.xml");

                if (!File.Exists(path)) return false;

                FieldInfo[] fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);
                object[,] a;
                Stream f = File.Open(path, FileMode.Open);
                SoapFormatter formatter = new SoapFormatter();
                a = formatter.Deserialize(f) as object[,];
                f.Close();
                if (a.GetLength(0) != fields.Length) return false;
                int i = 0;
                foreach (FieldInfo field in fields)
                {
                    if (field.Name == (a[i, 0] as string))
                    {
                        field.SetValue(null, a[i, 1]);
                    }
                    i++;
                };
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
