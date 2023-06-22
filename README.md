## Units

A common library for sharing physical quantities. A `Quantity` has a `Value` and a `Unit`.

    A physical quantity is a property of a material or system that can be quantified by measurement. A physical quantity can be expressed as the combination of a numerical value and a unit. For example, the physical quantity mass can be quantified as n kg, where n is the numerical value and kg is the unit. A physical quantity possesses at least two characteristics in common, one is numerical magnitude and other is the unit in which it is measured.

https://en.m.wikipedia.org/wiki/Physical_quantity

## Examples

```cs
  Quantity m = new Quantity(1.001, Unit.Meter);
  Quantity mm = m.As(Unit.Millimeter);
  Assert.AreEqual(mm.Value, 1001, 1e-12);
```
```cs
  Quantity angle = new Quantity(90, Unit.Degree);
  angle.Unit = Unit.Radian;
  Assert.AreEqual(Math.PI / 2, angle.Value, 1e-12);
```

See `QuantityFixture` for more examples.

## Adding Units

1. Add your new unit to the `Unit` enum.

```cs
  public enum Unit
  {
    ...
    Furlong // My cool new Unit
  }
```

2. Add unit conversions in ```UnitConverter```

```cs
  public static class UnitConverter
  {
    public static double Convert(Unit from, Unit to) => to switch
    {
      ...
      Unit.Meter when from == Unit.Furlong => 1 / 201.168,
      Unit.Furlong when from == Unit.Meter => 201.168,
    };
```

3. Add string conversions in `UnitMapper`

```cs
    public static Unit MapUnit(this string s) => s switch
    {
      ...
      "fur" => Unit.Furlong,
    };

    public static string MapString(this Unit unit) => unit switch
    {
      ...
      Unit.Furlong => "fur",
    };
```