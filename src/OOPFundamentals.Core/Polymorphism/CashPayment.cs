namespace OOPFundamentals.Core.Polymorphism;

/// <summary>
/// Represents a cash payment.
/// Demonstrates polymorphism with the simplest payment method - physical cash.
/// Shows how vastly different implementations can share the same interface.
/// </summary>
/// <remarks>
/// Cash payments have:
/// - No transaction fees (unlike cards and PayPal)
/// - No electronic processing
/// - Manual verification
/// - Physical currency exchange
/// Yet they still implement the same Payment interface!
/// </remarks>
public class CashPayment : Payment
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the currency type.
    /// </summary>
    public string Currency { get; set; }
    
    /// <summary>
    /// Gets or sets the amount tendered (amount given by customer).
    /// </summary>
    public decimal AmountTendered { get; set; }
    
    /// <summary>
    /// Gets the change to be returned.
    /// </summary>
    public decimal Change => AmountTendered > Amount ? AmountTendered - Amount : 0;
    
    /// <summary>
    /// Gets or sets the cashier who received the payment.
    /// </summary>
    public string? CashierName { get; set; }
    
    /// <summary>
    /// Gets or sets the register number where payment was received.
    /// </summary>
    public int? RegisterNumber { get; set; }
    
    /// <summary>
    /// Gets whether the cash has been verified (counterfeit check).
    /// </summary>
    public bool IsCashVerified { get; private set; }
    
    /// <summary>
    /// Overrides the payment method name.
    /// </summary>
    public override string PaymentMethod => "Cash";
    
    /// <summary>
    /// Overrides the transaction fee - cash has NO fees!
    /// This is a key difference showing polymorphic behavior.
    /// </summary>
    public override decimal TransactionFeePercentage => 0.0m;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the CashPayment class.
    /// </summary>
    /// <param name="amount">Payment amount</param>
    /// <param name="amountTendered">Amount given by customer</param>
    /// <param name="currency">Currency type (USD, EUR, etc.)</param>
    public CashPayment(decimal amount, decimal amountTendered, string currency = "USD")
        : base(amount)
    {
        if (amountTendered < amount)
        {
            throw new ArgumentException("Amount tendered must be greater than or equal to payment amount.");
        }
        
        AmountTendered = amountTendered;
        Currency = currency;
        IsCashVerified = false;
        
        Console.WriteLine($"[CashPayment Constructor] Cash payment: ${amount:N2} in {currency}");
        
        if (Change > 0)
        {
            Console.WriteLine($"Change due: ${Change:N2}");
        }
    }
    
    #endregion
    
    #region Abstract Method Implementations
    
    /// <summary>
    /// Processes the cash payment.
    /// Completely different from electronic payments - demonstrates polymorphism!
    /// No API calls, no authorization, just physical cash handling.
    /// </summary>
    /// <returns>True if payment successful</returns>
    public override bool ProcessPayment()
    {
        LogPaymentActivity("Processing cash payment...");
        
        // Step 1: Validate
        if (!ValidatePayment())
        {
            MarkAsFailed("Validation failed");
            return false;
        }
        
        // Step 2: Verify cash authenticity
        Status = PaymentStatus.Processing;
        Console.WriteLine("\n💵 Receiving cash payment...");
        Console.WriteLine($"Amount due: ${Amount:N2} {Currency}");
        Console.WriteLine($"Amount tendered: ${AmountTendered:N2} {Currency}");
        
        if (!VerifyCash())
        {
            MarkAsFailed("Cash verification failed - possible counterfeit");
            return false;
        }
        
        // Step 3: Count and accept payment
        Console.WriteLine("💵 Counting cash...");
        Thread.Sleep(300); // Simulate counting time
        
        Console.WriteLine($"✅ Cash received: ${Amount:N2}");
        
        // Step 4: Provide change if needed
        if (Change > 0)
        {
            Console.WriteLine($"💰 Providing change: ${Change:N2}");
            Console.WriteLine($"Change breakdown: {GetChangeBreakdown()}");
        }
        else
        {
            Console.WriteLine("💯 Exact change received - no change needed");
        }
        
        // Step 5: Place in register
        if (RegisterNumber.HasValue)
        {
            Console.WriteLine($"📦 Placing cash in register #{RegisterNumber}");
        }
        
        // Mark as completed
        MarkAsCompleted();
        
        if (!string.IsNullOrEmpty(CashierName))
        {
            Console.WriteLine($"Processed by: {CashierName}");
        }
        
        return true;
    }
    
    /// <summary>
    /// Validates the cash payment details.
    /// Different validation than electronic payments - demonstrates polymorphism.
    /// </summary>
    /// <returns>True if validation passes</returns>
    public override bool ValidatePayment()
    {
        Console.WriteLine("Validating cash payment...");
        
        // Check if amount tendered is sufficient
        if (AmountTendered < Amount)
        {
            Console.WriteLine($"❌ Insufficient cash: ${AmountTendered:N2} < ${Amount:N2}");
            return false;
        }
        
        // Check for reasonable amounts (no payments over $10,000 in cash)
        if (Amount > 10000)
        {
            Console.WriteLine("❌ Cash amount exceeds policy limit ($10,000)");
            Console.WriteLine("Large cash transactions require manager approval");
            return false;
        }
        
        Console.WriteLine("✅ Cash payment validation passed");
        return true;
    }
    
    /// <summary>
    /// Gets cash payment specific details.
    /// </summary>
    /// <returns>Cash payment details</returns>
    public override string GetPaymentDetails()
    {
        string details = $"""
                Cash Payment Details
                ====================
                Currency: {Currency}
                Amount Tendered: ${AmountTendered:N2}
                Change Returned: ${Change:N2}
                Cash Verified: {(IsCashVerified ? "Yes ✅" : "No ⚠️")}
                """;
        
        if (!string.IsNullOrEmpty(CashierName))
        {
            details += $"\nCashier: {CashierName}";
        }
        
        if (RegisterNumber.HasValue)
        {
            details += $"\nRegister: #{RegisterNumber}";
        }
        
        return details;
    }
    
    #endregion
    
    #region Virtual Method Overrides
    
    /// <summary>
    /// Overrides Refund - cash refunds are immediate and different from electronic refunds.
    /// Demonstrates polymorphic refund behavior.
    /// </summary>
    /// <param name="refundAmount">Amount to refund</param>
    /// <returns>True if refund successful</returns>
    public override bool Refund(decimal refundAmount)
    {
        Console.WriteLine($"\n💵 Processing cash refund...");
        
        if (!base.Refund(refundAmount))
        {
            return false;
        }
        
        // Cash refund is immediate - just give money back
        Console.WriteLine($"💵 Counting ${refundAmount:N2} for refund...");
        Console.WriteLine($"Refund breakdown: {GetChangeBreakdown(refundAmount)}");
        Console.WriteLine("💰 Cash refund provided immediately");
        
        return true;
    }
    
    /// <summary>
    /// Overrides Cancel - cancelling cash payment just means not accepting it.
    /// </summary>
    /// <returns>True if cancellation successful</returns>
    public override bool Cancel()
    {
        Console.WriteLine($"\n💵 Cancelling cash transaction...");
        
        if (!base.Cancel())
        {
            return false;
        }
        
        // Return cash to customer
        if (AmountTendered > 0)
        {
            Console.WriteLine($"💰 Returning ${AmountTendered:N2} to customer");
        }
        
        return true;
    }
    
    /// <summary>
    /// Overrides SendReceipt - cash receipts are printed, not emailed.
    /// </summary>
    /// <param name="email">Email address (not used for cash receipts)</param>
    public override void SendReceipt(string email)
    {
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot provide receipt - payment not completed.");
            return;
        }
        
        Console.WriteLine($"\n🖨️ Printing cash receipt...");
        Console.WriteLine("================================");
        Console.WriteLine($"   CASH RECEIPT");
        Console.WriteLine("================================");
        Console.WriteLine($"Date: {PaymentDate:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Transaction: {TransactionId}");
        Console.WriteLine($"Amount: ${Amount:N2} {Currency}");
        Console.WriteLine($"Tendered: ${AmountTendered:N2}");
        Console.WriteLine($"Change: ${Change:N2}");
        
        if (!string.IsNullOrEmpty(CashierName))
        {
            Console.WriteLine($"Cashier: {CashierName}");
        }
        
        Console.WriteLine("================================");
        Console.WriteLine("   Thank you for your business!");
        Console.WriteLine("================================\n");
    }
    
    #endregion
    
    #region Cash Specific Methods
    
    /// <summary>
    /// Verifies the authenticity of the cash (counterfeit check).
    /// Cash-specific security measure.
    /// </summary>
    /// <returns>True if cash is genuine</returns>
    private bool VerifyCash()
    {
        Console.WriteLine("🔍 Verifying cash authenticity...");
        
        // Simulate counterfeit detection
        if (Amount >= 50)
        {
            Console.WriteLine("Using counterfeit detection pen...");
            Thread.Sleep(200);
        }
        
        if (Amount >= 100)
        {
            Console.WriteLine("Checking watermarks and security features...");
            Thread.Sleep(200);
        }
        
        IsCashVerified = true;
        Console.WriteLine("✅ Cash verified as genuine");
        
        return true;
    }
    
    /// <summary>
    /// Gets a breakdown of change in bills and coins.
    /// </summary>
    /// <param name="amount">Amount to break down (uses Change if not specified)</param>
    /// <returns>String representation of change breakdown</returns>
    private string GetChangeBreakdown(decimal? amount = null)
    {
        decimal changeAmount = amount ?? Change;
        
        if (changeAmount == 0)
        {
            return "No change";
        }
        
        // Simple breakdown (in production, calculate actual bills/coins)
        var breakdown = new List<string>();
        
        decimal remaining = changeAmount;
        
        // Bills
        int twenties = (int)(remaining / 20);
        if (twenties > 0)
        {
            breakdown.Add($"{twenties} × $20");
            remaining -= twenties * 20;
        }
        
        int tens = (int)(remaining / 10);
        if (tens > 0)
        {
            breakdown.Add($"{tens} × $10");
            remaining -= tens * 10;
        }
        
        int fives = (int)(remaining / 5);
        if (fives > 0)
        {
            breakdown.Add($"{fives} × $5");
            remaining -= fives * 5;
        }
        
        int ones = (int)(remaining / 1);
        if (ones > 0)
        {
            breakdown.Add($"{ones} × $1");
            remaining -= ones * 1;
        }
        
        // Coins
        if (remaining > 0)
        {
            breakdown.Add($"${remaining:N2} in coins");
        }
        
        return string.Join(", ", breakdown);
    }
    
    /// <summary>
    /// Opens the cash register drawer.
    /// Cash-specific operation.
    /// </summary>
    public void OpenCashDrawer()
    {
        Console.WriteLine("\n💵 Opening cash drawer...");
        Console.WriteLine("*DING* Drawer opened");
    }
    
    /// <summary>
    /// Counts the cash in the register.
    /// Cash-specific operation for end-of-shift reconciliation.
    /// </summary>
    /// <returns>Total cash amount in register</returns>
    public decimal CountRegister()
    {
        Console.WriteLine("\n💰 Counting register cash...");
        Console.WriteLine("Counting bills...");
        Thread.Sleep(500);
        Console.WriteLine("Counting coins...");
        Thread.Sleep(300);
        
        // Simulate count (in production, actual counting)
        decimal total = 1547.83m;
        
        Console.WriteLine($"Total in register: ${total:N2}");
        
        return total;
    }
    
    /// <summary>
    /// Creates a cash deposit slip for bank deposit.
    /// </summary>
    public void CreateDepositSlip()
    {
        Console.WriteLine("\n📝 Creating deposit slip...");
        Console.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd}");
        Console.WriteLine($"Amount: ${Amount:N2}");
        Console.WriteLine("Deposit slip created for bank deposit");
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the cash payment.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"Cash: ${Amount:N2} {Currency} (Tendered: ${AmountTendered:N2}, Change: ${Change:N2}) - {Status}";
    }
    
    #endregion
}