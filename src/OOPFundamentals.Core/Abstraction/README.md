# Abstraction (Abstracción)

## Overview

**Abstraction** is one of the four fundamental pillars of Object-Oriented Programming. It means hiding the complex implementation details and showing only the necessary features of an object. Abstraction helps us focus on WHAT an object does rather than HOW it does it.

## Key Concepts

### 1. **Abstract Classes**
Classes that cannot be instantiated directly and may contain abstract methods that must be implemented by derived classes.

### 2. **Abstract Methods**
Methods declared without implementation that must be implemented by non-abstract derived classes.

### 3. **Interfaces**
Pure abstraction - contracts that define what a class must do, but not how it does it.

### 4. **Virtual Methods**
Methods with default implementation that can be optionally overridden by derived classes.

## Abstract Classes vs Interfaces

| Feature | Abstract Class | Interface |
|---------|---------------|-----------|
| Can have implementation | ✅ Yes | ❌ No (C# 8+ allows default implementations) |
| Multiple inheritance | ❌ No (single inheritance only) | ✅ Yes (multiple interfaces) |
| Access modifiers | ✅ Yes (public, protected, private) | ❌ No (implicitly public) |
| Fields | ✅ Yes | ❌ No (only properties) |
| Constructors | ✅ Yes | ❌ No |
| When to use | IS-A relationship (e.g., Circle IS-A Shape) | CAN-DO relationship (e.g., Circle CAN-BE-DRAWN) |

## Examples in This Project

### Shape (Abstract Base Class)

The `Shape` class demonstrates abstraction through:

```csharp
public abstract class Shape
{
    // Abstract methods - must be implemented by derived classes
    public abstract double CalculateArea();
    public abstract double CalculatePerimeter();
    public abstract string GetShapeInfo();
    
    // Virtual methods - can be optionally overridden
    public virtual void Draw() { }
    public virtual void Resize(double scaleFactor) { }
    
    // Concrete methods - provide common functionality
    public void DisplayInfo() { }
    public string CompareAreaWith(Shape other) { }
}
```

**Key Points:**
- ❌ Cannot create `new Shape()` - it's abstract
- ✅ Defines common interface for all shapes
- ✅ Provides template method pattern (`DisplayInfo`)
- ✅ Hides calculation complexity from users

### Concrete Implementations

#### Circle
```csharp
public class Circle : Shape
{
    public override double CalculateArea()
    {
        return Math.PI * _radius * _radius;  // Implementation specific to Circle
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * _radius;  // Implementation specific to Circle
    }
}
```

#### Rectangle
```csharp
public class Rectangle : Shape
{
    public override double CalculateArea()
    {
        return _width * _height;  // Implementation specific to Rectangle
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * (_width + _height);  // Implementation specific to Rectangle
    }
}
```

#### Triangle
```csharp
public class Triangle : Shape
{
    public override double CalculateArea()
    {
        // Uses Heron's formula - complex but hidden from users
        double s = (_sideA + _sideB + _sideC) / 2;
        return Math.Sqrt(s * (s - _sideA) * (s - _sideB) * (s - _sideC));
    }
}
```

### Interface-Based Abstraction

#### IDrawable Interface
```csharp
public interface IDrawable
{
    void Draw();
    void DrawAt(int x, int y);
    string Color { get; }
    void Clear();
}
```

#### IResizable Interface
```csharp
public interface IResizable
{
    void Resize(double scaleFactor);
    void ResizeToFit(double maxWidth, double maxHeight);
}
```

#### Multiple Interface Implementation
```csharp
public class DrawableCircle : Circle, IDrawable, IResizable
{
    // Implements both IDrawable and IResizable
    // Inherits from Circle (abstract Shape)
}
```

## Benefits of Abstraction

### 1. **Simplicity**
Users don't need to know complex implementation details.

```csharp
Shape circle = new Circle(5);
double area = circle.CalculateArea();  // Don't need to know π * r²
```

### 2. **Flexibility**
Implementation can change without affecting users.

```csharp
// Today: Uses Heron's formula
// Tomorrow: Could use different formula
// Users don't need to change their code!
```

### 3. **Code Reusability**
Common functionality in base class, specific in derived classes.

```csharp
// All shapes share DisplayInfo() method
circle.DisplayInfo();
rectangle.DisplayInfo();
triangle.DisplayInfo();
```

### 4. **Polymorphism**
Work with different types through a common interface.

```csharp
List<Shape> shapes = new List<Shape>
{
    new Circle(5),
    new Rectangle(4, 6),
    new Triangle(3, 4, 5)
};

foreach (Shape shape in shapes)
{
    Console.WriteLine(shape.CalculateArea());  // Polymorphic call
}
```

## Levels of Abstraction

### Level 1: Complete Abstraction (Interfaces)
```csharp
public interface IShape
{
    double CalculateArea();  // No implementation at all
}
```

### Level 2: Partial Abstraction (Abstract Classes)
```csharp
public abstract class Shape
{
    public abstract double CalculateArea();  // No implementation
    public virtual void Draw() { }            // Default implementation
    public void DisplayInfo() { }             // Complete implementation
}
```

### Level 3: Concrete Implementation
```csharp
public class Circle : Shape
{
    public override double CalculateArea()
    {
        return Math.PI * _radius * _radius;  // Complete implementation
    }
}
```

## Real-World Analogy

Think of a **TV Remote Control**:

- **Abstraction**: You press buttons (interface) without knowing the complex electronics inside
- **Abstract Class**: "RemoteControl" defines buttons all remotes have (power, volume, channel)
- **Concrete Class**: "SamsungRemote" implements these buttons for Samsung TVs specifically
- **Interface**: "IVoiceControlled" adds voice commands to some remotes

You interact with simple buttons (abstraction), while complex IR signals, frequency modulation, and electronic circuits are hidden from you.

## C# Features for Abstraction

### 1. Abstract Keyword
```csharp
public abstract class BaseClass
{
    public abstract void AbstractMethod();
}
```

### 2. Virtual and Override
```csharp
public virtual void VirtualMethod() 
{ 
    // Default implementation 
}

public override void VirtualMethod() 
{ 
    // Custom implementation 
}
```

### 3. Interface Implementation
```csharp
public class MyClass : IMyInterface
{
    public void InterfaceMethod()
    {
        // Implementation
    }
}
```

### 4. Multiple Interfaces
```csharp
public class MyClass : IDrawable, IResizable, IComparable
{
    // Implements all three interfaces
}
```

## Design Patterns Using Abstraction

### 1. Template Method Pattern
```csharp
public abstract class DataProcessor
{
    // Template method - defines algorithm skeleton
    public void Process()
    {
        LoadData();      // Abstract
        ValidateData();  // Abstract
        SaveData();      // Concrete
    }
    
    protected abstract void LoadData();
    protected abstract void ValidateData();
    
    protected void SaveData()
    {
        // Common implementation
    }
}
```

### 2. Strategy Pattern
```csharp
public interface IPaymentStrategy
{
    void ProcessPayment(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy { }
public class PayPalPayment : IPaymentStrategy { }
```

### 3. Factory Pattern
```csharp
public abstract class ShapeFactory
{
    public abstract Shape CreateShape();
}
```

## Common Pitfalls

### ❌ Over-Abstraction
```csharp
// Too abstract - adds unnecessary complexity
public interface IClickable { void Click(); }
public interface IHoverable { void Hover(); }
public interface IFocusable { void Focus(); }
// ... 20 more interfaces for one button
```

### ✅ Right Level of Abstraction
```csharp
// Just right - practical abstraction
public interface IInteractive
{
    void Click();
    void Hover();
    void Focus();
}
```

### ❌ Leaky Abstraction
```csharp
// Bad - exposes implementation details
public abstract class Database
{
    public abstract string GetConnectionString();  // Exposes MySQL detail
}
```

### ✅ Proper Abstraction
```csharp
// Good - hides implementation
public abstract class Database
{
    public abstract void Connect();
    public abstract void Query(string sql);
}
```

## Testing Abstraction

See `tests/OOPFundamentals.Tests/AbstractionTests.cs` for:
- Abstract method implementation tests
- Polymorphic behavior tests
- Interface implementation tests
- Virtual method override tests

## Comparison with Java

| Feature | C# | Java |
|---------|----|----- |
| Abstract class | `abstract class` | `abstract class` |
| Abstract method | `public abstract void Method();` | `public abstract void method();` |
| Interface | `interface IMyInterface` | `interface MyInterface` |
| Multiple interfaces | ✅ Yes | ✅ Yes |
| Default interface methods | ✅ C# 8+ | ✅ Java 8+ |
| Virtual methods | `virtual` keyword required | All non-final methods are virtual |
| Sealed/Final | `sealed` | `final` |

## Best Practices

1. ✅ **Use abstract classes for IS-A relationships**
   - Circle IS-A Shape ✅
   
2. ✅ **Use interfaces for CAN-DO capabilities**
   - Circle CAN-BE-DRAWN (IDrawable) ✅
   - Circle CAN-BE-RESIZED (IResizable) ✅

3. ✅ **Keep abstractions simple and focused**
   - Single Responsibility Principle

4. ✅ **Don't expose implementation details**
   - Hide complexity behind simple methods

5. ✅ **Use meaningful names**
   - `IDrawable` not `IInterface1`
   - `Shape` not `BaseClass`

6. ❌ **Don't create abstractions too early**
   - Wait until you have at least 2-3 concrete examples

7. ✅ **Document the abstraction's purpose**
   - Explain WHY not just WHAT

## Further Reading

- [Microsoft Docs: Abstract Classes](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/abstract-and-sealed-classes-and-class-members)
- [Microsoft Docs: Interfaces](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces)
- [SOLID Principles: Dependency Inversion](https://en.wikipedia.org/wiki/Dependency_inversion_principle)
- [Design Patterns](https://refactoring.guru/design-patterns)
