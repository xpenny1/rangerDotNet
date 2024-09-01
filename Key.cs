using System;

namespace rangerDotNet;

public class Key
{
    public String? KeyString { get; set; }
    public String? KeyName { get; set; }
    public KeyModifiers? Modifiers { get; set; }

    public Key(String keyString)
    {
        KeyString = keyString;
        KeyName   = null;
        Modifiers = null;
    }

    public Key(String name, KeyModifiers modifiers)
    {
        KeyString = null;
        KeyName   = name;
        Modifiers = modifiers;
    }
}

public enum KeyModifiers
{
    None = 0,
    Alt = 1,
    Ctrl = 2,
    Shift = 4
}
