using System;
using NUnit.Framework;

namespace Units.Tests;

[TestFixture]
public class QuantityFixture
{
  [Test]
  public void SerializesNewtonsoftJson()
  {
    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new Quantity(1.001, Unit.Radian));

    var q = Newtonsoft.Json.JsonConvert.DeserializeObject<Quantity>(json);

    Assert.AreEqual(1.001, q.Value, 1e-12);
    Assert.AreEqual(Unit.Radian, q.Unit);
  }

  [Test]
  public void SerializesSystemTextJson()
  {
    string json = System.Text.Json.JsonSerializer.Serialize(new Quantity(1.001, Unit.Radian));

    var q = System.Text.Json.JsonSerializer.Deserialize<Quantity>(json);

    Assert.AreEqual(1.001, q.Value, 1e-12);
    Assert.AreEqual(Unit.Radian, q.Unit);
  }
  
  [Test]
  public void ConvertsMeterToMillimeter()
  {
    var q = new Quantity(1.001, Unit.Meter); // Arrange

    q.Unit = Unit.Millimeter; // Act
    
    Assert.AreEqual(1001, q.Value, 1e-12); // Assert
  }

  [Test]
  public void ConvertsMeterToCentimeter()
  {
    var q = new Quantity(1.001, Unit.Meter);

    q.Unit = Unit.Centimeter;

    Assert.AreEqual(100.1, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsMeterToMeter()
  {
    var q = new Quantity(1.001, Unit.Meter);

    q.Unit = Unit.Meter;

    Assert.AreEqual(1.001, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsMillimeterToMillimeter()
  {
    var q = new Quantity(1001, Unit.Millimeter);

    q.Unit = Unit.Millimeter;

    Assert.AreEqual(1001, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsMillimeterToCentimeter()
  {
    var q = new Quantity(1001, Unit.Millimeter);

    q.Unit = Unit.Centimeter;

    Assert.AreEqual(100.1, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsMillimeterToMeter()
  {
    var q = new Quantity(1001, Unit.Millimeter);

    q.Unit = Unit.Meter;

    Assert.AreEqual(1.001, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsCentimeterToMillimeter()
  {
    var q = new Quantity(1001, Unit.Centimeter);

    q.Unit = Unit.Millimeter;

    Assert.AreEqual(10010, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsCentimeterToCentimeter()
  {
    var q = new Quantity(1001, Unit.Centimeter);

    q.Unit = Unit.Centimeter;

    Assert.AreEqual(1001, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsCentimeterToMeter()
  {
    var q = new Quantity(1001, Unit.Centimeter);

    q.Unit = Unit.Meter;

    Assert.AreEqual(10.01, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsDegreeToRadian()
  {
    var q = new Quantity(90, Unit.Degree);

    q.Unit = Unit.Radian;

    Assert.AreEqual(Math.PI / 2, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsRadianToDegree()
  {
    var q = new Quantity(Math.PI / 2, Unit.Radian);

    q.Unit = Unit.Degree;

    Assert.AreEqual(90, q.Value, 1e-12);
  }

  [Test]
  public void ConvertsRatioToPercent()
  {
    var q = new Quantity(0.05, Unit.Ratio);

    q.Unit = Unit.Percent;

    Assert.AreEqual(5, q.Value);
  }

  [Test]
  public void ConvertsPercentToRatio()
  {
    var q = new Quantity(5, Unit.Percent);

    q.Unit = Unit.Ratio;

    Assert.AreEqual(0.05, q.Value);
  }

  [Test]
  public void ConvertsHzToSecond()
  {
    var q = new Quantity(5, Unit.Hz);

    q.Unit = Unit.Second;

    Assert.AreEqual(1/5.0, q.Value);
  }

  [Test]
  public void ConvertsSecondToHz()
  {
    var q = new Quantity(1/5.0, Unit.Second);

    q.Unit = Unit.Hz;

    Assert.AreEqual(5, q.Value);
  }

  [Test]
  public void ConvertsHzToMillisecond()
  {
    var q = new Quantity(5, Unit.Hz);

    q.Unit = Unit.Millisecond;

    Assert.AreEqual(200, q.Value);
  }

  [Test]
  public void ConvertsMillisecondToHz()
  {
    var q = new Quantity(200, Unit.Millisecond);

    q.Unit = Unit.Hz;

    Assert.AreEqual(5, q.Value);
  }

  [Test]
  public void Extensions()
  {
    Assert.AreEqual(new Quantity(1.001, Unit.Meter).Meters(), 1.001, 1e-12);
    Assert.AreEqual(new Quantity(1.001, Unit.Centimeter).Centimeters(), 1.001, 1e-12);
    Assert.AreEqual(new Quantity(1.001, Unit.Millimeter).Millimeters(), 1.001, 1e-12);
    Assert.AreEqual(new Quantity(1.001, Unit.Radian).Radians(), 1.001, 1e-12);
    Assert.AreEqual(new Quantity(1.001, Unit.Degree).Degrees(), 1.001, 1e-12);
    Assert.AreEqual(new Quantity(1.001, Unit.Percent).Percent(), 1.001, 1e-12);
  }

  [Test]
  public void As_ConvertsUnit()
  {
    var m = new Quantity(1.001, Unit.Meter);

    var mm = m.As(Unit.Millimeter);

    Assert.AreEqual(mm.Value, 1001, 1e-12);
  }

  [Test]
  public void As_DoesNotModifySource()
  {
    var m = new Quantity(1.001, Unit.Meter);

    var _ = m.As(Unit.Millimeter);
    
    Assert.AreEqual(m.Value, 1.001);
  }

  [Test]
  public void Meter_ToString_Correct() => Assert.AreEqual("1 m", new Quantity(1, Unit.Meter).ToString());

  [Test]
  public void Centimeter_ToString_Correct() => Assert.AreEqual("1 cm", $"{new Quantity(1, Unit.Centimeter)}");

  [Test]
  public void Mllimeter_ToString_Correct() => Assert.AreEqual("1 mm", $"{new Quantity(1, Unit.Millimeter)}");

  [Test]
  public void Degree_ToString_Correct() => Assert.AreEqual("1 deg", $"{new Quantity(1, Unit.Degree)}");

  [Test]
  public void Radian_ToString_Correct() => Assert.AreEqual("1 rad", $"{new Quantity(1, Unit.Radian)}");

  [Test]
  public void Parse_TakesFirstQuantity()
  {
    var si = new Quantity("-100.1 cm, -100.2 m");

    Assert.AreEqual(-100.1, si.Value);
    Assert.AreEqual(Unit.Centimeter, si.Unit);
  }

  [Test]
  public void Parse_AcceptsScientificNotation()
  {
    var q = new Quantity("-143e-2 rad");

    Assert.AreEqual(-1.43, q.Value);
    Assert.AreEqual(Unit.Radian, q.Unit);
  }

  [Test]
  public void Parse_ParsesMeter()
  {
    var si = new Quantity("1001 m");
    var nonSi = new Quantity("1001m");

    Assert.AreEqual(1001, si.Value);
    Assert.AreEqual(1001, nonSi.Value);
    Assert.AreEqual(Unit.Meter, si.Unit);
    Assert.AreEqual(Unit.Meter, nonSi.Unit);
  }

  [Test]
  public void Parse_ParsesCentimeter()
  {
    var si = new Quantity("-100.1 cm");
    var nonSi = new Quantity("-100.1cm");

    Assert.AreEqual(-100.1, si.Value);
    Assert.AreEqual(-100.1, nonSi.Value);
    Assert.AreEqual(Unit.Centimeter, si.Unit);
    Assert.AreEqual(Unit.Centimeter, nonSi.Unit);
  }

  [Test]
  public void Parse_ParsesMillimeter()
  {
    var si = new Quantity("1001 mm");
    var nonSi = new Quantity("1001mm");

    Assert.AreEqual(1001, nonSi.Value);
    Assert.AreEqual(1001, si.Value);
    Assert.AreEqual(Unit.Millimeter, nonSi.Unit);
    Assert.AreEqual(Unit.Millimeter, si.Unit);
  }

  [Test]
  public void Parse_ParsesDegree()
  {
    var si1 = new Quantity("-1.43°");
    var si2 = new Quantity("-1.43 deg");
    var nonSi1 = new Quantity("-1.43 °");
    var nonSi2 = new Quantity("-1.43deg");

    Assert.AreEqual(-1.43, si1.Value);
    Assert.AreEqual(-1.43, si2.Value);
    Assert.AreEqual(-1.43, nonSi1.Value);
    Assert.AreEqual(-1.43, nonSi2.Value);
    Assert.AreEqual(Unit.Degree, si1.Unit);
    Assert.AreEqual(Unit.Degree, si2.Unit);
    Assert.AreEqual(Unit.Degree, nonSi1.Unit);
    Assert.AreEqual(Unit.Degree, nonSi2.Unit);
  }

  [Test]
  public void Parse_ParsesRadian()
  {
    var si = new Quantity("-1.43 rad");
    var nonSi = new Quantity("-1.43rad");

    Assert.AreEqual(-1.43, si.Value);
    Assert.AreEqual(-1.43, nonSi.Value);
    Assert.AreEqual(Unit.Radian, si.Unit);
    Assert.AreEqual(Unit.Radian, nonSi.Unit);
  }

  [Test]
  public void Parse_ParsesHertz()
  {
    var si = new Quantity("1.23 Hz");

    Assert.AreEqual(1.23, si.Value);
    Assert.AreEqual(Unit.Hz, si.Unit);
  }

  [Test]
  public void Parse_ParsesScalar()
  {
    var si = new Quantity("1.23");

    Assert.AreEqual(1.23, si.Value);
    Assert.AreEqual(Unit.Scalar, si.Unit);
  }

  [Test]
  public void Parse_InvalidString_Throws() => Assert.Throws<ArgumentException>(() => new Quantity("1 im"));

  [Test]
  public void Object_Equals_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    object equal = new Quantity(1.23 + 1e-13, Unit.Meter);
    object unequal = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsTrue(equal.Equals(q));
    Assert.IsFalse(unequal.Equals(q));
  }

  [Test]
  public void IEquatable_Equals_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(1.23 + 1e-13, Unit.Meter);
    var unequal = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsTrue(equal.Equals(q));
    Assert.IsFalse(unequal.Equals(q));
  }

  [Test]
  public void OperatorEquals_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(1.23 + 1e-13, Unit.Meter);
    var unequal = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsTrue(equal == q);
    Assert.IsFalse(unequal == q);
  }

  [Test]
  public void OperatorEquals_DifferentUnits_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(123 + 1e-11, Unit.Centimeter);
    var unequal = new Quantity(123 + 1e-9, Unit.Centimeter);

    Assert.IsTrue(equal == q);
    Assert.IsFalse(unequal == q);
  }

  [Test]
  public void OperatorGreaterOrEqual_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(1.23, Unit.Meter);
    var less = new Quantity(1.23 - 1e-11, Unit.Meter);
    var greater = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsTrue(equal >= q);
    Assert.IsFalse(less >= q);
    Assert.IsTrue(greater >= q);
  }

  [Test]
  public void OperatorGreater_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(1.23, Unit.Meter);
    var less = new Quantity(1.23 - 1e-11, Unit.Meter);
    var greater = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsFalse(equal > q);
    Assert.IsFalse(less > q);
    Assert.IsTrue(greater > q);
  }

  [Test]
  public void OperatorLessOrEqual_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(1.23, Unit.Meter);
    var less = new Quantity(1.23 - 1e-11, Unit.Meter);
    var greater = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsTrue(equal <= q);
    Assert.IsTrue(less <= q);
    Assert.IsFalse(greater <= q);
  }

  [Test]
  public void OperatorLess_Correct()
  {
    var q = new Quantity(1.23, Unit.Meter);
    var equal = new Quantity(1.23, Unit.Meter);
    var less = new Quantity(1.23 - 1e-11, Unit.Meter);
    var greater = new Quantity(1.23 + 1e-11, Unit.Meter);

    Assert.IsFalse(equal < q);
    Assert.IsTrue(less < q);
    Assert.IsFalse(greater < q);
  }

  [Test]
  public void Millimeters_Correct() => Assert.AreEqual(1.23, new Quantity(1.23, Unit.Millimeter).Millimeters());

  [Test]
  public void Centimeters_Correct() => Assert.AreEqual(1.23, new Quantity(1.23, Unit.Centimeter).Centimeters());

  [Test]
  public void Meters_Correct() => Assert.AreEqual(1.23, new Quantity(1.23, Unit.Meter).Meters());

  [Test]
  public void Degrees_Correct() => Assert.AreEqual(1.23, new Quantity(1.23, Unit.Degree).Degrees());

  [Test]
  public void Radians_Correct() => Assert.AreEqual(1.23, new Quantity(1.23, Unit.Radian).Radians());
}