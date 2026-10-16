public static class Validation
{
    /// Comments from the teacher, create a class that hanldes all the validation from the porject tehn can use this class in controller
    /// Create a master repository that handles other repository by user, tehy cointains admin actions list the list of user or podcast or epsiodes(be aware of the single responsability)
    /// Create a class that handles the dispaly input from each user 
    /// create pcrivate set validation for the proeprties of the classes and then i woiuld not need to validaed it my controller becuase the class property is alredy validating this use fiedl isnetdad of instance varialbles  and argumentException for the sets and i can replcae for display. 

/* from a constant classs
public void DisplayInvalidInfo(string info)
   {
      CmdLineUI.DisplayErrorAgain(info)
   }
*//// <summary>
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


