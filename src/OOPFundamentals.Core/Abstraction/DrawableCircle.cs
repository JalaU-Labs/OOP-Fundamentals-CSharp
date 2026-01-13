namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Represents a drawable and resizable circle.
/// Demonstrates implementing multiple interfaces along with inheriting from an abstract class.
/// </summary>
/// <remarks>
/// This class shows:
/// - Multiple interface implementation (IDrawable, IResizable)
/// - Inheritance from abstract class (Shape via Circle)
/// - Combining abstract class functionality with interface contracts
/// 
/// C# allows:
/// - Single inheritance (one base class)
/// - Multiple interface implementation (many interfaces)
/// </remarks>
public class DrawableCircle : Circle, IDrawable, IResizable
{
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the DrawableCircle class.
    /// </summary>
    /// <param name="radius">The radius of the circle</param>
    /// <param name="color">The color of the circle</param>
    public DrawableCircle(double radius, string color = "Black") 
        : base(radius, color)
    {
        Name = "Drawable Circle";
    }
    
    #endregion
    
    #region IDrawable Implementation
    
    /// <summary>
    /// Draws the circle to the console.
    /// Implements IDrawable.Draw()
    /// </summary>
    /// <remarks>
    /// This method fulfills the contract defined by IDrawable interface.
    /// Note: Shape class already has a Draw() method, but we're providing
    /// a more specific implementation for DrawableCircle.
    /// </remarks>
    public new void Draw()
    {
        Console.WriteLine($"\n[Drawing {Name}]");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Radius: {Radius:F2}");
        Console.WriteLine("\nVisual Representation:");
        
        // More detailed ASCII art for drawable version
        int size = Math.Min((int)(Radius * 2), 20);
        
        for (int y = 0; y <= size; y++)
        {
            for (int x = 0; x <= size * 2; x++)
            {
                double distance = Math.Sqrt(
                    Math.Pow(x - size, 2) + 
                    Math.Pow((y - size / 2.0) * 2, 2)
                );
                
                if (Math.Abs(distance - size) < 1.5)
                {
                    Console.Write("●");
                }
                else if (distance < size)
                {
                    Console.Write("·");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    
    /// <summary>
    /// Draws the circle at a specific console position.
    /// Implements IDrawable.DrawAt()
    /// </summary>
    /// <param name="x">X coordinate (column)</param>
    /// <param name="y">Y coordinate (row)</param>
    public void DrawAt(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine($"● {Name} (r={Radius:F1})");
    }
    
    /// <summary>
    /// Clears the drawn circle from the display.
    /// Implements IDrawable.Clear()
    /// </summary>
    public void Clear()
    {
        Console.Clear();
        Console.WriteLine($"{Name} cleared from display.");
    }
    
    #endregion
    
    #region IResizable Implementation
    
    /// <summary>
    /// Resizes the circle to fit within specified dimensions.
    /// Implements IResizable.ResizeToFit()
    /// </summary>
    /// <param name="maxWidth">Maximum width constraint</param>
    /// <param name="maxHeight">Maximum height constraint</param>
    public void ResizeToFit(double maxWidth, double maxHeight)
    {
        double maxDimension = Math.Min(maxWidth, maxHeight);
        double targetRadius = maxDimension / 2;
        
        if (Radius > targetRadius)
        {
            double scaleFactor = targetRadius / Radius;
            Resize(scaleFactor);
            Console.WriteLine($"Circle resized to fit within {maxWidth:F2}x{maxHeight:F2} bounds.");
        }
        else
        {
            Console.WriteLine($"Circle already fits within {maxWidth:F2}x{maxHeight:F2} bounds.");
        }
    }
    
    // Note: Resize(double scaleFactor) is inherited from Circle/Shape
    
    #endregion
    
    #region Additional Methods
    
    /// <summary>
    /// Demonstrates the circle with animation effect.
    /// Shows additional functionality beyond the interfaces.
    /// </summary>
    /// <param name="steps">Number of animation steps</param>
    public void AnimateGrowth(int steps = 5)
    {
        double originalRadius = Radius;
        double step = originalRadius / steps;
        
        Console.WriteLine($"\nAnimating {Name} growth:");
        
        for (int i = 1; i <= steps; i++)
        {
            Radius = step * i;
            Console.WriteLine($"Step {i}/{steps}: Radius = {Radius:F2}");
            Thread.Sleep(200); // Small delay for animation effect
        }
        
        Radius = originalRadius;
        Console.WriteLine("Animation complete!");
    }
    
    /// <summary>
    /// Gets a detailed status report of the drawable circle.
    /// </summary>
    /// <returns>A formatted status report</returns>
    public string GetStatus()
    {
        return $"""
                Drawable Circle Status
                =====================
                Radius: {Radius:F2} units
                Diameter: {Diameter:F2} units
                Color: {Color}
                Area: {CalculateArea():F2} sq units
                Perimeter: {CalculatePerimeter():F2} units
                Can be drawn: Yes (IDrawable)
                Can be resized: Yes (IResizable)
                """;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the drawable circle.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"DrawableCircle (r={Radius:F2}, {Color}) [IDrawable, IResizable]";
    }
    
    #endregion
}