/// <summary>
/// Defines the contract for user registration, authentication, and lookup operations.
/// </summary>
public interface IUserService
{
    /// <summary>Registers a new regular listener and returns the created user.</summary>
    User RegisterUser(string name, int age, string email, string mobileNumber, string password, int userType);

    /// <summary>Registers a new premium listener and returns the created user.</summary>
    User RegisterPremiumUser(string name, int age, string email, string mobileNumber, string password, DateTime registrationDate, int payId);

    /// <summary>Registers a new podcaster with their podcast and returns the created user.</summary>
    User RegisterPodcaster(string name, int age, string email, string mobileNumber, string password, string podcastName, string podcastDescription, Genre podcastGenre);

    /// <summary>Authenticates a user by email and password, returning the user or null.</summary>
    User AuthenticateUser(string email, string password);

    /// <summary>Changes a user's password and returns the updated user.</summary>
    User ChangePassword(string email, string oldPassword, string newUserPassword);

    /// <summary>Returns true if at least one user is registered in the system.</summary>
    bool CheckUserExistence();

    /// <summary>Returns true if a user with the given email is registered.</summary>
    bool CheckEmailExistence(string email);

    /// <summary>Returns the user with the given email, or null if not found.</summary>
    User GetUserByEmail(string email);
}
