using OOPFundamentals.Core.Inheritance;

namespace OOPFundamentals.ConsoleApp.Demos;

/// <summary>
/// Demonstration of Inheritance concepts.
/// Shows how classes can inherit properties and methods from base classes.
/// </summary>
public class InheritanceDemo
{
    public void Run()
    {
        Console.WriteLine("📝 Herencia es el tercer pilar de OOP.");
        Console.WriteLine("   Permite que una clase herede propiedades y métodos de otra,");
        Console.WriteLine("   promoviendo la reutilización de código y relaciones IS-A.\n");
        
        DemonstrateBasicInheritance();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstrateMultiLevelInheritance();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstratePolymorphicBehavior();
        
        Console.WriteLine("\n✅ Concepto clave:");
        Console.WriteLine("   La herencia permite crear jerarquías de clases, donde las clases");
        Console.WriteLine("   derivadas extienden y especializan el comportamiento de las clases base.");
    }
    
    private void DemonstrateBasicInheritance()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🚗 EJEMPLO 1: Herencia Simple (Car, Motorcycle, Truck)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Creando diferentes vehículos:");
        
        // Create different vehicles
        var car = new Car("Toyota", "Camry", 2024, 4, "Plata");
        var motorcycle = new Motorcycle("Harley-Davidson", "Street 750", 2024, 
                                       MotorcycleType.Cruiser, 750, "Negro");
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Light, 1000, "Blanco");
        
        Console.WriteLine("   ✅ Auto: Toyota Camry 2024");
        Console.WriteLine("   ✅ Motocicleta: Harley-Davidson Street 750");
        Console.WriteLine("   ✅ Camioneta: Ford F-150");
        
        // Demonstrate inherited methods
        Console.WriteLine("\n2️⃣ Usando métodos heredados de Vehicle:");
        car.Start();
        car.Accelerate(50);
        car.Honk();
        Console.WriteLine($"   Velocidad actual: {car.CurrentSpeed} km/h");
        
        Console.WriteLine("\n3️⃣ Usando métodos específicos de Car:");
        car.OpenTrunk();
        car.TurnOnAirConditioning();
        car.Park();
        
        Console.WriteLine("\n4️⃣ Motorcycle con comportamiento diferente:");
        motorcycle.PutOnHelmet();
        motorcycle.RetractKickstand();
        motorcycle.Start();
        motorcycle.Accelerate(30);  // Motocicletas aceleran más rápido (1.5x)
        motorcycle.DoWheelie();
        
        Console.WriteLine("\n5️⃣ Truck con lógica de carga:");
        truck.Start();
        truck.OpenCargoBed();
        truck.LoadCargo(500);
        truck.LoadCargo(300);
        truck.PerformWeightInspection();
        truck.Accelerate(40);  // Camiones aceleran más lento con carga
        
        Console.WriteLine("\n💡 Herencia en acción:");
        Console.WriteLine("   ✅ Car, Motorcycle y Truck heredan de Vehicle");
        Console.WriteLine("   ✅ Todos comparten: Start(), Accelerate(), Brake(), etc.");
        Console.WriteLine("   ✅ Cada uno tiene métodos específicos:");
        Console.WriteLine("      • Car: OpenTrunk(), Park()");
        Console.WriteLine("      • Motorcycle: DoWheelie(), PutOnHelmet()");
        Console.WriteLine("      • Truck: LoadCargo(), AttachTrailer()");
    }
    
    private void DemonstrateMultiLevelInheritance()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n⚡ EJEMPLO 2: Herencia Multinivel (ElectricCar)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n📊 Jerarquía de herencia:");
        Console.WriteLine("   Vehicle (abuelo)");
        Console.WriteLine("      ↓");
        Console.WriteLine("   Car (padre)");
        Console.WriteLine("      ↓");
        Console.WriteLine("   ElectricCar (hijo)");
        
        Console.WriteLine("\n1️⃣ Creando un auto eléctrico:");
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Rojo");
        
        Console.WriteLine($"   ✅ {tesla}");
        
        // Using methods from Vehicle (grandparent)
        Console.WriteLine("\n2️⃣ Usando métodos de Vehicle (abuelo):");
        tesla.Honk();  // Heredado de Vehicle
        Console.WriteLine($"   Tipo de combustible: {tesla.FuelType}");  // Overridden: "Electric"
        
        // Using methods from Car (parent)
        Console.WriteLine("\n3️⃣ Usando métodos de Car (padre):");
        tesla.LockDoors();  // Heredado de Car
        tesla.TurnOnAirConditioning();  // Heredado de Car
        
        // Using ElectricCar specific methods
        Console.WriteLine("\n4️⃣ Usando métodos específicos de ElectricCar:");
        tesla.DisplayBatteryHealth();
        tesla.StartCharging(ChargingSpeed.Supercharger);
        
        Console.WriteLine("\n   Simulando carga...");
        tesla.RechargeBattery(30);
        tesla.RechargeBattery(40);
        tesla.RechargeBattery(30);  // Llegará a 100%
        
        // Start and drive
        Console.WriteLine("\n5️⃣ Arrancando y conduciendo:");
        tesla.Start();  // Completamente diferente a Car.Start()
        tesla.Accelerate(60);  // Consume batería
        tesla.Brake(20);  // Frenos regenerativos - recupera energía!
        tesla.Accelerate(40);
        
        Console.WriteLine($"\n   Batería: {tesla.BatteryChargePercentage:F1}%");
        Console.WriteLine($"   Rango restante: {tesla.RemainingRange:F1} km");
        
        // Stop
        Console.WriteLine("\n6️⃣ Deteniendo el vehículo:");
        tesla.Brake(80);
        tesla.Stop();
        
        // Display full info
        Console.WriteLine("\n7️⃣ Información completa (heredada de todos los niveles):");
        Console.WriteLine(tesla.GetInfo());
        
        Console.WriteLine("\n💡 Herencia Multinivel:");
        Console.WriteLine("   ✅ ElectricCar hereda de Car");
        Console.WriteLine("   ✅ Car hereda de Vehicle");
        Console.WriteLine("   ✅ ElectricCar tiene acceso a TODOS los miembros de ambos");
        Console.WriteLine("   ✅ Puede override métodos de cualquier nivel");
        Console.WriteLine("   ✅ Ejemplo: FuelType viene de Vehicle, pero ElectricCar lo override");
    }
    
    private void DemonstratePolymorphicBehavior()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n🔄 EJEMPLO 3: Comportamiento Polimórfico con Herencia");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Creando una flota de vehículos:");
        List<Vehicle> fleet = new List<Vehicle>
        {
            new Car("Honda", "Civic", 2024, 4, "Azul"),
            new Motorcycle("Yamaha", "YZF-R3", 2024, MotorcycleType.Sport, 321, "Rojo"),
            new Truck("Chevrolet", "Silverado", 2024, TruckType.Light, 800, "Negro"),
            new ElectricCar("Tesla", "Model Y", 2024, 4, 80, 480, "Blanco")
        };
        
        Console.WriteLine($"   ✅ Flota creada con {fleet.Count} vehículos");
        
        // Polymorphic behavior - same method call, different behavior
        Console.WriteLine("\n2️⃣ Arrancando TODOS los vehículos (polimorfismo):");
        Console.WriteLine("   (Nota: Cada vehículo arranca de manera diferente)\n");
        
        foreach (var vehicle in fleet)
        {
            vehicle.Start();  // Polymorphic call!
            Console.WriteLine($"   ✓ {vehicle.Brand} {vehicle.Model} arrancado");
            Console.WriteLine();
        }
        
        Console.WriteLine("\n3️⃣ Información de la flota:");
        Console.WriteLine(new string('-', 80));
        
        foreach (var vehicle in fleet)
        {
            Console.WriteLine($"\n{vehicle}");
            Console.WriteLine($"Tipo: {vehicle.VehicleType}");
            Console.WriteLine($"Velocidad máxima: {vehicle.MaxSpeed:F0} km/h");
            Console.WriteLine($"Combustible: {vehicle.FuelType}");
            Console.WriteLine($"Ruedas: {vehicle.NumberOfWheels}");
        }
        
        Console.WriteLine("\n💡 Polimorfismo con Herencia:");
        Console.WriteLine("   ✅ Tratamos objetos diferentes (Car, Motorcycle, etc.) como Vehicle");
        Console.WriteLine("   ✅ El MISMO código funciona para TODOS los tipos");
        Console.WriteLine("   ✅ Cada objeto se comporta según SU propia implementación");
        Console.WriteLine("   ✅ Esto es herencia + polimorfismo trabajando juntos");
    }
}