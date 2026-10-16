/// <summary>
/// Entry point for the PodCastWorld application.
/// </summary>
public class Program
{
    /// <summary>
    /// Creates the repository, view, and controller, then starts the application.
    /// </summary>
    static void Main(string[] args)
    {
        MasterRepository repository = new MasterRepository();
        IAppMenu appMenu = new AppMenu();

        AppController app = new AppController(appMenu, repository.UserService, repository.PodcastService, repository.ListeningService);
        app.Run();
    }
}
