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
        private bool _isMuted;
        private bool _isForced;
        private DgMic _mic;

        public bool IsMuted
        {
            get
            {
                return _isMuted;
            }
            set
            {
                _isMuted = value;
                IconPath = IsMuted ? @"res\microphone_muted.ico" : @"res\microphone_unmuted.ico";
                _mic.SetMicStateTo(IsMuted ? DgMicStates.Muted : DgMicStates.Unmuted);
                OnPropertyChanged("IsMuted");
            }
        }

        public bool IsForced
        {
            get
            {
                return _isForced;
            }
            set
            {
                _isForced = value;
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

        public NotifyIconViewModel()
        {
            IconPath = @"res\microphone_unmuted.ico";
            _mic = new DgMic();
            _mic.OnVolumeNotification += _mic_OnVolumeNotification;
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            ForkOnGitHubCommand = new RelayCommand(ForkOnGithub);
        }

        private void ForkOnGithub(object obj)
        {
            Process.Start("https://github.com/DanielGilbert/dgMicMute");
        }

        private void _mic_OnVolumeNotification(Implementations.AudioVolumeNotificationData data)
        {
            //Everything hat happens in the callback,
            //must be done in a non-blocking way.
            //Therefore, we need to invoke a new thread via the dispatcher
            if (IsForced)
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
