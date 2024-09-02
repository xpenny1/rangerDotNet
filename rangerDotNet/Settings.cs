using System;

namespace rangerDotNet;


public enum Style
{
    Centered
}
public class Settings
{
    public Key[] Prev { get; set; }
    public Key[] Next { get; set; }
    public Key[] Down { get; set; }
    public Key[] Up { get; set; }
    public Key[] Quit { get; set; }
    public Key[] Command { get; set; }


    public String Shell { get; set; }
    public String Terminal { get; set; }


    public Dictionary<String,String> CommandReplacements { get; set; }


    public Boolean BuildIn_mv { get; set; }
    public Boolean BuildIn_cp { get; set; }


    public Style Style { get; set; }

    public Settings(Key[] prev,
                    Key[] next,
                    Key[] up,
                    Key[] down,
                    Key[] quit,
                    Key[] command,
                    String shell,
                    String terminal,
                    Dictionary<String,String> commandReplacements,
                    Boolean buildIn_mv,
                    Boolean buildIn_cp,
                    Style style
                    )
    {
        Prev                = prev;
        Next                = next;
        Up                  = up;
        Down                = down;
        Quit                = quit;
        Command             = command;
        Shell               = shell;
        Terminal            = terminal;
        CommandReplacements = commandReplacements;
        BuildIn_mv          = buildIn_mv;
        BuildIn_cp          = buildIn_cp;
        Style               = style;
    }

}
