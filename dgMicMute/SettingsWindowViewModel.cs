using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dgMicMute.MvvmHelper;

namespace dgMicMute
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private List<string> _modifiersList = new List<string>{"None", "Ctrl", "Alt", "Shift"};
        private List<string> _keysList = new List<string>();

        private string _firstModifier;
        private string _secondModifier;
        private string _selectedKey;

        private bool _usesHotkeys;
        private bool _startsWithWindows;

        public List<string> ModifiersList
        {
            get
            {
                return _modifiersList;
            }
            set
            {
                _modifiersList = value;
                OnPropertyChanged("ModifiersList");
            }
        }

        public List<string> KeysList
        {
            get
            {
                return _keysList;
            }
            set
            {
                _keysList = value;
                OnPropertyChanged("KeysList");
            }
        }

        public string FirstModifier
        {
            get
            {
                return _firstModifier;
            }
            set
            {
                _firstModifier = value;
                OnPropertyChanged("FirstModifier");
            }
        }

        public string SecondModifier
        {
            get
            {
                return _secondModifier;
            }
            set
            {
                _secondModifier = value;
                OnPropertyChanged("SecondModifier");
            }
        }

        public string SelectedKey
        {
            get
            {
                return _selectedKey;
            }
            set
            {
                _selectedKey = value;
                OnPropertyChanged("SelectedKey");
            }
        }

        public bool StartsWithWindows
        {
            get
            {
                return _startsWithWindows;
            }
            set
            {
                _startsWithWindows = value;
                OnPropertyChanged("StartsWithWindows");
            }
        }

        public bool UsesHotkeys
        {
            get
            {
                return _usesHotkeys;
            }
            set
            {
                _usesHotkeys = value;
                OnPropertyChanged("UsesHotkeys");
            }
        }

        public SettingsWindowViewModel()
        {
            FirstModifier = ModifiersList[0];
            SecondModifier = ModifiersList[0];

            KeysList.AddRange(new List<string>
            {
                "None",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z",
		        "F1",
                "F2",
                "F3",
                "F4",
                "F5",
                "F6",
                "F7",
                "F8",
                "F9",
                "F10",
                "F11",
                "F12",
                "F13",
                "F14",
                "F15",
                "F16",
                "F17",
                "F18",
                "F19",
                "F20",
                "F21",
                "F22",
                "F23",
                "F24",
		        "BrowserBack",
                "BrowserForward",
                "BrowserRefresh",
                "BrowserStop",
                "BrowserSearch",
                "BrowserFavorites",
                "BrowserHome",
		        "VolumeMute",
                "VolumeDown",
                "VolumeUp",
		        "NumPad0",
                "NumPad1",
                "NumPad2",
                "NumPad3",
                "NumPad4",
                "NumPad5",
                "NumPad6",
                "NumPad7",
                "NumPad8",
                "NumPad9",
		        "PageUp",
                "PageDown",
                "End",
                "Home",
		        "Print"
            });

            SelectedKey = KeysList[0];
        }
    }
}
