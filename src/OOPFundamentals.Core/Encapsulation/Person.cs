namespace OOPFundamentals.Core.Encapsulation;

/// <summary>
/// Represents a person demonstrating property-based encapsulation in C#.
/// Shows different types of properties and access modifiers.
/// </summary>
/// <remarks>
/// This class demonstrates:
/// - Auto-implemented properties
/// - Properties with backing fields
/// - Calculated/computed properties
/// - Private setters for controlled modification
/// - Property validation
/// </remarks>
public class Person
{
    #region Private Fields
    
    /// <summary>
    /// Private backing field for age with validation
    /// </summary>
    private int _age;
    
    /// <summary>
    /// Private backing field for email with validation
    /// </summary>
    private string _email;
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets or sets the first name.
    /// Auto-implemented property with private setter.
    /// </summary>
    public string FirstName { get; private set; }
    
    /// <summary>
    /// Gets or sets the last name.
    /// Auto-implemented property with private setter.
    /// </summary>
    public string LastName { get; private set; }
    
    /// <summary>
    /// Gets the full name (computed property).
    /// Demonstrates a read-only calculated property.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
    
    /// <summary>
    /// Gets or sets the age with validation.
    /// Demonstrates property with backing field and validation logic.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when age is invalid</exception>
    public int Age
    {
        get => _age;
        set
        {
            if (value < 0 || value > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Age must be between 0 and 150.");
            }
            _age = value;
        }
    }
    
    /// <summary>
    /// Gets or sets the email address with validation.
    /// Demonstrates property with backing field and format validation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when email format is invalid</exception>
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(value));
            }
            
            if (!value.Contains('@') || !value.Contains('.'))
            {
                throw new ArgumentException("Invalid email format.", nameof(value));
            }
            
            _email = value.ToLower();
        }
    }
    
    /// <summary>
    /// Gets the birth year (calculated property based on age).
    /// Demonstrates a computed property using other properties.
    /// </summary>
    public int BirthYear => DateTime.Now.Year - _age;
    
    /// <summary>
    /// Gets whether the person is an adult (18 or older).
    /// Demonstrates a boolean calculated property.
    /// </summary>
    public bool IsAdult => _age >= 18;
    
    /// <summary>
    /// Gets the person's ID.
    /// Init-only property - can only be set during object initialization.
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// Gets the date when the person record was created.
    /// Read-only property set in constructor.
    /// </summary>
    public DateTime CreatedAt { get; }
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the Person class.
    /// </summary>
    /// <param name="firstName">The person's first name</param>
    /// <param name="lastName">The person's last name</param>
    /// <param name="age">The person's age</param>
    /// <param name="email">The person's email address</param>
    /// <exception cref="ArgumentException">Thrown when names are invalid</exception>
    public Person(string firstName, string lastName, int age, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        }
        
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        }
        
        FirstName = firstName;
        LastName = lastName;
        Age = age; // Uses property setter with validation
        Email = email; // Uses property setter with validation
        CreatedAt = DateTime.Now;
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// Updates the person's name.
    /// Demonstrates controlled modification through a method.
    /// </summary>
    /// <param name="firstName">New first name</param>
    /// <param name="lastName">New last name</param>
    /// <exception cref="ArgumentException">Thrown when names are invalid</exception>
    public void UpdateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        }
        
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        }
        
        FirstName = firstName;
        LastName = lastName;
        Console.WriteLine($"Name updated to: {FullName}");
    }
    
    /// <summary>
    /// Celebrates a birthday by incrementing age.
    /// Demonstrates encapsulated state modification.
    /// </summary>
    public void CelebrateBirthday()
    {
        _age++;
        Console.WriteLine($"Happy Birthday {FirstName}! You are now {_age} years old.");
    }
    
    /// <summary>
    /// Gets a formatted introduction string.
    /// </summary>
    /// <returns>A personalized introduction message</returns>
    public string Introduce()
    {
        string ageCategory = _age switch
        {
            < 13 => "a child",
            < 20 => "a teenager",
            < 60 => "an adult",
            _ => "a senior"
        };
        
        return $"Hello! My name is {FullName}. I am {_age} years old and I am {ageCategory}.";
    }
    
    /// <summary>
    /// Gets detailed information about the person.
    /// </summary>
    /// <returns>Formatted person information</returns>
    public string GetDetails()
    {
        return $"""
                Person Details
                ==============
                Name: {FullName}
                Age: {Age} years old
                Birth Year: {BirthYear}
                Email: {Email}
                Status: {(IsAdult ? "Adult" : "Minor")}
                ID: {Id ?? "N/A"}
                Created: {CreatedAt:yyyy-MM-dd HH:mm:ss}
                """;
    }
    
    /// <summary>
    /// Validates if the person can perform an adult activity.
    /// </summary>
    /// <param name="activityName">Name of the activity</param>
    /// <returns>True if person is adult, false otherwise</returns>
    public bool CanPerformAdultActivity(string activityName)
    {
        if (IsAdult)
        {
            Console.WriteLine($"{FirstName} can {activityName}.");
            return true;
        }
        
        Console.WriteLine($"{FirstName} cannot {activityName} - must be 18 or older.");
        return false;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the person.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{FullName} ({Age} years old)";
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current person.
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if objects are equal</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Person other)
        {
            return Id == other.Id;
        }
        return false;
    }
    
    /// <summary>
    /// Gets the hash code for the person.
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }
    
    #endregion
}