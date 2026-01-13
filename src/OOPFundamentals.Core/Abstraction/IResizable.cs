namespace OOPFundamentals.Core.Abstraction;

/// <summary>
/// Interface representing objects that can be resized.
/// Demonstrates interface-based abstraction for transformation operations.
/// </summary>
/// <remarks>
/// This interface can be implemented by any shape or object that supports resizing.
/// Multiple interfaces can be combined to add different capabilities to a class.
/// </remarks>
public interface IResizable
{
    /// <summary>
    /// Resizes the object by a scale factor.
    /// </summary>
    /// <param name="scaleFactor">The factor to scale by (e.g., 2.0 = double size, 0.5 = half size)</param>
    void Resize(double scaleFactor);
    
    /// <summary>
    /// Resizes the object to fit within specified dimensions.
    /// </summary>
    /// <param name="maxWidth">Maximum width</param>
    /// <param name="maxHeight">Maximum height</param>
    void ResizeToFit(double maxWidth, double maxHeight);
}