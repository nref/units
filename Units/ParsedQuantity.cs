using System;
using System.Text.RegularExpressions;

namespace Units;

public partial class ParsedQuantity
{
  public double Value { get; set; }
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
      throw new ArgumentException($"Could not parse \'{s}\'");
    }

    // Groups[0] == the whole matched string e.g. 1.23 m
    Value = double.Parse(match.Groups[1].Value);
    // Groups[2] == exponential notation e.g. "e-1" of "1e-1m"
    Unit = match.Groups[3].Value;
  }

  [GeneratedRegex(@"(^[-+]?[0-9]+[.]?[0-9]*([eE][-+]?[0-9]+)?)[\s+]?([\w/°]+)?")]
  private static partial Regex regex_();
}

public partial class ParsedTime
{
  public double Value { get; set; }
  public string Unit { get; set; }

  public ParsedTime(string s)
  {
    var match = regex_().Match(s);

    if (!match.Success || match.Groups.Count < 4)
    {
      throw new ArgumentException($"Could not parse \'{s}\'");
    }

    string hour = match.Groups[1].Value; // Hour, can be empty if not present
    string minute = match.Groups[2].Value; // Minute
    string second = match.Groups[3].Value; // Second
    string unit = match.Groups[4].Value; // Unit (min/mi or min/km or /mi or /km)

    if (double.TryParse(hour, out double hh))
    {
      Value += 60 * hh; // hour to minute
    }

    if (double.TryParse(minute, out double mm))
    {
      Value += mm;
    }

    if (double.TryParse(second, out double ss))
    {
      Value += ss / 60; // second to minute
    }

    Unit = unit;
  }

  [GeneratedRegex(@"^(?:(\d{1,2}):)?(\d{1,2}):(\d{2})\s*(min\/mi|min\/km|\/mi|\/km)$")]
  private static partial Regex regex_();

}
