# Polymorphism (Polimorfismo)

## Overview

**Polymorphism** is the fourth and final pillar of Object-Oriented Programming. It means "many forms" and allows objects of different types to be treated through a common interface while maintaining their specific behavior. Polymorphism is what makes OOP truly powerful and flexible.

## Key Concepts

### 1. **Runtime Polymorphism (Dynamic)**
Method calls are resolved at runtime based on the actual object type.

### 2. **Compile-time Polymorphism (Static)**
Method calls are resolved at compile time (method overloading, operator overloading).

### 3. **Interface Polymorphism**
Different classes implementing the same interface can be used interchangeably.

### 4. **Subtype Polymorphism**
A derived class object can be treated as its base class type.

## Types of Polymorphism in C#

### 1. Method Overriding (Runtime Polymorphism)

The most common form - virtual methods overridden in derived classes.

```csharp
public abstract class Payment
{
    public abstract bool ProcessPayment();  // Different for each payment type
}

public class CreditCardPayment : Payment
{
    public override bool ProcessPayment()
    {
        // Credit card specific processing
    }
}

public class PayPalPayment : Payment
{
    public override bool ProcessPayment()
    {
        // PayPal specific processing
    }
}
```

### 2. Method Overloading (Compile-time Polymorphism)

Same method name, different parameters.

```csharp
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public double Add(double a, double b) => a + b;
    public int Add(int a, int b, int c) => a + b + c;
}
```

### 3. Operator Overloading (Compile-time Polymorphism)

Redefining operators for custom types.

```csharp
public class Payment
{
    public static decimal operator +(Payment p1, Payment p2)
    {
        return p1.Amount + p2.Amount;
    }
    
    public static bool operator >(Payment p1, Payment p2)
    {
        return p1.Amount > p2.Amount;
    }
}
```

### 4. Interface Polymorphism

Different classes implementing the same interface.

```csharp
public interface INotifiable
{
    void SendNotification(string message);
}

public class CreditCardPayment : Payment, INotifiable
{
    public void SendNotification(string message)
    {
        // Send via email
    }
}

public class BitcoinPayment : Payment, INotifiable
{
    public void SendNotification(string message)
    {
        // Send via blockchain event
    }
}
```

## Examples in This Project

### Payment System (Core Example)

The Payment hierarchy demonstrates all forms of polymorphism:

```csharp
// Base class with abstract methods
public abstract class Payment
{
    public abstract bool ProcessPayment();  // Runtime polymorphism
    public abstract bool ValidatePayment();
    public abstract string GetPaymentDetails();
}
```

### Different Payment Implementations

#### 1. CreditCardPayment
```csharp
public class CreditCardPayment : Payment
{
    public override bool ProcessPayment()
    {
        // 1. Validate card
        // 2. Contact payment gateway
        // 3. Authorize card
        // 4. Capture payment
        return true;
    }
}
```

#### 2. PayPalPayment
```csharp
public class PayPalPayment : Payment
{
    public override bool ProcessPayment()
    {
        // 1. Validate PayPal account
        // 2. Redirect to PayPal OAuth
        // 3. Request authorization
        // 4. Capture payment
        return true;
    }
}
```

#### 3. CashPayment
```csharp
public class CashPayment : Payment
{
    public override bool ProcessPayment()
    {
        // 1. Verify cash authenticity
        // 2. Count cash
        // 3. Provide change
        // 4. Place in register
        return true;
    }
}
```

#### 4. BitcoinPayment
```csharp
public class BitcoinPayment : Payment
{
    public override bool ProcessPayment()
    {
        // 1. Validate wallet address
        // 2. Broadcast to blockchain
        // 3. Wait for confirmations
        // 4. Mark as confirmed
        return true;
    }
}
```

### Polymorphism in Action

```csharp
// Create different payment types
List<Payment> payments = new List<Payment>
{
    new CreditCardPayment(100m, "4111111111111111", "John Doe", 
                         DateTime.Now.AddYears(2), "123", CardType.Visa),
    new PayPalPayment(50m, "user@example.com"),
    new CashPayment(25m, 30m, "USD"),
    new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 45000m)
};

// Process all payments polymorphically
foreach (Payment payment in payments)
{
    // The SAME method call, but different behavior for each type!
    // This is runtime polymorphism in action
    bool success = payment.ProcessPayment();  // Polymorphic call
    
    if (success)
    {
        payment.SendReceipt("customer@example.com");
    }
}

// Output will show completely different processing for each payment type:
// - Credit card: Gateway authorization
// - PayPal: OAuth redirect
// - Cash: Physical cash handling
// - Bitcoin: Blockchain confirmation
```

