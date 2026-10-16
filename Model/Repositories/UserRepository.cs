/// <summary>
/// Manages user registration, authentication, and lookup.
/// </summary>
public class UserRepository : IUserService
{
    private readonly List<User> Users = new List<User>();
    private readonly IPodcastService podcastService;

    /// <summary>
    /// Creates a new user repository with the specified podcast service dependency.
    /// </summary>
    public UserRepository(IPodcastService podcastService)
    {
        this.podcastService = podcastService;
    }

    /// <summary>
    /// Registers a new regular listener and returns the created user.
    /// </summary>
    public User RegisterUser(string name, int age, string email, string mobileNumber, string password, int userType)
    {
        User newUser = new Listener(name, age, email, mobileNumber, password);
        Users.Add(newUser);
        return newUser;
    }

    /// <summary>
    /// Registers a new premium listener and returns the created user.
    /// </summary>
    public User RegisterPremiumUser(string name, int age, string email, string mobileNumber, string password, DateTime registrationDate, int payId)
    {
        PremiumListener newPremiumUser = new PremiumListener(name, age, email, mobileNumber, password, registrationDate, payId);
        Users.Add(newPremiumUser);
        return newPremiumUser;
    }

    /// <summary>
    /// Registers a new podcaster with their podcast and returns the created user.
    /// </summary>
    public User RegisterPodcaster(string name, int age, string email, string mobileNumber, string password, string podcastName, string podcastDescription, Genre podcastGenre)
    {
        Podcaster newPodcaster = new Podcaster(name, age, email, mobileNumber, password);
        Podcast podcast = new Podcast(podcastName, podcastDescription, podcastGenre, newPodcaster);
        newPodcaster.AddPodcast(podcast);
        podcastService.AddPodcast(podcast);
        Users.Add(newPodcaster);
        return newPodcaster;
    }

    /// <summary>
    /// Authenticates a user by email and password, returning the user or null.
    /// </summary>
    public User AuthenticateUser(string email, string password)
    {
        return Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    /// <summary>
    /// Changes a user's password and returns the updated user.
    /// </summary>
    public User ChangePassword(string email, string oldPassword, string newUserPassword)
    {
        User user = Users.FirstOrDefault(u => u.Email == email && u.Password == oldPassword);
        if (user != null)
        {
            user.Password = newUserPassword;
        }
        return user;
    }

    /// <summary>
    /// Returns true if at least one user is registered.
    /// </summary>
    public bool CheckUserExistence()
    {
        return Users.Any();
    }

    /// <summary>
    /// Returns true if a user with the given email is registered.
    /// </summary>
    public bool CheckEmailExistence(string email)
    {
        return Users.Any(u => u.Email == email);
    }

    /// <summary>
    /// Returns the user with the given email, or null if not found.
    /// </summary>
    public User GetUserByEmail(string email)
    {
        return Users.FirstOrDefault(u => u.Email == email);
    }
}
