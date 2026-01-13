namespace OOPFundamentals.Core.Polymorphism;

/// <summary>
/// Interface for objects that can send notifications.
/// Demonstrates interface-based polymorphism - different payment types
/// can notify users in different ways.
/// </summary>
/// <remarks>
/// Interface polymorphism allows:
/// - Multiple classes to implement the same contract
/// - Different implementations of the same methods
/// - Treating different types uniformly through the interface
/// - Adding notification capability to any class
/// </remarks>
public interface INotifiable
{
    /// <summary>
    /// Sends a notification to the user.
    /// </summary>
    /// <param name="message">The notification message</param>
    void SendNotification(string message);
    
    /// <summary>
    /// Sends a notification with a specific notification type.
    /// Method overloading within an interface.
    /// </summary>
    /// <param name="message">The notification message</param>
    /// <param name="type">The type of notification</param>
    void SendNotification(string message, NotificationType type);
    
    /// <summary>
    /// Gets the preferred notification method for this payment type.
    /// </summary>
    string NotificationMethod { get; }
}

/// <summary>
/// Enumeration for notification types.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Information message
    /// </summary>
    Info,
    
    /// <summary>
    /// Success message
    /// </summary>
    Success,
    
    /// <summary>
    /// Warning message
    /// </summary>
    Warning,
    
    /// <summary>
    /// Error message
    /// </summary>
    Error,
    
    /// <summary>
    /// Payment reminder
    /// </summary>
    Reminder
}