namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Represents a triangle shape.
/// Concrete implementation of the abstract Shape class.
/// </summary>
/// <remarks>
/// This class demonstrates:
/// - Implementation of abstract methods with triangle-specific calculations
/// - Heron's formula for area calculation
/// - Triangle type classification (Equilateral, Isosceles, Scalene)
/// - Triangle validity validation
/// </remarks>
public class Triangle : Shape
{
    #region Private Fields
    
    /// <summary>
    /// Private backing field for side A.
    /// </summary>
    private double _sideA;
    
    /// <summary>
    /// Private backing field for side B.
    /// </summary>
    private double _sideB;
    
    /// <summary>
    /// Private backing field for side C.
    /// </summary>
    private double _sideC;
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets or sets the length of side A.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when side length is not positive</exception>
    /// <exception cref="ArgumentException">Thrown when sides don't form a valid triangle</exception>
    public double SideA
    {
        get => _sideA;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Side length must be positive.");
            }
            
            double oldValue = _sideA;
            _sideA = value;
            
            if (!IsValidTriangle())
            {
                _sideA = oldValue;
                throw new ArgumentException(
                    "These side lengths do not form a valid triangle (triangle inequality violated).");
            }
        }
    }
    
    /// <summary>
    /// Gets or sets the length of side B.
    /// </summary>
    public double SideB
    {
        get => _sideB;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Side length must be positive.");
            }
            
            double oldValue = _sideB;
            _sideB = value;
            
            if (!IsValidTriangle())
            {
                _sideB = oldValue;
                throw new ArgumentException(
                    "These side lengths do not form a valid triangle (triangle inequality violated).");
            }
        }
    }
    
    /// <summary>
    /// Gets or sets the length of side C.
    /// </summary>
    public double SideC
    {
        get => _sideC;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Side length must be positive.");
            }
            
            double oldValue = _sideC;
            _sideC = value;
            
            if (!IsValidTriangle())
            {
                _sideC = oldValue;
                throw new ArgumentException(
                    "These side lengths do not form a valid triangle (triangle inequality violated).");
            }
        }
    }
    
    /// <summary>
    /// Gets the type of triangle based on side lengths.
    /// </summary>
    public TriangleType Type
    {
        get
        {
            const double tolerance = 0.001;
            
            bool ab = Math.Abs(_sideA - _sideB) < tolerance;
            bool bc = Math.Abs(_sideB - _sideC) < tolerance;
            bool ac = Math.Abs(_sideA - _sideC) < tolerance;
            
            if (ab && bc && ac)
                return TriangleType.Equilateral;
            
            if (ab || bc || ac)
                return TriangleType.Isosceles;
            
            return TriangleType.Scalene;
        }
    }
    
    /// <summary>
    /// Gets whether the triangle is a right triangle.
    /// Uses the Pythagorean theorem to check.
    /// </summary>
    public bool IsRightTriangle
    {
        get
        {
            const double tolerance = 0.001;
            
            // Sort sides to identify potential hypotenuse
            var sides = new[] { _sideA, _sideB, _sideC }.OrderBy(s => s).ToArray();
            
            // Check if a² + b² = c² (Pythagorean theorem)
            double sumOfSquares = sides[0] * sides[0] + sides[1] * sides[1];
            double hypotenuseSquared = sides[2] * sides[2];
            
            return Math.Abs(sumOfSquares - hypotenuseSquared) < tolerance;
        }
    }
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Triangle class.
    /// </summary>
    /// <param name="sideA">Length of side A</param>
    /// <param name="sideB">Length of side B</param>
    /// <param name="sideC">Length of side C</param>
    /// <param name="color">The color of the triangle</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any side is not positive</exception>
    /// <exception cref="ArgumentException">Thrown when sides don't form a valid triangle</exception>
    public Triangle(double sideA, double sideB, double sideC, string color = "Black") 
        : base("Triangle", color)
    {
        // Validate all sides are positive
        if (sideA <= 0 || sideB <= 0 || sideC <= 0)
        {
            throw new ArgumentOutOfRangeException(
                "All side lengths must be positive.");
        }
        
        _sideA = sideA;
        _sideB = sideB;
        _sideC = sideC;
        
        // Validate triangle inequality
        if (!IsValidTriangle())
        {
            throw new ArgumentException(
                "These side lengths do not form a valid triangle. " +
                "The sum of any two sides must be greater than the third side.");
        }
        
        // Update name based on type
        Name = $"{Type} Triangle";
    }
    
    /// <summary>
    /// Creates an equilateral triangle (all sides equal).
    /// </summary>
    /// <param name="sideLength">Length of each side</param>
    /// <param name="color">The color of the triangle</param>
    /// <returns>A Triangle instance representing an equilateral triangle</returns>
    public static Triangle CreateEquilateral(double sideLength, string color = "Black")
    {
        return new Triangle(sideLength, sideLength, sideLength, color);
    }
    
    /// <summary>
    /// Creates an isosceles triangle (two sides equal).
    /// </summary>
    /// <param name="equalSides">Length of the two equal sides</param>
    /// <param name="base">Length of the base</param>
    /// <param name="color">The color of the triangle</param>
    /// <returns>A Triangle instance representing an isosceles triangle</returns>
    public static Triangle CreateIsosceles(double equalSides, double @base, string color = "Black")
    {
        return new Triangle(equalSides, equalSides, @base, color);
    }
    
    #endregion
    
    #region Abstract Methods Implementation
    
    /// <summary>
    /// Calculates the area of the triangle using Heron's formula.
    /// Formula: √(s(s-a)(s-b)(s-c)) where s is the semi-perimeter
    /// </summary>
    /// <returns>The area of the triangle</returns>
    public override double CalculateArea()
    {
        // Calculate semi-perimeter
        double s = (_sideA + _sideB + _sideC) / 2;
        
        // Apply Heron's formula
        double area = Math.Sqrt(s * (s - _sideA) * (s - _sideB) * (s - _sideC));
        
        return area;
    }
    
    /// <summary>
    /// Calculates the perimeter of the triangle.
    /// Formula: a + b + c
    /// </summary>
    /// <returns>The perimeter of the triangle</returns>
    public override double CalculatePerimeter()
    {
        return _sideA + _sideB + _sideC;
    }
    
    /// <summary>
    /// Gets information specific to the triangle.
    /// </summary>
    /// <returns>A string with triangle-specific properties</returns>
    public override string GetShapeInfo()
    {
        string info = $"Side A: {_sideA:F2} units\n" +
                     $"Side B: {_sideB:F2} units\n" +
                     $"Side C: {_sideC:F2} units\n" +
                     $"Type: {Type}";
        
        if (IsRightTriangle)
        {
            info += "\nSpecial: Right Triangle (90° angle)";
        }
        
        return info;
    }
    
    #endregion
    
    #region Virtual Methods Override
    
    /// <summary>
    /// Draws the triangle with a custom ASCII representation.
    /// </summary>
    public override void Draw()
    {
        base.Draw();
        
        Console.WriteLine("\nASCII Representation:");
        Console.WriteLine("       /\\      ");
        Console.WriteLine("      /  \\     ");
        Console.WriteLine("     /    \\    ");
        Console.WriteLine("    /      \\   ");
        Console.WriteLine("   /        \\  ");
        Console.WriteLine("  /__________\\ \n");
    }
    
    /// <summary>
    /// Resizes the triangle by scaling all sides.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale all sides by</param>
    public override void Resize(double scaleFactor)
    {
        base.Resize(scaleFactor);
        
        double oldA = _sideA;
        double oldB = _sideB;
        double oldC = _sideC;
        
        _sideA *= scaleFactor;
        _sideB *= scaleFactor;
        _sideC *= scaleFactor;
        
        Console.WriteLine($"Sides changed from [{oldA:F2}, {oldB:F2}, {oldC:F2}] " +
                         $"to [{_sideA:F2}, {_sideB:F2}, {_sideC:F2}]");
    }
    
    #endregion
    
    #region Triangle-Specific Methods
    
    /// <summary>
    /// Validates if the three sides can form a valid triangle.
    /// Uses the triangle inequality theorem: the sum of any two sides must be greater than the third.
    /// </summary>
    /// <returns>True if sides form a valid triangle</returns>
    private bool IsValidTriangle()
    {
        return _sideA + _sideB > _sideC &&
               _sideB + _sideC > _sideA &&
               _sideA + _sideC > _sideB;
    }
    
    /// <summary>
    /// Calculates the height of the triangle relative to side A as the base.
    /// </summary>
    /// <returns>The height of the triangle</returns>
    public double CalculateHeight()
    {
        // Area = (base * height) / 2
        // Therefore: height = (2 * Area) / base
        return (2 * CalculateArea()) / _sideA;
    }
    
    /// <summary>
    /// Calculates the angles of the triangle in degrees.
    /// Uses the law of cosines.
    /// </summary>
    /// <returns>A tuple containing the three angles (A, B, C) in degrees</returns>
    public (double angleA, double angleB, double angleC) CalculateAngles()
    {
        // Law of cosines: cos(A) = (b² + c² - a²) / (2bc)
        double angleA = Math.Acos((_sideB * _sideB + _sideC * _sideC - _sideA * _sideA) / 
                                   (2 * _sideB * _sideC));
        
        double angleB = Math.Acos((_sideA * _sideA + _sideC * _sideC - _sideB * _sideB) / 
                                   (2 * _sideA * _sideC));
        
        double angleC = Math.Acos((_sideA * _sideA + _sideB * _sideB - _sideC * _sideC) / 
                                   (2 * _sideA * _sideB));
        
        // Convert from radians to degrees
        return (
            angleA * 180 / Math.PI,
            angleB * 180 / Math.PI,
            angleC * 180 / Math.PI
        );
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the triangle.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{Type} Triangle ([{_sideA:F2}, {_sideB:F2}, {_sideC:F2}], {Color}) - Area: {CalculateArea():F2}";
    }
    
    #endregion
}

/// <summary>
/// Enumeration of triangle types based on side lengths.
/// </summary>
public enum TriangleType
{
    /// <summary>
    /// All three sides are equal.
    /// </summary>
    Equilateral,
    
    /// <summary>
    /// Two sides are equal.
    /// </summary>
    Isosceles,
    
    /// <summary>
    /// All three sides are different.
    /// </summary>
    Scalene
}