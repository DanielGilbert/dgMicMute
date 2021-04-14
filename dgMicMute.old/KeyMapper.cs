using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace dgMicMute
{
	public static class KeyMapper
	{
		private static IDictionary<string, Keys> _availableKeys = null;

		/// <summary>
		/// Returns a dictionary of <string, Keys> where key = a friendly/descriptive name for the key and value = the Keys enum value of that key.
		/// The IEnumerable<string> AvailableKeys.Keys (i.e. the names) are used when populating the drop-down in the SettingsWindow, then the 
		/// selected result is saved as a string in Settings.
		/// That string identifier is resolved via AvailableKeys.TryGetValue(Settings.SelectedKey) when it is time to register the hotkey (in Bootstrapper).
		/// </summary>
		public static IDictionary<string, Keys> AvailableKeys
		{
			get
			{
				if (_availableKeys == null)
				{
					_availableKeys = new Dictionary<string, Keys>();
					Regex splitter = new Regex("(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=[0-9])(?=[A-Z][a-z])|(?<=[a-z])(?=[0-9])", RegexOptions.Compiled);
					Regex numKeyMatch = new Regex(@"^D\d{1}$");

					// We'll use enum .ToString() for most of these, but a few need to be handled via this lookup since the Keys enumeration has some duplication,
					// especially when it comes to the OemXXX enum values.  I.e. something like Keys.OemSemicolon.ToString() will actually return "Oem1" instead.
					Dictionary<Keys, string> keyNameLookup = new Dictionary<Keys, string>();
					keyNameLookup.Add(Keys.CapsLock, "Caps Lock"); // ToString() returns "Capital" for this one
					keyNameLookup.Add(Keys.OemSemicolon, "Semicolon"); // "Oem1"
					keyNameLookup.Add(Keys.Oemplus, "Plus");
					keyNameLookup.Add(Keys.OemMinus, "Minus");
					keyNameLookup.Add(Keys.OemPeriod, "Period");
					keyNameLookup.Add(Keys.OemQuestion, "Question"); // "Oem2"
					keyNameLookup.Add(Keys.Oemtilde, "Tilde"); // "Oem3"
					keyNameLookup.Add(Keys.OemOpenBrackets, "Open Bracket"); // "Oem4"
					keyNameLookup.Add(Keys.OemCloseBrackets, "Close Bracket"); // "Oem6"
					keyNameLookup.Add(Keys.OemQuotes, "Quote"); // "Oem7"
					keyNameLookup.Add(Keys.OemBackslash, "Backslash"); // "Oem102"
					keyNameLookup.Add(Keys.OemPipe, "Pipe"); // "Oem5"
					keyNameLookup.Add(Keys.OemClear, "Clear");

					foreach (Keys key in new Keys[] {
						// Function keys
						Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12,
						// Ignoring these because no one on earth has this many Fn keys
						//Keys.F13, Keys.F14, Keys.F15, Keys.F16, Keys.F17, Keys.F18, Keys.F19, Keys.F20, Keys.F21, Keys.F22, Keys.F23, Keys.F24,
						
						// Command keys
						Keys.CapsLock, Keys.PrintScreen, Keys.Insert, Keys.Delete,
						Keys.NumLock, Keys.Scroll,
						
						// Standard numeric keys
						Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,
						
						// Standard letter keys
						Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, 
						Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z,

						// Numpad keys
						Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
						Keys.Multiply, Keys.Add, Keys.Separator, Keys.Subtract, Keys.Decimal, Keys.Divide,

						// Media keys
						Keys.BrowserBack, Keys.BrowserForward, Keys.BrowserRefresh, Keys.BrowserStop, Keys.BrowserSearch, Keys.BrowserFavorites, Keys.BrowserHome,
						Keys.VolumeMute, Keys.VolumeDown, Keys.VolumeUp, Keys.MediaNextTrack, Keys.MediaPreviousTrack, Keys.MediaStop, Keys.MediaPlayPause,
						Keys.LaunchMail, Keys.SelectMedia, Keys.LaunchApplication1, Keys.LaunchApplication2,

						// Character keys
						Keys.OemSemicolon, Keys.Oemplus, Keys.Oemcomma, Keys.OemMinus, Keys.OemPeriod, Keys.OemQuestion, Keys.Oemtilde, Keys.OemOpenBrackets, Keys.OemCloseBrackets, 
						Keys.OemPipe, Keys.OemQuotes, Keys.OemBackslash
						// Ignoring these atypical keys
						// , Keys.ProcessKey, Keys.Packet, Keys.Attn, Keys.Crsel, Keys.Exsel, Keys.EraseEof, Keys.Play, Keys.Zoom, Keys.Pa1, Keys.OemClear
						// Ignoring these as they are duplicates of some of the above...
						// ,Keys.Oem1, Keys.Oem2, Keys.Oem3, Keys.Oem4, Keys.Oem5, Keys.Oem6, Keys.Oem7, Keys.Oem8, Keys.Oem102
					})
					{
						string keyName = null;
						if (keyNameLookup.ContainsKey(key))
						{
							keyName = keyNameLookup[key];
						}
						else
						{
							// The splitter Regex will convert names like "NumLock" to "Num Lock", "NumPad3" to "Num Pad 3", etc. but leave names 
							// like "F1" alone (i.e. where there's a capital letter followed immediately by a digit)
							keyName = String.Join(" ", splitter.Split(key.ToString()));
							if (numKeyMatch.IsMatch(keyName))
							{
								// just take the digit for the number keys, ignore the D prefix
								keyName = keyName.Substring(1); 
							}
							else if (keyName.StartsWith("Oem"))
							{
								// Trim "Oem" off all the keys like "Oemcomma", "OemQuotes", etc. for any that weren't resolved via the dictionary
								keyName = keyName.Substring(3);
								keyName = Char.ToUpper(keyName[0]) + keyName.Substring(1); // and fix the improperly cased items like "Oemcomma" to "Comma";;
							}
						}
						_availableKeys[keyName] = key;
					}

				}
				return _availableKeys;
			}
		}


	}
}
