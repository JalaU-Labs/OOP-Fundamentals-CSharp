# OOP Fundamentals in C#

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat-square&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](https://opensource.org/licenses/MIT)

A comprehensive educational project demonstrating the **Four Pillars of Object-Oriented Programming** (OOP) in C# with practical examples and comparisons with Java data structures.

## 📚 Project Overview

This project is part of **Programming 3** course and covers:

1. **Four Pillars of OOP:**
   - 🔒 **Encapsulation** - Data hiding and access control
   - 🎭 **Abstraction** - Hiding complex implementation details
   - 🧬 **Inheritance** - Code reuse through hierarchical relationships
   - 🔄 **Polymorphism** - Multiple forms of the same interface

2. **Data Structures Comparison:**
   - Arrays in C# vs Java
   - Lists in C# vs Java
   - Dictionaries/Maps in C# vs Java

## 🏗️ Project Structure

```
OOP-Fundamentals-CSharp/
├── src/
│   ├── OOPFundamentals.Core/          # Core library with OOP examples
│   │   ├── Encapsulation/             # Encapsulation examples
│   │   ├── Abstraction/               # Abstraction examples
│   │   ├── Inheritance/               # Inheritance examples
│   │   ├── Polymorphism/              # Polymorphism examples
│   │   └── DataStructures/            # Data structures comparison
│   └── OOPFundamentals.ConsoleApp/    # Console application demos
├── tests/
│   └── OOPFundamentals.Tests/         # Unit tests
├── docs/                               # Documentation
│   ├── OOP-Pillars.md                 # Detailed OOP documentation
│   └── DataStructures-Comparison.md   # Data structures comparison
├── LICENSE                             # MIT License
└── README.md                           # This file
```

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or higher
- IDE: [JetBrains Rider](https://www.jetbrains.com/rider/), [Visual Studio](https://visualstudio.microsoft.com/), or [VS Code](https://code.visualstudio.com/)
- Git

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/JalaU-Labs/OOP-Fundamentals-CSharp.git
   cd OOP-Fundamentals-CSharp
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Run the console application:**
   ```bash
   dotnet run --project src/OOPFundamentals.ConsoleApp
   ```

5. **Run tests:**
   ```bash
   dotnet test
   ```

## 🎯 Usage Examples

### Running the Interactive Demo

The console application provides an interactive menu to explore each OOP principle:

```bash
cd src/OOPFundamentals.ConsoleApp
dotnet run
```

**Available Demos:**
1. 🔒 Demostración de Encapsulamiento
2. 🎭 Demostración de Abstracción
3. 🧬 Demostración de Herencia
4. 🔄 Demostración de Polimorfismo
5. 📊 Comparación de Estructuras de Datos
6. 🚪 Salir

### Running Specific Tests

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity detailed

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "ClassName=EncapsulationTests"
```

## 📖 Documentation

Detailed documentation for each concept is available in the `docs/` directory:

- **[OOP Pillars Documentation](docs/OOP-Pillars.md)** - Deep dive into each pillar with examples
- **[Data Structures Comparison](docs/DataStructures-Comparison.md)** - Comprehensive C# vs Java comparison

## 🔒 1. Encapsulation

Encapsulation is demonstrated through practical examples:

```csharp
// Example: Bank Account with private fields and public methods
var account = new BankAccount("ACC001", "John Doe");
account.Deposit(1000m);
account.Withdraw(200m);
Console.WriteLine($"Balance: {account.GetBalance()}");
```

**Key Concepts:**
- Private fields with public properties
- Data validation in setters
- Controlled access through methods

**Files:** `src/OOPFundamentals.Core/Encapsulation/`

## 🎭 2. Abstraction

Abstraction examples include abstract classes and interfaces:

```csharp
// Example: Shape hierarchy with abstract methods
Shape circle = new Circle(5);
Shape rectangle = new Rectangle(4, 6);

Console.WriteLine($"Circle Area: {circle.CalculateArea()}");
Console.WriteLine($"Rectangle Area: {rectangle.CalculateArea()}");
```

**Key Concepts:**
- Abstract classes and methods
- Interface implementation
- Hiding complex implementation details

**Files:** `src/OOPFundamentals.Core/Abstraction/`

## 🧬 3. Inheritance

Inheritance is demonstrated with a vehicle hierarchy:

```csharp
// Example: Vehicle inheritance
Car car = new Car("Toyota", "Corolla", 2024, 4);
Motorcycle bike = new Motorcycle("Harley", "Street 750", 2024, "Cruiser");

car.Start();
bike.Start();
```

**Key Concepts:**
- Base and derived classes
- Method overriding with `virtual` and `override`
- Using `base` keyword
- Multi-level inheritance

**Files:** `src/OOPFundamentals.Core/Inheritance/`

## 🔄 4. Polymorphism

Polymorphism examples through payment processing:

```csharp
// Example: Different payment methods using polymorphism
List<Payment> payments = new List<Payment>
{
    new CreditCardPayment(100m, "1234-5678-9012-3456"),
    new PayPalPayment(50m, "user@example.com"),
    new CashPayment(25m)
};

foreach (var payment in payments)
{
    payment.ProcessPayment();
}
```

**Key Concepts:**
- Method overriding
- Runtime polymorphism
- Interface-based polymorphism
- Method overloading

**Files:** `src/OOPFundamentals.Core/Polymorphism/`

## 📊 Data Structures Comparison

### Arrays
```csharp
// C#
int[] numbers = new int[5];
int[] values = { 1, 2, 3, 4, 5 };

// Java equivalent
// int[] numbers = new int[5];
// int[] values = { 1, 2, 3, 4, 5 };
```

### Lists
```csharp
// C#
List<string> names = new List<string>();
names.Add("Alice");

// Java equivalent
// ArrayList<String> names = new ArrayList<>();
// names.add("Alice");
```

### Dictionaries/Maps
```csharp
// C#
Dictionary<string, int> ages = new Dictionary<string, int>();
ages["Alice"] = 25;

// Java equivalent
// HashMap<String, Integer> ages = new HashMap<>();
// ages.put("Alice", 25);
```

**Detailed comparison:** `docs/DataStructures-Comparison.md`

## 🧪 Testing

The project includes comprehensive unit tests using **xUnit**:

- `EncapsulationTests.cs` - Tests for encapsulation examples
- `AbstractionTests.cs` - Tests for abstraction examples
- `InheritanceTests.cs` - Tests for inheritance examples
- `PolymorphismTests.cs` - Tests for polymorphism examples
- `DataStructuresTests.cs` - Tests for data structure operations

```bash
# Run all tests with detailed output
dotnet test --verbosity normal

# Generate test coverage report
dotnet test --collect:"XPlat Code Coverage"
```

## 🛠️ Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 9.0 | Framework |
| C# | 12.0 | Programming Language |
| xUnit | Latest | Unit Testing |
| JetBrains Rider | Latest | IDE (Recommended) |

## 📚 Learning Resources

### Official Documentation
- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Object-Oriented Programming (C#)](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop)

### Project Documentation
- [OOP Pillars Guide](docs/OOP-Pillars.md)
- [Data Structures Comparison](docs/DataStructures-Comparison.md)

## 🤝 Contributing

This is an educational project for university coursework. However, suggestions and improvements are welcome:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👨‍💻 Author

**CodeWithBotinaOficial**

- GitHub: [@CodeWithBotinaOficial](https://github.com/CodeWithBotinaOficial)
- Organization: [JalaU-Labs](https://github.com/JalaU-Labs)

## 🎓 Academic Context

- **Course:** Programming 3
- **Institution:** Universidad
- **Activity:** Activity 1 - OOP Fundamentals
- **Objectives:**
  - Understand the four fundamental pillars of OOP
  - Compare data structures between C# and Java
  - Apply OOP principles in practical examples

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

```
MIT License - Copyright (c) 2026 CodeWithBotinaOficial
```

## 🙏 Acknowledgments

- Thanks to the Programming 3 course instructors
- Microsoft for excellent C# and .NET documentation
- JalaU-Labs community

---

<div align="center">

**⭐ If you find this project helpful, please give it a star! ⭐**

Made with ❤️ by [CodeWithBotinaOficial](https://github.com/CodeWithBotinaOficial)

</div>
