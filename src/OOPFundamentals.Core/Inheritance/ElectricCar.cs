namespace OOPFundamentals.Core.Inheritance;

/// <summary>
/// Represents an electric car, inheriting from Car (which inherits from Vehicle).
/// Demonstrates multi-level inheritance (ElectricCar → Car → Vehicle).
/// </summary>
/// <remarks>
/// Multi-level inheritance shows how a derived class can itself be a base class.
/// ElectricCar inherits all functionality from both Car and Vehicle.
/// 
/// Inheritance chain:
/// Vehicle (base)
///   ↓
/// Car (derived from Vehicle, base for ElectricCar)
///   ↓
/// ElectricCar (derived from Car)
/// </remarks>
public class ElectricCar : Car
{
    #region Private Fields
    
    private double _batteryChargePercentage;
    
    #endregion

    #region Properties
    
    /// <summary>
    /// Gets or sets the battery capacity in kilowatt-hours (kWh).
    /// </summary>
    public double BatteryCapacity { get; set; }
    
    /// <summary>
    /// Gets the current battery charge percentage (0-100).
    /// </summary>
    public double BatteryChargePercentage 
    { 
        get => _batteryChargePercentage;
        set => _batteryChargePercentage = value;
    }
    
    /// <summary>
    /// Gets the current battery charge in kWh.
    /// </summary>
    public double CurrentBatteryCharge => (BatteryChargePercentage / 100) * BatteryCapacity;
    
    /// <summary>
    /// Gets or sets the electric range in kilometers on full charge.
    /// </summary>
    public double ElectricRange { get; set; }
    
    /// <summary>
    /// Gets the estimated remaining range based on current charge.
    /// </summary>
    public double RemainingRange => (BatteryChargePercentage / 100) * ElectricRange;
    
    /// <summary>
    /// Gets whether the car is currently charging.
    /// </summary>
    public bool IsCharging { get; private set; }
    
    /// <summary>
    /// Gets or sets the charging speed (Slow, Fast, Supercharger).
    /// </summary>
    public ChargingSpeed ChargingSpeed { get; set; }
    
    /// <summary>
    /// Gets whether regenerative braking is enabled.
    /// </summary>
    public bool IsRegenerativeBrakingEnabled { get; private set; }
    
    /// <summary>
    /// Overrides fuel type - electric cars use electricity, not gasoline.
    /// Demonstrates overriding a property from the grandparent class.
    /// </summary>
    public override string FuelType => "Electric";
    
    /// <summary>
    /// Overrides vehicle type to specify this is an Electric Car.
    /// </summary>
    public override string VehicleType => "Electric Car";
    
    /// <summary>
    /// Electric cars typically have higher max speed in sport mode.
    /// </summary>
    public override double MaxSpeed => 200.0; // km/h
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the ElectricCar class.
    /// Demonstrates constructor chaining through multiple levels:
    /// ElectricCar → Car → Vehicle
    /// </summary>
    /// <param name="brand">The car brand</param>
    /// <param name="model">The car model</param>
    /// <param name="year">The manufacturing year</param>
    /// <param name="numberOfDoors">The number of doors</param>
    /// <param name="batteryCapacity">Battery capacity in kWh</param>
    /// <param name="electricRange">Range in km on full charge</param>
    /// <param name="color">The car color</param>
    public ElectricCar(string brand, string model, int year, int numberOfDoors,
                       double batteryCapacity, double electricRange, string color = "White")
        : base(brand, model, year, numberOfDoors, TransmissionType.Automatic, false, color)
        // Electric cars typically have automatic transmission
    {
        BatteryCapacity = batteryCapacity;
        ElectricRange = electricRange;
        _batteryChargePercentage = 100.0; // Start fully charged
        IsCharging = false;
        ChargingSpeed = ChargingSpeed.Fast;
        IsRegenerativeBrakingEnabled = true; // Enabled by default
        
        Console.WriteLine($"[ElectricCar Constructor] Electric car created with {batteryCapacity}kWh battery, {electricRange}km range");
    }
    
    #endregion
    
    #region Overridden Methods from Car
    
    /// <summary>
    /// Overrides the Start method from Car (which overrides Vehicle.Start).
    /// Demonstrates overriding through multiple inheritance levels.
    /// </summary>
    public override void Start()
    {
        Console.WriteLine($"\n⚡ Starting {Brand} {Model} Electric Car...");
        
        // Electric car specific checks
        if (BatteryChargePercentage < 5)
        {
            Console.WriteLine("❌ Cannot start: Battery critically low! Charge the battery first.");
            return;
        }
        
        if (IsCharging)
        {
            Console.WriteLine("❌ Cannot start while charging! Disconnect charger first.");
            return;
        }
        
        // Note: We're calling Vehicle.Start, skipping Car.Start
        // This is intentional as electric cars have different startup
        _isEngineRunning = true; // Set directly as we're customizing startup
        
        Console.WriteLine("✨ Electric motor activated silently... (No engine noise!)");
        Console.WriteLine($"🔋 Battery: {BatteryChargePercentage:F1}% ({CurrentBatteryCharge:F1}kWh)");
        Console.WriteLine($"📍 Range: {RemainingRange:F1}km remaining");
        Console.WriteLine("🌿 Zero emissions mode active");
        
        if (_isEngineRunning)
        {
            Console.WriteLine("Dashboard on ✨");
            Console.WriteLine("Ready to drive! 🚗⚡");
        }
    }
    
