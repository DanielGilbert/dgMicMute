using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace dgMicMute.Enumerations
{
    public enum ModifierEnum : uint
    {
        None = Key.None,
        LeftShift = Key.LeftShift,
        RightShift = Key.RightShift,
        LeftCtrl = Key.LeftCtrl,
        RightCtrl = Key.RightCtrl,
        LeftAlt = Key.LeftAlt,
        RightAlt = Key.RightAlt
    }
}