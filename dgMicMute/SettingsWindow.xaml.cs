using dgMicMute.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dgMicMute
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            Mediator.Instance.Register(CloseWindow, MediatorMessages.CloseSettings);
        }

        private void CloseWindow(object obj)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SerializeStatic.Save(typeof(Settings));
        }
    }
}