## Benefits of Polymorphism

### 1. **Flexibility**
Add new payment types without changing existing code.

```csharp
// Adding a new payment type
public class ApplePayPayment : Payment
{
    public override bool ProcessPayment()
    {
        // Apple Pay specific logic
    }
}

// Existing code works without modification!
payments.Add(new ApplePayPayment(75m));
```

### 2. **Maintainability**
Changes are localized to specific classes.

```csharp
// If PayPal changes their API:
// - Only modify PayPalPayment class
// - All other code remains unchanged
```

### 3. **Code Reusability**
Write code once that works with many types.

```csharp
public decimal ProcessAllPayments(List<Payment> payments)
{
    decimal total = 0;
    foreach (Payment p in payments)
    {
        if (p.ProcessPayment())  // Works for ALL payment types!
        {
            total += p.Amount;
        }
    }
    return total;
}
```

### 4. **Extensibility**
Easy to add new functionality without breaking existing code.

## Real-World Analogy

Think of a **universal remote control**:

- **Interface**: Power button, volume buttons, channel buttons
- **Polymorphism**: Same buttons work for TV, DVD player, sound system
- **Different Behavior**: Power button turns on different devices in different ways
  - TV: Powers on screen and tuner
  - DVD Player: Spins up disc drive
  - Sound System: Activates amplifier

Same interface (buttons), different implementations (devices), same usage pattern!

Similarly in our payment system:
- **Interface**: ProcessPayment() method
- **Polymorphism**: Works for all payment types
- **Different Behavior**:
  - Credit Card: Gateway processing
  - PayPal: OAuth flow
  - Cash: Physical handling
  - Bitcoin: Blockchain confirmation

## Polymorphic Features in This Project

### 1. Virtual Properties
```csharp
public abstract class Payment
{
    public virtual string PaymentMethod => "Generic Payment";
    public virtual decimal TransactionFeePercentage => 0.0m;
}

public class CreditCardPayment : Payment
{
    public override string PaymentMethod => "Credit Card";
    public override decimal TransactionFeePercentage => 2.9m;
}

public class PayPalPayment : Payment
{
    public override string PaymentMethod => "PayPal";
    public override decimal TransactionFeePercentage => 3.5m;
}
```

### 2. Template Method Pattern
```csharp
public abstract class Payment
{
    // Template method - defines algorithm skeleton
    public string GetPaymentSummary()
    {
        return $"""
                Transaction: {TransactionId}
                Method: {PaymentMethod}        // Polymorphic
                Amount: ${Amount}
                {GetPaymentDetails()}         // Polymorphic
                """;
    }
    
    public abstract string GetPaymentDetails();  // Implemented differently
}
```

### 3. Operator Overloading
```csharp
Payment card = new CreditCardPayment(100m, ...);
Payment paypal = new PayPalPayment(50m, ...);

// Use + operator to combine amounts
decimal total = card + paypal;  // Returns 150

// Use comparison operators
if (card > paypal)  // Compares amounts
{
    Console.WriteLine("Card payment is larger");
}
```

### 4. Interface Polymorphism
```csharp
public interface INotifiable
{
    void SendNotification(string message);
}

// Different implementations
public class CreditCardPayment : Payment, INotifiable
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"📧 Email notification: {message}");
    }
}

public class BitcoinPayment : Payment, INotifiable
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"⛓️ Blockchain event: {message}");
    }
}

// Use polymorphically
INotifiable notifiable = new CreditCardPayment(...);
notifiable.SendNotification("Payment processed");  // Email

notifiable = new BitcoinPayment(...);
notifiable.SendNotification("Payment processed");  // Blockchain
```

## Compile-time vs Runtime Polymorphism

### Compile-time (Static)

#### Method Overloading
```csharp
public class Calculator
{
    public int Add(int a, int b) => a + b;              // Two ints
    public double Add(double a, double b) => a + b;     // Two doubles
    public int Add(int a, int b, int c) => a + b + c;   // Three ints
}

var calc = new Calculator();
calc.Add(5, 3);        // Calls first method (int, int)
calc.Add(5.5, 3.2);    // Calls second method (double, double)
calc.Add(1, 2, 3);     // Calls third method (int, int, int)
```

#### Operator Overloading
```csharp
Payment p1 = new CashPayment(100m, 100m);
Payment p2 = new CashPayment(50m, 50m);

decimal total = p1 + p2;  // Compiler resolves to operator+ method
bool isGreater = p1 > p2; // Compiler resolves to operator> method
```

