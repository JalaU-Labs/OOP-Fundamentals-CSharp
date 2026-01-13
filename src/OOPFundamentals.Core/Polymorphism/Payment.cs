namespace OOPFundamentals.Core.Polymorphism;

/// <summary>
/// Abstract base class representing a payment method.
/// Demonstrates polymorphism by providing a common interface for different payment types.
/// </summary>
/// <remarks>
/// Polymorphism is demonstrated through:
/// - Abstract methods that are implemented differently by each payment type
/// - Virtual methods with default behavior that can be overridden
/// - A common interface allowing different payment types to be treated uniformly
/// - Runtime polymorphism where the actual method called depends on the object type
/// </remarks>
public abstract class Payment
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the payment amount.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Gets the unique payment transaction ID.
    /// </summary>
    public string TransactionId { get; }
    
    /// <summary>
    /// Gets the payment date and time.
    /// </summary>
    public DateTime PaymentDate { get; }
    
    /// <summary>
    /// Gets the payment status.
    /// </summary>
    public PaymentStatus Status { get; protected set; }
    
    /// <summary>
    /// Gets the payment method type name.
    /// Virtual property that derived classes can override.
    /// </summary>
    public virtual string PaymentMethod => "Generic Payment";
    
    /// <summary>
    /// Gets the transaction fee percentage.
    /// Virtual property that different payment methods override with their rates.
    /// </summary>
    public virtual decimal TransactionFeePercentage => 0.0m;
    
    /// <summary>
    /// Gets the calculated transaction fee.
    /// </summary>
    public decimal TransactionFee => Amount * (TransactionFeePercentage / 100);
    
    /// <summary>
    /// Gets the total amount including fees.
    /// </summary>
    public decimal TotalAmount => Amount + TransactionFee;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Payment class.
    /// Protected constructor - can only be called by derived classes.
    /// </summary>
    /// <param name="amount">The payment amount</param>
    protected Payment(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), 
                "Payment amount must be positive.");
        }
        
        Amount = amount;
        TransactionId = GenerateTransactionId();
        PaymentDate = DateTime.Now;
        Status = PaymentStatus.Pending;
        
        Console.WriteLine($"[Payment Constructor] Creating {PaymentMethod} for ${Amount:N2}");
    }
    
    #endregion
    
    #region Abstract Methods
    
    /// <summary>
    /// Processes the payment.
    /// Abstract method - each payment type implements this differently.
    /// This is the core of runtime polymorphism.
    /// </summary>
    /// <returns>True if payment was successful, false otherwise</returns>
    /// <remarks>
    /// Implementation varies by payment type:
    /// - Credit card: Process through payment gateway
    /// - PayPal: Redirect to PayPal for authorization
    /// - Cash: Mark as received
    /// - Bitcoin: Wait for blockchain confirmation
    /// </remarks>
    public abstract bool ProcessPayment();
    
    /// <summary>
    /// Validates the payment details.
    /// Abstract method - validation rules differ by payment type.
    /// </summary>
    /// <returns>True if payment details are valid</returns>
    public abstract bool ValidatePayment();
    
    /// <summary>
    /// Gets payment-specific details.
    /// Abstract method - each payment type has different details to display.
    /// </summary>
    /// <returns>A string with payment-specific information</returns>
    public abstract string GetPaymentDetails();
    
    #endregion
    
    #region Virtual Methods
    
    /// <summary>
    /// Refunds the payment.
    /// Virtual method with default implementation that can be overridden.
    /// </summary>
    /// <param name="refundAmount">Amount to refund</param>
    /// <returns>True if refund was successful</returns>
    public virtual bool Refund(decimal refundAmount)
    {
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot refund - payment not completed.");
            return false;
        }
        
        if (refundAmount <= 0 || refundAmount > Amount)
        {
            Console.WriteLine($"Invalid refund amount. Must be between $0 and ${Amount:N2}");
            return false;
        }
        
        Status = PaymentStatus.Refunded;
        Console.WriteLine($"Refund processed: ${refundAmount:N2} via {PaymentMethod}");
        return true;
    }
    
    /// <summary>
    /// Cancels the payment.
    /// Virtual method that can be overridden for payment-specific cancellation logic.
    /// </summary>
    /// <returns>True if cancellation was successful</returns>
    public virtual bool Cancel()
    {
        if (Status == PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot cancel - payment already completed. Use Refund instead.");
            return false;
        }
        
        if (Status == PaymentStatus.Cancelled)
        {
            Console.WriteLine("Payment already cancelled.");
            return false;
        }
        
        Status = PaymentStatus.Cancelled;
        Console.WriteLine($"Payment cancelled: {TransactionId}");
        return true;
    }
    
    /// <summary>
    /// Sends a receipt for the payment.
    /// Virtual method with default email receipt behavior.
    /// </summary>
    /// <param name="email">Email address to send receipt to</param>
    public virtual void SendReceipt(string email)
    {
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot send receipt - payment not completed.");
            return;
        }
        
        Console.WriteLine($"\n📧 Sending receipt to: {email}");
        Console.WriteLine($"Transaction ID: {TransactionId}");
        Console.WriteLine($"Amount: ${Amount:N2}");
        Console.WriteLine($"Method: {PaymentMethod}");
        Console.WriteLine($"Date: {PaymentDate:yyyy-MM-dd HH:mm}");
        Console.WriteLine("Receipt sent successfully!\n");
    }
    
    #endregion
    
    #region Concrete Methods
    
    /// <summary>
    /// Gets the complete payment summary.
    /// Template method that uses abstract and virtual methods.
    /// </summary>
    /// <returns>A formatted payment summary</returns>
    public string GetPaymentSummary()
    {
        return $"""
                Payment Summary
                ===============
                Transaction ID: {TransactionId}
                Method: {PaymentMethod}
                Status: {Status}
                Date: {PaymentDate:yyyy-MM-dd HH:mm:ss}
                Amount: ${Amount:N2}
                Transaction Fee: ${TransactionFee:N2} ({TransactionFeePercentage}%)
                Total: ${TotalAmount:N2}
                
                {GetPaymentDetails()}
                """;
    }
    
    /// <summary>
    /// Displays the payment information to the console.
    /// </summary>
    public void DisplayPaymentInfo()
    {
        Console.WriteLine($"\n{GetPaymentSummary()}\n");
    }
    
    /// <summary>
    /// Checks if the payment has been completed.
    /// </summary>
    /// <returns>True if payment is completed</returns>
    public bool IsCompleted()
    {
        return Status == PaymentStatus.Completed;
    }
    
    /// <summary>
    /// Checks if the payment can be refunded.
    /// </summary>
    /// <returns>True if payment can be refunded</returns>
    public bool CanRefund()
    {
        return Status == PaymentStatus.Completed;
    }
    
    #endregion
    
    #region Protected Methods
    
    /// <summary>
    /// Protected method to mark payment as completed.
    /// Can be called by derived classes during payment processing.
    /// </summary>
    protected void MarkAsCompleted()
    {
        Status = PaymentStatus.Completed;
        Console.WriteLine($"✅ Payment completed: {TransactionId}");
    }
    
    /// <summary>
    /// Protected method to mark payment as failed.
    /// Can be called by derived classes if payment processing fails.
    /// </summary>
    /// <param name="reason">Reason for failure</param>
    protected void MarkAsFailed(string reason)
    {
        Status = PaymentStatus.Failed;
        Console.WriteLine($"❌ Payment failed: {reason}");
    }
    
    /// <summary>
    /// Protected method to log payment activity.
    /// </summary>
    /// <param name="message">Log message</param>
    protected void LogPaymentActivity(string message)
    {
        Console.WriteLine($"[{TransactionId}] {message}");
    }
    
    #endregion
    
    #region Private Methods
    
    /// <summary>
    /// Generates a unique transaction ID.
    /// </summary>
    /// <returns>A unique transaction ID</returns>
    private string GenerateTransactionId()
    {
        return $"TXN-{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";
    }
    
    #endregion
    
    #region Operator Overloading (Polymorphism)
    
    /// <summary>
    /// Overloads the + operator to combine two payments.
    /// Demonstrates operator overloading as a form of polymorphism.
    /// </summary>
    /// <param name="payment1">First payment</param>
    /// <param name="payment2">Second payment</param>
    /// <returns>Combined amount (returns as the first payment type)</returns>
    public static decimal operator +(Payment payment1, Payment payment2)
    {
        return payment1.Amount + payment2.Amount;
    }

    /// <summary>
    /// Overloads the + operator to add a payment amount to a decimal.
    /// Allows chaining additions like payment1 + payment2 + payment3.
    /// </summary>
    public static decimal operator +(decimal amount, Payment payment)
    {
        return amount + payment.Amount;
    }

    /// <summary>
    /// Overloads the + operator to add a decimal to a payment amount.
    /// </summary>
    public static decimal operator +(Payment payment, decimal amount)
    {
        return payment.Amount + amount;
    }
    
    /// <summary>
    /// Overloads the > operator to compare payment amounts.
    /// </summary>
    public static bool operator >(Payment payment1, Payment payment2)
    {
        return payment1.Amount > payment2.Amount;
    }
    
    /// <summary>
    /// Overloads the < operator to compare payment amounts.
    /// </summary>
    public static bool operator <(Payment payment1, Payment payment2)
    {
        return payment1.Amount < payment2.Amount;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the payment.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{PaymentMethod}: ${Amount:N2} - {Status}";
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current payment.
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if objects are equal</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Payment other)
        {
            return TransactionId == other.TransactionId;
        }
        return false;
    }
    
    /// <summary>
    /// Gets the hash code for the payment.
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        return TransactionId.GetHashCode();
    }
    
    #endregion
}

/// <summary>
/// Enumeration for payment status.
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Payment is pending processing
    /// </summary>
    Pending,
    
    /// <summary>
    /// Payment is being processed
    /// </summary>
    Processing,
    
    /// <summary>
    /// Payment completed successfully
    /// </summary>
    Completed,
    
    /// <summary>
    /// Payment failed
    /// </summary>
    Failed,
    
    /// <summary>
    /// Payment was cancelled
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// Payment was refunded
    /// </summary>
    Refunded
}