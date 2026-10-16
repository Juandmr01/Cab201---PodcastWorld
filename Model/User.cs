/// <summary>
/// Abstract base class for all users in the PodCastWorld system.
/// Properties validate themselves on assignment and throw
/// ArgumentException if given invalid data, so a User can never
/// hold an invalid state.
/// </summary>
public abstract class User
{
    private string _name;
    private int _age;
    private string _email;
    private string _mobileNumber;
    private string _password;

    /// <summary>The user's full name.</summary>
    public string Name
    {
        get => _name;
        private set
        {
            if (!Validation.IsValidName(value))
                throw new ArgumentException(MessageBox.InvalidName);
            _name = value;
        }
    }

    /// <summary>The user's age (must be between 18 and 99).</summary>
    public int Age
    {
        get => _age;
        private set
        {
            if (!Validation.IsValidAge(value))
                throw new ArgumentException(MessageBox.InvalidAge);
            _age = value;
        }
    }

    /// <summary>The user's email address.</summary>
    public string Email
    {
        get => _email;
        private set
        {
            if (!Validation.IsValidEmail(value))
                throw new ArgumentException(MessageBox.InvalidEmail);
            _email = value;
        }
    }

    /// <summary>The user's mobile phone number.</summary>
    public string MobileNumber
    {
        get => _mobileNumber;
        private set
        {
            if (!Validation.IsValidMobileNumber(value))
                throw new ArgumentException(MessageBox.InvalidMobileNumber);
            _mobileNumber = value;
        }
    }

    /// <summary>The user's password. Mutable (can be changed) but always validated.</summary>
    public string Password
    {
        get => _password;
        set
        {
            if (!Validation.IsValidPassword(value))
                throw new ArgumentException(MessageBox.InvalidPassword);
            _password = value;
        }
    }

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