### Runtime (Dynamic)

#### Method Overriding
```csharp
Payment payment;  // Base type reference

// At runtime, determine which ProcessPayment to call
if (useCreditCard)
    payment = new CreditCardPayment(...);
else if (usePayPal)
    payment = new PayPalPayment(...);
else
    payment = new CashPayment(...);

// Runtime determines which implementation to call
payment.ProcessPayment();  // Resolved at runtime!
```

## Polymorphism and SOLID Principles

### Liskov Substitution Principle (LSP)
Derived classes must be substitutable for their base classes.

```csharp
public void ProcessPayment(Payment payment)  // Accepts base type
{
    payment.ProcessPayment();  // Works with ANY derived type
}

// All of these work:
ProcessPayment(new CreditCardPayment(...));  ✅
ProcessPayment(new PayPalPayment(...));      ✅
ProcessPayment(new CashPayment(...));        ✅
ProcessPayment(new BitcoinPayment(...));     ✅
```

### Open/Closed Principle (OCP)
Open for extension, closed for modification.

```csharp
// Adding new payment type doesn't modify existing code
public class ApplePayPayment : Payment  // Extension
{
    public override bool ProcessPayment() { ... }
}

// Existing code still works without modification!
```

## Common Patterns Using Polymorphism

### 1. Strategy Pattern
```csharp
public interface IPaymentStrategy
{
    bool Process(decimal amount);
}

public class CreditCardStrategy : IPaymentStrategy { }
public class PayPalStrategy : IPaymentStrategy { }

public class PaymentProcessor
{
    private IPaymentStrategy _strategy;
    
    public void SetStrategy(IPaymentStrategy strategy)
    {
        _strategy = strategy;  // Polymorphic assignment
    }
    
    public bool Process(decimal amount)
    {
        return _strategy.Process(amount);  // Polymorphic call
    }
}
```

### 2. Factory Pattern
```csharp
public class PaymentFactory
{
    public static Payment CreatePayment(string type, decimal amount)
    {
        return type switch
        {
            "card" => new CreditCardPayment(amount, ...),
            "paypal" => new PayPalPayment(amount, ...),
            "cash" => new CashPayment(amount, ...),
            "bitcoin" => new BitcoinPayment(amount, ...),
            _ => throw new ArgumentException("Unknown payment type")
        };
    }
}

// Usage
Payment payment = PaymentFactory.CreatePayment("card", 100m);
payment.ProcessPayment();  // Polymorphic - works for any type returned
```

## Testing Polymorphism

See `tests/OOPFundamentals.Tests/PolymorphismTests.cs` for:
- Polymorphic method calls
- Interface polymorphism tests
- Operator overloading tests
- Runtime type behavior tests

## Comparison with Java

| Feature | C# | Java |
|---------|----|----- |
| Virtual methods | Explicit `virtual` keyword | All non-final methods virtual |
| Override | `override` keyword required | `@Override` annotation (optional) |
| Operator overloading | ✅ Supported | ❌ Not supported |
| Properties | ✅ Native support | ❌ Use getters/setters |
| Multiple interfaces | ✅ Yes | ✅ Yes |
| Abstract classes | ✅ Yes | ✅ Yes |

## Best Practices

1. ✅ **Use polymorphism for behavior variation**
   - Different ProcessPayment implementations ✅

2. ✅ **Design for extension**
   - Make methods virtual if they might need override

3. ✅ **Follow LSP**
   - Derived classes should work wherever base class works

4. ✅ **Prefer composition over inheritance when appropriate**
   - Use interfaces for capabilities (INotifiable)

5. ✅ **Use abstract classes for IS-A relationships**
   - CreditCardPayment IS-A Payment ✅

6. ✅ **Use interfaces for CAN-DO capabilities**
   - Payment CAN-NOTIFY via INotifiable ✅

7. ❌ **Don't override just to override**
   - Only override when behavior actually needs to change

8. ✅ **Document polymorphic behavior**
   - Explain how derived classes should implement methods

## Further Reading

- [Microsoft Docs: Polymorphism](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [Design Patterns using Polymorphism](https://refactoring.guru/design-patterns)
- [Liskov Substitution Principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle)

---

**Polymorphism is the culmination of OOP** - it ties together encapsulation, abstraction, and inheritance to create flexible, maintainable, and extensible code. It's what makes object-oriented programming truly powerful!
