using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace dgMicMute
{
    public class Bootstrapper
    {
        Settings settings = new Settings();
        KeyboardHook hook;

        public void Init()
        {
            //Loads the settings
            settings = Settings.Load();

            if (!settings.UseHotkey) return;

            hook = new KeyboardHook();
            hook.RegisterHotKey(settings.FirstModifier | settings.SecondModifier, settings.SelectedKey);
        }

        public void Shutdown()
        {
            
        }
    }
}
