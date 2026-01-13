namespace OOPFundamentals.Core.Inheritance;

/// <summary>
/// Represents a car, inheriting from Vehicle.
/// Demonstrates inheritance by extending the base class with car-specific functionality.
/// </summary>
/// <remarks>
/// This class demonstrates:
/// - Inheriting from a base class using the colon syntax (: Vehicle)
/// - Calling base class constructor with base()
/// - Overriding virtual methods with override keyword
/// - Adding new properties and methods specific to cars
/// - Using base keyword to call parent class methods
/// </remarks>
public class Car : Vehicle
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the number of doors.
    /// This is a car-specific property not in the base Vehicle class.
    /// </summary>
    public int NumberOfDoors { get; set; }
    
    /// <summary>
    /// Gets or sets the trunk capacity in liters.
    /// </summary>
    public double TrunkCapacity { get; set; }
    
    /// <summary>
    /// Gets or sets whether the car has a sunroof.
    /// </summary>
    public bool HasSunroof { get; set; }
    
    /// <summary>
    /// Gets or sets the transmission type (Automatic or Manual).
    /// </summary>
    public TransmissionType Transmission { get; set; }
    
    /// <summary>
    /// Gets whether air conditioning is on.
    /// </summary>
    public bool IsAirConditioningOn { get; private set; }
    
    /// <summary>
    /// Overrides the vehicle type to specify this is a Car.
    /// </summary>
    public override string VehicleType => "Car";
    
    /// <summary>
    /// Overrides the max speed with car-specific value.
    /// </summary>
    public override double MaxSpeed => 180.0; // km/h
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Car class.
    /// Demonstrates constructor chaining using base() to call parent constructor.
    /// </summary>
    /// <param name="brand">The car brand</param>
    /// <param name="model">The car model</param>
    /// <param name="year">The manufacturing year</param>
    /// <param name="numberOfDoors">The number of doors</param>
    /// <param name="color">The car color</param>
    public Car(string brand, string model, int year, int numberOfDoors, string color = "Black")
        : base(brand, model, year, color) // Call base class constructor
    {
        NumberOfDoors = numberOfDoors;
        TrunkCapacity = 400; // Default trunk capacity
        HasSunroof = false;
        Transmission = TransmissionType.Manual;
        IsAirConditioningOn = false;
        
        Console.WriteLine($"[Car Constructor] Car created with {numberOfDoors} doors");
    }
    
    /// <summary>
    /// Overloaded constructor with additional car-specific parameters.
    /// </summary>
    public Car(string brand, string model, int year, int numberOfDoors, 
               TransmissionType transmission, bool hasSunroof, string color = "Black")
        : this(brand, model, year, numberOfDoors, color) // Call other constructor
    {
        Transmission = transmission;
        HasSunroof = hasSunroof;
        
        Console.WriteLine($"[Car Constructor] Transmission: {transmission}, Sunroof: {hasSunroof}");
    }
    
    #endregion
    
    #region Overridden Methods
    
    /// <summary>
    /// Overrides the Start method to add car-specific startup behavior.
    /// Demonstrates method overriding with the override keyword.
    /// </summary>
    public override void Start()
    {
        Console.WriteLine($"\n🚗 Starting {Brand} {Model}...");
        
        // Perform car-specific pre-start checks
        if (!PerformEngineCheck())
        {
            Console.WriteLine("Engine check failed. Cannot start vehicle.");
            return;
        }
        
        // Call base class Start method using base keyword
        base.Start();
        
        // Car-specific post-start actions
        if (_isEngineRunning)
        {
            Console.WriteLine("Dashboard lights on ✨");
            Console.WriteLine("Seatbelt reminder active 🔔");
        }
    }
    
    /// <summary>
    /// Overrides the Stop method with car-specific behavior.
    /// </summary>
    public override void Stop()
    {
        Console.WriteLine($"\n🛑 Stopping {Brand} {Model}...");
        
        // Car-specific actions before stopping
        if (IsAirConditioningOn)
        {
            TurnOffAirConditioning();
        }
        
        // Call base class Stop method
        base.Stop();
        
        // Car-specific post-stop actions
        if (!_isEngineRunning)
        {
            Console.WriteLine("Dashboard lights off 💤");
            Console.WriteLine("All systems shutdown complete");
        }
    }
    
    /// <summary>
    /// Overrides the Accelerate method with car-specific acceleration.
    /// </summary>
    /// <param name="speedIncrease">Speed increase in km/h</param>
    public override void Accelerate(double speedIncrease)
    {
        if (!_isEngineRunning)
        {
            Console.WriteLine("Cannot accelerate. Start the engine first!");
            return;
        }
        
        // Car-specific acceleration message based on transmission
        if (Transmission == TransmissionType.Automatic)
        {
            Console.WriteLine("⚙️ Automatic transmission: Smooth acceleration");
        }
        else
        {
            Console.WriteLine("⚙️ Manual transmission: Shifting gears");
        }
        
        // Call base class Accelerate
        base.Accelerate(speedIncrease);
    }
    
    /// <summary>
    /// Overrides the Brake method with car-specific braking.
    /// </summary>
    /// <param name="speedDecrease">Speed decrease in km/h</param>
    public override void Brake(double speedDecrease)
    {
        Console.WriteLine("🛑 Applying car brakes (ABS active)");
        
        // Call base class Brake
        base.Brake(speedDecrease);
    }
    
    /// <summary>
    /// Overrides the Honk method with a car-specific horn sound.
    /// </summary>
    public override void Honk()
    {
        Console.WriteLine("BEEP BEEP! 🚗📢 (Car horn)");
    }
    
    /// <summary>
    /// Overrides GetInfo to include car-specific information.
    /// Demonstrates extending parent method functionality.
    /// </summary>
    /// <returns>Detailed car information</returns>
    public override string GetInfo()
    {
        // Get base vehicle information
        string baseInfo = base.GetInfo();
        
        // Add car-specific information
        string carInfo = $"""
                
                Car-Specific Details
                ====================
                Doors: {NumberOfDoors}
                Trunk Capacity: {TrunkCapacity} liters
                Transmission: {Transmission}
                Sunroof: {(HasSunroof ? "Yes ☀️" : "No")}
                Air Conditioning: {(IsAirConditioningOn ? "On ❄️" : "Off")}
                """;
        
        return baseInfo + carInfo;
    }
    
    #endregion
    
    #region Car-Specific Methods
    
    /// <summary>
    /// Opens the trunk. Car-specific functionality.
    /// </summary>
    public void OpenTrunk()
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot open trunk while car is moving! ⚠️");
            return;
        }
        
        Console.WriteLine($"Trunk opened. Capacity: {TrunkCapacity} liters 🧳");
    }
    
    /// <summary>
    /// Closes the trunk. Car-specific functionality.
    /// </summary>
    public void CloseTrunk()
    {
        Console.WriteLine("Trunk closed and locked 🔒");
    }
    
    /// <summary>
    /// Opens or closes the sunroof (if available).
    /// </summary>
    /// <param name="open">True to open, false to close</param>
    public void OperateSunroof(bool open)
    {
        if (!HasSunroof)
        {
            Console.WriteLine("This car doesn't have a sunroof.");
            return;
        }
        
        if (_currentSpeed > 100)
        {
            Console.WriteLine("Cannot operate sunroof at high speeds! ⚠️");
            return;
        }
        
        if (open)
        {
            Console.WriteLine("Sunroof opening... ☀️ Fresh air coming in!");
        }
        else
        {
            Console.WriteLine("Sunroof closing... 🌙 Sealed tight!");
        }
    }
    
    /// <summary>
    /// Turns on the air conditioning.
    /// </summary>
    public void TurnOnAirConditioning()
    {
        if (!_isEngineRunning)
        {
            Console.WriteLine("Cannot turn on A/C. Engine must be running.");
            return;
        }
        
        if (IsAirConditioningOn)
        {
            Console.WriteLine("Air conditioning is already on.");
            return;
        }
        
        IsAirConditioningOn = true;
        Console.WriteLine("Air conditioning turned on ❄️ Cooling down...");
    }
    
    /// <summary>
    /// Turns off the air conditioning.
    /// </summary>
    public void TurnOffAirConditioning()
    {
        if (!IsAirConditioningOn)
        {
            Console.WriteLine("Air conditioning is already off.");
            return;
        }
        
        IsAirConditioningOn = false;
        Console.WriteLine("Air conditioning turned off 🌡️");
    }
    
    /// <summary>
    /// Activates parking mode (car-specific feature).
    /// </summary>
    public void Park()
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot park while moving. Stop the car first! ⚠️");
            return;
        }
        
        Console.WriteLine($"Parking {Brand} {Model}...");
        Console.WriteLine("✅ Parking brake engaged");
        Console.WriteLine("✅ Transmission in Park mode");
        Console.WriteLine("✅ Steering wheel locked");
        Console.WriteLine("Car parked successfully! 🅿️");
    }
    
    /// <summary>
    /// Locks all car doors.
    /// </summary>
    public void LockDoors()
    {
        Console.WriteLine($"Locking all {NumberOfDoors} doors... 🔒 Click!");
    }
    
    /// <summary>
    /// Unlocks all car doors.
    /// </summary>
    public void UnlockDoors()
    {
        Console.WriteLine($"Unlocking all {NumberOfDoors} doors... 🔓 Click!");
    }
    
    #endregion
    
    #region Protected Method Override
    
    /// <summary>
    /// Overrides the protected PerformEngineCheck method.
    /// Demonstrates overriding protected members.
    /// </summary>
    /// <returns>True if engine check passes</returns>
    protected override bool PerformEngineCheck()
    {
        // Call base implementation
        base.PerformEngineCheck();
        
        // Add car-specific checks
        LogDiagnostic("Checking car-specific systems...");
        LogDiagnostic("✓ Transmission system: OK");
        LogDiagnostic("✓ Brake system: OK");
        LogDiagnostic("✓ Battery: OK");
        
        return true;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a detailed string representation of the car.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{Year} {Brand} {Model} ({Color}) - {NumberOfDoors}-door {Transmission} Car";
    }
    
    #endregion
}

/// <summary>
/// Enumeration for transmission types.
/// </summary>
public enum TransmissionType
{
    /// <summary>
    /// Manual transmission (stick shift)
    /// </summary>
    Manual,
    
    /// <summary>
    /// Automatic transmission
    /// </summary>
    Automatic,
    
    /// <summary>
    /// Continuously Variable Transmission
    /// </summary>
    CVT,
    
    /// <summary>
    /// Dual-clutch transmission
    /// </summary>
    DualClutch
}