using OOPFundamentals.Core.Encapsulation;
using Xunit;

namespace OOPFundamentals.Tests;

/// <summary>
/// Unit tests for Encapsulation pillar classes.
/// Tests BankAccount and Person to ensure data protection and validation work correctly.
/// </summary>
public class EncapsulationTests
{
    #region BankAccount Tests
    
    [Fact]
    public void BankAccount_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Assert
        Assert.Equal("ACC-001", account.AccountNumber);
        Assert.Equal("John Doe", account.AccountHolder);
        Assert.Equal(1000m, account.GetBalance());
        Assert.True(account.IsActive);
    }
    
    [Fact]
    public void BankAccount_Constructor_ShouldThrowException_WhenInitialBalanceIsNegative()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new BankAccount("ACC-001", "John Doe", -100m));
    }
    
    [Fact]
    public void BankAccount_Deposit_ShouldIncreaseBalance()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Act
        account.Deposit(500m);
        
        // Assert
        Assert.Equal(1500m, account.GetBalance());
    }
    
    [Fact]
    public void BankAccount_Deposit_ShouldThrowException_WhenAmountIsNegative()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(-100m));
    }
    
    [Fact]
    public void BankAccount_Deposit_ShouldThrowException_WhenAccountIsInactive()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        account.Withdraw(1000m);
        account.IsActive = false;
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => account.Deposit(100m));
    }
    
    [Fact]
    public void BankAccount_Withdraw_ShouldDecreaseBalance()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Act
        bool success = account.Withdraw(300m);
        
        // Assert
        Assert.True(success);
        Assert.Equal(700m, account.GetBalance());
    }
    
    [Fact]
    public void BankAccount_Withdraw_ShouldReturnFalse_WhenInsufficientFunds()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Act
        bool success = account.Withdraw(1500m);
        
        // Assert
        Assert.False(success);
        Assert.Equal(1000m, account.GetBalance()); // Balance unchanged
    }
    
    [Fact]
    public void BankAccount_Withdraw_ShouldThrowException_WhenAmountIsNegative()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(-100m));
    }
    
    [Fact]
    public void BankAccount_Transfer_ShouldTransferFunds_BetweenAccounts()
    {
        // Arrange
        var sourceAccount = new BankAccount("ACC-001", "John Doe", 1000m);
        var destAccount = new BankAccount("ACC-002", "Jane Smith", 500m);
        
        // Act
        bool success = sourceAccount.Transfer(destAccount, 300m);
        
        // Assert
        Assert.True(success);
        Assert.Equal(700m, sourceAccount.GetBalance());
        Assert.Equal(800m, destAccount.GetBalance());
    }
    
    [Fact]
    public void BankAccount_Transfer_ShouldReturnFalse_WhenInsufficientFunds()
    {
        // Arrange
        var sourceAccount = new BankAccount("ACC-001", "John Doe", 1000m);
        var destAccount = new BankAccount("ACC-002", "Jane Smith", 500m);
        
        // Act
        bool success = sourceAccount.Transfer(destAccount, 1500m);
        
        // Assert
        Assert.False(success);
        Assert.Equal(1000m, sourceAccount.GetBalance()); // Unchanged
        Assert.Equal(500m, destAccount.GetBalance());    // Unchanged
    }
    
    [Theory]
    [InlineData(100, 100, 200)]
    [InlineData(500, 250, 750)]
    [InlineData(1000, 1000, 2000)]
    public void BankAccount_MultipleDeposits_ShouldAccumulateCorrectly(
        decimal deposit1, decimal deposit2, decimal expectedBalance)
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 0m);
        
        // Act
        account.Deposit(deposit1);
        account.Deposit(deposit2);
        
        // Assert
        Assert.Equal(expectedBalance, account.GetBalance());
    }
    
    [Fact]
    public void BankAccount_GetAccountSummary_ShouldReturnFormattedString()
    {
        // Arrange
        var account = new BankAccount("ACC-001", "John Doe", 1000m);
        
        // Act
        string summary = account.GetAccountSummary();
        
        // Assert
        Assert.Contains("ACC-001", summary);
        Assert.Contains("John Doe", summary);
        Assert.Contains("1,000.00", summary);
    }
    
    #endregion
    
    #region Person Tests
    
    [Fact]
    public void Person_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Assert
        Assert.Equal("John", person.FirstName);
        Assert.Equal("Doe", person.LastName);
        Assert.Equal(30, person.Age);
        Assert.Equal("john@example.com", person.Email);
    }
    
    [Fact]
    public void Person_FullName_ShouldReturnCombinedName()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act
        string fullName = person.FullName;
        
        // Assert
        Assert.Equal("John Doe", fullName);
    }
    
    [Fact]
    public void Person_BirthYear_ShouldCalculateCorrectly()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        int expectedBirthYear = DateTime.Now.Year - 30;
        
        // Act
        int birthYear = person.BirthYear;
        
        // Assert
        Assert.Equal(expectedBirthYear, birthYear);
    }
    
    [Theory]
    [InlineData(17, false)]
    [InlineData(18, true)]
    [InlineData(25, true)]
    [InlineData(100, true)]
    public void Person_IsAdult_ShouldReturnCorrectValue(int age, bool expectedIsAdult)
    {
        // Arrange
        var person = new Person("John", "Doe", age, "john@example.com");
        
        // Act
        bool isAdult = person.IsAdult;
        
        // Assert
        Assert.Equal(expectedIsAdult, isAdult);
    }
    
    [Fact]
    public void Person_Age_ShouldThrowException_WhenNegative()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => person.Age = -5);
    }
    
    [Fact]
    public void Person_Age_ShouldThrowException_WhenTooHigh()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => person.Age = 151);
    }
    
    [Fact]
    public void Person_Email_ShouldThrowException_WhenInvalid()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => person.Email = "invalid-email");
    }
    
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name@domain.co.uk")]
    [InlineData("first.last+tag@example.com")]
    public void Person_Email_ShouldAcceptValidEmails(string validEmail)
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act
        person.Email = validEmail;
        
        // Assert
        Assert.Equal(validEmail, person.Email);
    }
    
    [Fact]
    public void Person_CelebrateBirthday_ShouldIncreaseAge()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act
        person.CelebrateBirthday();
        
        // Assert
        Assert.Equal(31, person.Age);
    }
    
    [Fact]
    public void Person_UpdateName_ShouldChangeNames()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act
        person.UpdateName("Jane", "Smith");
        
        // Assert
        Assert.Equal("Jane", person.FirstName);
        Assert.Equal("Smith", person.LastName);
        Assert.Equal("Jane Smith", person.FullName);
    }
    
    [Fact]
    public void Person_Introduce_ShouldReturnFormattedIntroduction()
    {
        // Arrange
        var person = new Person("John", "Doe", 30, "john@example.com");
        
        // Act
        string introduction = person.Introduce();
        
        // Assert
        Assert.Contains("John Doe", introduction);
        Assert.Contains("30", introduction);
    }
    
    [Fact]
    public void Person_CanPerformAdultActivity_ShouldReturnTrue_WhenAdult()
    {
        // Arrange
        var person = new Person("John", "Doe", 25, "john@example.com");
        
        // Act
        bool canPerform = person.CanPerformAdultActivity("voting");
        
        // Assert
        Assert.True(canPerform);
    }
    
    [Fact]
    public void Person_CanPerformAdultActivity_ShouldReturnFalse_WhenMinor()
    {
        // Arrange
        var person = new Person("John", "Doe", 16, "john@example.com");
        
        // Act
        bool canPerform = person.CanPerformAdultActivity("voting");
        
        // Assert
        Assert.False(canPerform);
    }
    
    [Fact]
    public void Person_Equals_ShouldReturnTrue_WhenSameId()
    {
        // Arrange
        var person1 = new Person("John", "Doe", 30, "john@example.com") { Id = "P-001" };
        var person2 = new Person("Jane", "Smith", 25, "jane@example.com") { Id = "P-001" };
        
        // Act
        bool areEqual = person1.Equals(person2);
        
        // Assert
        Assert.True(areEqual);
    }
    
    [Fact]
    public void Person_Equals_ShouldReturnFalse_WhenDifferentId()
    {
        // Arrange
        var person1 = new Person("John", "Doe", 30, "john@example.com") { Id = "P-001" };
        var person2 = new Person("John", "Doe", 30, "john@example.com") { Id = "P-002" };
        
        // Act
        bool areEqual = person1.Equals(person2);
        
        // Assert
        Assert.False(areEqual);
    }
    
    [Fact]
    public void Person_GetHashCode_ShouldBeSame_WhenSameId()
    {
        // Arrange
        var person1 = new Person("John", "Doe", 30, "john@example.com") { Id = "P-001" };
        var person2 = new Person("Jane", "Smith", 25, "jane@example.com") { Id = "P-001" };
        
        // Act
        int hash1 = person1.GetHashCode();
        int hash2 = person2.GetHashCode();
        
        // Assert
        Assert.Equal(hash1, hash2);
    }
    
    #endregion
}