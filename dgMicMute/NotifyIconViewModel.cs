using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using dgMicMute.MvvmHelper;

namespace dgMicMute
{
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        private string _iconPath;
        private DgMic _mic;

        public bool IsMuted
        {
            get
            {
                IconPath = Settings.IsMuted ? @"res\microphone_muted.ico" : @"res\microphone_unmuted.ico";
                _mic.SetMicStateTo(Settings.IsMuted ? DgMicStates.Muted : DgMicStates.Unmuted);
                return Settings.IsMuted;
            }
            set
            {
                Settings.IsMuted = value;
                IconPath = IsMuted ? @"res\microphone_muted.ico" : @"res\microphone_unmuted.ico";
                _mic.SetMicStateTo(IsMuted ? DgMicStates.Muted : DgMicStates.Unmuted);
                OnPropertyChanged("IsMuted");
            }
        }

        public bool IsForced
        {
            get
            {
                return Settings.IsForced;
            }
            set
            {
                Settings.IsForced = value;
                OnPropertyChanged("IsForced");
            }
        }

        public string IconPath
        {
            get
            {
                return _iconPath;
            }
            set
            {
                _iconPath = value;
                OnPropertyChanged("IconPath");
            }
        }

        public ICommand ExitApplicationCommand { get; set; }
        public ICommand ForkOnGitHubCommand { get; set; }
        public ICommand OpenSettingsWindowCommand { get; set; }

        public NotifyIconViewModel()
        {
            IconPath = @"res\microphone_unmuted.ico";
            _mic = new DgMic();
            _mic.OnVolumeNotification += _mic_OnVolumeNotification;
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            ForkOnGitHubCommand = new RelayCommand(ForkOnGithub);
            OpenSettingsWindowCommand = new RelayCommand(OpenSettingsWindow);
        }

        private void OpenSettingsWindow(object obj)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void ForkOnGithub(object obj)
        {
            Process.Start("https://github.com/DanielGilbert/dgMicMute");
        }

        private void _mic_OnVolumeNotification(Implementations.AudioVolumeNotificationData data)
        {
            if (IsForced)
                //Everything that happens in the callback,
                //must be done in a non-blocking way.
                //Therefore, we need to invoke a new thread via the dispatcher,
                //because we cannot simply get information from the interface while
                //we are handling the callback.
                Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                {
                    IsMuted = IsMuted;
                }));
            else
                IsMuted = data.Muted;
        }

        private void ExitApplication(object obj)
        {
            _mic.SetMicStateTo(DgMicStates.Unmuted);
            Application.Current.Shutdown();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
