namespace Units;

public static class QuantityExtensions
{
  public static double Meters(this Quantity q) => q.As(Unit.Meter).Value;
  public static double Centimeters(this Quantity q) => q.As(Unit.Centimeter).Value;
  public static double Millimeters(this Quantity q) => q.As(Unit.Millimeter).Value;
  public static double Degrees(this Quantity q) => q.As(Unit.Degree).Value;
  public static double Radians(this Quantity q) => q.As(Unit.Radian).Value;
  public static double Percent(this Quantity q) => q.As(Unit.Percent).Value;
  public static double Ratio(this Quantity q) => q.As(Unit.Ratio).Value;

  public static double Meters(this Quantity? q) => q.Value.Meters();
  public static double Centimeters(this Quantity? q) => q.Value.Centimeters();
  public static double Millimeters(this Quantity? q) => q.Value.Millimeters();
  public static double Degrees(this Quantity? q) => q.Value.Degrees();
  public static double Radians(this Quantity? q) => q.Value.Radians();
  public static double Percent(this Quantity? q) => q.Value.Percent();
  public static double Ratio(this Quantity? q) => q.Value.Ratio();
}