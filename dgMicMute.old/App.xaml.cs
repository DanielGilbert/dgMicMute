using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace dgMicMute
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _notifyIcon;
        private Bootstrapper _bootstrapper ;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (Environment.OSVersion.Version.Major < 6 || Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 1)
            {
                MessageBox.Show(
                    "This Tool can only run under Windows 7 or newer.\n\rSorry, blame Microsoft for not implementing the Enumeration API.",
                    "Wrong Operating System", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)Application.Current.FindResource("NotifyIcon");
            NotifyIconViewModel nivm = _notifyIcon.DataContext as NotifyIconViewModel;

            _bootstrapper = new Bootstrapper();
            _bootstrapper.Hook.KeyPressed += nivm.HotkeyPressed;
            _bootstrapper.Hook.DevicesChanged += nivm.RefreshMicList;

            _bootstrapper.RegistrationException += (s, ex) =>
            {
                _notifyIcon.ShowBalloonTip("Hotkey registration error", ((System.Exception)ex.ExceptionObject).Message, BalloonIcon.Error);
                if (nivm.OpenSettingsWindowCommand.CanExecute(null))
				{
                    nivm.OpenSettingsWindowCommand.Execute(null);
				}
            };

            _bootstrapper.Init();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            _bootstrapper.Shutdown();
            base.OnExit(e);
        }
    }
}
