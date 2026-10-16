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

    /// <summary>Number of unique episodes the user has started listening to.</summary>
    public int EpisodesStarted { get; set; } = 0;

    /// <summary>Number of episodes the user has finished.</summary>
    public int EpisodesCompleted { get; set; } = 0;

    /// <summary>Total minutes the user has spent listening across all episodes.</summary>
    public int TotalMinutesListenedTo { get; set; } = 0;

    /// <summary>Number of advertisements heard, calculated as TotalMinutesListenedTo / 30.</summary>
    public int AdvertisementsListenedTo { get; set; } = 0;

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

    /// <summary>
    /// Validates that the name is at least 3 characters and contains only
    /// letters, spaces, hyphens, apostrophes, or periods.
    /// </summary>
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Any(char.IsDigit))
        {
            return false;
        }
        return name.All(c => char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '.');
    }

    /// <summary>
    /// Validates that the password is at least 8 characters and contains
    /// an uppercase letter, a lowercase letter, and a digit.
    /// </summary>
    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return false;
        }
        return password.Any(char.IsUpper)
            && password.Any(char.IsLower)
            && password.Any(char.IsDigit);
    }

    /// <summary>
    /// Validates an email address using the .NET MailAddress parser.
    /// </summary>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates that the mobile number is exactly 10 digits starting with 0.
    /// </summary>
    public static bool IsValidMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
        {
            return false;
        }
        return mobileNumber.All(char.IsDigit) && mobileNumber.Length == 10 && mobileNumber.StartsWith("0");
    }

    /// <summary>
    /// Validates that the age is between 18 and 99 inclusive.
    /// </summary>
    public static bool IsValidAge(int age)
    {
        return age >= 18 && age <= 99;
    }
}
