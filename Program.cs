public class Program
{
    static void Main(string[] args)
    {
        MasterRepository repository = new MasterRepository();
        IAppMenu appMenu = new AppMenu();

        AppController app = new AppController(appMenu, repository.UserService, repository.PodcastService, repository.ListeningService);
        app.Run();
    }
}
