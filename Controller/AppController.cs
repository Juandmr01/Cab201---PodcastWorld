/// <summary>
/// Main controller that handles user interaction flow and delegates
/// between the View (AppMenu) and Model (Repositories) layers.
/// </summary>
class AppController
{
    // Main menu choices
    private const int MainLogin = 1;
    private const int MainRegister = 2;
    private const int MainExit = 3;

    // Registration menu choices
    private const int RegisterRegular = 1;
    private const int RegisterPremium = 2;
    private const int RegisterPodcaster = 3;

    // Shared menu choices (all user types)
    private const int SeeDetails = 1;
    private const int ChangePassword = 2;
    private const int ViewAllPodcasts = 3;

    // Regular listener menu choices
    private const int ListenerListen = 4;
    private const int ListenerFeedback = 5;
    private const int ListenerLogOut = 6;

    // Premium listener menu choices
    private const int PremiumListen = 4;
    private const int PremiumListenSubscribed = 5;
    private const int PremiumSubscribe = 6;
    private const int PremiumUnsubscribe = 7;
    private const int PremiumFeedback = 8;
    private const int PremiumLogOut = 9;

    // Podcaster menu choices
    private const int CreateEpisode = 3;
    private const int ViewEpisodes = 4;
    private const int PodcasterStatistics = 5;
    private const int PodcasterViewFeedback = 6;
    private const int PodcasterLogOut = 7;
    

    // Interfaces
    
    private readonly IAppMenu appMenu;
    private readonly IUserService userService;
    private readonly IPodcastService podcastService;
    private readonly IListeningService listeningService;

    // Cons

    public AppController(IAppMenu appMenu, IUserService userService, IPodcastService podcastService, IListeningService listeningService)
    {
        this.appMenu = appMenu;
        this.userService = userService;
        this.podcastService = podcastService;
        this.listeningService = listeningService;
    }




