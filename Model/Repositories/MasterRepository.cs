/// <summary>
/// Coordinates all repositories, providing access to user, podcast,
/// and listening services through their respective interfaces.
/// </summary>
public class MasterRepository
{
    /// <summary>Service for user registration and authentication.</summary>
    public IUserService UserService { get; }

    /// <summary>Service for podcast management and subscriptions.</summary>
    public IPodcastService PodcastService { get; }

    /// <summary>Service for listening records and feedback.</summary>
    public IListeningService ListeningService { get; }

    /// <summary>
    /// Creates all repositories and wires their dependencies.
    /// </summary>
    public MasterRepository()
    {
        PodcastRepository podcastRepository = new PodcastRepository();
        PodcastService = podcastRepository;
        UserService = new UserRepository(podcastRepository);
        ListeningService = new ListeningRepository();
    }
}
