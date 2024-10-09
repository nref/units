using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Units;

public struct Quantity : IEquatable<Quantity>, IComparable<Quantity>, IComparable
{
  /// <summary>
  /// The setter is private so that the value isn't changed without through to the unit.
  /// To set the value, construct a new Quantity. 
  /// If you want to change the unit, use As(Unit unit).
  /// </summary>
  [JsonProperty]
  [System.Text.Json.Serialization.JsonInclude]
  public double Value 
  { 
    get => value_; 
    private set => value_ = value; 
  }

  [JsonConverter(typeof(StringEnumConverter))]
  [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
  public Unit Unit
  {
    get => unit_;
    set
    {
      if (unit_ == value)
      {
        return;
      }

      value_ = UnitConvert.Convert(unit_, value, value_);
      unit_ = value;
    }
  }

  private double value_;
  private Unit unit_;

  public Quantity(Quantity other)
  {
    value_ = other.Value;
    unit_ = other.Unit;
  }

  public Quantity(double value, Unit unit)
  {
    unit_ = unit;
    value_ = value;
  }

  /// <summary>
  /// Parse from string e.g. "1.23m" => Value = 1.23, Unit = Unit.Meter.
  /// 
  /// <para/>
  /// If the number has a fractional part, it must be separted form the whole part
  /// with a dot ".". Cultural variations such as comma are by design not supported.
  /// 
  /// <para/>
  /// Tolerates non-SI whitespace e.g. "1.23m" and "1.23 °".
  /// </summary>
  public Quantity(string s)
  {
    var q = new ParsedQuantity(s);

    value_ = Convert.ToDouble(q.Value, CultureInfo.InvariantCulture);
    unit_ = q.Unit.MapUnit();
  }

  public override string ToString() => $"{FormattableString.Invariant($"{Value}")}{Unit.MapString()}";

  /// <summary>
  /// Returns a copy of the source Quantity converted the given unit.
  /// Does not modify the source Quantity.
  /// </summary>
  public Quantity As(Unit unit) => new Quantity(this) { Unit = unit };

  public override int GetHashCode()
  {
    int hashCode = 2096794833;
    hashCode = hashCode * -1521134295 + Value.GetHashCode();
    hashCode = hashCode * -1521134295 + Unit.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object? other) => other is Quantity q && Equals(q);

  public bool Equals(Quantity other) => Equals(other, this);
  public int CompareTo(object? other) => other is Quantity q ? CompareTo(q) : -1;

  public int CompareTo(Quantity other)
  {
    if (other < this) return 1;
    if (other > this) return -1;
    return 0;
  }

  public static bool operator ==(Quantity left, Quantity right) => Equals(left, right);
  public static bool operator !=(Quantity left, Quantity right) => !Equals(left, right);
  public static bool operator >=(Quantity left, Quantity right) => GreaterOrEqual(left, right);
  public static bool operator <=(Quantity left, Quantity right) => LessOrEqual(left, right);
  public static bool operator <(Quantity left, Quantity right) => Less(left, right);
  public static bool operator >(Quantity left, Quantity right) => Greater(left, right);

  public static bool Equals(Quantity left, Quantity right, double tol = 1e-12) => Math.Abs(left.As(right.Unit).Value - right.Value) < tol;
  public static bool GreaterOrEqual(Quantity left, Quantity right) => left.As(right.Unit).Value >= right.Value;
  public static bool Greater(Quantity left, Quantity right) => left.As(right.Unit).Value > right.Value;
  public static bool Less(Quantity left, Quantity right) => left.As(right.Unit).Value < right.Value;
  public static bool LessOrEqual(Quantity left, Quantity right) => left.As(right.Unit).Value <= right.Value;
}
