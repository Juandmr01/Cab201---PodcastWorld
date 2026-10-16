/// <summary>
/// Coordinates all repositories, providing access to user, podcast,
/// and listening services through their respective interfaces.
/// </summary>
public class MasterRepository
{
    public IUserService UserService { get; }
    public IPodcastService PodcastService { get; }
    public IListeningService ListeningService { get; }

    public MasterRepository()
    {
        PodcastRepository podcastRepository = new PodcastRepository();
        PodcastService = podcastRepository;
        UserService = new UserRepository(podcastRepository);
        ListeningService = new ListeningRepository();
    }
}
