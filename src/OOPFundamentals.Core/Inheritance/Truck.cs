namespace OOPFundamentals.Core.Inheritance;

/// <summary>
/// Represents a truck, inheriting from Vehicle.
/// Demonstrates inheritance with truck-specific cargo and commercial features.
/// </summary>
/// <remarks>
/// Shows how derived classes can have significantly different characteristics
/// while still inheriting common vehicle functionality.
/// </remarks>
public class Truck : Vehicle
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the cargo capacity in kilograms.
    /// </summary>
    public double CargoCapacity { get; set; }
    
    /// <summary>
    /// Gets the current cargo weight in kilograms.
    /// </summary>
    public double CurrentCargoWeight { get; private set; }
    
    /// <summary>
    /// Gets or sets the number of axles.
    /// </summary>
    public int NumberOfAxles { get; set; }
    
    /// <summary>
    /// Gets whether the truck has a trailer attached.
    /// </summary>
    public bool HasTrailer { get; private set; }
    
    /// <summary>
    /// Gets or sets whether the truck bed is covered.
    /// </summary>
    public bool IsCovered { get; set; }
    
    /// <summary>
    /// Gets whether the cargo bed is currently open.
    /// </summary>
    public bool IsCargoBedOpen { get; private set; }
    
    /// <summary>
    /// Gets the truck type/class.
    /// </summary>
    public TruckType Type { get; set; }
    
    /// <summary>
    /// Overrides the vehicle type to specify this is a Truck.
    /// </summary>
    public override string VehicleType => $"{Type} Truck";
    
    /// <summary>
    /// Overrides max speed - trucks are generally slower due to weight.
    /// </summary>
    public override double MaxSpeed
    {
        get
        {
            double baseSpeed = 110.0; // Base truck speed
            
            // Reduce speed based on cargo weight
            double cargoFactor = 1.0 - (CurrentCargoWeight / CargoCapacity * 0.3);
            
            // Heavy trucks are slower
            if (Type == TruckType.Heavy)
            {
                baseSpeed = 90.0;
            }
            
            return baseSpeed * cargoFactor;
        }
    }
    
    /// <summary>
    /// Overrides fuel type - many trucks use diesel.
    /// </summary>
    public override string FuelType => Type == TruckType.Light ? "Gasoline" : "Diesel";
    
    /// <summary>
    /// Overrides number of wheels based on truck type and axles.
    /// </summary>
    public override int NumberOfWheels => NumberOfAxles * 2 + (HasTrailer ? 4 : 0);
    
    /// <summary>
    /// Gets the remaining cargo capacity.
    /// </summary>
    public double RemainingCapacity => CargoCapacity - CurrentCargoWeight;
    
    /// <summary>
    /// Gets the cargo load percentage.
    /// </summary>
    public double LoadPercentage => (CurrentCargoWeight / CargoCapacity) * 100;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Truck class.
    /// </summary>
    /// <param name="brand">The truck brand</param>
    /// <param name="model">The truck model</param>
    /// <param name="year">The manufacturing year</param>
    /// <param name="type">The truck type</param>
    /// <param name="cargoCapacity">Maximum cargo capacity in kg</param>
    /// <param name="color">The truck color</param>
    public Truck(string brand, string model, int year, TruckType type, 
                 double cargoCapacity, string color = "White")
        : base(brand, model, year, color)
    {
        Type = type;
        CargoCapacity = cargoCapacity;
        CurrentCargoWeight = 0;
        NumberOfAxles = type == TruckType.Heavy ? 3 : 2;
        HasTrailer = false;
        IsCovered = false;
        IsCargoBedOpen = false;
        
        Console.WriteLine($"[Truck Constructor] {type} truck created with {cargoCapacity}kg capacity");
    }
    
    #endregion
    
    #region Overridden Methods
    
    /// <summary>
    /// Overrides the Start method with truck-specific startup.
    /// </summary>
    public override void Start()
    {
        Console.WriteLine($"\n🚛 Starting {Brand} {Model} {Type} Truck...");
        
        // Truck-specific pre-start checks
        if (IsCargoBedOpen)
        {
            Console.WriteLine("⚠️ WARNING: Cargo bed is open! Close before driving.");
        }
        
        if (CurrentCargoWeight > CargoCapacity)
        {
            Console.WriteLine("⚠️ OVERWEIGHT! Cannot start - exceeds cargo capacity!");
            return;
        }
        
        if (!PerformEngineCheck())
        {
            Console.WriteLine("Engine check failed.");
            return;
        }
        
        // Call base Start
        base.Start();
        
        // Truck-specific post-start
        if (_isEngineRunning)
        {
            Console.WriteLine($"Diesel engine rumbling... 🚛 ({FuelType})");
            Console.WriteLine($"Cargo load: {LoadPercentage:F1}% ({CurrentCargoWeight}kg / {CargoCapacity}kg)");
        }
    }
    
    /// <summary>
    /// Overrides the Stop method with truck-specific behavior.
    /// </summary>
    public override void Stop()
    {
        Console.WriteLine($"\n🛑 Stopping {Brand} {Model} Truck...");
        
        // Call base Stop
        base.Stop();
        
        // Truck-specific post-stop
        if (!_isEngineRunning)
        {
            Console.WriteLine("Truck parked. Engage parking brake! 🅿️");
        }
    }
    
    /// <summary>
    /// Overrides the Accelerate method - trucks accelerate slower, especially when loaded.
    /// </summary>
    /// <param name="speedIncrease">Speed increase in km/h</param>
    public override void Accelerate(double speedIncrease)
    {
        if (!_isEngineRunning)
        {
            Console.WriteLine("Cannot accelerate. Start the engine first!");
            return;
        }
        
        if (IsCargoBedOpen)
        {
            Console.WriteLine("Cannot accelerate with cargo bed open! ⚠️");
            return;
        }
        
        // Trucks accelerate slower - reduce by load percentage
        double loadFactor = 1.0 - (LoadPercentage / 200); // Max 50% reduction
        double truckAcceleration = speedIncrease * loadFactor;
        
        if (CurrentCargoWeight > 0)
        {
            Console.WriteLine($"🐢 Heavy load ({LoadPercentage:F1}%): Slow acceleration");
        }
        
        // Call base with reduced acceleration
        base.Accelerate(truckAcceleration);
    }
    
    /// <summary>
    /// Overrides the Brake method - trucks need more distance to brake when loaded.
    /// </summary>
    /// <param name="speedDecrease">Speed decrease in km/h</param>
    public override void Brake(double speedDecrease)
    {
        if (CurrentCargoWeight > CargoCapacity * 0.8)
        {
            Console.WriteLine("⚠️ HEAVY LOAD: Extra braking distance required!");
        }
        
        Console.WriteLine("🛑 Applying truck air brakes (PSSSHHH)");
        
        // Call base Brake
        base.Brake(speedDecrease);
    }
    
    /// <summary>
    /// Overrides the Honk method with a truck-specific air horn.
    /// </summary>
    public override void Honk()
    {
        Console.WriteLine("HOOOONK! 🚛📢📢 (Truck air horn - LOUD!)");
    }
    
    /// <summary>
    /// Overrides GetInfo to include truck-specific information.
    /// </summary>
    /// <returns>Detailed truck information</returns>
    public override string GetInfo()
    {
        string baseInfo = base.GetInfo();
        
        string truckInfo = $"""
                
                Truck-Specific Details
                =====================
                Type: {Type}
                Cargo Capacity: {CargoCapacity}kg
                Current Load: {CurrentCargoWeight}kg ({LoadPercentage:F1}%)
                Remaining Capacity: {RemainingCapacity:F1}kg
                Axles: {NumberOfAxles}
                Trailer: {(HasTrailer ? "Attached 🚛➕" : "None")}
                Cargo Bed: {(IsCargoBedOpen ? "Open ⬆️" : "Closed ⬇️")}
                Covered: {(IsCovered ? "Yes ☂️" : "No")}
                """;
        
        return baseInfo + truckInfo;
    }
    
    #endregion
    
    #region Truck-Specific Methods
    
    /// <summary>
    /// Loads cargo into the truck.
    /// </summary>
    /// <param name="weight">Weight of cargo in kg</param>
    public void LoadCargo(double weight)
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot load cargo while truck is moving! ⚠️");
            return;
        }
        
        if (!IsCargoBedOpen)
        {
            Console.WriteLine("Open cargo bed first before loading!");
            return;
        }
        
        if (weight <= 0)
        {
            Console.WriteLine("Cargo weight must be positive.");
            return;
        }
        
        if (CurrentCargoWeight + weight > CargoCapacity)
        {
            Console.WriteLine($"Cannot load {weight}kg - exceeds capacity!");
            Console.WriteLine($"Maximum additional load: {RemainingCapacity:F1}kg");
            return;
        }
        
        CurrentCargoWeight += weight;
        Console.WriteLine($"Loaded {weight}kg of cargo 📦");
        Console.WriteLine($"Total cargo: {CurrentCargoWeight}kg ({LoadPercentage:F1}% capacity)");
        
        if (LoadPercentage > 90)
        {
            Console.WriteLine("⚠️ WARNING: Near maximum capacity!");
        }
    }
    
    /// <summary>
    /// Unloads cargo from the truck.
    /// </summary>
    /// <param name="weight">Weight of cargo to unload in kg</param>
    public void UnloadCargo(double weight)
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot unload cargo while truck is moving! ⚠️");
            return;
        }
        
        if (!IsCargoBedOpen)
        {
            Console.WriteLine("Open cargo bed first before unloading!");
            return;
        }
        
        if (weight <= 0)
        {
            Console.WriteLine("Unload weight must be positive.");
            return;
        }
        
        if (weight > CurrentCargoWeight)
        {
            Console.WriteLine($"Cannot unload {weight}kg - only {CurrentCargoWeight}kg loaded!");
            return;
        }
        
        CurrentCargoWeight -= weight;
        Console.WriteLine($"Unloaded {weight}kg of cargo 📦");
        Console.WriteLine($"Remaining cargo: {CurrentCargoWeight}kg ({LoadPercentage:F1}% capacity)");
    }
    
    /// <summary>
    /// Opens the cargo bed.
    /// </summary>
    public void OpenCargoBed()
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot open cargo bed while moving! ⚠️");
            return;
        }
        
        if (IsCargoBedOpen)
        {
            Console.WriteLine("Cargo bed is already open.");
            return;
        }
        
        IsCargoBedOpen = true;
        Console.WriteLine("Cargo bed opened ⬆️ Ready for loading/unloading.");
    }
    
    /// <summary>
    /// Closes the cargo bed.
    /// </summary>
    public void CloseCargoBed()
    {
        if (!IsCargoBedOpen)
        {
            Console.WriteLine("Cargo bed is already closed.");
            return;
        }
        
        IsCargoBedOpen = false;
        Console.WriteLine("Cargo bed closed ⬇️ Cargo secured.");
    }
    
    /// <summary>
    /// Attaches a trailer to the truck.
    /// </summary>
    public void AttachTrailer()
    {
        if (_isEngineRunning)
        {
            Console.WriteLine("Turn off engine before attaching trailer!");
            return;
        }
        
        if (HasTrailer)
        {
            Console.WriteLine("Trailer is already attached.");
            return;
        }
        
        if (Type == TruckType.Light)
        {
            Console.WriteLine("Light trucks cannot safely tow trailers! ⚠️");
            return;
        }
        
        HasTrailer = true;
        Console.WriteLine("Trailer attached 🚛➕ Total wheels: " + NumberOfWheels);
    }
    
    /// <summary>
    /// Detaches the trailer from the truck.
    /// </summary>
    public void DetachTrailer()
    {
        if (_isEngineRunning)
        {
            Console.WriteLine("Turn off engine before detaching trailer!");
            return;
        }
        
        if (!HasTrailer)
        {
            Console.WriteLine("No trailer attached.");
            return;
        }
        
        HasTrailer = false;
        Console.WriteLine("Trailer detached 🚛 Total wheels: " + NumberOfWheels);
    }
    
    /// <summary>
    /// Performs a weight inspection.
    /// </summary>
    public void PerformWeightInspection()
    {
        Console.WriteLine("\n=== Weight Inspection ===");
        Console.WriteLine($"Cargo Capacity: {CargoCapacity}kg");
        Console.WriteLine($"Current Load: {CurrentCargoWeight}kg");
        Console.WriteLine($"Load Percentage: {LoadPercentage:F1}%");
        Console.WriteLine($"Remaining Capacity: {RemainingCapacity:F1}kg");
        
        if (CurrentCargoWeight > CargoCapacity)
        {
            Console.WriteLine("❌ OVERWEIGHT - Violation!");
        }
        else if (LoadPercentage > 95)
        {
            Console.WriteLine("⚠️ Warning: Near maximum capacity");
        }
        else
        {
            Console.WriteLine("✅ Within legal limits");
        }
        
        Console.WriteLine("========================\n");
    }
    
    #endregion
    
    #region Protected Method Override
    
    /// <summary>
    /// Overrides the protected PerformEngineCheck method.
    /// </summary>
    /// <returns>True if engine check passes</returns>
    protected override bool PerformEngineCheck()
    {
        base.PerformEngineCheck();
        
        LogDiagnostic("Checking truck-specific systems...");
        LogDiagnostic($"✓ Cargo capacity: {CargoCapacity}kg");
        LogDiagnostic($"✓ Current load: {CurrentCargoWeight}kg");
        LogDiagnostic("✓ Air brake system: OK");
        LogDiagnostic("✓ Suspension: OK");
        
        return CurrentCargoWeight <= CargoCapacity;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a detailed string representation of the truck.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        string trailerInfo = HasTrailer ? " with Trailer" : "";
        return $"{Year} {Brand} {Model} ({Color}) - {Type} Truck {CargoCapacity}kg{trailerInfo}";
    }
    
    #endregion
}

/// <summary>
/// Enumeration for truck types/classes.
/// </summary>
public enum TruckType
{
    /// <summary>
    /// Light truck (pickup truck)
    /// </summary>
    Light,
    
    /// <summary>
    /// Medium-duty truck
    /// </summary>
    Medium,
    
    /// <summary>
    /// Heavy-duty truck (18-wheeler, semi-truck)
    /// </summary>
    Heavy,
    
    /// <summary>
    /// Dump truck for construction
    /// </summary>
    Dump,
    
    /// <summary>
    /// Delivery/Box truck
    /// </summary>
    Delivery
}