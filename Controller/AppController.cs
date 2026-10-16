/// <summary>
/// Main controller that handles the top-level menu, login routing,
/// and registration flows. Delegates user-type menu loops to
/// dedicated controllers.
/// </summary>
class AppController : BaseController
{
    private const int MainLogin = 1;
    private const int MainRegister = 2;
    private const int MainExit = 3;

    private const int RegisterRegular = 1;
    private const int RegisterPremium = 2;
    private const int RegisterPodcaster = 3;

    /// <summary>Service for user registration and authentication.</summary>
    private readonly IUserService userService;

    /// <summary>Controller for regular listener actions.</summary>
    private readonly ListenerController listenerController;

    /// <summary>Controller for premium listener actions.</summary>
    private readonly PremiumListenerController premiumListenerController;

    /// <summary>Controller for podcaster actions.</summary>
    private readonly PodcasterController podcasterController;

    /// <summary>
    /// Initialises the application controller and its sub-controllers.
    /// </summary>
    public AppController(IAppMenu appMenu, IUserService userService, IPodcastService podcastService, IListeningService listeningService)
        : base(appMenu, podcastService, listeningService)
    {
        this.userService = userService;
        this.listenerController = new ListenerController(appMenu, podcastService, listeningService);
        this.premiumListenerController = new PremiumListenerController(appMenu, podcastService, listeningService);
        this.podcasterController = new PodcasterController(appMenu, podcastService, listeningService);
    }

    /// <summary>
    /// Runs the main application loop: displays the top-level menu
    /// and routes to login, registration, or exit.
    /// </summary>
    public void Run()
    {
        appMenu.DisplayHeader();

        bool running = true;
        while (running)
        {
            int userChoice = appMenu.DisplayMenuOptions();
            if (userChoice == MainLogin)
                HandleLogin();
            else if (userChoice == MainRegister)
                HandleRegistration();
            else if (userChoice == MainExit)
            {
                appMenu.DisplayExit();
                running = false;
            }
        }
    }

    /// <summary>
    /// Handles the login flow: validates email, then password,
    /// and dispatches to the appropriate user-type controller.
    /// </summary>
    private void HandleLogin()
    {
        if (!userService.CheckUserExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoUsersRegistered);
            return;
        }
        string email;
        string password;
        bool loggedIn = false;

        appMenu.DisplayLoginMenu();

        do
        {
            email = appMenu.GetEmail();

            if (!userService.CheckEmailExistence(email))
            {
                appMenu.DisplayErrorMessage(MessageBox.EmailNotRegistered);
            }
        } while (!userService.CheckEmailExistence(email));

