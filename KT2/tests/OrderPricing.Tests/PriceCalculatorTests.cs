using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPricing;

namespace OrderPricing.Tests;

[TestClass]
public class PriceCalculatorTests
{
  [TestMethod]
  public void RegularOrder_1000WithoutLoyalty_TotalIs1300()
  {
    // Arrange: данные и ожидание, рассчитанное по требованиям.
    decimal subtotal = 1000m;
    decimal expected = 1300m;

    // Act: один вызов проверяемого поведения.
    decimal actual = PriceCalculator.CalculateTotal(subtotal, false, false);

    // Assert: сравнение ожидания и результата.
    Assert.AreEqual(expected, actual);
  }

  // Названия писала ИИ, мне в лом такое писать. Все они идут подряд по таблице

  [TestMethod]
  public void CalculateTotal_SmallOrderWithoutDiscounts_ReturnsSubtotalPlusStandardDelivery_1()
  {
    decimal subtotal = 3200m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(3500m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderAtFreeDeliveryThreshold_ReturnsSubtotal_2()
  {
    decimal subtotal = 4000m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(4000m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderWithLoyaltyCard_ReturnsSubtotalMinus10Percent_3()
  {
    decimal subtotal = 4500m;
    bool loyalty = true;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(4050m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderBelow5PercentDiscountThreshold_ReturnsSubtotal_4()
  {
    decimal subtotal = 4999m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(4999m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderAt5PercentDiscountThreshold_ReturnsSubtotalMinus5Percent_5()
  {
    decimal subtotal = 5000m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(4750m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderBelowFreeDeliveryThreshold_ReturnsSubtotalPlusStandardDelivery_6()
  {
    decimal subtotal = 3999m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(4299m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderAbove5PercentThresholdWithLoyaltyCard_ReturnsSubtotalMinus10Percent_7()
  {
    decimal subtotal = 5100m;
    bool loyalty = true;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(4590m, result);
  }

  [TestMethod]
  public void CalculateTotal_SmallOrderWithExpressDelivery_ReturnsSubtotalPlusExpressDelivery_8()
  {
    decimal subtotal = 1200m;
    bool loyalty = false;
    bool express = true;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(1700m, result);
  }

  [TestMethod]
  public void CalculateTotal_OrderWithExpressDeliveryAnd5PercentDiscount_ReturnsDiscountedSubtotalPlusExpressDelivery_9()
  {
    decimal subtotal = 13000m;
    bool loyalty = false;
    bool express = true;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(12850m, result);
  }

  [TestMethod]
  public void CalculateTotal_MinimalOrder_ReturnsSubtotalPlusStandardDelivery_10()
  {
    decimal subtotal = 1m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(301m, result);
  }

  [TestMethod]
  public void CalculateTotal_MaximumAllowedSubtotal_ReturnsDiscountedSubtotal_11()
  {
    decimal subtotal = 100000m;
    bool loyalty = false;
    bool express = false;

    var result = PriceCalculator.CalculateTotal(subtotal, loyalty, express);

    Assert.AreEqual(95000m, result);
  }

  [TestMethod]
  public void CalculateTotal_SubtotalAboveMaximum_ThrowsArgumentOutOfRangeException_12()
  {
    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        PriceCalculator.CalculateTotal(1001300m, false, false));
  }
}