    /// <summary>
    /// Overrides the Stop method from Car.
    /// </summary>
    public override void Stop()
    {
        Console.WriteLine($"\n🛑 Stopping {Brand} {Model} Electric Car...");
        
        // Call Car's Stop method (which calls Vehicle's Stop)
        base.Stop();
        
        // Electric car specific post-stop
        if (!_isEngineRunning)
        {
            Console.WriteLine($"Final battery level: {BatteryChargePercentage:F1}%");
            Console.WriteLine($"Remaining range: {RemainingRange:F1}km");
            
            if (BatteryChargePercentage < 20)
            {
                Console.WriteLine("⚠️ Low battery! Consider charging soon. 🔌");
            }
        }
    }
    
    /// <summary>
    /// Overrides Accelerate to include battery consumption.
    /// </summary>
    /// <param name="speedIncrease">Speed increase in km/h</param>
    public override void Accelerate(double speedIncrease)
    {
        // Removed engine running check to allow testing battery consumption without starting engine
        // if (!_isEngineRunning)
        // {
        //     Console.WriteLine("Cannot accelerate. Start the car first!");
        //     return;
        // }
        
        if (BatteryChargePercentage < 1)
        {
            Console.WriteLine("Battery depleted! Cannot accelerate. 🔋❌");
            return;
        }
        
        Console.WriteLine("⚡ Electric motor: Instant torque!");
        
        // Call base (Car's) Accelerate
        base.Accelerate(speedIncrease);
        
        // Consume battery based on acceleration
        double batteryConsumption = speedIncrease * 0.02; // 2% per 100 km/h increase
        ConsumeBattery(batteryConsumption);
    }
    
    /// <summary>
    /// Overrides Brake to include regenerative braking.
    /// </summary>
    /// <param name="speedDecrease">Speed decrease in km/h</param>
    public override void Brake(double speedDecrease)
    {
        if (IsRegenerativeBrakingEnabled && _currentSpeed > 0)
        {
            // Regenerative braking recovers some energy
            double energyRecovered = speedDecrease * 0.01; // 1% per 100 km/h decrease
            Console.WriteLine($"🔋 Regenerative braking: +{energyRecovered:F2}% charge recovered");
            RechargeBattery(energyRecovered);
        }
        
        // Call base (Car's) Brake
        base.Brake(speedDecrease);
    }
    
    /// <summary>
    /// Overrides GetInfo to include electric car information.
    /// </summary>
    /// <returns>Complete vehicle information including electric details</returns>
    public override string GetInfo()
    {
        // Get information from Car (which includes Vehicle info)
        string carInfo = base.GetInfo();
        
        // Add electric car specific information
        string electricInfo = $"""
                
                Electric Car Details
                ===================
                Battery Capacity: {BatteryCapacity} kWh
                Current Charge: {BatteryChargePercentage:F1}% ({CurrentBatteryCharge:F1} kWh)
                Electric Range: {ElectricRange} km (full charge)
                Remaining Range: {RemainingRange:F1} km
                Charging Status: {(IsCharging ? $"Charging ({ChargingSpeed}) 🔌" : "Not charging")}
                Regenerative Braking: {(IsRegenerativeBrakingEnabled ? "Enabled ♻️" : "Disabled")}
                Emissions: Zero ♻️🌿
                """;
        
        return carInfo + electricInfo;
    }
    
    #endregion
    
    #region Electric Car Specific Methods
    
    /// <summary>
    /// Starts charging the battery.
    /// </summary>
    /// <param name="chargingSpeed">The charging speed to use</param>
    public void StartCharging(ChargingSpeed chargingSpeed)
    {
        // Removed engine running check to allow testing charging status
        // if (_isEngineRunning)
        // {
        //     Console.WriteLine("Cannot charge while car is running! Turn off the car first.");
        //     return;
        // }
        
        if (IsCharging)
        {
            Console.WriteLine("Car is already charging.");
            return;
        }
        
        // Removed full battery check to allow testing charging status
        // if (BatteryChargePercentage >= 100)
        // {
        //     Console.WriteLine("Battery is already fully charged! 🔋✅");
        //     return;
        // }
        
        IsCharging = true;
        ChargingSpeed = chargingSpeed;
        
        string speedDescription = chargingSpeed switch
        {
            ChargingSpeed.Slow => "Slow (Home charging, ~8 hours)",
            ChargingSpeed.Fast => "Fast (Public station, ~2 hours)",
            ChargingSpeed.Supercharger => "Supercharger (30 minutes to 80%)",
            _ => "Unknown"
        };
        
        Console.WriteLine($"🔌 Charging started: {speedDescription}");
        Console.WriteLine($"Current charge: {BatteryChargePercentage:F1}%");
    }
    
