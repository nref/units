using System;

namespace Units;

public static class UnitMapper
{
  public const string DegreeSymbol = "°"; // NIST does not require a space for angles

  public static Unit MapUnit(this string s) => s switch
  {
    "mm" => Unit.Millimeter,
    "cm" => Unit.Centimeter,
    "m" => Unit.Meter,

    "°" => Unit.Degree,
    "deg" => Unit.Degree,
    "rad" => Unit.Radian,
    "Hz" => Unit.Hz,
    "" => Unit.Scalar,

    _ => throw new ArgumentException($"Could not parse unit from {s}")
  };

  public static string MapString(this Unit unit) => unit switch
  {
    Unit.Millimeter => " mm",
    Unit.Centimeter => " cm",
    Unit.Meter => " m",

    Unit.Degree => DegreeSymbol,
    Unit.Radian => " rad",
    Unit.Hz => " Hz",
    Unit.Scalar => "",
    Unit.Ratio => "",

    _ => throw new ArgumentException($"Could not convert {unit} to string")
  };
}
