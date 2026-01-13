# Inheritance (Herencia)

## Overview

**Inheritance** is one of the four fundamental pillars of Object-Oriented Programming. It allows a class (derived/child class) to inherit properties and methods from another class (base/parent class), enabling code reuse and establishing hierarchical relationships between classes.

## Key Concepts

### 1. **Base Class (Parent Class)**
The class whose members are inherited.

### 2. **Derived Class (Child Class)**
The class that inherits from the base class.

### 3. **IS-A Relationship**
Inheritance represents an "IS-A" relationship (e.g., Car IS-A Vehicle).

### 4. **Code Reusability**
Avoid duplicating code by inheriting common functionality from a base class.

## Inheritance Syntax in C#

```csharp
// Base class
public class Vehicle { }

// Derived class
public class Car : Vehicle { }  // Car inherits from Vehicle
```

## Types of Inheritance

### 1. Single Inheritance
A class inherits from one base class.

```csharp
Vehicle → Car
```

### 2. Multi-Level Inheritance
A class inherits from a derived class (inheritance chain).

```csharp
Vehicle → Car → ElectricCar
```

### 3. Hierarchical Inheritance
Multiple classes inherit from the same base class.

```csharp
        Vehicle
       /   |   \
     Car  Moto  Truck
```

**Note:** C# does **NOT** support multiple inheritance (inheriting from multiple classes). However, a class can implement multiple interfaces.

## Examples in This Project

### Vehicle (Base Class)

The `Vehicle` class serves as the base for all vehicles:

```csharp
public class Vehicle
{
    // Properties all vehicles share
    public string Brand { get; set; }
    public string Model { get; set; }
    protected bool _isEngineRunning;  // Protected - accessible to derived classes
    
    // Virtual methods - can be overridden
    public virtual void Start() { }
    public virtual void Accelerate(double speed) { }
    
    // Concrete methods - inherited as-is
    public void Refuel(double liters) { }
}
```

### Car : Vehicle (Derived Class)

```csharp
public class Car : Vehicle
{
    // Car-specific properties
    public int NumberOfDoors { get; set; }
    
    // Override base method
    public override void Start()
    {
        base.Start();  // Call parent method
        Console.WriteLine("Car-specific startup...");
    }
    
    // Car-specific method
    public void OpenTrunk() { }
}
```

### Motorcycle : Vehicle (Derived Class)

```csharp
public class Motorcycle : Vehicle
{
    public MotorcycleType Type { get; set; }
    public int EngineCC { get; set; }
    
    // Override with motorcycle-specific behavior
    public override void Start()
    {
        // Different implementation than Car
        Console.WriteLine("Motorcycle startup...");
        base.Start();
    }
    
    // Motorcycle-specific methods
    public void DoWheelie() { }
}
```

### Truck : Vehicle (Derived Class)

```csharp
public class Truck : Vehicle
{
    public double CargoCapacity { get; set; }
    
    // Override with truck-specific behavior
    public override void Accelerate(double speed)
    {
        // Trucks accelerate slower
        base.Accelerate(speed * 0.7);
    }
    
    public void LoadCargo(double weight) { }
}
```

### ElectricCar : Car : Vehicle (Multi-Level)

```csharp
public class ElectricCar : Car  // ElectricCar inherits from Car
{
    // Inherits from both Car AND Vehicle
    
    public double BatteryCapacity { get; set; }
    
    // Override method from grandparent (Vehicle) through parent (Car)
    public override void Start()
    {
        Console.WriteLine("Electric motor activated...");
        // Custom electric car startup
    }
    
    // New method for electric cars only
    public void StartCharging() { }
}
```

## Key Keywords

### 1. `: BaseClass` (Inheritance)
```csharp
public class Car : Vehicle  // Car inherits from Vehicle
```

### 2. `base` Keyword
Calls base class members from derived class.

```csharp
public override void Start()
{
    base.Start();  // Call parent's Start method
    // Add derived class specific code
}
```

