namespace OOPFundamentals.Core.Polymorphism;

/// <summary>
/// Represents a PayPal payment.
/// Demonstrates polymorphism with a completely different payment processing approach than credit cards.
/// </summary>
/// <remarks>
/// Shows how the same Payment interface can be implemented very differently:
/// - Credit cards: Direct card processing
/// - PayPal: OAuth redirect flow
/// Both use the same base Payment class but with completely different implementations.
/// </remarks>
public class PayPalPayment : Payment
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the PayPal account email.
    /// </summary>
    public string PayPalEmail { get; set; }
    
    /// <summary>
    /// Gets or sets whether the payment is using PayPal balance or linked card.
    /// </summary>
    public PayPalFundingSource FundingSource { get; set; }
    
    /// <summary>
    /// Gets the authorization token from PayPal.
    /// </summary>
    public string? AuthorizationToken { get; private set; }
    
    /// <summary>
    /// Gets whether the user has authorized the payment.
    /// </summary>
    public bool IsAuthorized { get; private set; }
    
    /// <summary>
    /// Overrides the payment method name.
    /// </summary>
    public override string PaymentMethod => "PayPal";
    
    /// <summary>
    /// Overrides the transaction fee for PayPal (3.5% + $0.30).
    /// PayPal has different fee structure than credit cards!
    /// </summary>
    public override decimal TransactionFeePercentage => 3.5m;
    
    /// <summary>
    /// Gets the fixed PayPal transaction fee.
    /// </summary>
    public decimal FixedFee => 0.30m;
    
    /// <summary>
    /// Gets the total transaction fee including fixed fee.
    /// </summary>
    public new decimal TransactionFee => (Amount * (TransactionFeePercentage / 100)) + FixedFee;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the PayPalPayment class.
    /// </summary>
    /// <param name="amount">Payment amount</param>
    /// <param name="paypalEmail">PayPal account email</param>
    /// <param name="fundingSource">Funding source for the payment</param>
    public PayPalPayment(decimal amount, string paypalEmail, PayPalFundingSource fundingSource = PayPalFundingSource.Balance)
        : base(amount)
    {
        PayPalEmail = paypalEmail ?? throw new ArgumentNullException(nameof(paypalEmail));
        FundingSource = fundingSource;
        IsAuthorized = false;
        
        Console.WriteLine($"[PayPalPayment Constructor] PayPal payment for {paypalEmail}");
    }
    
    #endregion
    
    #region Abstract Method Implementations
    
    /// <summary>
    /// Processes the PayPal payment.
    /// Completely different implementation than credit card!
    /// Demonstrates runtime polymorphism.
    /// </summary>
    /// <returns>True if payment successful</returns>
    public override bool ProcessPayment()
    {
        LogPaymentActivity("Starting PayPal payment processing...");
        
        // Step 1: Validate
        if (!ValidatePayment())
        {
            MarkAsFailed("Validation failed");
            return false;
        }
        
        // Step 2: Request user authorization (redirect flow)
        Status = PaymentStatus.Processing;
        Console.WriteLine("\n🔵 Redirecting to PayPal for authorization...");
        Console.WriteLine($"📧 PayPal account: {PayPalEmail}");
        
        // Simulate redirect and authorization
        Thread.Sleep(700); // Simulate user interaction time
        
        bool authorized = RequestAuthorization();
        if (!authorized)
        {
            MarkAsFailed("User cancelled PayPal authorization");
            return false;
        }
        
        // Step 3: Capture payment
        Console.WriteLine($"\n💰 Capturing PayPal payment...");
        Console.WriteLine($"💵 Amount: ${Amount:N2}");
        Console.WriteLine($"💵 PayPal fee: ${TransactionFee:N2} ({TransactionFeePercentage}% + ${FixedFee:N2})");
        Console.WriteLine($"💸 You receive: ${Amount - TransactionFee:N2}");
        Console.WriteLine($"Funding source: {FundingSource}");
        
        // Mark as completed
        MarkAsCompleted();
        Console.WriteLine("Payment received via PayPal ✅");
        
        return true;
    }
    
    /// <summary>
    /// Validates the PayPal payment details.
    /// Different validation than credit cards - demonstrates polymorphism.
    /// </summary>
    /// <returns>True if validation passes</returns>
    public override bool ValidatePayment()
    {
        Console.WriteLine("Validating PayPal payment details...");
        
        // Validate email format
        if (string.IsNullOrWhiteSpace(PayPalEmail) || !PayPalEmail.Contains("@"))
        {
            Console.WriteLine("❌ Invalid PayPal email");
            return false;
        }
        
        // Check amount limits
        if (Amount > 10000)
        {
            Console.WriteLine("❌ Amount exceeds PayPal transaction limit ($10,000)");
            return false;
        }
        
        Console.WriteLine("✅ PayPal validation passed");
        return true;
    }
    
    /// <summary>
    /// Gets PayPal specific payment details.
    /// </summary>
    /// <returns>PayPal payment details</returns>
    public override string GetPaymentDetails()
    {
        return $"""
                PayPal Details
                ==============
                PayPal Email: {PayPalEmail}
                Funding Source: {FundingSource}
                Authorization: {(IsAuthorized ? "Approved" : "Pending")}
                Auth Token: {AuthorizationToken ?? "N/A"}
                Fixed Fee: ${FixedFee:N2}
                """;
    }
    
    #endregion
    
    #region Virtual Method Overrides
    
    /// <summary>
    /// Overrides Refund with PayPal-specific refund logic.
    /// PayPal refunds work differently than credit card refunds!
    /// </summary>
    /// <param name="refundAmount">Amount to refund</param>
    /// <returns>True if refund successful</returns>
    public override bool Refund(decimal refundAmount)
    {
        Console.WriteLine($"\n🔵 Processing PayPal refund to {PayPalEmail}...");
        
        if (!base.Refund(refundAmount))
        {
            return false;
        }
        
        // PayPal specific refund logic
        Console.WriteLine("Initiating PayPal refund API call...");
        Thread.Sleep(300);
        Console.WriteLine($"Refund sent to PayPal account: {PayPalEmail}");
        Console.WriteLine("Refund will be available immediately in PayPal balance");
        
        return true;
    }
    
    /// <summary>
    /// Overrides Cancel with PayPal-specific cancellation.
    /// </summary>
    /// <returns>True if cancellation successful</returns>
    public override bool Cancel()
    {
        Console.WriteLine($"\n🔵 Cancelling PayPal payment...");
        
        if (!base.Cancel())
        {
            return false;
        }
        
        // Void authorization
        if (IsAuthorized)
        {
            Console.WriteLine("Voiding PayPal authorization...");
            IsAuthorized = false;
            AuthorizationToken = null;
        }
        
        return true;
    }
    
    /// <summary>
    /// Overrides SendReceipt to send via PayPal notification system.
    /// </summary>
    /// <param name="email">Email address</param>
    public override void SendReceipt(string email)
    {
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot send receipt - payment not completed.");
            return;
        }
        
        Console.WriteLine($"\n📧 Sending receipt via PayPal notification system...");
        Console.WriteLine($"To: {PayPalEmail}");
        Console.WriteLine($"Amount: ${Amount:N2}");
        Console.WriteLine($"Transaction ID: {TransactionId}");
        Console.WriteLine("PayPal receipt sent! ✅\n");
    }
    
    #endregion
    
    #region PayPal Specific Methods
    
    /// <summary>
    /// Requests authorization from the user via PayPal.
    /// Simulates the OAuth redirect flow.
    /// </summary>
    /// <returns>True if user authorized the payment</returns>
    private bool RequestAuthorization()
    {
        Console.WriteLine("\n=== PayPal Authorization ===");
        Console.WriteLine($"PayPal Account: {PayPalEmail}");
        Console.WriteLine($"Amount to authorize: ${Amount:N2}");
        Console.WriteLine($"Funding: {FundingSource}");
        Console.WriteLine("User reviewing payment details...");
        Console.WriteLine("============================\n");
        
        Thread.Sleep(500);
        
        // Simulate user approval
        IsAuthorized = true;
        AuthorizationToken = GenerateAuthToken();
        
        Console.WriteLine("✅ User authorized payment via PayPal");
        Console.WriteLine($"Authorization token: {AuthorizationToken}");
        
        return true;
    }
    
    /// <summary>
    /// Generates a PayPal authorization token.
    /// </summary>
    /// <returns>Authorization token</returns>
    private string GenerateAuthToken()
    {
        return $"PP-{Guid.NewGuid().ToString().Substring(0, 12).ToUpper()}";
    }
    
    /// <summary>
    /// Sends money to another PayPal account (PayPal-specific feature).
    /// </summary>
    /// <param name="recipientEmail">Recipient's PayPal email</param>
    /// <param name="amount">Amount to send</param>
    public void SendMoneyToFriend(string recipientEmail, decimal amount)
    {
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Complete the payment first before sending money.");
            return;
        }
        
        Console.WriteLine($"\n💸 Sending ${amount:N2} to {recipientEmail} via PayPal...");
        Console.WriteLine("Friends and Family - No fees!");
        Console.WriteLine("Money sent instantly ✅");
    }
    
    /// <summary>
    /// Creates a PayPal subscription.
    /// PayPal-specific recurring payment feature.
    /// </summary>
    /// <param name="interval">Billing interval</param>
    public void CreateSubscription(string interval)
    {
        Console.WriteLine($"\n🔄 Creating PayPal subscription...");
        Console.WriteLine($"Amount: ${Amount:N2} every {interval}");
        Console.WriteLine($"PayPal account: {PayPalEmail}");
        Console.WriteLine("Subscription created ✅");
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the PayPal payment.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"PayPal ({PayPalEmail}): ${Amount:N2} - {Status}";
    }
    
    #endregion
}

/// <summary>
/// Enumeration for PayPal funding sources.
/// </summary>
public enum PayPalFundingSource
{
    /// <summary>
    /// PayPal balance
    /// </summary>
    Balance,
    
    /// <summary>
    /// Bank account linked to PayPal
    /// </summary>
    BankAccount,
    
    /// <summary>
    /// Credit or debit card linked to PayPal
    /// </summary>
    Card,
    
    /// <summary>
    /// PayPal Credit
    /// </summary>
    PayPalCredit
}