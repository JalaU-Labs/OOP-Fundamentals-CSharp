# Encapsulation (Encapsulamiento)

## Overview

**Encapsulation** is one of the four fundamental pillars of Object-Oriented Programming. It refers to the bundling of data (fields) and methods that operate on that data within a single unit (class), while restricting direct access to some of the object's components.

## Key Concepts

### 1. **Data Hiding**
Private fields and controlled access through public methods and properties.

### 2. **Access Modifiers**
- `private`: Accessible only within the class
- `public`: Accessible from anywhere
- `protected`: Accessible within the class and derived classes
- `internal`: Accessible within the same assembly

### 3. **Properties**
C# properties provide a flexible mechanism to read, write, or compute the values of private fields.

### 4. **Validation**
Encapsulation allows validation logic in setters to maintain object integrity.

## Examples in This Project

### BankAccount Class
Demonstrates encapsulation through:
- **Private balance field**: Cannot be accessed directly
- **Public methods**: Controlled deposit and withdrawal operations
- **Validation**: Ensures business rules are followed
- **Read-only properties**: AccountNumber cannot be changed after creation

```csharp
var account = new BankAccount("ACC001", "John Doe", 1000m);
account.Deposit(500m);              // ✅ Allowed through public method
account.Withdraw(200m);             // ✅ Allowed through public method
decimal balance = account.GetBalance();  // ✅ Read-only access
// account._balance = 5000m;        // ❌ Compilation error - private field
```

### Person Class
Demonstrates property-based encapsulation:
- **Auto-implemented properties**: Simple properties with private setters
- **Calculated properties**: FullName, BirthYear, IsAdult
- **Properties with validation**: Age and Email with validation logic
- **Init-only properties**: Id can only be set during initialization

```csharp
var person = new Person("Alice", "Smith", 25, "alice@example.com") 
{ 
    Id = "P001" 
};

Console.WriteLine(person.FullName);     // ✅ Computed property
person.CelebrateBirthday();             // ✅ Controlled state change
// person.Age = -5;                     // ❌ Throws exception - validation
// person.FirstName = "Bob";            // ❌ Compilation error - private setter
```

## Benefits of Encapsulation

1. **Data Protection**: Prevents unauthorized access and modification
2. **Flexibility**: Internal implementation can change without affecting external code
3. **Maintainability**: Changes are localized to the class
4. **Validation**: Ensures data integrity through validation logic
5. **Abstraction**: Hides complexity and exposes only necessary functionality

## Encapsulation vs. Data Hiding

While related, these are not the same:
- **Encapsulation**: Bundling data and methods together
- **Data Hiding**: Making fields private (a technique to achieve encapsulation)

## C# Features for Encapsulation

### 1. Properties (Most common in C#)
```csharp
// Auto-implemented property
public string Name { get; set; }

// Property with backing field and validation
private int _age;
public int Age
{
    get => _age;
    set => _age = value >= 0 ? value : throw new ArgumentException();
}

// Read-only property
public string FullName => $"{FirstName} {LastName}";

// Init-only property (C# 9+)
public string Id { get; init; }
```

### 2. Access Modifiers
```csharp
private decimal _balance;           // Only this class
public void Deposit() { }           // Everywhere
protected void ValidateAmount() { } // This class and derived classes
internal class Helper { }           // Same assembly
```

### 3. Methods for Controlled Access
```csharp
public void Deposit(decimal amount)
{
    // Validation
    if (amount <= 0) throw new ArgumentException();
    
    // Modify private field through controlled method
    _balance += amount;
}
```

## Common Patterns

### 1. Immutable Objects
```csharp
public class ImmutablePerson
{
    public string Name { get; init; }
    public int Age { get; init; }
}
```

### 2. Builder Pattern with Encapsulation
```csharp
public class PersonBuilder
{
    private string _firstName;
    private string _lastName;
    
    public PersonBuilder WithFirstName(string name)
    {
        _firstName = name;
        return this;
    }
    
    public Person Build() => new Person(_firstName, _lastName, ...);
}
```

## Testing Encapsulation

See `tests/OOPFundamentals.Tests/EncapsulationTests.cs` for:
- Property validation tests
- Method behavior tests
- Access control tests
- Business logic validation

## Comparison with Java

| Feature | C# | Java |
|---------|----|----- |
| Properties | Native support with `get`/`set` | Manual getters/setters |
| Auto-properties | `public string Name { get; set; }` | Not available |
| Backing fields | Optional | Always required |
| Init-only | `{ get; init; }` | Not available (use final) |
| Records | `record Person(string Name)` | Records (Java 14+) |

## Best Practices

1. ✅ **Make fields private by default**
2. ✅ **Use properties for public access**
3. ✅ **Validate data in setters**
4. ✅ **Use readonly/init for immutable data**
5. ✅ **Keep validation logic in one place**
6. ❌ **Avoid public fields**
7. ❌ **Don't expose mutable collections directly**

## Real-World Analogy

Think of a **bank ATM**:
- You cannot directly access the cash inside (private field)
- You interact through specific operations: withdraw, deposit, check balance (public methods)
- The ATM validates your operations (amount limits, sufficient funds)
- The internal mechanism is hidden from you (implementation details)

This is encapsulation in action!

## Further Reading

- [Microsoft Docs: Encapsulation](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop)
- [C# Properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties)
- [Access Modifiers](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers)
