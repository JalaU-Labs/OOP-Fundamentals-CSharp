namespace OOPFundamentals.Core.Polymorphism;

/// <summary>
/// Represents a credit card payment.
/// Demonstrates polymorphism by implementing abstract methods with credit card-specific logic.
/// </summary>
/// <remarks>
/// Shows runtime polymorphism: when ProcessPayment() is called on a Payment reference
/// pointing to a CreditCardPayment object, this implementation is executed.
/// </remarks>
public class CreditCardPayment : Payment
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the credit card number (last 4 digits shown).
    /// </summary>
    public string CardNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the cardholder name.
    /// </summary>
    public string CardholderName { get; set; }
    
    /// <summary>
    /// Gets or sets the card expiration date.
    /// </summary>
    public DateTime ExpirationDate { get; set; }
    
    /// <summary>
    /// Gets or sets the CVV code (not stored, just validated).
    /// </summary>
    private string _cvv;
    
    /// <summary>
    /// Gets or sets the card type (Visa, Mastercard, etc.).
    /// </summary>
    public CardType CardType { get; set; }
    
    /// <summary>
    /// Overrides the payment method name.
    /// Demonstrates property polymorphism.
    /// </summary>
    public override string PaymentMethod => $"{CardType} Credit Card";
    
    /// <summary>
    /// Overrides the transaction fee for credit cards (2.9%).
    /// Different payment types have different fees - polymorphism in action!
    /// </summary>
    public override decimal TransactionFeePercentage => 2.9m;
    
    /// <summary>
    /// Gets the masked card number for display.
    /// </summary>
    public string MaskedCardNumber => $"****-****-****-{CardNumber.Substring(Math.Max(0, CardNumber.Length - 4))}";
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the CreditCardPayment class.
    /// </summary>
    /// <param name="amount">Payment amount</param>
    /// <param name="cardNumber">Credit card number</param>
    /// <param name="cardholderName">Cardholder name</param>
    /// <param name="expirationDate">Card expiration date</param>
    /// <param name="cvv">CVV security code</param>
    /// <param name="cardType">Type of card</param>
    public CreditCardPayment(decimal amount, string cardNumber, string cardholderName, 
                            DateTime expirationDate, string cvv, CardType cardType)
        : base(amount)
    {
        CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
        CardholderName = cardholderName ?? throw new ArgumentNullException(nameof(cardholderName));
        ExpirationDate = expirationDate;
        _cvv = cvv;
        CardType = cardType;
        
        Console.WriteLine($"[CreditCardPayment Constructor] {CardType} card ending in {CardNumber.Substring(Math.Max(0, CardNumber.Length - 4))}");
    }
    
    #endregion
    
    #region Abstract Method Implementations
    
    /// <summary>
    /// Processes the credit card payment.
    /// Implements abstract method from Payment class.
    /// This is runtime polymorphism - the actual method called depends on the object type.
    /// </summary>
    /// <returns>True if payment successful</returns>
    public override bool ProcessPayment()
    {
        LogPaymentActivity("Starting credit card payment processing...");
        
        // Step 1: Validate payment
        if (!ValidatePayment())
        {
            MarkAsFailed("Validation failed");
            return false;
        }
        
        // Step 2: Contact payment gateway
        Status = PaymentStatus.Processing;
        Console.WriteLine($"💳 Contacting payment gateway for {CardType}...");
        
        // Simulate payment gateway processing
        Thread.Sleep(500); // Simulate network delay
        
        // Step 3: Authorize card
        Console.WriteLine($"🔒 Authorizing card ending in {CardNumber.Substring(Math.Max(0, CardNumber.Length - 4))}...");
        
        // Simulate authorization (in real world, this would call a payment API)
        bool authorized = SimulateAuthorization();
        
        if (!authorized)
        {
            MarkAsFailed("Card authorization declined");
            return false;
        }
        
        // Step 4: Capture payment
        Console.WriteLine($"💰 Capturing payment of ${Amount:N2}...");
        Console.WriteLine($"💵 Transaction fee: ${TransactionFee:N2} ({TransactionFeePercentage}%)");
        Console.WriteLine($"💸 Total charged: ${TotalAmount:N2}");
        
        // Mark as completed
        MarkAsCompleted();
        
        return true;
    }
    
    /// <summary>
    /// Validates the credit card payment details.
    /// Implements abstract method with credit card-specific validation.
    /// </summary>
    /// <returns>True if validation passes</returns>
    public override bool ValidatePayment()
    {
        Console.WriteLine("Validating credit card details...");
        
        // Validate card number (basic Luhn algorithm check)
        if (!ValidateCardNumber(CardNumber))
        {
            Console.WriteLine("❌ Invalid card number");
            return false;
        }
        
        // Validate expiration date
        if (ExpirationDate < DateTime.Now)
        {
            Console.WriteLine("❌ Card expired");
            return false;
        }
        
        // Validate CVV
        if (string.IsNullOrWhiteSpace(_cvv) || _cvv.Length != 3)
        {
            Console.WriteLine("❌ Invalid CVV");
            return false;
        }
        
        // Validate cardholder name
        if (string.IsNullOrWhiteSpace(CardholderName))
        {
            Console.WriteLine("❌ Invalid cardholder name");
            return false;
        }
        
        Console.WriteLine("✅ Credit card validation passed");
        return true;
    }
    
    /// <summary>
    /// Gets credit card specific payment details.
    /// Implements abstract method.
    /// </summary>
    /// <returns>Credit card payment details</returns>
    public override string GetPaymentDetails()
    {
        return $"""
                Credit Card Details
                ===================
                Card Type: {CardType}
                Card Number: {MaskedCardNumber}
                Cardholder: {CardholderName}
                Expiration: {ExpirationDate:MM/yy}
                Authorization: Approved
                """;
    }
    
    #endregion
    
    #region Virtual Method Overrides
    
    /// <summary>
    /// Overrides the Refund method with credit card-specific refund logic.
    /// Demonstrates polymorphic behavior - refund process differs by payment type.
    /// </summary>
    /// <param name="refundAmount">Amount to refund</param>
    /// <returns>True if refund successful</returns>
    public override bool Refund(decimal refundAmount)
    {
        Console.WriteLine($"\n💳 Processing credit card refund to {MaskedCardNumber}...");
        
        if (!base.Refund(refundAmount))
        {
            return false;
        }
        
        // Credit card specific refund logic
        Console.WriteLine("Contacting card issuer...");
        Thread.Sleep(300);
        Console.WriteLine("Refund posted to card - will appear in 3-5 business days");
        
        return true;
    }
    
    /// <summary>
    /// Overrides Cancel with credit card-specific cancellation.
    /// </summary>
    /// <returns>True if cancellation successful</returns>
    public override bool Cancel()
    {
        Console.WriteLine($"\n💳 Cancelling credit card payment...");
        
        if (!base.Cancel())
        {
            return false;
        }
        
        // Release authorization hold
        Console.WriteLine("Releasing authorization hold on card...");
        Console.WriteLine("Hold will be released within 24 hours");
        
        return true;
    }
    
    #endregion
    
    #region Credit Card Specific Methods
    
    /// <summary>
    /// Validates card number using Luhn algorithm.
    /// </summary>
    /// <param name="cardNumber">Card number to validate</param>
    /// <returns>True if valid</returns>
    private bool ValidateCardNumber(string cardNumber)
    {
        // Remove spaces and dashes
        cardNumber = cardNumber.Replace(" ", "").Replace("-", "");
        
        // Check if all digits
        if (!cardNumber.All(char.IsDigit))
        {
            return false;
        }
        
        // Check length (13-19 digits)
        if (cardNumber.Length < 13 || cardNumber.Length > 19)
        {
            return false;
        }
        
        // Simplified validation (in production, use full Luhn algorithm)
        return true;
    }
    
    /// <summary>
    /// Simulates payment authorization.
    /// In production, this would call a real payment gateway API.
    /// </summary>
    /// <returns>True if authorized</returns>
    private bool SimulateAuthorization()
    {
        // Simulate different outcomes based on amount
        if (Amount > 10000)
        {
            Console.WriteLine("⚠️ Large transaction - additional verification required");
            Thread.Sleep(200);
        }
        
        // Random simulation (in real world, this calls payment processor)
        return true; // For demo purposes, always authorize
    }
    
    /// <summary>
    /// Performs a chargeback (dispute).
    /// Credit card-specific feature.
    /// </summary>
    /// <param name="reason">Reason for chargeback</param>
    public void InitiateChargeback(string reason)
    {
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot initiate chargeback - payment not completed.");
            return;
        }
        
        Console.WriteLine($"\n💳 Initiating chargeback for {MaskedCardNumber}");
        Console.WriteLine($"Reason: {reason}");
        Console.WriteLine("Chargeback request submitted to card issuer");
        Console.WriteLine("Investigation will take 30-90 days");
        
        Status = PaymentStatus.Processing; // Chargeback in progress
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the credit card payment.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{CardType} {MaskedCardNumber}: ${Amount:N2} - {Status}";
    }
    
    #endregion
}

/// <summary>
/// Enumeration for credit card types.
/// </summary>
public enum CardType
{
    /// <summary>
    /// Visa card
    /// </summary>
    Visa,
    
    /// <summary>
    /// Mastercard
    /// </summary>
    Mastercard,
    
    /// <summary>
    /// American Express
    /// </summary>
    AmericanExpress,
    
    /// <summary>
    /// Discover card
    /// </summary>
    Discover,
    
    /// <summary>
    /// Diners Club
    /// </summary>
    DinersClub
}