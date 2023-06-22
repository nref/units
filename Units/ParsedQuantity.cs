using System;
using System.Text.RegularExpressions;

namespace Units;

public partial class ParsedQuantity
{
  public string Value { get; set; }
  public string Unit { get; set; }

  /// <summary>
  /// Return the substring of the first number in the string
  /// e.g. "1.23m" => "1.23"
  /// </summary>
  public ParsedQuantity(string s)
  {
    var match = regex_().Match(s);

    if (!match.Success || match.Groups.Count < 4)
    {
      throw new ArgumentException($"Could not parse \'{s}\' to {nameof(Quantity)}");
    }
      
    // Groups[0] == the whole matched string e.g. 1.23 m
    Value = match.Groups[1].Value;
    // Groups[2] == exponential notation e.g. "e-1" of "1e-1m"
    Unit = match.Groups[3].Value;
  }

  [GeneratedRegex("(^[-+]?[0-9]+[.]?[0-9]*([eE][-+]?[0-9]+)?)[\\s+]?([\\w°]+)?")]
  private static partial Regex regex_();
}
