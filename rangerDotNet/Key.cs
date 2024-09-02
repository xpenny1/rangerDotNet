using System;

namespace rangerDotNet;

public class Key
{
    public String? KeyString { get; set; }
    public String? KeyName { get; set; }
    public ConsoleModifiers? Modifiers { get; set; }

    public Key(String keyString)
    {
        KeyString = keyString;
        KeyName   = null;
        Modifiers = null;
    }

    public Key(String name, ConsoleModifiers modifiers)
    {
        KeyString = null;
        KeyName   = name;
        Modifiers = modifiers;
    }

    public bool matchesConsoleKeyInfo(ConsoleKeyInfo key2)
    {
        if (KeyString != null && KeyString == key2.KeyChar.ToString())
        {
            return true;
        } else if (KeyName != null && Modifiers != null && KeyName == key2.Key.ToString() && key2.Modifiers == Modifiers)
        {
            return true;
        } else
        {
            return false;
        }
    }

}

//public enum KeyModifiers
//{
//    None = 0,
//    Alt = 1,
//    Ctrl = 2,
//    Shift = 4
//}
