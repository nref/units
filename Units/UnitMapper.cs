using System;

namespace Units;

public static class UnitMapper
{
  public const string DegreeSymbol = "°"; // NIST does not require a space for angles

  public static Unit MapUnit(this string s) => s.Trim() switch
  {
    "mm" => Unit.Millimeter,
    "cm" => Unit.Centimeter,
    "m" => Unit.Meter,
    "m/s" => Unit.MetersPerSecond,
    "mi" => Unit.Mile,
    "mi/h" => Unit.MilesPerHour,
    "km" => Unit.Kilometer,
    "km/h" => Unit.KilometersPerHour,
    "min/mi" => Unit.MinutesPerMile,
    "/mi" => Unit.MinutesPerMile,
    "min/km" => Unit.MinutesPerKilometer,
    "/km" => Unit.MinutesPerKilometer,

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
    Unit.MetersPerSecond => " m/s",
    Unit.Mile => " mi",
    Unit.Kilometer => " km",
    Unit.MilesPerHour => " mi/h",
    Unit.KilometersPerHour => " km/h",
    Unit.MinutesPerMile => " min/mi",
    Unit.MinutesPerKilometer => " min/km",

    Unit.Degree => DegreeSymbol,
    Unit.Radian => " rad",
    Unit.Hz => " Hz",
    Unit.Scalar => "",
    Unit.Ratio => "",

    _ => throw new ArgumentException($"Could not convert {unit} to string")
  };
}
