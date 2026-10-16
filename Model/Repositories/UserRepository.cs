/// <summary>
/// Manages user registration, authentication, and lookup.
/// </summary>
public class UserRepository : IUserService
{
    private readonly List<User> Users = new List<User>();
    private readonly IPodcastService podcastService;

    public UserRepository(IPodcastService podcastService)
    {
        this.podcastService = podcastService;
    }

    public User RegisterUser(string name, int age, string email, string mobileNumber, string password, int userType)
    {
        User newUser = new Listener(name, age, email, mobileNumber, password);
        Users.Add(newUser);
        return newUser;
    }

    public User RegisterPremiumUser(string name, int age, string email, string mobileNumber, string password, DateTime registrationDate, int payId)
    {
        PremiumListener newPremiumUser = new PremiumListener(name, age, email, mobileNumber, password, registrationDate, payId);
        Users.Add(newPremiumUser);
        return newPremiumUser;
    }

    public User RegisterPodcaster(string name, int age, string email, string mobileNumber, string password, string podcastName, string podcastDescription, Genre podcastGenre)
    {
        Podcaster newPodcaster = new Podcaster(name, age, email, mobileNumber, password);
        Podcast podcast = new Podcast();
        podcast.Title = podcastName;
        podcast.Description = podcastDescription;
        podcast.Genre = podcastGenre;
        podcast.Host = newPodcaster;
        newPodcaster.AddPodcast(podcast);
        podcastService.AddPodcast(podcast);
        Users.Add(newPodcaster);
        return newPodcaster;
    }

    public User AuthenticateUser(string email, string password)
    {
        return Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public User ChangePassword(string email, string oldPassword, string newUserPassword)
    {
        User user = Users.FirstOrDefault(u => u.Email == email && u.Password == oldPassword);
        if (user != null)
        {
            user.Password = newUserPassword;
        }
        return user;
    }

    public bool CheckUserExistence()
    {
        return Users.Any();
    }

    public bool CheckEmailExistence(string email)
    {
        return Users.Any(u => u.Email == email);
    }

    public User GetUserByEmail(string email)
    {
        return Users.FirstOrDefault(u => u.Email == email);
    }
}
