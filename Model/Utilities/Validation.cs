/// <summary>
/// Single source of truth for all input validation rules used
/// by both the controller (pre-validation) and model (setter guards).
/// </summary>
public static class Validation
{
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

    /// <summary>
    /// Validates that the registration date is a valid date.
    /// </summary>
    public static bool IsValidRegistrationDate(DateTime registrationDate)
    {
        return true;
    }

    /// <summary>
    /// Validates that the pay ID is a 6-digit number.
    /// </summary>
    public static bool IsValidPayId(int payId)
    {
        return payId >= 100000 && payId <= 999999;
    }
}