    public void Run()
    {

        appMenu.DisplayHeader();


        bool running = true;
        while (running)
        {

            int userChoice = appMenu.DisplayMenuOptions();
            if (userChoice == MainLogin)
            {
                if (!userService.CheckUserExistence())
                {
                    appMenu.DisplayErrorMessage(MessageBox.NoUsersRegistered);
                    continue;
                }
                string email;
                string password;
                bool loggedIn = false;

                appMenu.DisplayLoginMenu();

                // 1. Keep asking for email until a registered one is entered
                do
                {

                    email = appMenu.GetEmail();

                    if (!userService.CheckEmailExistence(email))
                    {
                        appMenu.DisplayErrorMessage(MessageBox.EmailNotRegistered);
                        // loop continues = asks for email again
                    }
                } while (!userService.CheckEmailExistence(email));

                //  Now that we have a valid email, ask for password
                do
                {
                    password = appMenu.GetNewPassword();   // or GetNewPassword() if you prefer

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

                    // Success
                    if (user is PremiumListener premiumUser)
                    {
                        appMenu.DisplayWelcomeBack(premiumUser.Name);
                        int premiumChoice;
                        do
                        {
                            premiumChoice = appMenu.DisplayPremiumListenerMenuOptions();
                            if (premiumChoice == SeeDetails)
                            {
                                appMenu.DisplayPremiumUserDetails(premiumUser,
                                    listeningService.GetEpisodesStarted(premiumUser),
                                    listeningService.GetEpisodesCompleted(premiumUser),
                                    listeningService.GetTotalMinutesListened(premiumUser));
                            }
                            else if (premiumChoice == ViewAllPodcasts)
                            {
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                    continue;
                                }
                                List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                                appMenu.DisplayPodcastList($"Displaying all podcasts for {premiumUser.Name}.", podcasts);
                                int podcastChoice = appMenu.GetPodcastMenuChoice();
                                if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
                                {
                                    Podcast selectedPodcast = podcasts[podcastChoice - 1];
                                    appMenu.DisplayPodcastDetails(selectedPodcast);
                                    if (selectedPodcast.Episodes.Count == 0)
                                    {
                                        appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
                                    }
                                    else
                                    {
                                        appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);
                                    }
                                }
                            }
                            else if (premiumChoice == PremiumListen)
                            {
                                List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                    continue;
                                }
                                appMenu.DisplayPodcastList($"Displaying podcasts for {premiumUser.Name} to listen to.", podcasts);

                                int podcastChoice;
                                do
                                {
                                    podcastChoice = appMenu.GetPodcastMenuChoice();
                                    if (podcastChoice == 0) break;
                                    if (podcastChoice < 0 || podcastChoice > podcasts.Count)
                                        appMenu.DisplayRetryError(MessageBox.OutOfRange);
                                } while (podcastChoice < 0 || podcastChoice > podcasts.Count);

                                if (podcastChoice == 0) continue;

                                HandleListenFlow(premiumUser, podcasts[podcastChoice - 1], false);
                            }
                            else if (premiumChoice == PremiumSubscribe)
                            {
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                    continue;
                                }
                                List<Podcast> subscribedPodcasts = premiumUser.SubscribedPodcasts.OrderBy(p => p.Title).ToList();
                                if (subscribedPodcasts.Count == 0)
                                {
                                    appMenu.DisplaySubscriptedHeader();
                                    appMenu.DisplayNotSubscribed();
                                }
                                else
                                {
                                    appMenu.DisplayPodcastList("Subscribed podcasts.", subscribedPodcasts);
                                }

                                List<Podcast> availablePodcasts = podcastService.GetAvailablePodcasts(premiumUser);
                                if (availablePodcasts.Count == 0)
                                {
                                    appMenu.DisplayAvailableHeader();
                                    appMenu.DisplaySubscribedToAll();
                                }
                                else
                                {
                                    appMenu.DisplayPodcastList("Available podcasts.", availablePodcasts);
                                    int podcastChoice = appMenu.GetSubscribeMenuChoice();
                                    if (podcastChoice > 0 && podcastChoice <= availablePodcasts.Count)
                                    {
                                        Podcast selectedPodcast = availablePodcasts[podcastChoice - 1];
                                        podcastService.SubscribeToPodcast(premiumUser, selectedPodcast);
                                        appMenu.DisplaySubscribeSuccess(selectedPodcast.Title);
                                    }
                                }
                            }
                            else if (premiumChoice == PremiumUnsubscribe)
                            {
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                    continue;
                                }
                                List<Podcast> subscribedPodcasts = premiumUser.SubscribedPodcasts.OrderBy(p => p.Title).ToList();
                                if (subscribedPodcasts.Count == 0)
                                {
                                    appMenu.DisplaySubscriptedHeader();
                                    appMenu.DisplayNotSubscribed();
                                }
                                else
                                {
                                    appMenu.DisplayPodcastList("Subscribed podcasts.", subscribedPodcasts);
                                    int podcastChoice = appMenu.GetUnsubscribeMenuChoice();
                                    if (podcastChoice > 0 && podcastChoice <= subscribedPodcasts.Count)
                                    {
                                        Podcast selectedPodcast = subscribedPodcasts[podcastChoice - 1];
                                        podcastService.UnsubscribeFromPodcast(premiumUser, selectedPodcast);
                                        appMenu.DisplayUnsubscribeSuccess(selectedPodcast.Title);
                                    }
                                }
                            }
                            else if (premiumChoice == PremiumFeedback)
                            {
                                List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                    continue;
                                }
                                appMenu.DisplayPodcastList($"Displaying potential podcasts for {premiumUser.Name} to provide feedback on.", podcasts);
                                int podcastChoice = appMenu.GetPodcastMenuChoice();
                                if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
                                {
                                    Podcast selectedPodcast = podcasts[podcastChoice - 1];
                                    if (selectedPodcast.Episodes.Count == 0)
                                    {
                                        appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
                                    }
                                    else
                                    {
                                        appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);
                                        int episodeChoice = appMenu.GetFeedbackEpisodeMenuChoice();
                                        if (episodeChoice > 0 && episodeChoice <= selectedPodcast.Episodes.Count)
                                        {
                                            Episode selectedEpisode = selectedPodcast.Episodes[episodeChoice - 1];
                                            bool thumbsUp = appMenu.GetThumbsUp();
                                            string feedbackText = appMenu.GetFeedbackText();
                                            listeningService.AddFeedback(premiumUser, selectedEpisode, thumbsUp, feedbackText);
                                        }
                                    }
                                }
                            }
                            else if (premiumChoice == PremiumListenSubscribed)
                            {
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                    continue;
                                }
                                List<Podcast> subscribedPodcasts = premiumUser.SubscribedPodcasts.OrderBy(p => p.Title).ToList();
                                if (subscribedPodcasts.Count == 0)
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NotSubscribed);
                                    continue;
                                }
                                appMenu.DisplayPodcastList($"Displaying subscribed podcasts for {premiumUser.Name} to listen to.", subscribedPodcasts);

                                int podcastChoice;
                                do
                                {
                                    podcastChoice = appMenu.GetPodcastMenuChoice();
                                    if (podcastChoice == 0) break;
                                    if (podcastChoice < 0 || podcastChoice > subscribedPodcasts.Count)
                                        appMenu.DisplayRetryError(MessageBox.OutOfRange);
                                } while (podcastChoice < 0 || podcastChoice > subscribedPodcasts.Count);

                                if (podcastChoice == 0) continue;

                                HandleListenFlow(premiumUser, subscribedPodcasts[podcastChoice - 1], false);
                            }
                            else if (premiumChoice != PremiumLogOut && premiumChoice != ChangePassword)
                            {
                                appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
                            }
                        } while (premiumChoice != PremiumLogOut);
                        loggedIn = true;
                        continue;
                    }

                    if (user is Podcaster podcasterUser)
                    {
                        appMenu.DisplayWelcomeBack(podcasterUser.Name);
                        int PodcastChoice;
                        do
                        {
                            PodcastChoice = appMenu.DisplayPodcasterMenu();
                            if (PodcastChoice == SeeDetails)
                            {
                                appMenu.DisplayPodcasterDetails(podcasterUser);
                            }
                            else if (PodcastChoice == CreateEpisode)
                            {
                                string EpisodeName = GetValidatedInput(
                                    () => appMenu.GetEpisodeName(),
                                    a => Episode.IsValidEpisodeName(a),
                                    () => appMenu.DisplayRetryError(MessageBox.InvalidEpisodeName)
                                );

                                string EpisodeDescription = GetValidatedInput(
                                    () => appMenu.GetPodcastDescription(),
                                    a => Episode.IsValidDescription(a),
                                    () => appMenu.DisplayRetryError(MessageBox.InvalidPodcastDescription)
                                );

                                DateTime? EpisodeRecorded = GetValidatedInput(
                                    () => appMenu.GetEpisodeRecordedDate(),
                                    a => Episode.IsValidRecordedDate(a),
                                    () => appMenu.DisplayRetryError(MessageBox.InvalidDate)
                                );

                                int EpisodeLength = GetValidatedInput(
                                    () => appMenu.GetEpisodeLength(),
                                    a => Episode.IsValidEpisodeLength(a),
                                    () => appMenu.DisplayRetryError(MessageBox.OutOfRange)
                                );

                                string premiumInput = GetValidatedInput(
                                    () => appMenu.GetPremiumEpisodeInput(),
                                    a => bool.TryParse(a, out _),
                                    () => appMenu.DisplayRetryError(MessageBox.InvalidBoolean)
                                );
                                bool PremiumEpisode = bool.Parse(premiumInput);

                                Podcast podcast = podcasterUser.Podcasts[0];
                                podcastService.RegisterEpisode(podcast, EpisodeName, EpisodeDescription, EpisodeRecorded, EpisodeLength, PremiumEpisode);
                            }
                            else if (PodcastChoice == ViewEpisodes)
                            {
                                IReadOnlyList<Episode> episodes = podcasterUser.Podcasts[0].Episodes;
                                if (episodes.Count == 0)
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoEpisodesPodcaster);
                                }
                                else
                                {
                                    appMenu.DisplayEpisodeList(episodes);
                                }
                            }
                            else if (PodcastChoice == PodcasterStatistics)
                            {
                                IReadOnlyList<Episode> episodes = podcasterUser.Podcasts[0].Episodes;
                                if (episodes.Count == 0)
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoEpisodesPodcaster);
                                }
                                else
                                {
                                    List<int> started = episodes.Select(e => listeningService.GetListenersStarted(e)).ToList();
                                    List<int> completed = episodes.Select(e => listeningService.GetListenersCompleted(e)).ToList();
                                    appMenu.DisplayStatistics(episodes, started, completed);
                                }
                            }
                            else if (PodcastChoice == PodcasterViewFeedback)
                            {
                                IReadOnlyList<Episode> episodes = podcasterUser.Podcasts[0].Episodes;
                                if (episodes.Count == 0)
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoEpisodesPodcaster);
                                }
                                else
                                {
                                    List<int> thumbsUp = episodes.Select(e => listeningService.GetThumbsUpCount(e)).ToList();
                                    appMenu.DisplayFeedbackSummary(episodes, thumbsUp);
                                    int episodeChoice = appMenu.GetFeedbackViewMenuChoice();
                                    if (episodeChoice > 0 && episodeChoice <= episodes.Count)
                                    {
                                        List<Feedback> feedbacks = listeningService.GetFeedbackForEpisode(episodes[episodeChoice - 1]);
                                        List<string> texts = feedbacks.Select(f => f.TextFeedback).ToList();
                                        appMenu.DisplayFeedbackTexts(texts);
                                    }
                                }
                            }
                            else if (PodcastChoice != PodcasterLogOut && PodcastChoice != ChangePassword)
                            {
                                appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
                            }
                        } while (PodcastChoice != PodcasterLogOut);
                        loggedIn = true;
                        continue;
                    }


                    appMenu.DisplayWelcomeBack(user.Name);
                    int ListenerChoice;
                    do
                    {
                        ListenerChoice = appMenu.DisplayRegularListenerMenuOptions();
                        if (ListenerChoice == SeeDetails)
                        {
                            appMenu.DisplayUserDetails(user,
                                listeningService.GetEpisodesStarted(user),
                                listeningService.GetEpisodesCompleted(user),
                                listeningService.GetTotalMinutesListened(user),
                                listeningService.GetAdvertisementsListened(user));
                        }
                        else if (ListenerChoice == ChangePassword)
                        {
                            string currentPassword = appMenu.GetCurrentPassword();

                            do
                            {
                                if (!user.Password.Equals(currentPassword))
                                {
                                    appMenu.DisplayRetryError(MessageBox.PasswordNotMatch);
                                    currentPassword = appMenu.GetCurrentPassword();
                                }
                            } while (!user.Password.Equals(currentPassword));

                            string newPassword = appMenu.GetChangedPassword();
                            do
                            {
                                if (!Validation.IsValidPassword(newPassword))
                                {
                                    appMenu.DisplayRetryError(MessageBox.InvalidPassword);
                                    newPassword = appMenu.GetChangedPassword();
                                }
                                else if (newPassword.Equals(currentPassword))
                                {
                                    appMenu.DisplayRetryError(MessageBox.SamePassword);
                                    newPassword = appMenu.GetChangedPassword();
                                }
                            } while (!Validation.IsValidPassword(newPassword) || newPassword.Equals(currentPassword));

                            user.Password = newPassword;
                            appMenu.DisplayPasswordUpdateSuccess();
                        }
                        else if (ListenerChoice == ViewAllPodcasts)
                        {
                            if (!podcastService.CheckPodcastExistence())
                            {
                                appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                continue;
                            }
                            List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                            appMenu.DisplayPodcastList($"Displaying all podcasts for {user.Name}.", podcasts);
                            int podcastChoice = appMenu.GetPodcastMenuChoice();
                            if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
                            {
                                Podcast selectedPodcast = podcasts[podcastChoice - 1];
                                appMenu.DisplayPodcastDetails(selectedPodcast);
                                if (selectedPodcast.Episodes.Count == 0)
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
                                }
                                else
                                {
                                    appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);
                                }
                            }
                        }
                        else if (ListenerChoice == ListenerListen)
                        {
                            List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                            if (!podcastService.CheckPodcastExistence())
                            {
                                appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                continue;
                            }
                            appMenu.DisplayPodcastList($"Displaying podcasts for {user.Name} to listen to.", podcasts);

                            int podcastChoice;
                            do
                            {
                                podcastChoice = appMenu.GetPodcastMenuChoice();
                                if (podcastChoice == 0) break;
                                if (podcastChoice < 0 || podcastChoice > podcasts.Count)
                                    appMenu.DisplayRetryError(MessageBox.OutOfRange);
                            } while (podcastChoice < 0 || podcastChoice > podcasts.Count);

                            if (podcastChoice == 0) continue;

                            HandleListenFlow(user, podcasts[podcastChoice - 1], true);
                        }
                        else if (ListenerChoice == ListenerFeedback)
                        {
                            List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                            if (!podcastService.CheckPodcastExistence())
                            {
                                appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
                                continue;
                            }
                            appMenu.DisplayPodcastList($"Displaying potential podcasts for {user.Name} to provide feedback on.", podcasts);
                            int podcastChoice = appMenu.GetPodcastMenuChoice();
                            if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
                            {
                                Podcast selectedPodcast = podcasts[podcastChoice - 1];
                                if (selectedPodcast.Episodes.Count == 0)
                                {
                                    appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
                                }
                                else
                                {
                                    appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);
                                    int episodeChoice = appMenu.GetFeedbackEpisodeMenuChoice();
                                    if (episodeChoice > 0 && episodeChoice <= selectedPodcast.Episodes.Count)
                                    {
                                        Episode selectedEpisode = selectedPodcast.Episodes[episodeChoice - 1];
                                        bool thumbsUp = appMenu.GetThumbsUp();
                                        string feedbackText = appMenu.GetFeedbackText();
                                        listeningService.AddFeedback(user, selectedEpisode, thumbsUp, feedbackText);
                                    }
                                }
                            }
                        }
                        else if (ListenerChoice != ListenerLogOut)
                        {
                            appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
                        }
                    } while (ListenerChoice != ListenerLogOut);
                    loggedIn = true;
                    continue;

                } while (!loggedIn);
            }
            else if (userChoice == MainRegister)
            {
                int RegistrationChoice = appMenu.DisplayRegisterOptions();

                if (RegistrationChoice == RegisterRegular)
                {
                    appMenu.DisplayRegularRegisteration();
                    var (name, age, mobileNumber, email, password) = GetValidatedCommonFields();
                    userService.RegisterUser(name, age, email, mobileNumber, password, (int)UserType.Regular);
                    appMenu.DisplayCongratulationsMessage(name);
                }
                else if (RegistrationChoice == RegisterPremium)
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
                else if (RegistrationChoice == RegisterPodcaster)
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

            }
            else if (userChoice == MainExit)
            {
                // exit
                appMenu.DisplayExit();
                running = false;
            }
        }




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
    private T GetValidatedInput<T>(Func<T> getValue, Func<T, bool> isValid, Action displayError)
    {
        T value = getValue();
        while (!isValid(value))
        {
            displayError();
            value = getValue();
        }
        return value;
    }

    private bool HandleListenFlow(User user, Podcast selectedPodcast, bool checkPremium)
    {
        if (selectedPodcast.Episodes.Count == 0)
        {
            appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
            return false;
        }

        appMenu.DisplayPodcastDetails(selectedPodcast);
        appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);

        int episodeChoice;
        do
        {
            episodeChoice = appMenu.GetEpisodeMenuChoice();
            if (episodeChoice == 0) break;
            if (episodeChoice < 0 || episodeChoice > selectedPodcast.Episodes.Count)
                appMenu.DisplayRetryError(MessageBox.OutOfRange);
        } while (episodeChoice < 0 || episodeChoice > selectedPodcast.Episodes.Count);

        if (episodeChoice == 0) return false;

        Episode selectedEpisode = selectedPodcast.Episodes[episodeChoice - 1];

        ListeningRecord record = listeningService.GetListeningRecord(user, selectedEpisode);
        if (record != null && record.Completed)
        {
            appMenu.DisplayErrorMessage(MessageBox.AlreadyFinished);
            return false;
        }

        if (checkPremium && selectedEpisode.PremmiumEpisode && user is not PremiumListener)
        {
            appMenu.DisplayErrorMessage(MessageBox.PremiumOnly);
            return false;
        }

        int alreadyListened = record != null ? record.MinutesListened : 0;
        int remaining = selectedEpisode.EpisodeLength - alreadyListened;

        if (record != null)
        {
            appMenu.DisplayListeningProgress(selectedPodcast.Title, selectedEpisode.Name, record.MinutesListened, record.LastListenDate, remaining);
        }

        DateTime requiredDate = record != null ? record.LastListenDate : selectedEpisode.RecordedDate.Value;
        DateTime? listenDate;
        do
        {
            listenDate = appMenu.GetListenDate();
            if (listenDate == null)
            {
                appMenu.DisplayRetryError(MessageBox.InvalidDate);
            }
            else if (listenDate.Value.Date < requiredDate.Date)
            {
                appMenu.DisplayRetryError($"Supplied date must be after {requiredDate.ToString("d/MM/yyyy")}");
                listenDate = null;
            }
        } while (listenDate == null);

        int minutesListened;
        do
        {
            minutesListened = appMenu.GetListenMinutes();
            if (minutesListened < 1 || minutesListened > remaining)
                appMenu.DisplayRetryError(MessageBox.OutOfRange);
        } while (minutesListened < 1 || minutesListened > remaining);

        ListeningRecord updatedRecord = listeningService.ListenToEpisode(user, selectedPodcast, selectedEpisode, minutesListened, listenDate.Value);

        if (updatedRecord.Completed)
        {
            appMenu.DisplayEpisodeFinished(user.Name, selectedEpisode.Name);
        }
        else
        {
            int newRemaining = selectedEpisode.EpisodeLength - updatedRecord.MinutesListened;
            appMenu.DisplayListeningProgressTotal(selectedPodcast.Title, selectedEpisode.Name, updatedRecord.MinutesListened, listenDate.Value, newRemaining);
        }
        return true;
    }
}

