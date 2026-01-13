namespace OOPFundamentals.Core.Inheritance;

/// <summary>
/// Base class representing a generic vehicle.
/// Demonstrates inheritance concepts by providing common functionality
/// that all vehicles share.
/// </summary>
/// <remarks>
/// Inheritance is demonstrated through:
/// - Base class with common properties and methods
/// - Virtual methods that can be overridden by derived classes
/// - Protected members accessible to derived classes
/// - Constructor chaining with the base keyword
/// </remarks>
public class Vehicle
{
    #region Protected Fields
    
    /// <summary>
    /// Protected field accessible to derived classes.
    /// Demonstrates protected access modifier for inheritance.
    /// </summary>
    protected string _vehicleId;
    
    /// <summary>
    /// Protected field for tracking engine status.
    /// </summary>
    protected bool _isEngineRunning;
    
    /// <summary>
    /// Protected field for current speed.
    /// </summary>
    protected double _currentSpeed;
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets the unique vehicle identifier.
    /// </summary>
    public string VehicleId => _vehicleId;
    
    /// <summary>
    /// Gets or sets the manufacturer/brand of the vehicle.
    /// </summary>
    public string Brand { get; set; }
    
    /// <summary>
    /// Gets or sets the model of the vehicle.
    /// </summary>
    public string Model { get; set; }
    
    /// <summary>
    /// Gets or sets the manufacturing year.
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Gets or sets the color of the vehicle.
    /// </summary>
    public string Color { get; set; }
    
    /// <summary>
    /// Gets the current speed of the vehicle.
    /// </summary>
    public double CurrentSpeed => _currentSpeed;
    
    /// <summary>
    /// Gets whether the engine is currently running.
    /// </summary>
    public bool IsEngineRunning => _isEngineRunning;
    
    /// <summary>
    /// Gets the maximum speed of the vehicle.
    /// Virtual property that can be overridden by derived classes.
    /// </summary>
    public virtual double MaxSpeed => 120.0; // Default max speed in km/h
    
    /// <summary>
    /// Gets the fuel type used by the vehicle.
    /// Virtual property that can be overridden by derived classes.
    /// </summary>
    public virtual string FuelType => "Gasoline";
    
    /// <summary>
    /// Gets the number of wheels.
    /// Virtual property that can be overridden by derived classes.
    /// </summary>
    public virtual int NumberOfWheels => 4;
    
    /// <summary>
    /// Gets the vehicle type description.
    /// Virtual property that derived classes should override.
    /// </summary>
    public virtual string VehicleType => "Generic Vehicle";
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Vehicle class.
    /// This constructor will be called by derived classes using base().
    /// </summary>
    /// <param name="brand">The manufacturer/brand</param>
    /// <param name="model">The model name</param>
    /// <param name="year">The manufacturing year</param>
    /// <param name="color">The vehicle color</param>
    public Vehicle(string brand, string model, int year, string color = "Black")
    {
        _vehicleId = GenerateVehicleId();
        Brand = brand ?? throw new ArgumentNullException(nameof(brand));
        Model = model ?? throw new ArgumentNullException(nameof(model));
        Year = year;
        Color = color;
        _isEngineRunning = false;
        _currentSpeed = 0;
        
        Console.WriteLine($"[Vehicle Constructor] Creating {VehicleType}: {Brand} {Model}");
    }
    
    #endregion
    
    #region Virtual Methods
    
    /// <summary>
    /// Starts the vehicle's engine.
    /// Virtual method that can be overridden by derived classes for specific behavior.
    /// </summary>
    public virtual void Start()
    {
        if (_isEngineRunning)
        {
            Console.WriteLine($"{Brand} {Model} engine is already running.");
            return;
        }
        
        _isEngineRunning = true;
        Console.WriteLine($"Starting {Brand} {Model}... Engine started successfully! 🚗");
    }
    
    /// <summary>
    /// Stops the vehicle's engine.
    /// Virtual method that can be overridden by derived classes.
    /// </summary>
    public virtual void Stop()
    {
        if (!_isEngineRunning)
        {
            Console.WriteLine($"{Brand} {Model} engine is already off.");
            return;
        }
        
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot stop engine while vehicle is moving. Brake first!");
            return;
        }
        
