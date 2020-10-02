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
        public event EventHandler<KeyPressedEventArgs> HotkeyPressed;
        public event EventHandler<UnhandledExceptionEventArgs> RegistrationException;

        KeyboardHook hook;

        public void Init()
        {
            //Loads the settings
            SerializeStatic.Load(typeof(Settings));
            hook = new KeyboardHook();
            hook.KeyPressed += (s, e) => HotkeyPressed?.Invoke(s, e);
            RegisterHotkey();
            Settings.OnSettingsChanged += Settings_OnSettingsChanged;
        }

        private void Settings_OnSettingsChanged(string propertyName)
        {
            if (String.Compare(propertyName, "ConfigureHotkey") == 0)
            {
                // This will be called whenever the hot key settings are altered upon closing the SettingsWindow.
                // This approach is necessary since all of the hotkey-related settings need to be handled at once after
                // they've been finalized, rather than each time an individual setting (e.g. FirstModifier, SelectedKey, etc.)
                // is changed.
                RegisterHotkey();
            }
        }

        private void RegisterHotkey()
        {
            try
            {
                hook.UnregisterHotkeys(); // unregister any hotkeys that might already have been registered

                // Make sure "UsesHotKey" is true and we have, at minimum, a SelectedKey defined
                if (Settings.UsesHotkey && !String.IsNullOrWhiteSpace(Settings.SelectedKey))
                {
                    Func<string, ModifierKeys> getModifierKey = (keyString) =>
                        String.IsNullOrWhiteSpace(keyString) ? ModifierKeys.None : (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keyString);

                    ModifierKeys modifierKeys = ModifierKeys.None | getModifierKey(Settings.FirstModifier) | getModifierKey(Settings.SecondModifier);
                    //Keys selectedKey = (Keys)Enum.Parse(typeof(Keys), Settings.SelectedKey);
                    Keys selectedKey = Keys.None;
                    if (KeyMapper.AvailableKeys.TryGetValue(Settings.SelectedKey, out selectedKey))
                    {
                        hook.RegisterHotKey(modifierKeys, selectedKey);
                    }
                    else
                    {
                        throw new ArgumentException($"Unknown key name '{Settings.SelectedKey}'");
                    }
                }
            }
            catch(Exception ex)
			{
                //MessageBox.Show(ex.Message + "\r\n\r\nPlease open 'Settings' and choose a different key combination", "Unable to register hotkey", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RegistrationException?.Invoke(this, new UnhandledExceptionEventArgs(ex, false));
			}
        }

        public void Shutdown()
        {
            hook.UnregisterHotkeys();
            SerializeStatic.Save(typeof(Settings));
        }
    }
}
