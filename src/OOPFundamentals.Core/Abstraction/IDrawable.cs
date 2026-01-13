namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Interface representing objects that can be drawn.
/// Demonstrates abstraction through interface-based design.
/// </summary>
/// <remarks>
/// Interfaces provide a contract that implementing classes must follow.
/// This is pure abstraction - no implementation details are provided.
/// 
/// Key differences between interfaces and abstract classes:
/// - Interfaces: Define WHAT (contract), not HOW (implementation)
/// - Abstract classes: Can provide both contract and partial implementation
/// - Classes can implement multiple interfaces but inherit from only one base class
/// </remarks>
public interface IDrawable
{
    /// <summary>
    /// Draws the object to the console.
    /// </summary>
    void Draw();
    
    /// <summary>
    /// Draws the object at a specific position.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    void DrawAt(int x, int y);
    
    /// <summary>
    /// Gets the color of the drawable object.
    /// </summary>
    string Color { get; }
    
    /// <summary>
    /// Clears the drawn object from the display.
    /// </summary>
    void Clear();
}