        _isEngineRunning = false;
        Console.WriteLine($"Stopping {Brand} {Model}... Engine turned off. 🛑");
    }
    
    /// <summary>
    /// Accelerates the vehicle.
    /// Virtual method that can be overridden by derived classes for specific acceleration behavior.
    /// </summary>
    /// <param name="speedIncrease">The amount to increase speed by (km/h)</param>
    public virtual void Accelerate(double speedIncrease)
    {
        if (!_isEngineRunning)
        {
            Console.WriteLine("Cannot accelerate. Engine is not running. Start the engine first!");
            return;
        }
        
        if (speedIncrease <= 0)
        {
            Console.WriteLine("Speed increase must be positive.");
            return;
        }
        
        double newSpeed = _currentSpeed + speedIncrease;
        
        if (newSpeed > MaxSpeed)
        {
            _currentSpeed = MaxSpeed;
            Console.WriteLine($"Maximum speed reached! Current speed: {_currentSpeed:F1} km/h 🏎️");
        }
        else
        {
            _currentSpeed = newSpeed;
            Console.WriteLine($"Accelerating... Current speed: {_currentSpeed:F1} km/h");
        }
    }
    
    /// <summary>
    /// Applies brakes to slow down the vehicle.
    /// Virtual method that can be overridden by derived classes.
    /// </summary>
    /// <param name="speedDecrease">The amount to decrease speed by (km/h)</param>
    public virtual void Brake(double speedDecrease)
    {
        if (speedDecrease <= 0)
        {
            Console.WriteLine("Speed decrease must be positive.");
            return;
        }
        
        double newSpeed = _currentSpeed - speedDecrease;
        
        if (newSpeed <= 0)
        {
            _currentSpeed = 0;
            Console.WriteLine("Vehicle stopped completely. 🛑");
        }
        else
        {
            _currentSpeed = newSpeed;
            Console.WriteLine($"Braking... Current speed: {_currentSpeed:F1} km/h");
        }
    }
    
    /// <summary>
    /// Sounds the vehicle's horn.
    /// Virtual method that can be overridden for different horn sounds.
    /// </summary>
    public virtual void Honk()
    {
        Console.WriteLine("Beep beep! 📢");
    }
    
    /// <summary>
    /// Gets detailed information about the vehicle.
    /// Virtual method that derived classes can override to add specific information.
    /// </summary>
    /// <returns>A formatted string with vehicle information</returns>
    public virtual string GetInfo()
    {
        return $"""
                Vehicle Information
                ==================
                Type: {VehicleType}
                ID: {_vehicleId}
                Brand: {Brand}
                Model: {Model}
                Year: {Year}
                Color: {Color}
                Fuel Type: {FuelType}
                Wheels: {NumberOfWheels}
                Max Speed: {MaxSpeed:F1} km/h
                Current Speed: {_currentSpeed:F1} km/h
                Engine Status: {(_isEngineRunning ? "Running 🟢" : "Off 🔴")}
                """;
    }
    
    #endregion
    
    #region Protected Methods
    
    /// <summary>
    /// Protected method accessible only to this class and derived classes.
    /// Demonstrates protected access modifier in inheritance.
    /// </summary>
    /// <param name="message">The diagnostic message</param>
    protected void LogDiagnostic(string message)
    {
        Console.WriteLine($"[DIAGNOSTIC - {_vehicleId}] {message}");
    }
    
    /// <summary>
    /// Protected method to perform engine check.
    /// Can be called by derived classes.
    /// </summary>
    /// <returns>True if engine is healthy</returns>
    protected virtual bool PerformEngineCheck()
    {
        LogDiagnostic("Performing engine check...");
        return true; // Base implementation always returns true
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// Refuels the vehicle.
    /// This is a concrete method that all vehicles share.
    /// </summary>
    /// <param name="liters">Amount of fuel in liters</param>
    public void Refuel(double liters)
    {
        if (liters <= 0)
        {
            Console.WriteLine("Fuel amount must be positive.");
            return;
        }
        
        if (_isEngineRunning)
        {
            Console.WriteLine("Warning: Turn off engine before refueling! ⚠️");
            return;
        }
        
        Console.WriteLine($"Refueling {Brand} {Model} with {liters:F1} liters of {FuelType}... ⛽");
    }
    
    /// <summary>
    /// Displays the current status of the vehicle.
    /// </summary>
    public void DisplayStatus()
    {
        Console.WriteLine($"\n{GetInfo()}\n");
    }
    
    #endregion
    
    #region Private Methods
    
    /// <summary>
    /// Generates a unique vehicle identifier.
    /// Private method - not accessible to derived classes.
    /// </summary>
    /// <returns>A unique vehicle ID</returns>
    private string GenerateVehicleId()
    {
        return $"VEH-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the vehicle.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{Year} {Brand} {Model} ({Color}) - {VehicleType}";
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current vehicle.
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if objects are equal</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Vehicle other)
        {
            return _vehicleId == other._vehicleId;
        }
        return false;
    }
    
    /// <summary>
    /// Gets the hash code for the vehicle.
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        return _vehicleId.GetHashCode();
    }
    
    #endregion
}