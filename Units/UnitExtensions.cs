namespace Units;

public static class UnitExtensions
{
  public static bool IsMetric(this Unit unit) => unit switch
  {
    Unit.Mile => false,
    Unit.MilesPerHour  => false,
    Unit.MinutesPerMile => false,
    _ => true,
  };
}
