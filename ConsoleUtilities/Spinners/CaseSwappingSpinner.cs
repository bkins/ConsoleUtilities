using Spectre.Console;

namespace ConsoleUtilities.Spinners;

public sealed class CaseSwappingSpinner : Spinner
{
    private readonly string[] _frames;

    public override TimeSpan Interval => TimeSpan.FromMilliseconds(40);

    public override bool IsUnicode => false;

    public override IReadOnlyList<string> Frames => _frames;

    public CaseSwappingSpinner( string text )
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Spinner text cannot be empty."
                                      , nameof(text));
        }

        var forward = Enumerable.Range(0
                                     , text.Length)
                                .Select(i => SwapCharacter(text
                                                         , i))
                                .ToList();

        var backward = forward.Skip(1)
                              .Take(forward.Count - 2)
                              .Reverse();

        _frames = forward.Concat(backward)
                         .ToArray();
    }

    private static string SwapCharacter( string text
                                       , int    index )
    {
        var chars = text.ToCharArray();

        if (char.IsLetter(chars[index]))
        {
            chars[index] = char.IsUpper(chars[index])
                                   ? char.ToLower(chars[index])
                                   : char.ToUpper(chars[index]);
        }

        return new string(chars);
    }
}