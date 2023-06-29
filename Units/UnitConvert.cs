using System;
using System.Collections.Generic;

namespace Units;

public static class UnitConvert
{
  private static readonly Func<double, double> inverse_ = new(value => 1 / value);

  /// <summary>
  /// The value of SI metric prefixes expressed as powers of 10
  /// </summary>
  public static readonly Dictionary<SiPrefix, double> Prefixes = new()
  {
    { SiPrefix.Giga, 1e9 },
    { SiPrefix.Mega, 1e6 },
    { SiPrefix.Kilo, 1e3 },
    { SiPrefix.None, 1e0 },
    { SiPrefix.Centi, 1e-2 },
    { SiPrefix.Milli, 1e-3 },
    { SiPrefix.Micro, 1e-6 },
    { SiPrefix.Nano, 1e-9 },
  };

  /// <summary>
  /// Get the ratio to convert an SI metric prefix pair, e.g. "milli" and "deca"
  /// </summary>
  public static double Ratio(SiPrefix from, SiPrefix to) => Prefixes[from] / Prefixes[to];

  /// <summary>
  /// Convert the given value between the given units.
  /// </summary>
  public static double Convert(Unit from, Unit to, double value) => Convert(from, to)(value);

  /// <summary>
  /// Some unit conversions require the current value.
  /// Return a Func that when given the value to be converted converts between the given units.
  /// </summary>
  public static Func<double, double> Convert(Unit from, Unit to) => to switch
  {
    _ when to == from => value => value,

    Unit.Hz when from == Unit.Second => inverse_,
    Unit.Second when from == Unit.Hz => inverse_,

    // Millisecond => Second => Hz
    Unit.Hz when from == Unit.Millisecond => value => inverse_(value * Ratio(Unit.Millisecond, Unit.Second)),
    // Hz => Second => Millisecond
    Unit.Millisecond when from == Unit.Hz => value => inverse_(value) * Ratio(Unit.Second, Unit.Millisecond),
   
    _ when from == Unit.MinutesPerMile => value => inverse_(value) * Ratio(from, to),
    _ when from == Unit.MinutesPerKilometer => value => inverse_(value) * Ratio(from, to),
    Unit.MinutesPerMile => value => inverse_(value) * Ratio(from, to),
    Unit.MinutesPerKilometer => value => inverse_(value) * Ratio(from, to),

    // The conversion can be done without knowledge of the value
    _ => value => value * Ratio(from, to)
  };

  /// <summary>
  /// Get the ratio to convert between the given units.
  /// Throw ArgumentException if there is no such ratio.
  /// </summary>
  public static double Ratio(Unit from, Unit to) => to switch
  {
    Unit.MetersPerSecond when from == Unit.MetersPerSecond => 1,
    Unit.MilesPerHour when from == Unit.MilesPerHour => 1,
    Unit.KilometersPerHour when from == Unit.KilometersPerHour => 1,
    Unit.MinutesPerMile when from == Unit.MinutesPerMile => 1,
    Unit.MinutesPerKilometer when from == Unit.MinutesPerKilometer => 1,

    Unit.MetersPerSecond when from == Unit.MilesPerHour => 0.44704,
    Unit.MetersPerSecond when from == Unit.KilometersPerHour => 0.27777778,
    Unit.MetersPerSecond when from == Unit.MinutesPerMile => 26.8224,
    Unit.MetersPerSecond when from == Unit.MinutesPerKilometer => 16.6666667,

    Unit.MilesPerHour when from == Unit.MetersPerSecond => 2.2369362920544,
    Unit.KilometersPerHour when from == Unit.MetersPerSecond => 3.6,
    Unit.MinutesPerMile when from == Unit.MetersPerSecond => 0.3728227153424,
    Unit.MinutesPerKilometer when from == Unit.MetersPerSecond => 0.06,

    Unit.Meter when from == Unit.Kilometer => Ratio(SiPrefix.Kilo, SiPrefix.None),
    Unit.Kilometer when from == Unit.Meter => Ratio(SiPrefix.None, SiPrefix.Kilo),

    Unit.Meter when from == Unit.Mile => 1609.344,
    Unit.Mile when from == Unit.Meter => 0.0006213712,

    Unit.Kilometer when from == Unit.Mile => 1.609344,
    Unit.Mile when from == Unit.Kilometer => 0.6213712,

    Unit.Millimeter when from == Unit.Millimeter => Ratio(SiPrefix.Milli, SiPrefix.Milli),
    Unit.Millimeter when from == Unit.Centimeter => Ratio(SiPrefix.Centi, SiPrefix.Milli),
    Unit.Millimeter when from == Unit.Meter => Ratio(SiPrefix.None, SiPrefix.Milli),

    Unit.Centimeter when from == Unit.Millimeter => Ratio(SiPrefix.Milli, SiPrefix.Centi),
    Unit.Centimeter when from == Unit.Centimeter => Ratio(SiPrefix.Centi, SiPrefix.Centi),
    Unit.Centimeter when from == Unit.Meter => Ratio(SiPrefix.None, SiPrefix.Centi),

    Unit.Meter when from == Unit.Millimeter => Ratio(SiPrefix.Milli, SiPrefix.None),
    Unit.Meter when from == Unit.Centimeter => Ratio(SiPrefix.Centi, SiPrefix.None),
    Unit.Meter when from == Unit.Meter => Ratio(SiPrefix.None, SiPrefix.None),

    Unit.Degree when from == Unit.Degree => 1,
    Unit.Degree when from == Unit.Radian => 180 / Math.PI,

    Unit.Radian when from == Unit.Radian => 1,
    Unit.Radian when from == Unit.Degree => Math.PI / 180,

    Unit.Percent when from == Unit.Ratio => 100.0,
    Unit.Ratio when from == Unit.Percent => 1 / 100.0,

    Unit.Millisecond when from == Unit.Second => Ratio(SiPrefix.None, SiPrefix.Milli),
    Unit.Second when from == Unit.Millisecond => Ratio(SiPrefix.Milli, SiPrefix.None),

    // Allow identity transition to None
    Unit.Scalar => 1,

    // Allow identity transition from None
    _ when from == Unit.Scalar => 1,

    _ => throw new ArgumentException($"Cannot convert from {nameof(Unit)}.{from} to {nameof(Unit)}.{to}"),
  };
}
