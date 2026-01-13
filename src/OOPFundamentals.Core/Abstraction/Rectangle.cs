namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Represents a rectangle shape.
/// Concrete implementation of the abstract Shape class.
/// </summary>
/// <remarks>
/// This class demonstrates:
/// - Implementation of abstract methods with rectangle-specific logic
/// - Additional properties for width and height
/// - Methods specific to rectangular shapes
/// </remarks>
public class Rectangle : Shape
{
    #region Private Fields
    
    /// <summary>
    /// Private backing field for width.
    /// </summary>
    private double _width;
    
    /// <summary>
    /// Private backing field for height.
    /// </summary>
    private double _height;
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets or sets the width of the rectangle.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when width is not positive</exception>
    public double Width
    {
        get => _width;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Width must be positive.");
            }
            _width = value;
        }
    }
    
    /// <summary>
    /// Gets or sets the height of the rectangle.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when height is not positive</exception>
    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Height must be positive.");
            }
            _height = value;
        }
    }
    
    /// <summary>
    /// Gets the diagonal length of the rectangle (calculated property).
    /// Uses the Pythagorean theorem: √(width² + height²)
    /// </summary>
    public double Diagonal => Math.Sqrt(_width * _width + _height * _height);
    
    /// <summary>
    /// Gets whether this rectangle is actually a square.
    /// </summary>
    public bool IsSquare => Math.Abs(_width - _height) < 0.001;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Rectangle class.
    /// </summary>
    /// <param name="width">The width of the rectangle</param>
    /// <param name="height">The height of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    public Rectangle(double width, double height, string color = "Black") 
        : base("Rectangle", color)
    {
        Width = width;   // Uses property setter with validation
        Height = height; // Uses property setter with validation
        
        // Update name if it's actually a square
        if (IsSquare)
        {
            Name = "Square";
        }
    }
    
    /// <summary>
    /// Creates a square (rectangle with equal sides).
    /// </summary>
    /// <param name="size">The size of each side</param>
    /// <param name="color">The color of the square</param>
    /// <returns>A Rectangle instance representing a square</returns>
    public static Rectangle CreateSquare(double size, string color = "Black")
    {
        return new Rectangle(size, size, color);
    }
    
    #endregion
    
    #region Abstract Methods Implementation
    
    /// <summary>
    /// Calculates the area of the rectangle using the formula: width * height
    /// </summary>
    /// <returns>The area of the rectangle</returns>
    public override double CalculateArea()
    {
        return _width * _height;
    }
    
    /// <summary>
    /// Calculates the perimeter of the rectangle using the formula: 2 * (width + height)
    /// </summary>
    /// <returns>The perimeter of the rectangle</returns>
    public override double CalculatePerimeter()
    {
        return 2 * (_width + _height);
    }
    
    /// <summary>
    /// Gets information specific to the rectangle.
    /// </summary>
    /// <returns>A string with rectangle-specific properties</returns>
    public override string GetShapeInfo()
    {
        string info = $"Width: {_width:F2} units\nHeight: {_height:F2} units\nDiagonal: {Diagonal:F2} units";
        
        if (IsSquare)
        {
            info += "\nType: Square (all sides equal)";
        }
        
        return info;
    }
    
    #endregion
    
    #region Virtual Methods Override
    
    /// <summary>
    /// Draws the rectangle with a custom ASCII representation.
    /// Overrides the base class Draw method.
    /// </summary>
    public override void Draw()
    {
        base.Draw();
        
        Console.WriteLine("\nASCII Representation:");
        
        // Determine dimensions for ASCII art (scale down if needed)
        int displayWidth = Math.Min((int)(_width * 2), 30);
        int displayHeight = Math.Min((int)_height, 10);
        
        // Draw top border
        Console.WriteLine("┌" + new string('─', displayWidth) + "┐");
        
        // Draw middle rows
        for (int i = 0; i < displayHeight; i++)
        {
            Console.WriteLine("│" + new string(' ', displayWidth) + "│");
        }
        
        // Draw bottom border
        Console.WriteLine("└" + new string('─', displayWidth) + "┘\n");
    }
    
    /// <summary>
    /// Resizes the rectangle by adjusting both width and height.
    /// Overrides the base class Resize method.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale both dimensions by</param>
    public override void Resize(double scaleFactor)
    {
        base.Resize(scaleFactor);
        
        double oldWidth = _width;
        double oldHeight = _height;
        
        _width *= scaleFactor;
        _height *= scaleFactor;
        
        Console.WriteLine($"Dimensions changed from {oldWidth:F2}x{oldHeight:F2} to {_width:F2}x{_height:F2}");
    }
    
    #endregion
    
    #region Rectangle-Specific Methods
    
    /// <summary>
    /// Checks if a point is inside the rectangle.
    /// Assumes rectangle is axis-aligned with bottom-left corner at origin.
    /// </summary>
    /// <param name="x">X coordinate of the point</param>
    /// <param name="y">Y coordinate of the point</param>
    /// <returns>True if point is inside the rectangle</returns>
    public bool ContainsPoint(double x, double y)
    {
        return x >= 0 && x <= _width && y >= 0 && y <= _height;
    }
    
    /// <summary>
    /// Rotates the rectangle 90 degrees (swaps width and height).
    /// </summary>
    public void Rotate90Degrees()
    {
        double temp = _width;
        _width = _height;
        _height = temp;
        
        Console.WriteLine($"Rectangle rotated 90°. New dimensions: {_width:F2}x{_height:F2}");
    }
    
    /// <summary>
    /// Scales the width independently.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale width by</param>
    public void ScaleWidth(double scaleFactor)
    {
        if (scaleFactor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scaleFactor), 
                "Scale factor must be positive.");
        }
        
        double oldWidth = _width;
        _width *= scaleFactor;
        
        Console.WriteLine($"Width scaled from {oldWidth:F2} to {_width:F2}");
    }
    
    /// <summary>
    /// Scales the height independently.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale height by</param>
    public void ScaleHeight(double scaleFactor)
    {
        if (scaleFactor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scaleFactor), 
                "Scale factor must be positive.");
        }
        
        double oldHeight = _height;
        _height *= scaleFactor;
        
        Console.WriteLine($"Height scaled from {oldHeight:F2} to {_height:F2}");
    }
    
    /// <summary>
    /// Calculates the aspect ratio (width / height).
    /// </summary>
    /// <returns>The aspect ratio</returns>
    public double GetAspectRatio()
    {
        return _width / _height;
    }
    
    /// <summary>
    /// Checks if this rectangle can fit inside another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle to check against</param>
    /// <returns>True if this rectangle can fit inside the other</returns>
    public bool CanFitInside(Rectangle other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }
        
        // Check both orientations
        bool normalFit = _width <= other._width && _height <= other._height;
        bool rotatedFit = _height <= other._width && _width <= other._height;
        
        return normalFit || rotatedFit;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the rectangle.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        string type = IsSquare ? "Square" : "Rectangle";
        return $"{type} ({_width:F2}x{_height:F2}, {Color}) - Area: {CalculateArea():F2}";
    }
    
    #endregion
}