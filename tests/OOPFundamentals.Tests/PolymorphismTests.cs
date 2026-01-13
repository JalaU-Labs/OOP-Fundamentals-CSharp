using OOPFundamentals.Core.Polymorphism;
using Xunit;

namespace OOPFundamentals.Tests;

/// <summary>
/// Unit tests for Polymorphism pillar classes.
/// Tests the payment system: Payment, CreditCardPayment, PayPalPayment, CashPayment, BitcoinPayment.
/// </summary>
public class PolymorphismTests
{
    #region Payment Base Class Tests
    
    [Fact]
    public void Payment_Status_ShouldBeInitiallyPending()
    {
        // Arrange & Act
        var payment = new CashPayment(100m, 100m);
        
        // Assert
        Assert.Equal(PaymentStatus.Pending, payment.Status);
    }
    
    [Fact]
    public void Payment_TransactionId_ShouldBeUnique()
    {
        // Arrange & Act
        var payment1 = new CashPayment(100m, 100m);
        var payment2 = new CashPayment(100m, 100m);
        
        // Assert
        Assert.NotEqual(payment1.TransactionId, payment2.TransactionId);
    }
    
    [Fact]
    public void Payment_Constructor_ShouldThrowException_WhenAmountIsNegative()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new CashPayment(-100m, 0m));
    }
    
    [Fact]
    public void Payment_TotalAmount_ShouldIncludeFees()
    {
        // Arrange
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(2), "123", CardType.Visa);
        
        // Act
        decimal total = payment.TotalAmount;
        
        // Assert
        Assert.True(total > payment.Amount); // Should include fee
    }
    
    #endregion
    
    #region CreditCardPayment Tests
    
    [Fact]
    public void CreditCardPayment_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(2), "123", CardType.Visa);
        
        // Assert
        Assert.Equal(100m, payment.Amount);
        Assert.Equal(CardType.Visa, payment.CardType);
        Assert.Equal("John Doe", payment.CardholderName);
    }
    
    [Fact]
    public void CreditCardPayment_PaymentMethod_ShouldReturnCorrectName()
    {
        // Arrange
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(2), "123", CardType.Visa);
        
        // Act
        string method = payment.PaymentMethod;
        
        // Assert
        Assert.Contains("Visa", method);
        Assert.Contains("Credit Card", method);
    }
    
    [Fact]
    public void CreditCardPayment_TransactionFee_ShouldBe2Point9Percent()
    {
        // Arrange
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(2), "123", CardType.Visa);
        
        // Act
        decimal feePercentage = payment.TransactionFeePercentage;
        
        // Assert
        Assert.Equal(2.9m, feePercentage);
    }
    
    [Fact]
    public void CreditCardPayment_ProcessPayment_ShouldSucceed_WithValidCard()
    {
        // Arrange
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(2), "123", CardType.Visa);
        
        // Act
        bool success = payment.ProcessPayment();
        
        // Assert
        Assert.True(success);
        Assert.Equal(PaymentStatus.Completed, payment.Status);
    }
    
    [Fact]
    public void CreditCardPayment_ValidatePayment_ShouldFail_WithExpiredCard()
    {
        // Arrange
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(-1), "123", CardType.Visa);
        
        // Act
        bool isValid = payment.ValidatePayment();
        
        // Assert
        Assert.False(isValid);
    }
    
    [Fact]
    public void CreditCardPayment_MaskedCardNumber_ShouldHideDigits()
    {
        // Arrange
        var payment = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                           DateTime.Now.AddYears(2), "123", CardType.Visa);
        
        // Act
        string masked = payment.MaskedCardNumber;
        
        // Assert
        Assert.Contains("****", masked);
        Assert.EndsWith("1111", masked);
    }
    
    #endregion
    
    #region PayPalPayment Tests
    
    [Fact]
    public void PayPalPayment_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var payment = new PayPalPayment(50m, "user@example.com");
        
        // Assert
        Assert.Equal(50m, payment.Amount);
        Assert.Equal("user@example.com", payment.PayPalEmail);
    }
    
    [Fact]
    public void PayPalPayment_PaymentMethod_ShouldReturnPayPal()
    {
        // Arrange
        var payment = new PayPalPayment(50m, "user@example.com");
        
        // Act
        string method = payment.PaymentMethod;
        
        // Assert
        Assert.Equal("PayPal", method);
    }
    
    [Fact]
    public void PayPalPayment_TransactionFee_ShouldIncludeFixedFee()
    {
        // Arrange
        var payment = new PayPalPayment(100m, "user@example.com");
        
        // Act
        decimal fee = payment.TransactionFee;
        
        // Assert
        Assert.True(fee > 3.5m); // Should be 3.5% + $0.30
    }
    
    [Fact]
    public void PayPalPayment_ProcessPayment_ShouldSucceed_WithValidEmail()
    {
        // Arrange
        var payment = new PayPalPayment(50m, "user@example.com");
        
        // Act
        bool success = payment.ProcessPayment();
        
        // Assert
        Assert.True(success);
        Assert.True(payment.IsAuthorized);
        Assert.NotNull(payment.AuthorizationToken);
    }
    
    [Fact]
    public void PayPalPayment_ValidatePayment_ShouldFail_WithInvalidEmail()
    {
        // Arrange
        var payment = new PayPalPayment(50m, "invalid-email");
        
        // Act
        bool isValid = payment.ValidatePayment();
        
        // Assert
        Assert.False(isValid);
    }
    
    [Fact]
    public void PayPalPayment_ValidatePayment_ShouldFail_WhenAmountExceedsLimit()
    {
        // Arrange
        var payment = new PayPalPayment(15000m, "user@example.com"); // Over $10,000 limit
        
        // Act
        bool isValid = payment.ValidatePayment();
        
        // Assert
        Assert.False(isValid);
    }
    
    #endregion
    
    #region CashPayment Tests
    
    [Fact]
    public void CashPayment_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var payment = new CashPayment(100m, 120m, "USD");
        
        // Assert
        Assert.Equal(100m, payment.Amount);
        Assert.Equal(120m, payment.AmountTendered);
        Assert.Equal("USD", payment.Currency);
    }
    
    [Fact]
    public void CashPayment_Constructor_ShouldThrowException_WhenTenderedLessThanAmount()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new CashPayment(100m, 50m));
    }
    
    [Fact]
    public void CashPayment_Change_ShouldCalculateCorrectly()
    {
        // Arrange
        var payment = new CashPayment(100m, 120m, "USD");
        
        // Act
        decimal change = payment.Change;
        
        // Assert
        Assert.Equal(20m, change);
    }
    
    [Fact]
    public void CashPayment_Change_ShouldBeZero_WithExactAmount()
    {
        // Arrange
        var payment = new CashPayment(100m, 100m, "USD");
        
        // Act
        decimal change = payment.Change;
        
        // Assert
        Assert.Equal(0m, change);
    }
    
    [Fact]
    public void CashPayment_TransactionFee_ShouldBeZero()
    {
        // Arrange
        var payment = new CashPayment(100m, 100m, "USD");
        
        // Act
        decimal fee = payment.TransactionFeePercentage;
        
        // Assert
        Assert.Equal(0m, fee);
    }
    
    [Fact]
    public void CashPayment_ProcessPayment_ShouldSucceed()
    {
        // Arrange
        var payment = new CashPayment(100m, 120m, "USD");
        
        // Act
        bool success = payment.ProcessPayment();
        
        // Assert
        Assert.True(success);
        Assert.True(payment.IsCashVerified);
    }
    
    #endregion
    
    #region BitcoinPayment Tests
    
    [Fact]
    public void BitcoinPayment_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var payment = new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        
        // Assert
        Assert.Equal(200m, payment.Amount);
        Assert.Equal(50000m, payment.ExchangeRate);
    }
    
    [Fact]
    public void BitcoinPayment_BitcoinAmount_ShouldCalculateFromUSD()
    {
        // Arrange & Act
        var payment = new BitcoinPayment(100m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        
        // Act
        decimal btcAmount = payment.BitcoinAmount;
        
        // Assert
        Assert.Equal(0.002m, btcAmount); // 100 / 50000 = 0.002
    }
    
    [Fact]
    public void BitcoinPayment_ProcessPayment_ShouldBroadcastTransaction()
    {
        // Arrange
        var payment = new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        
        // Act
        bool success = payment.ProcessPayment();
        
        // Assert
        Assert.True(success);
        Assert.NotNull(payment.BlockchainTransactionHash);
        Assert.True(payment.Confirmations >= payment.RequiredConfirmations);
    }
    
    [Fact]
    public void BitcoinPayment_IsConfirmed_ShouldReturnTrue_AfterEnoughConfirmations()
    {
        // Arrange
        var payment = new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        payment.ProcessPayment();
        
        // Act
        bool isConfirmed = payment.IsConfirmed;
        
        // Assert
        Assert.True(isConfirmed);
    }
    
    [Fact]
    public void BitcoinPayment_ConvertToBitcoin_ShouldCalculateCorrectly()
    {
        // Arrange
        var payment = new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        
        // Act
        decimal btc = payment.ConvertToBitcoin(100m);
        
        // Assert
        Assert.Equal(0.002m, btc);
    }
    
    [Fact]
    public void BitcoinPayment_ConvertToUSD_ShouldCalculateCorrectly()
    {
        // Arrange
        var payment = new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        
        // Act
        decimal usd = payment.ConvertToUSD(0.002m);
        
        // Assert
        Assert.Equal(100m, usd);
    }
    
    #endregion
    
    #region Polymorphism Tests
    
    [Fact]
    public void Payment_Polymorphism_AllPaymentTypesShouldProcessSuccessfully()
    {
        // Arrange - Create different payment types
        List<Payment> payments = new List<Payment>
        {
            new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                 DateTime.Now.AddYears(2), "123", CardType.Visa),
            new PayPalPayment(50m, "user@example.com"),
            new CashPayment(75m, 100m, "USD"),
            new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m)
        };
        
        // Act - Process all payments polymorphically
        var results = new List<bool>();
        foreach (var payment in payments)
        {
            results.Add(payment.ProcessPayment());
        }
        
        // Assert - All should succeed
        Assert.All(results, result => Assert.True(result));
        Assert.All(payments, p => Assert.Equal(PaymentStatus.Completed, p.Status));
    }
    
    [Fact]
    public void Payment_Polymorphism_TransactionFeesShouldVaryByType()
    {
        // Arrange
        Payment creditCard = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                                   DateTime.Now.AddYears(2), "123", CardType.Visa);
        Payment paypal = new PayPalPayment(100m, "user@example.com");
        Payment cash = new CashPayment(100m, 100m, "USD");
        
        // Act
        decimal creditCardFee = creditCard.TransactionFeePercentage;
        decimal paypalFee = paypal.TransactionFeePercentage;
        decimal cashFee = cash.TransactionFeePercentage;
        
        // Assert
        Assert.Equal(2.9m, creditCardFee);
        Assert.Equal(3.5m, paypalFee);
        Assert.Equal(0m, cashFee);
        
        // All different fees!
        Assert.NotEqual(creditCardFee, paypalFee);
        Assert.NotEqual(paypalFee, cashFee);
    }
    
    [Fact]
    public void Payment_Polymorphism_GetPaymentDetailsShouldReturnDifferentInfo()
    {
        // Arrange
        Payment creditCard = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                                   DateTime.Now.AddYears(2), "123", CardType.Visa);
        Payment paypal = new PayPalPayment(100m, "user@example.com");
        
        // Act
        string creditCardDetails = creditCard.GetPaymentDetails();
        string paypalDetails = paypal.GetPaymentDetails();
        
        // Assert
        Assert.Contains("Card", creditCardDetails);
        Assert.Contains("PayPal", paypalDetails);
        Assert.NotEqual(creditCardDetails, paypalDetails);
    }
    
    #endregion
    
    #region Operator Overloading Tests
    
    [Fact]
    public void Payment_OperatorPlus_ShouldCombineAmounts()
    {
        // Arrange
        Payment payment1 = new CashPayment(100m, 100m);
        Payment payment2 = new CashPayment(50m, 50m);
        
        // Act
        decimal total = payment1 + payment2;
        
        // Assert
        Assert.Equal(150m, total);
    }
    
    [Fact]
    public void Payment_OperatorGreaterThan_ShouldCompareAmounts()
    {
        // Arrange
        Payment largePayment = new CashPayment(100m, 100m);
        Payment smallPayment = new CashPayment(50m, 50m);
        
        // Act
        bool isGreater = largePayment > smallPayment;
        
        // Assert
        Assert.True(isGreater);
    }
    
    [Fact]
    public void Payment_OperatorLessThan_ShouldCompareAmounts()
    {
        // Arrange
        Payment smallPayment = new CashPayment(50m, 50m);
        Payment largePayment = new CashPayment(100m, 100m);
        
        // Act
        bool isLess = smallPayment < largePayment;
        
        // Assert
        Assert.True(isLess);
    }
    
    [Fact]
    public void Payment_OperatorOverloading_WorksWithDifferentPaymentTypes()
    {
        // Arrange
        Payment creditCard = new CreditCardPayment(100m, "4111111111111111", "John Doe",
                                                   DateTime.Now.AddYears(2), "123", CardType.Visa);
        Payment paypal = new PayPalPayment(50m, "user@example.com");
        Payment bitcoin = new BitcoinPayment(75m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 50000m);
        
        // Act
        decimal total = creditCard + paypal + bitcoin;
        bool creditCardIsLargest = (creditCard > paypal) && (creditCard > bitcoin);
        
        // Assert
        Assert.Equal(225m, total);
        Assert.True(creditCardIsLargest);
    }
    
    #endregion
    
    #region Refund Tests
    
    [Fact]
    public void Payment_Refund_ShouldWork_AfterCompletion()
    {
        // Arrange
        var payment = new CashPayment(100m, 100m);
        payment.ProcessPayment();
        
        // Act
        bool refundSuccess = payment.Refund(50m);
        
        // Assert
        Assert.True(refundSuccess);
        Assert.Equal(PaymentStatus.Refunded, payment.Status);
    }
    
    [Fact]
    public void Payment_Refund_ShouldFail_BeforeCompletion()
    {
        // Arrange
        var payment = new CashPayment(100m, 100m);
        // Don't process payment
        
        // Act
        bool refundSuccess = payment.Refund(50m);
        
        // Assert
        Assert.False(refundSuccess);
    }
    
    [Fact]
    public void Payment_Refund_ShouldFail_WhenAmountExceedsOriginal()
    {
        // Arrange
        var payment = new CashPayment(100m, 100m);
        payment.ProcessPayment();
        
        // Act
        bool refundSuccess = payment.Refund(150m);
        
        // Assert
        Assert.False(refundSuccess);
    }
    
    #endregion
}