namespace Application.Utils;

public static class NicknameGeneratorUtil
{
    private static readonly string[] Prefixes = new[]
    {
        "Happy", "Cool", "Lazy", "Funny", "Smart", "Sleepy", "Crazy", "Mighty", "Swift", "Epic"
    };

    private static readonly string[] Names = new[]
    {
        "Tiger", "Panda", "Fox", "Wolf", "Bear", "Neko", "Ninja", "Wizard", "Robot", "Knight"
    };

    private static readonly Random _random = new();

    public static string GenerateNickname()
    {
        var prefix = Prefixes[_random.Next(Prefixes.Length)];
        var name = Names[_random.Next(Names.Length)];
        var number = _random.Next(10, 1000); // Optional: adds 2-3 digit number

        return $"{prefix}{name}{number}";
    }
}