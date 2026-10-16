/// <summary>
/// Abstract base class for all users in the PodCastWorld system.
/// Provides common properties and validation methods shared by
/// Listener, PremiumListener, and Podcaster.
/// </summary>
public abstract class User
{
    /// <summary>The user's full name.</summary>
    public string Name { get; set; }

    /// <summary>The user's age (must be between 18 and 99).</summary>
    public int Age { get; set; }

    /// <summary>The user's email address.</summary>
    public string Email { get; set; }

    /// <summary>The user's mobile phone number.</summary>
    public string MobileNumber { get; set; }

    /// <summary>The user's password.</summary>
    public string Password { get; set; }


    /// <summary>
    /// Initialises a new User with the required personal details.
    /// </summary>
    public User(string name, int age, string email, string mobileNumber, string password)
    {
        Name = name;
        Age = age;
        Email = email;
        MobileNumber = mobileNumber;
        Password = password;
    }

}