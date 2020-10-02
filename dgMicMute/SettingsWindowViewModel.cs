using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dgMicMute.MvvmHelper;
using System.Windows.Input;
using dgMicMute.Patterns;

namespace dgMicMute
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private List<string> _modifiersList = new List<string>{"None", "Control", "Alt", "Shift"};
        private List<string> _keysList = new List<string>(KeyMapper.AvailableKeys.Keys);

        private string _firstModifier;
        private string _secondModifier;
        private string _selectedKey;

        private bool _usesHotkeys;

        public ICommand CloseSettingsCommand { get; set; }

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
                return Settings.StartWithWindows;
            }
            set
            {
                Settings.StartWithWindows = value;
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
            CloseSettingsCommand = new RelayCommand(CloseSettings);
            _usesHotkeys = Settings.UsesHotkey;
            _firstModifier = Settings.FirstModifier;
            _secondModifier = Settings.SecondModifier;
            _selectedKey = Settings.SelectedKey;
        }

        private void CloseSettings(object obj)
        {
            Mediator.Instance.NotifyColleagues(MediatorMessages.CloseSettings, null);
            Settings.ConfigureHotkey(_usesHotkeys, _firstModifier, _secondModifier, _selectedKey);
        }
    }
}