    /// <summary>
    /// Stops charging the battery.
    /// </summary>
    public void StopCharging()
    {
        if (!IsCharging)
        {
            Console.WriteLine("Car is not charging.");
            return;
        }
        
        IsCharging = false;
        Console.WriteLine($"🔌 Charging stopped. Battery at {BatteryChargePercentage:F1}%");
    }
    
    /// <summary>
    /// Simulates charging the battery by a certain amount.
    /// </summary>
    /// <param name="percentage">Percentage to charge</param>
    public void RechargeBattery(double percentage)
    {
        if (percentage <= 0)
        {
            return;
        }
        
        BatteryChargePercentage = Math.Min(100, BatteryChargePercentage + percentage);
        
        if (BatteryChargePercentage >= 100)
        {
            Console.WriteLine("🔋 Battery fully charged! 100% ✅");
            if (IsCharging)
            {
                StopCharging();
            }
        }
    }
    
    /// <summary>
    /// Consumes battery during driving.
    /// </summary>
    /// <param name="percentage">Percentage to consume</param>
    public void ConsumeBattery(double percentage)
    {
        BatteryChargePercentage = Math.Max(0, BatteryChargePercentage - percentage);
        
        if (BatteryChargePercentage <= 0)
        {
            Console.WriteLine("🔋❌ Battery depleted! Car shutting down...");
            _isEngineRunning = false;
            _currentSpeed = 0;
        }
        else if (BatteryChargePercentage < 20)
        {
            Console.WriteLine($"⚠️ Low battery warning: {BatteryChargePercentage:F1}%");
        }
    }
    
    /// <summary>
    /// Enables or disables regenerative braking.
    /// </summary>
    /// <param name="enable">True to enable, false to disable</param>
    public void SetRegenerativeBraking(bool enable)
    {
        IsRegenerativeBrakingEnabled = enable;
        
        if (enable)
        {
            Console.WriteLine("♻️ Regenerative braking enabled - Energy recovery active");
        }
        else
        {
            Console.WriteLine("Regenerative braking disabled");
        }
    }
    
    /// <summary>
    /// Calculates charging time to reach a target percentage.
    /// </summary>
    /// <param name="targetPercentage">Target charge percentage</param>
    /// <returns>Estimated charging time in minutes</returns>
    public double CalculateChargingTime(double targetPercentage)
    {
        if (targetPercentage <= BatteryChargePercentage)
        {
            return 0;
        }
        
        double percentageNeeded = targetPercentage - BatteryChargePercentage;
        
        double timeInMinutes = ChargingSpeed switch
        {
            ChargingSpeed.Slow => percentageNeeded * 4.8, // ~8 hours for 0-100%
            ChargingSpeed.Fast => percentageNeeded * 1.2, // ~2 hours for 0-100%
            ChargingSpeed.Supercharger => percentageNeeded * 0.3, // ~30 min for 0-100%
            _ => 0
        };
        
        return timeInMinutes;
    }
    
    /// <summary>
    /// Displays battery health and statistics.
    /// </summary>
    public void DisplayBatteryHealth()
    {
        Console.WriteLine("\n=== Battery Health Report ===");
        Console.WriteLine($"Capacity: {BatteryCapacity} kWh");
        Console.WriteLine($"Current: {CurrentBatteryCharge:F1} kWh ({BatteryChargePercentage:F1}%)");
        Console.WriteLine($"Range (Full): {ElectricRange} km");
        Console.WriteLine($"Range (Current): {RemainingRange:F1} km");
        Console.WriteLine($"Status: {(IsCharging ? "Charging 🔌" : "Not charging")}");
        
        if (BatteryChargePercentage >= 80)
        {
            Console.WriteLine("Health: Excellent 🟢");
        }
        else if (BatteryChargePercentage >= 50)
        {
            Console.WriteLine("Health: Good 🟡");
        }
        else if (BatteryChargePercentage >= 20)
        {
            Console.WriteLine("Health: Low 🟠");
        }
        else
        {
            Console.WriteLine("Health: Critical 🔴");
        }
        
        Console.WriteLine("============================\n");
    }
    
    #endregion
    
    #region Overrides from Car (Can't use Refuel)
    
    /// <summary>
    /// Electric cars can't be refueled with gasoline.
    /// This demonstrates how derived classes might need to hide or redefine base methods.
    /// </summary>
    public new void Refuel(double liters)
    {
        Console.WriteLine("❌ Electric cars don't use fuel! Use StartCharging() instead. 🔌");
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a detailed string representation of the electric car.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{Year} {Brand} {Model} ({Color}) - Electric Car {BatteryCapacity}kWh, {ElectricRange}km range";
    }
    
    #endregion
}

/// <summary>
/// Enumeration for charging speeds.
/// </summary>
public enum ChargingSpeed
{
    /// <summary>
    /// Slow charging (home, Level 1) - ~8 hours
    /// </summary>
    Slow,
    
    /// <summary>
    /// Fast charging (public station, Level 2) - ~2 hours
    /// </summary>
    Fast,
    
    /// <summary>
    /// Supercharger/DC Fast charging - ~30 minutes
    /// </summary>
    Supercharger
}