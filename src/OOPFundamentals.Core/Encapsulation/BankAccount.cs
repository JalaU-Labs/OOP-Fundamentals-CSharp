namespace OOPFundamentals.Core.Encapsulation;

/// <summary>
/// Represents a bank account demonstrating encapsulation principles.
/// This class shows how to protect data through private fields and expose it
/// through controlled public methods and properties.
/// </summary>
/// <remarks>
/// Encapsulation is demonstrated by:
/// - Private fields (_balance, _accountNumber) that cannot be accessed directly
/// - Public properties with validation (AccountHolder)
/// - Public methods that control how balance is modified (Deposit, Withdraw)
/// - Read-only access to balance through GetBalance() method
/// </remarks>
public class BankAccount
{
    #region Private Fields
    
    /// <summary>
    /// Private field storing the account balance.
    /// Cannot be accessed directly from outside the class.
    /// </summary>
    private decimal _balance;
    
    /// <summary>
    /// Private readonly field for account number.
    /// Can only be set during construction.
    /// </summary>
    private readonly string _accountNumber;
    
    /// <summary>
    /// Private field for tracking if account is active
    /// </summary>
    private bool _isActive;
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets the account number. This is read-only from outside the class.
    /// </summary>
    public string AccountNumber => _accountNumber;
    
    /// <summary>
    /// Gets or sets the account holder's name with validation.
    /// Demonstrates property encapsulation with validation logic.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when name is null or empty</exception>
    public string AccountHolder { get; private set; }
    
    /// <summary>
    /// Gets the account creation date. Read-only property.
    /// </summary>
    public DateTime CreatedDate { get; }
    
    /// <summary>
    /// Gets or sets whether the account is active.
    /// Demonstrates encapsulation with business logic in setter.
    /// </summary>
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (!value && _balance > 0)
            {
                throw new InvalidOperationException(
                    "Cannot deactivate account with positive balance. Withdraw all funds first.");
            }
            _isActive = value;
        }
    }
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the BankAccount class.
    /// </summary>
    /// <param name="accountNumber">The unique account number</param>
    /// <param name="accountHolder">The name of the account holder</param>
    /// <param name="initialDeposit">Optional initial deposit amount (default: 0)</param>
    /// <exception cref="ArgumentException">Thrown when account number or holder name is invalid</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when initial deposit is negative</exception>
    public BankAccount(string accountNumber, string accountHolder, decimal initialDeposit = 0)
    {
        // Validate account number
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));
        }
        
        // Validate account holder
        if (string.IsNullOrWhiteSpace(accountHolder))
        {
            throw new ArgumentException("Account holder name cannot be null or empty.", nameof(accountHolder));
        }
        
        // Validate initial deposit
        if (initialDeposit < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(initialDeposit), 
                "Initial deposit cannot be negative.");
        }
        
        _accountNumber = accountNumber;
        AccountHolder = accountHolder;
        _balance = initialDeposit;
        CreatedDate = DateTime.Now;
        _isActive = true;
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// Deposits money into the account.
    /// Demonstrates encapsulation by controlling how balance is modified.
    /// </summary>
    /// <param name="amount">The amount to deposit</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative or zero</exception>
    /// <exception cref="InvalidOperationException">Thrown when account is not active</exception>
    public void Deposit(decimal amount)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Cannot deposit to an inactive account.");
        }
        
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), 
                "Deposit amount must be greater than zero.");
        }
        
        _balance += amount;
        Console.WriteLine($"Deposited ${amount:N2}. New balance: ${_balance:N2}");
    }
    
    /// <summary>
    /// Withdraws money from the account.
    /// Demonstrates encapsulation with validation and business rules.
    /// </summary>
    /// <param name="amount">The amount to withdraw</param>
    /// <returns>True if withdrawal was successful, false otherwise</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative or zero</exception>
    /// <exception cref="InvalidOperationException">Thrown when account is not active or insufficient funds</exception>
    public bool Withdraw(decimal amount)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Cannot withdraw from an inactive account.");
        }
        
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), 
                "Withdrawal amount must be greater than zero.");
        }
        
        if (amount > _balance)
        {
            Console.WriteLine($"Insufficient funds. Current balance: ${_balance:N2}, Requested: ${amount:N2}");
            return false;
        }
        
        _balance -= amount;
        Console.WriteLine($"Withdrew ${amount:N2}. New balance: ${_balance:N2}");
        return true;
    }
    
    /// <summary>
    /// Gets the current account balance.
    /// Demonstrates read-only access to private data through a method.
    /// </summary>
    /// <returns>The current balance</returns>
    public decimal GetBalance()
    {
        return _balance;
    }
    
    /// <summary>
    /// Transfers money to another account.
    /// Demonstrates encapsulation by using public methods to maintain consistency.
    /// </summary>
    /// <param name="destinationAccount">The account to transfer money to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>True if transfer was successful, false otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when destination account is null</exception>
    public bool Transfer(BankAccount destinationAccount, decimal amount)
    {
        if (destinationAccount == null)
        {
            throw new ArgumentNullException(nameof(destinationAccount), 
                "Destination account cannot be null.");
        }
        
        if (destinationAccount == this)
        {
            throw new InvalidOperationException("Cannot transfer to the same account.");
        }
        
        Console.WriteLine($"\nTransferring ${amount:N2} from {AccountNumber} to {destinationAccount.AccountNumber}");
        
        // Use existing methods to maintain encapsulation
        if (Withdraw(amount))
        {
            try
            {
                destinationAccount.Deposit(amount);
                Console.WriteLine("Transfer completed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Rollback: restore the withdrawn amount if deposit fails
                _balance += amount;
                Console.WriteLine($"Transfer failed: {ex.Message}. Transaction rolled back.");
                return false;
            }
        }
        
        Console.WriteLine("Transfer failed due to insufficient funds.");
        return false;
    }
    
    /// <summary>
    /// Gets a summary of the account information.
    /// </summary>
    /// <returns>A formatted string with account details</returns>
    public string GetAccountSummary()
    {
        return $"""
                Account Summary
                ===============
                Account Number: {AccountNumber}
                Account Holder: {AccountHolder}
                Balance: ${_balance:N2}
                Status: {(_isActive ? "Active" : "Inactive")}
                Created: {CreatedDate:yyyy-MM-dd}
                """;
    }
    
    #endregion
    
    #region Private Helper Methods
    
    /// <summary>
    /// Private helper method to validate transaction amount.
    /// Demonstrates internal encapsulation - used only within the class.
    /// </summary>
    /// <param name="amount">Amount to validate</param>
    /// <returns>True if amount is valid</returns>
    private bool IsValidAmount(decimal amount)
    {
        return amount > 0 && amount <= 1_000_000; // Max transaction limit
    }
    
    #endregion
}