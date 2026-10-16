public class Program
{
    static void Main(string[] args)
    {
        PodcastWorld podcastWorld = new PodcastWorld();
        IAppMenu appMenu = new AppMenu();
        IUserService userService = podcastWorld;
        IPodcastService podcastService = podcastWorld;
        IListeningService listeningService = podcastWorld;

        AppController app = new AppController(appMenu, userService, podcastService, listeningService);
        app.Run();
    }
}