### 3. `virtual` Keyword
Marks method/property as overridable in base class.

```csharp
public virtual void Drive() { }  // Can be overridden
```

### 4. `override` Keyword
Overrides a virtual/abstract method from base class.

```csharp
public override void Drive() { }  // Overrides base implementation
```

### 5. `sealed` Keyword
Prevents further inheritance.

```csharp
public sealed class FinalCar : Car { }  // Cannot be inherited
```

### 6. `new` Keyword (Hiding)
Hides base class member (not recommended over override).

```csharp
public new void Start() { }  // Hides base method (use override instead)
```

## Access Modifiers in Inheritance

| Modifier | Base Class | Derived Class | Outside |
|----------|------------|---------------|---------|
| `public` | ✅ | ✅ | ✅ |
| `protected` | ✅ | ✅ | ❌ |
| `private` | ✅ | ❌ | ❌ |
| `internal` | ✅ | ✅ (same assembly) | ❌ |
| `protected internal` | ✅ | ✅ | ❌ |

### Example:
```csharp
public class Vehicle
{
    private string _vin;        // Only Vehicle can access
    protected bool _isRunning;  // Vehicle and derived classes
    public string Brand;        // Everyone can access
}

public class Car : Vehicle
{
    public void Start()
    {
        // _vin;  // ❌ Error - private in base
        _isRunning = true;  // ✅ OK - protected
        Brand = "Toyota";   // ✅ OK - public
    }
}
```

## Constructor Chaining

### Base Constructor Calls

```csharp
public class Vehicle
{
    public Vehicle(string brand, string model)
    {
        Brand = brand;
        Model = model;
    }
}

public class Car : Vehicle
{
    public Car(string brand, string model, int doors)
        : base(brand, model)  // Call base constructor
    {
        NumberOfDoors = doors;
    }
}
```

### Execution Order:
1. Base class constructor runs first
2. Derived class constructor runs second

```csharp
var car = new Car("Toyota", "Camry", 4);
// Output:
// [Vehicle Constructor] Creating Vehicle: Toyota Camry
// [Car Constructor] Car created with 4 doors
```

## Method Overriding

### Virtual Methods (Base Class)
```csharp
public class Vehicle
{
    public virtual void Start()  // Can be overridden
    {
        Console.WriteLine("Vehicle starting...");
    }
}
```

### Override in Derived Class
```csharp
public class Car : Vehicle
{
    public override void Start()  // Override base method
    {
        base.Start();  // Optionally call base
        Console.WriteLine("Car starting...");
    }
}
```

### Sealed Override
```csharp
public class ElectricCar : Car
{
    public sealed override void Start()  // Can't be overridden further
    {
        Console.WriteLine("Electric car starting...");
    }
}
```

## Benefits of Inheritance

### 1. **Code Reusability**
Write once, use in multiple derived classes.

```csharp
// All vehicles share Start, Stop, Accelerate from Vehicle
var car = new Car();
var motorcycle = new Motorcycle();
var truck = new Truck();

car.Start();        // Inherited from Vehicle
motorcycle.Start(); // Inherited from Vehicle
truck.Start();      // Inherited from Vehicle
```

### 2. **Polymorphism**
Treat derived objects as base class objects.

```csharp
List<Vehicle> vehicles = new List<Vehicle>
{
    new Car("Toyota", "Camry", 2024, 4),
    new Motorcycle("Harley", "Street 750", 2024, MotorcycleType.Cruiser, 750),
    new Truck("Ford", "F-150", 2024, TruckType.Light, 1000)
};

foreach (Vehicle vehicle in vehicles)
{
    vehicle.Start();  // Polymorphic call - each behaves differently
}
```

### 3. **Maintainability**
Change base class once, affects all derived classes.

### 4. **Logical Hierarchy**
Models real-world relationships (IS-A).

## Real-World Analogy

Think of **biological classification**:

