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
        KeyboardHook hook;

        public void Init()
        {
            //Loads the settings
            SerializeStatic.Load(typeof(Settings));
        }

        public void Shutdown()
        {
            SerializeStatic.Save(typeof(Settings));
        }
    }
}
