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
        if (text.HasNoValue())
        {
            throw new ArgumentException("Spinner text cannot be empty."
                                      , nameof(text));
        }

        var forward = text.Select(( _, index ) => GetAnimatedFrame(text, index))
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

    private string GetAnimatedFrame( string text
                                   , int    index )
    {
        var chars = text.ToCharArray();

        chars[index] = TransformChar(chars[index]);

        return new string(chars);
    }

    private char TransformChar( char c )
    {
        return c switch
        {
                //Alphas
                >= 'a' and <= 'z' => char.ToUpper(c)
              , >= 'A' and <= 'Z' => char.ToLower(c)
                
                //Special characters
              , '.'  => '\''
              , ':'  => ';'
              , ';'  => ':'
              , '-'  => '='
              , '='  => '-'
              , '_'  => '¯'
              , '/'  => '\\'
              , '\\' => '/'
              , ' '  => '·'
                
                //Numbers
              , '0' => 'O'
              , '1' => '|'
              , '2' => 'Z'
              , '3' => 'E'
              , '5' => 'S'
              , '6' => 'G'
              , '7' => 'L'
              , '8' => '&'
              , '9' => 'P'
              
              , _ => c
        };
    }
}