        do
        {
            password = appMenu.GetNewPassword();

            if (!Validation.IsValidPassword(password))
            {
                appMenu.DisplayRetryError(MessageBox.InvalidPassword);
                continue;
            }
            if (!userService.CheckEmailExistence(email) || !userService.GetUserByEmail(email).Password.Equals(password))
            {
                appMenu.DisplayErrorMessage(MessageBox.PasswordNotMatch);
                continue;
            }

            User user = userService.AuthenticateUser(email, password);

            if (user == null)
            {
                appMenu.DisplayErrorMessage(MessageBox.PasswordNotMatch);
                continue;
            }

            if (user is PremiumListener premiumUser)
            {
                premiumListenerController.Run(premiumUser);
                loggedIn = true;
                continue;
            }

            if (user is Podcaster podcasterUser)
            {
                podcasterController.Run(podcasterUser);
                loggedIn = true;
                continue;
            }

            listenerController.Run(user);
            loggedIn = true;
            continue;

        } while (!loggedIn);
    }

    /// <summary>
    /// Displays the registration menu and routes to the chosen registration type.
    /// </summary>
    private void HandleRegistration()
    {
        int registrationChoice = appMenu.DisplayRegisterOptions();

        if (registrationChoice == RegisterRegular)
            HandleRegularRegistration();
        else if (registrationChoice == RegisterPremium)
            HandlePremiumRegistration();
        else if (registrationChoice == RegisterPodcaster)
            HandlePodcasterRegistration();
    }

    /// <summary>
    /// Collects validated fields and registers a regular listener.
    /// </summary>
    private void HandleRegularRegistration()
    {
        appMenu.DisplayRegularRegisteration();
        var (name, age, mobileNumber, email, password) = GetValidatedCommonFields();
        userService.RegisterUser(name, age, email, mobileNumber, password, (int)UserType.Regular);
        appMenu.DisplayCongratulationsMessage(name);
    }

    /// <summary>
    /// Collects validated fields and registers a premium listener.
    /// </summary>
    private void HandlePremiumRegistration()
    {
        appMenu.DisplayPremiumRegisteration();
        var (name, age, mobileNumber, email, password) = GetValidatedCommonFields();

        DateTime? registrationDate = GetValidatedInput(
            () => appMenu.GetRegistrationDate(),
            a => a != null && Validation.IsValidRegistrationDate(a.Value),
            () => appMenu.DisplayRetryError(MessageBox.InvalidDate)
        );

        int payId = GetValidatedInput(
            () => appMenu.GetPayId(),
            a => Validation.IsValidPayId(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidPayId)
        );

        userService.RegisterPremiumUser(name, age, email, mobileNumber, password, registrationDate.Value, payId);
        appMenu.DisplayCongratulationsMessagePremium(name);
    }

    /// <summary>
    /// Collects validated fields and registers a podcaster with their podcast.
    /// </summary>
    private void HandlePodcasterRegistration()
    {
        appMenu.DisplayPodcasterRegistration();
        var (name, age, mobileNumber, email, password) = GetValidatedCommonFields();

        Genre[] genres = Enum.GetValues<Genre>();
        int genreChoice = appMenu.DisplayPodcastGenre();
        do
        {
            if (!Podcast.IsValidGenreChoice(genreChoice))
            {
                appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
                genreChoice = appMenu.GetGenreChoiceReprompt(genres.Length);
            }
        } while (!Podcast.IsValidGenreChoice(genreChoice));

        Genre genre = genres[genreChoice - 1];

        string podcastName = GetValidatedInput(
            () => appMenu.GetPodcastName(),
            a => Episode.IsValidDescription(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidPodcastName)
        );

        string podcastDescription = appMenu.GetPodcastDescription();

        userService.RegisterPodcaster(name, age, email, mobileNumber, password, podcastName, podcastDescription, genre);
        appMenu.DisplayCongratsPD(name);
    }

    /// <summary>
    /// Collects and validates the common user fields shared by all registration types.
    /// </summary>
    private (string name, int age, string mobileNumber, string email, string password) GetValidatedCommonFields()
    {
        string name = GetValidatedInput(
            () => appMenu.GetName(),
            a => Validation.IsValidName(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidName)
        );

        int age = GetValidatedInput(
            () => appMenu.GetAge(),
            a => Validation.IsValidAge(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidAge));

        string mobileNumber = GetValidatedInput(
            () => appMenu.GetMobileNumber(),
            m => Validation.IsValidMobileNumber(m),
            () => appMenu.DisplayRetryError(MessageBox.InvalidMobileNumber)
        );

        string email = appMenu.GetEmail();
        while (userService.CheckEmailExistence(email) || !Validation.IsValidEmail(email))
        {
            if (userService.CheckEmailExistence(email))
                appMenu.DisplayRetryError(MessageBox.EmailAlreadyRegistered);
            else
                appMenu.DisplayRetryError(MessageBox.InvalidEmail);
            email = appMenu.GetEmail();
        }

        string password = GetValidatedInput(
            () => appMenu.GetPassword(),
            m => Validation.IsValidPassword(m),
            () => appMenu.DisplayRetryError(MessageBox.InvalidPassword)
        );

        return (name, age, mobileNumber, email, password);
    }
}