```
Living Thing (base)
    ↓
Animal (derived from Living Thing)
    ↓
Mammal (derived from Animal)
    ↓
Dog (derived from Mammal)
```

Each level inherits characteristics from above:
- Dog IS-A Mammal
- Mammal IS-A Animal
- Animal IS-A Living Thing

Therefore: Dog IS-A Living Thing (through inheritance chain)

Similarly in our project:
```
Vehicle (base)
    ↓
Car (derived from Vehicle)
    ↓
ElectricCar (derived from Car)
```

ElectricCar IS-A Car AND IS-A Vehicle.

## Common Patterns

### 1. Template Method Pattern
```csharp
public class Vehicle
{
    public void StartVehicle()  // Template method
    {
        PerformPreStartChecks();  // Step 1
        StartEngine();             // Step 2 (virtual)
        PerformPostStartChecks();  // Step 3
    }
    
    protected virtual void StartEngine()
    {
        // Default implementation
    }
}

public class ElectricCar : Vehicle
{
    protected override void StartEngine()
    {
        // Electric-specific implementation
    }
}
```

### 2. Factory Method Pattern
```csharp
public abstract class VehicleFactory
{
    public abstract Vehicle CreateVehicle();
}

public class CarFactory : VehicleFactory
{
    public override Vehicle CreateVehicle()
    {
        return new Car("Toyota", "Camry", 2024, 4);
    }
}
```

## Common Pitfalls

### ❌ Deep Inheritance Hierarchies
```csharp
// Bad - too deep
Vehicle → Car → Sedan → LuxurySedan → PremiumLuxurySedan
```

### ✅ Favor Composition Over Inheritance
```csharp
// Good - use composition when appropriate
public class Car
{
    private Engine _engine;  // Composition
    private Transmission _transmission;
}
```

### ❌ Breaking Liskov Substitution Principle
```csharp
// Bad - Square changing behavior of Rectangle unexpectedly
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}

public class Square : Rectangle  // Violates LSP
{
    public override int Width
    {
        set { base.Width = value; base.Height = value; }
    }
}
```

## Testing Inheritance

See `tests/OOPFundamentals.Tests/InheritanceTests.cs` for:
- Base class functionality tests
- Override behavior tests
- Constructor chaining tests
- Polymorphic behavior tests

## Comparison with Java

| Feature | C# | Java |
|---------|----|----- |
| Inheritance syntax | `: BaseClass` | `extends BaseClass` |
| Multiple inheritance | ❌ No (use interfaces) | ❌ No (use interfaces) |
| Base constructor call | `: base()` | `: super()` |
| Sealed/Final class | `sealed` | `final` |
| Virtual by default | ❌ No (must use `virtual`) | ✅ Yes (all non-final) |
| Override keyword | Required (`override`) | Optional (`@Override`) |
| Access to base | `base.Method()` | `super.method()` |

## Best Practices

1. ✅ **Use inheritance for IS-A relationships**
   - Car IS-A Vehicle ✅
   - Car HAS-A Engine (use composition) ✅

2. ✅ **Keep inheritance hierarchies shallow**
   - Maximum 3-4 levels deep

3. ✅ **Use `virtual` for methods that should be overridable**
   - Mark methods `virtual` if derived classes might need different behavior

4. ✅ **Always call `base.Method()` when appropriate**
   - Ensures base class behavior is preserved

5. ✅ **Use `sealed` to prevent unwanted inheritance**
   - If a class shouldn't be inherited, mark it `sealed`

6. ✅ **Document inheritance relationships**
   - Clear XML comments explaining the hierarchy

7. ❌ **Don't use inheritance just for code reuse**
   - Use composition if there's no IS-A relationship

8. ❌ **Don't override and completely ignore base behavior**
   - If you're not calling `base`, reconsider the design

## Further Reading

- [Microsoft Docs: Inheritance](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance)
- [SOLID Principles: Liskov Substitution Principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle)
- [Composition over Inheritance](https://en.wikipedia.org/wiki/Composition_over_inheritance)
