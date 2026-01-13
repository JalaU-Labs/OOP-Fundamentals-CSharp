namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Represents a circle shape.
/// Concrete implementation of the abstract Shape class.
/// </summary>
/// <remarks>
/// This class demonstrates:
/// - Implementation of abstract methods from the base class
/// - Override of virtual methods for custom behavior
/// - Additional properties specific to circles
/// </remarks>
public class Circle : Shape
{
    #region Private Fields
    
    /// <summary>
    /// Private backing field for the radius.
    /// </summary>
    private double _radius;
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets or sets the radius of the circle.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when radius is not positive</exception>
    public double Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Radius must be positive.");
            }
            _radius = value;
        }
    }
    
    /// <summary>
    /// Gets the diameter of the circle (calculated property).
    /// </summary>
    public double Diameter => _radius * 2;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Circle class.
    /// </summary>
    /// <param name="radius">The radius of the circle</param>
    /// <param name="color">The color of the circle</param>
    public Circle(double radius, string color = "Black") 
        : base("Circle", color)
    {
        Radius = radius; // Uses property setter with validation
    }
    
    #endregion
    
    #region Abstract Methods Implementation
    
    /// <summary>
    /// Calculates the area of the circle using the formula: π * r²
    /// </summary>
    /// <returns>The area of the circle</returns>
    public override double CalculateArea()
    {
        return Math.PI * _radius * _radius;
    }
    
    /// <summary>
    /// Calculates the perimeter (circumference) of the circle using the formula: 2 * π * r
    /// </summary>
    /// <returns>The perimeter of the circle</returns>
    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * _radius;
    }
    
    /// <summary>
    /// Gets information specific to the circle.
    /// </summary>
    /// <returns>A string with circle-specific properties</returns>
    public override string GetShapeInfo()
    {
        return $"Radius: {_radius:F2} units\nDiameter: {Diameter:F2} units";
    }
    
    #endregion
    
    #region Virtual Methods Override
    
    /// <summary>
    /// Draws the circle with a custom ASCII representation.
    /// Overrides the base class Draw method.
    /// </summary>
    public override void Draw()
    {
        base.Draw(); // Call base implementation first
        
        Console.WriteLine("\nASCII Representation:");
        Console.WriteLine("      ***      ");
        Console.WriteLine("   *       *   ");
        Console.WriteLine("  *         *  ");
        Console.WriteLine(" *           * ");
        Console.WriteLine("  *         *  ");
        Console.WriteLine("   *       *   ");
        Console.WriteLine("      ***      \n");
    }
    
    /// <summary>
    /// Resizes the circle by adjusting its radius.
    /// Overrides the base class Resize method.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale the radius by</param>
    public override void Resize(double scaleFactor)
    {
        base.Resize(scaleFactor); // Call base for validation and logging
        
        double oldRadius = _radius;
        _radius *= scaleFactor;
        
        Console.WriteLine($"Radius changed from {oldRadius:F2} to {_radius:F2}");
    }
    
    #endregion
    
    #region Circle-Specific Methods
    
    /// <summary>
    /// Checks if a point is inside the circle.
    /// </summary>
    /// <param name="x">X coordinate of the point</param>
    /// <param name="y">Y coordinate of the point</param>
    /// <param name="centerX">X coordinate of circle center (default: 0)</param>
    /// <param name="centerY">Y coordinate of circle center (default: 0)</param>
    /// <returns>True if point is inside the circle</returns>
    public bool ContainsPoint(double x, double y, double centerX = 0, double centerY = 0)
    {
        double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
        return distance <= _radius;
    }
    
    /// <summary>
    /// Calculates the area of a sector (portion) of the circle.
    /// </summary>
    /// <param name="angleInDegrees">The angle of the sector in degrees</param>
    /// <returns>The area of the sector</returns>
    public double CalculateSectorArea(double angleInDegrees)
    {
        if (angleInDegrees < 0 || angleInDegrees > 360)
        {
            throw new ArgumentOutOfRangeException(nameof(angleInDegrees), 
                "Angle must be between 0 and 360 degrees.");
        }
        
        return (angleInDegrees / 360.0) * CalculateArea();
    }
    
    /// <summary>
    /// Calculates the length of an arc.
    /// </summary>
    /// <param name="angleInDegrees">The angle of the arc in degrees</param>
    /// <returns>The length of the arc</returns>
    public double CalculateArcLength(double angleInDegrees)
    {
        if (angleInDegrees < 0 || angleInDegrees > 360)
        {
            throw new ArgumentOutOfRangeException(nameof(angleInDegrees), 
                "Angle must be between 0 and 360 degrees.");
        }
        
        return (angleInDegrees / 360.0) * CalculatePerimeter();
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the circle.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"Circle (r={_radius:F2}, {Color}) - Area: {CalculateArea():F2}";
    }
    
    #endregion
}