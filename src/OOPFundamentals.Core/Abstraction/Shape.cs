namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Abstract base class representing a geometric shape.
/// Demonstrates abstraction by defining a common interface for all shapes
/// while hiding the implementation details of area and perimeter calculations.
/// </summary>
/// <remarks>
/// Abstraction is demonstrated through:
/// - Abstract methods that must be implemented by derived classes
/// - Virtual methods that can be optionally overridden
/// - Common functionality shared across all shapes
/// - Template method pattern for displaying shape information
/// </remarks>
public abstract class Shape
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the name of the shape.
    /// </summary>
    public string Name { get; protected set; }
    
    /// <summary>
    /// Gets or sets the color of the shape.
    /// </summary>
    public string Color { get; set; }
    
    /// <summary>
    /// Gets the creation date of the shape instance.
    /// </summary>
    public DateTime CreatedAt { get; }
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Shape class.
    /// </summary>
    /// <param name="name">The name of the shape</param>
    /// <param name="color">The color of the shape</param>
    protected Shape(string name, string color = "Black")
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Color = color ?? "Black";
        CreatedAt = DateTime.Now;
    }
    
    #endregion
    
    #region Abstract Methods
    
    /// <summary>
    /// Calculates the area of the shape.
    /// Must be implemented by all derived classes.
    /// </summary>
    /// <returns>The area of the shape</returns>
    /// <remarks>
    /// This is an abstract method - each shape has its own formula for calculating area.
    /// Circle: π * r²
    /// Rectangle: width * height
    /// Triangle: (base * height) / 2
    /// </remarks>
    public abstract double CalculateArea();
    
    /// <summary>
    /// Calculates the perimeter of the shape.
    /// Must be implemented by all derived classes.
    /// </summary>
    /// <returns>The perimeter of the shape</returns>
    /// <remarks>
    /// This is an abstract method - each shape has its own formula for calculating perimeter.
    /// Circle: 2 * π * r
    /// Rectangle: 2 * (width + height)
    /// Triangle: side1 + side2 + side3
    /// </remarks>
    public abstract double CalculatePerimeter();
    
    /// <summary>
    /// Gets a description of the shape's properties.
    /// Must be implemented by all derived classes.
    /// </summary>
    /// <returns>A string describing the shape's specific properties</returns>
    public abstract string GetShapeInfo();
    
    #endregion
    
    #region Virtual Methods
    
    /// <summary>
    /// Draws the shape to the console.
    /// Can be overridden by derived classes for custom drawing behavior.
    /// </summary>
    /// <remarks>
    /// Virtual methods provide a default implementation that can be optionally overridden.
    /// This demonstrates partial abstraction.
    /// </remarks>
    public virtual void Draw()
    {
        Console.WriteLine($"Drawing {Name} in {Color} color");
    }
    
    /// <summary>
    /// Resizes the shape by a scale factor.
    /// Can be overridden by derived classes for custom resize logic.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale the shape by</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when scale factor is not positive</exception>
    public virtual void Resize(double scaleFactor)
    {
        if (scaleFactor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scaleFactor), 
                "Scale factor must be positive.");
        }
        
        Console.WriteLine($"Resizing {Name} by factor of {scaleFactor}");
    }
    
    #endregion
    
    #region Concrete Methods
    
    /// <summary>
    /// Displays complete information about the shape.
    /// This is a template method that uses abstract methods.
    /// </summary>
    /// <remarks>
    /// Template Method Pattern: This method defines the skeleton of an algorithm,
    /// delegating some steps to subclasses through abstract methods.
    /// </remarks>
    public void DisplayInfo()
    {
        Console.WriteLine($"\n{'═',40}");
        Console.WriteLine($"Shape: {Name}");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Created: {CreatedAt:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"{GetShapeInfo()}");
        Console.WriteLine($"Area: {CalculateArea():F2} square units");
        Console.WriteLine($"Perimeter: {CalculatePerimeter():F2} units");
        Console.WriteLine($"{'═',40}\n");
    }
    
    /// <summary>
    /// Compares the area of this shape with another shape.
    /// </summary>
    /// <param name="other">The shape to compare with</param>
    /// <returns>A message indicating which shape is larger</returns>
    public string CompareAreaWith(Shape other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }
        
        double thisArea = CalculateArea();
        double otherArea = other.CalculateArea();
        
        if (Math.Abs(thisArea - otherArea) < 0.001)
        {
            return $"{Name} and {other.Name} have approximately the same area.";
        }
        
        if (thisArea > otherArea)
        {
            return $"{Name} is larger than {other.Name} by {(thisArea - otherArea):F2} square units.";
        }
        
        return $"{Name} is smaller than {other.Name} by {(otherArea - thisArea):F2} square units.";
    }
    
    /// <summary>
    /// Checks if the shape is larger than a given area.
    /// </summary>
    /// <param name="area">The area to compare against</param>
    /// <returns>True if this shape's area is larger</returns>
    public bool IsLargerThan(double area)
    {
        return CalculateArea() > area;
    }

    /// <summary>
    /// Checks if the shape is larger than another shape.
    /// </summary>
    /// <param name="other">The shape to compare against</param>
    /// <returns>True if this shape's area is larger</returns>
    public bool IsLargerThan(Shape other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }
        return CalculateArea() > other.CalculateArea();
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the shape.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{Name} ({Color}) - Area: {CalculateArea():F2}";
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current shape.
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if objects are equal</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Shape other)
        {
            return Name == other.Name && 
                   Math.Abs(CalculateArea() - other.CalculateArea()) < 0.001;
        }
        return false;
    }
    
    /// <summary>
    /// Gets the hash code for the shape.
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, CalculateArea());
    }
    
    #endregion
}