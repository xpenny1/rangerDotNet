using System.Text.Json.Nodes;

namespace rangerDotNet.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    static string testString = 
            """
            {
                "Keys": {
                    "Prev": "k",
                    "Next": ["j",{"Name": "DownArrow", "Modifiers": "None"}],
                    "Down": "l",
                    "Up": "h",
                    "Quit": "q",
                    "Command": "$"
                },
                "Windows": {
                    "Shell": "cmd.exe",
                    "Terminal": "cmd.exe",
                    "CommandReplacements": {
                        "okular": "okular.exe",
                        "ls": "dir"
                    }
                },
                "Linux": {
                    "Shell": "bash",
                    "Terminal": "alacritty",
                    "CommandReplacements": {
                    }
                },
                "CommandBuild-Ins": {
                    "mv": true,
                    "cp": true
                },
                "Style": {
                    "centered": true
                }
            }        
            """;

    [Test]
    public static void testReadKeys1()
    {
        JsonNode node = JsonNode.Parse(testString);
        Key key = IO.readKey(node["Keys"]["Next"][1]);
        Assert.AreEqual(key.KeyName,"DownArrow");
        Assert.AreEqual(key.Modifiers,ConsoleModifiers.None);
        Assert.IsTrue(key.matchesConsoleKeyInfo(new ConsoleKeyInfo('?',ConsoleKey.DownArrow,false,false,false)));
    }

    [Test]
    public static void testReadKeys2()
    {
        JsonNode node = JsonNode.Parse(testString);
        Key key = IO.readKey(node["Keys"]["Next"][0]);
        Assert.AreEqual(key.KeyString,"j");
        Assert.IsTrue(key.matchesConsoleKeyInfo(new ConsoleKeyInfo('j',ConsoleKey.J,false,false,false)));
    }

    [Test]
    public static void testReadKeys3()
    {
        JsonNode node = JsonNode.Parse(testString);
        Key[] keys = IO.readKeysArray(node["Keys"]["Next"]);
        Assert.AreEqual(keys.Length,2);
        Assert.IsTrue(keys[0].matchesConsoleKeyInfo(new ConsoleKeyInfo('j',ConsoleKey.J,false,false,false)));
        Assert.IsTrue(keys[1].matchesConsoleKeyInfo(new ConsoleKeyInfo('?',ConsoleKey.DownArrow,false,false,false)));
    }

    [Test]
    public static void testReadKeys4()
    {
        JsonNode node = JsonNode.Parse(testString);
        Key[] keys = IO.readKeysArray(node["Keys"]["Down"]);
        Assert.AreEqual(keys.Length,1);
        Assert.IsFalse(keys[0].matchesConsoleKeyInfo(new ConsoleKeyInfo('L',ConsoleKey.L,true,false,false)));
    }
}