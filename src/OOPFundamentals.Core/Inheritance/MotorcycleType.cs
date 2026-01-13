namespace OOPFundamentals.Core.Inheritance;

/// <summary>
/// Represents a motorcycle, inheriting from Vehicle.
/// Demonstrates inheritance with motorcycle-specific characteristics.
/// </summary>
/// <remarks>
/// Shows how different derived classes can:
/// - Override the same base methods differently
/// - Have completely different properties
/// - Implement vehicle behavior specific to their type
/// </remarks>
public class Motorcycle : Vehicle
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the motorcycle type/style.
    /// </summary>
    public MotorcycleType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the engine displacement in cubic centimeters (cc).
    /// </summary>
    public int EngineCC { get; set; }
    
    /// <summary>
    /// Gets or sets whether the motorcycle has a sidecar.
    /// </summary>
    public bool HasSidecar { get; set; }
    
    /// <summary>
    /// Gets or sets whether the rider is wearing a helmet.
    /// </summary>
    public bool IsWearingHelmet { get; set; }
    
    /// <summary>
    /// Gets whether the kickstand is down.
    /// </summary>
    public bool IsKickstandDown { get; private set; }
    
    /// <summary>
    /// Overrides the vehicle type to specify this is a Motorcycle.
    /// </summary>
    public override string VehicleType => $"{Type} Motorcycle";
    
    /// <summary>
    /// Overrides the number of wheels for a motorcycle.
    /// Changes to 3 if has sidecar, otherwise 2.
    /// </summary>
    public override int NumberOfWheels => HasSidecar ? 3 : 2;
    
    /// <summary>
    /// Overrides max speed based on motorcycle type and engine size.
    /// </summary>
    public override double MaxSpeed
    {
        get
        {
            // Sport bikes are faster
            double baseSpeed = Type == MotorcycleType.Sport ? 250.0 : 160.0;
            
            // Larger engines = higher top speed
            double engineFactor = EngineCC / 1000.0;
            
            return baseSpeed * engineFactor;
        }
    }
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Motorcycle class.
    /// Demonstrates constructor chaining with base class.
    /// </summary>
    /// <param name="brand">The motorcycle brand</param>
    /// <param name="model">The motorcycle model</param>
    /// <param name="year">The manufacturing year</param>
    /// <param name="type">The motorcycle type</param>
    /// <param name="engineCC">Engine displacement in cc</param>
    /// <param name="color">The motorcycle color</param>
    public Motorcycle(string brand, string model, int year, MotorcycleType type, 
                      int engineCC, string color = "Black")
        : base(brand, model, year, color)
    {
        Type = type;
        EngineCC = engineCC;
        HasSidecar = false;
        IsWearingHelmet = false;
        IsKickstandDown = true; // Kickstand down by default when parked
        
        Console.WriteLine($"[Motorcycle Constructor] {type} motorcycle created with {engineCC}cc engine");
    }
    
    #endregion
    
    #region Overridden Methods
    
    /// <summary>
    /// Overrides the Start method with motorcycle-specific startup.
    /// </summary>
    public override void Start()
    {
        Console.WriteLine($"\n🏍️ Starting {Brand} {Model} {Type} Motorcycle...");
        
        // Motorcycle-specific pre-start checks
        if (!IsWearingHelmet)
        {
            Console.WriteLine("⚠️ WARNING: No helmet detected! Safety first! 🪖");
        }
        
        if (IsKickstandDown)
        {
            Console.WriteLine("⚠️ Cannot start with kickstand down! Retract kickstand first.");
            return;
        }
        
        if (!PerformEngineCheck())
        {
            Console.WriteLine("Engine check failed.");
            return;
        }
        
        // Call base Start
        base.Start();
        
        // Motorcycle-specific post-start
        if (_isEngineRunning)
        {
            Console.WriteLine($"Engine roaring! VROOM VROOM! 🏍️💨 ({EngineCC}cc)");
            Console.WriteLine("Headlight on 💡");
        }
    }
    
    /// <summary>
    /// Overrides the Stop method with motorcycle-specific behavior.
    /// </summary>
    public override void Stop()
    {
        Console.WriteLine($"\n🛑 Stopping {Brand} {Model}...");
        
        // Call base Stop
        base.Stop();
        
        // Motorcycle-specific post-stop
        if (!_isEngineRunning)
        {
            Console.WriteLine("Headlight off 🌙");
            Console.WriteLine("Don't forget to put the kickstand down!");
        }
    }
    
    /// <summary>
    /// Overrides the Accelerate method with motorcycle-specific acceleration.
    /// Motorcycles typically accelerate faster than cars.
    /// </summary>
    /// <param name="speedIncrease">Speed increase in km/h</param>
    public override void Accelerate(double speedIncrease)
    {
        if (!_isEngineRunning)
        {
            Console.WriteLine("Cannot accelerate. Start the engine first!");
            return;
        }
        
        if (IsKickstandDown)
        {
            Console.WriteLine("Cannot accelerate with kickstand down! ⚠️");
            return;
        }
        
        // Motorcycles accelerate faster - apply multiplier
        double motorcycleAcceleration = speedIncrease * 1.5;
        
        Console.WriteLine($"⚡ {Type} motorcycle: Rapid acceleration!");
        
        // Call base with boosted acceleration
        base.Accelerate(motorcycleAcceleration);
    }
    
    /// <summary>
    /// Overrides the Brake method with motorcycle-specific braking.
    /// </summary>
    /// <param name="speedDecrease">Speed decrease in km/h</param>
    public override void Brake(double speedDecrease)
    {
        Console.WriteLine("🛑 Applying motorcycle brakes (Front + Rear)");
        
        // Call base Brake
        base.Brake(speedDecrease);
        
        // If stopped, remind about kickstand
        if (_currentSpeed == 0)
        {
            Console.WriteLine("💡 Tip: Put kickstand down before dismounting!");
        }
    }
    
    /// <summary>
    /// Overrides the Honk method with a motorcycle-specific horn.
    /// </summary>
    public override void Honk()
    {
        Console.WriteLine("BEEP! 🏍️📢 (Motorcycle horn - higher pitch)");
    }
    
    /// <summary>
    /// Overrides GetInfo to include motorcycle-specific information.
    /// </summary>
    /// <returns>Detailed motorcycle information</returns>
    public override string GetInfo()
    {
        // Get base vehicle information
        string baseInfo = base.GetInfo();
        
        // Add motorcycle-specific information
        string motorcycleInfo = $"""
                
                Motorcycle-Specific Details
                ===========================
                Type: {Type}
                Engine: {EngineCC}cc
                Sidecar: {(HasSidecar ? "Yes" : "No")}
                Helmet: {(IsWearingHelmet ? "On 🪖" : "Off ⚠️")}
                Kickstand: {(IsKickstandDown ? "Down ⬇️" : "Up ⬆️")}
                Acceleration Boost: 1.5x
                """;
        
        return baseInfo + motorcycleInfo;
    }
    
    #endregion
    
    #region Motorcycle-Specific Methods
    
    /// <summary>
    /// Puts the kickstand down. Required before dismounting.
    /// </summary>
    public void PutKickstandDown()
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot put kickstand down while moving! ⚠️");
            return;
        }
        
        if (IsKickstandDown)
        {
            Console.WriteLine("Kickstand is already down.");
            return;
        }
        
        IsKickstandDown = true;
        Console.WriteLine("Kickstand down ⬇️ Motorcycle is stable.");
    }
    
    /// <summary>
    /// Retracts the kickstand. Required before riding.
    /// </summary>
    public void RetractKickstand()
    {
        if (!IsKickstandDown)
        {
            Console.WriteLine("Kickstand is already up.");
            return;
        }
        
        IsKickstandDown = false;
        Console.WriteLine("Kickstand retracted ⬆️ Ready to ride!");
    }
    
    /// <summary>
    /// Puts on the helmet (safety first!).
    /// </summary>
    public void PutOnHelmet()
    {
        if (IsWearingHelmet)
        {
            Console.WriteLine("Helmet is already on.");
            return;
        }
        
        IsWearingHelmet = true;
        Console.WriteLine("Helmet on 🪖 Safety first! Ready to ride safely.");
    }
    
    /// <summary>
    /// Removes the helmet.
    /// </summary>
    public void RemoveHelmet()
    {
        if (_currentSpeed > 0)
        {
            Console.WriteLine("Cannot remove helmet while riding! ⚠️ Safety first!");
            return;
        }
        
        if (!IsWearingHelmet)
        {
            Console.WriteLine("Helmet is already off.");
            return;
        }
        
        IsWearingHelmet = false;
        Console.WriteLine("Helmet removed.");
    }
    
    /// <summary>
    /// Performs a wheelie (sport bikes only).
    /// </summary>
    public void DoWheelie()
    {
        if (Type != MotorcycleType.Sport)
        {
            Console.WriteLine($"{Type} motorcycles are not designed for wheelies!");
            return;
        }
        
        if (!_isEngineRunning || _currentSpeed < 30)
        {
            Console.WriteLine("Need more speed to perform a wheelie! (minimum 30 km/h)");
            return;
        }
        
        if (!IsWearingHelmet)
        {
            Console.WriteLine("⚠️ Too dangerous without a helmet! Put helmet on first.");
            return;
        }
        
        Console.WriteLine("🏍️💨 WHEELIE! Front wheel up! 🎪");
        Console.WriteLine("⚠️ Warning: Dangerous maneuver! Professional rider on closed course.");
    }
    
    /// <summary>
    /// Attaches or detaches a sidecar.
    /// </summary>
    /// <param name="attach">True to attach, false to detach</param>
    public void ToggleSidecar(bool attach)
    {
        if (_isEngineRunning)
        {
            Console.WriteLine("Cannot modify sidecar while engine is running!");
            return;
        }
        
        if (attach && !HasSidecar)
        {
            HasSidecar = true;
            Console.WriteLine("Sidecar attached! Now have 3 wheels. 🏍️➕");
        }
        else if (!attach && HasSidecar)
        {
            HasSidecar = false;
            Console.WriteLine("Sidecar detached! Back to 2 wheels. 🏍️");
        }
        else
        {
            Console.WriteLine($"Sidecar is already {(HasSidecar ? "attached" : "detached")}.");
        }
    }
    
    /// <summary>
    /// Performs a complete pre-ride safety check.
    /// </summary>
    public void PerformSafetyCheck()
    {
        Console.WriteLine("\n=== Pre-Ride Safety Check ===");
        Console.WriteLine($"✓ Helmet: {(IsWearingHelmet ? "ON 🪖" : "MISSING ⚠️")}");
        Console.WriteLine($"✓ Kickstand: {(IsKickstandDown ? "DOWN (retract before riding)" : "UP ✓")}");
        Console.WriteLine($"✓ Engine: {(_isEngineRunning ? "RUNNING ✓" : "OFF")}");
        Console.WriteLine($"✓ Wheels: {NumberOfWheels} wheel(s)");
        Console.WriteLine("✓ Brakes: OK");
        Console.WriteLine("✓ Lights: OK");
        Console.WriteLine("✓ Tire pressure: OK");
        Console.WriteLine("============================\n");
        
        if (!IsWearingHelmet)
        {
            Console.WriteLine("⚠️ SAFETY WARNING: Always wear a helmet!");
        }
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
        
        LogDiagnostic("Checking motorcycle-specific systems...");
        LogDiagnostic($"✓ Engine: {EngineCC}cc - OK");
        LogDiagnostic("✓ Chain/Belt: OK");
        LogDiagnostic("✓ Suspension: OK");
        
        return true;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a detailed string representation of the motorcycle.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        string sidecarInfo = HasSidecar ? " with Sidecar" : "";
        return $"{Year} {Brand} {Model} ({Color}) - {Type} Motorcycle {EngineCC}cc{sidecarInfo}";
    }
    
    #endregion
}

/// <summary>
/// Enumeration for motorcycle types.
/// </summary>
public enum MotorcycleType
{
    /// <summary>
    /// Sport/Racing motorcycle - high performance
    /// </summary>
    Sport,
    
    /// <summary>
    /// Cruiser motorcycle - comfortable for long rides
    /// </summary>
    Cruiser,
    
    /// <summary>
    /// Touring motorcycle - designed for long distances
    /// </summary>
    Touring,
    
    /// <summary>
    /// Off-road/Dirt bike
    /// </summary>
    Dirt,
    
    /// <summary>
    /// Standard/Standard motorcycle
    /// </summary>
    Standard,
    
    /// <summary>
    /// Chopper - custom style
    /// </summary>
    Chopper
}