using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace dgMicMute
{
    public delegate void SettingsChanged(string propertyName);

    [Serializable]
    public class Settings
    {
        private bool _startWithWindows;
        private bool _useHotkey;

        private ModifierKeys _firstModifier;
        private ModifierKeys _secondModifier;
        private Keys _selectedKey;

        public event SettingsChanged OnSettingsChanged = delegate { };

        public bool StartWithWindows
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

        public bool UseHotkey
        {
            get
            {
                return _useHotkey;
            }
            set
            {
                _useHotkey = value;
                ToggleHotkey();
                OnSettingsChanged("UseHotkey");
            }
        }

        private void ToggleHotkey()
        {

        }

        public ModifierKeys FirstModifier
        {
            get
            {
                return _firstModifier;
            }
            set
            {
                _firstModifier = value;
                OnSettingsChanged("FirstModifier");
            }
        }

        public ModifierKeys SecondModifier
        {
            get
            {
                return _secondModifier;
            }
            set
            {
                _secondModifier = value;
                OnSettingsChanged("SecondModifier");
            }
        }

        public Keys SelectedKey
        {
            get
            {
                return _selectedKey;
            }
            set
            {
                _selectedKey = value;
                OnSettingsChanged("SelectedKey");
            }
        }

        private void ToggleAutostart()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the current settings
        /// </summary>
        /// <param name="path"></param>
        public static Settings Load(string path = "")
        {
            Settings settingsClass = new Settings();

            if (String.IsNullOrWhiteSpace(path))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dgmicmute\",
                    "settings.xml");

            if (!File.Exists(path)) return settingsClass;

            try
            {
                var serializer = new XmlSerializer(typeof(Settings));
                using (var reader = XmlReader.Create(path))
                {
                    settingsClass = (Settings)serializer.Deserialize(reader);
                }
            }
            catch
            {
                return settingsClass;
            }

            return settingsClass;
        }

        /// <summary>
        /// Saves the current settings.
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path = "")
        {
            if (String.IsNullOrWhiteSpace(path))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dgmicmute\",
                    "settings.xml");

            try
            {
                var serializer = new XmlSerializer(this.GetType());
                using (var writer = XmlWriter.Create(path))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch
            {
            }
        }
    }
}
