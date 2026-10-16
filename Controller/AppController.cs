/// <summary>
/// Main controller that handles user interaction flow and delegates
/// between the View (AppMenu) and Model (PodcastWorld) layers.
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
                    appMenu.DisplayErrorNoUser();
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
                        appMenu.DisplayErrorEmailNotRegister();
                        // loop continues = asks for email again
                    }
                } while (!userService.CheckEmailExistence(email));

                //  Now that we have a valid email, ask for password
                do
                {
                    password = appMenu.GetNewPassword();   // or GetNewPassword() if you prefer

                    if (!User.IsValidPassword(password))
                    {
                        appMenu.DisplayInvalidPassword();
                        continue;
                    }
                    if (!userService.CheckEmailExistence(email) || !userService.GetUserByEmail(email).Password.Equals(password))
                    {
                        appMenu.DisplayPasswordNotMatch();
                        continue;
                    }

                    User user = userService.AuthenticateUser(email, password);

                    if (user == null)
                    {
                        appMenu.DisplayPasswordNotMatch();
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
                                appMenu.DisplayPremiumUserDetails(premiumUser);
                            }
                            else if (premiumChoice == ViewAllPodcasts)
                            {
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayNoPodcasts();
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
                                        appMenu.DisplayNoEpisodesListener();
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
                                    appMenu.DisplayNoPodcasts();
                                    continue;
                                }
                                appMenu.DisplayPodcastList($"Displaying podcasts for {premiumUser.Name} to listen to.", podcasts);

                                int podcastChoice;
                                do
                                {
                                    podcastChoice = appMenu.GetPodcastMenuChoice();
                                    if (podcastChoice == 0) break;
                                    if (podcastChoice < 0 || podcastChoice > podcasts.Count)
                                        appMenu.DisplayOutOfRangeError();
                                } while (podcastChoice < 0 || podcastChoice > podcasts.Count);

                                if (podcastChoice == 0) continue;

                                HandleListenFlow(premiumUser, podcasts[podcastChoice - 1], false);
                            }
                            else if (premiumChoice == PremiumSubscribe)
                            {
                                if (!podcastService.CheckPodcastExistence())
                                {
                                    appMenu.DisplayNoPodcasts();
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
                                    appMenu.DisplayNoPodcasts();
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
                                    appMenu.DisplayNoPodcasts();
                                    continue;
                                }
                                appMenu.DisplayPodcastList($"Displaying potential podcasts for {premiumUser.Name} to provide feedback on.", podcasts);
                                int podcastChoice = appMenu.GetPodcastMenuChoice();
                                if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
                                {
                                    Podcast selectedPodcast = podcasts[podcastChoice - 1];
                                    if (selectedPodcast.Episodes.Count == 0)
                                    {
                                        appMenu.DisplayNoEpisodesListener();
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
                                    appMenu.DisplayNoPodcasts();
                                    continue;
                                }
                                List<Podcast> subscribedPodcasts = premiumUser.SubscribedPodcasts.OrderBy(p => p.Title).ToList();
                                if (subscribedPodcasts.Count == 0)
                                {
                                    appMenu.DisplayNotSubscribedError();
                                    continue;
                                }
                                appMenu.DisplayPodcastList($"Displaying subscribed podcasts for {premiumUser.Name} to listen to.", subscribedPodcasts);

                                int podcastChoice;
                                do
                                {
                                    podcastChoice = appMenu.GetPodcastMenuChoice();
                                    if (podcastChoice == 0) break;
                                    if (podcastChoice < 0 || podcastChoice > subscribedPodcasts.Count)
                                        appMenu.DisplayOutOfRangeError();
                                } while (podcastChoice < 0 || podcastChoice > subscribedPodcasts.Count);

                                if (podcastChoice == 0) continue;

                                HandleListenFlow(premiumUser, subscribedPodcasts[podcastChoice - 1], false);
                            }
                            else if (premiumChoice != PremiumLogOut && premiumChoice != ChangePassword)
                            {
                                appMenu.DisplayInvalidMenuChoice();
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
                                    () => appMenu.DisplayInvalidEpisodeName()
                                );

                                string EpisodeDescription = GetValidatedInput(
                                    () => appMenu.GetPodcastDescription(),
                                    a => Episode.IsValidDescription(a),
                                    () => appMenu.DisplayInvalidPodcastDescription()
                                );

                                DateTime? EpisodeRecorded = GetValidatedInput(
                                    () => appMenu.GetEpisodeRecordedDate(),
                                    a => Episode.IsValidRecordedDate(a),
                                    () => appMenu.DisplayInvalidEpisodeDate()
                                );

                                int EpisodeLength = GetValidatedInput(
                                    () => appMenu.GetEpisodeLength(),
                                    a => Episode.IsValidEpisodeLength(a),
                                    () => appMenu.DisplayInvalidEpisodeLength()
                                );

                                string premiumInput = GetValidatedInput(
                                    () => appMenu.GetPremiumEpisodeInput(),
                                    a => bool.TryParse(a, out _),
                                    () => appMenu.DisplayInvalidBoolean()
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
                                    appMenu.DisplayNoEpisodesPodcaster();
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
                                    appMenu.DisplayNoEpisodesPodcaster();
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
                                    appMenu.DisplayNoEpisodesPodcaster();
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
                                appMenu.DisplayInvalidMenuChoice();
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
                            appMenu.DisplayUserDetails(user);
                        }
                        else if (ListenerChoice == ChangePassword)
                        {
                            string currentPassword = appMenu.GetCurrentPassword();

                            do
                            {
                                if (!user.Password.Equals(currentPassword))
                                {
                                    appMenu.DisplayPasswordNotMatchAgain();
                                    currentPassword = appMenu.GetCurrentPassword();
                                }
                            } while (!user.Password.Equals(currentPassword));

                            string newPassword = appMenu.GetChangedPassword();
                            do
                            {
                                if (!User.IsValidPassword(newPassword))
                                {
                                    appMenu.DisplayInvalidPassword();
                                    newPassword = appMenu.GetChangedPassword();
                                }
                                else if (newPassword.Equals(currentPassword))
                                {
                                    appMenu.DisplaySamePasswordError();
                                    newPassword = appMenu.GetChangedPassword();
                                }
                            } while (!User.IsValidPassword(newPassword) || newPassword.Equals(currentPassword));

                            user.Password = newPassword;
                            appMenu.DisplayPasswordUpdateSuccess();
                        }
                        else if (ListenerChoice == ViewAllPodcasts)
                        {
                            if (!podcastService.CheckPodcastExistence())
                            {
                                appMenu.DisplayNoPodcasts();
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
                                    appMenu.DisplayNoEpisodesListener();
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
                                appMenu.DisplayNoPodcasts();
                                continue;
                            }
                            appMenu.DisplayPodcastList($"Displaying podcasts for {user.Name} to listen to.", podcasts);

                            int podcastChoice;
                            do
                            {
                                podcastChoice = appMenu.GetPodcastMenuChoice();
                                if (podcastChoice == 0) break;
                                if (podcastChoice < 0 || podcastChoice > podcasts.Count)
                                    appMenu.DisplayOutOfRangeError();
                            } while (podcastChoice < 0 || podcastChoice > podcasts.Count);

                            if (podcastChoice == 0) continue;

                            HandleListenFlow(user, podcasts[podcastChoice - 1], true);
                        }
                        else if (ListenerChoice == ListenerFeedback)
                        {
                            List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
                            if (!podcastService.CheckPodcastExistence())
                            {
                                appMenu.DisplayNoPodcasts();
                                continue;
                            }
                            appMenu.DisplayPodcastList($"Displaying potential podcasts for {user.Name} to provide feedback on.", podcasts);
                            int podcastChoice = appMenu.GetPodcastMenuChoice();
                            if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
                            {
                                Podcast selectedPodcast = podcasts[podcastChoice - 1];
                                if (selectedPodcast.Episodes.Count == 0)
                                {
                                    appMenu.DisplayNoEpisodesListener();
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
                            appMenu.DisplayInvalidMenuChoice();
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
                        a => a != null && PremiumListener.IsValidRegistrationDate(a.Value),
                        () => appMenu.DisplayInvalidEpisodeDate()
                    );

                    int payId = GetValidatedInput(
                        () => appMenu.GetPayId(),
                        a => PremiumListener.IsValidPayId(a),
                        () => appMenu.DisplayInvalidPayId()
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
                            appMenu.DisplayInvalidMenuChoice();
                            genreChoice = appMenu.GetGenreChoiceReprompt(genres.Length);
                        }
                    } while (!Podcast.IsValidGenreChoice(genreChoice));

            

                    Genre genre = genres[genreChoice - 1];

                    string podcastName = GetValidatedInput(
                        () => appMenu.GetPodcastName(),
                        a => Episode.IsValidDescription(a),
                        () => appMenu.DisplayInvalidPodcastName()
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
            a => User.IsValidName(a),
            () => appMenu.DisplayInvalidName()
        );


       int age = GetValidatedInput(
            () => appMenu.GetAge(),
            a => User.IsValidAge(a),
            () => appMenu.DisplayInvalidAge());


        string mobileNumber = GetValidatedInput(
            () => appMenu.GetMobileNumber(),
            m => User.IsValidMobileNumber(m),
            () => appMenu.DisplayInvalidMobileNumber()
        );
        

        string email = appMenu.GetEmail();
        while (userService.CheckEmailExistence(email) || !User.IsValidEmail(email))
        {
            if (userService.CheckEmailExistence(email))
                appMenu.DisplayErrorEmailRegister();
            else
                appMenu.DisplayInvalidEmail();
            email = appMenu.GetEmail();
        }

        string password = GetValidatedInput(
            () => appMenu.GetPassword(),
            m => User.IsValidPassword(m),
            () => appMenu.DisplayInvalidPassword()
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
            appMenu.DisplayNoEpisodesListener();
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
                appMenu.DisplayOutOfRangeError();
        } while (episodeChoice < 0 || episodeChoice > selectedPodcast.Episodes.Count);

        if (episodeChoice == 0) return false;

        Episode selectedEpisode = selectedPodcast.Episodes[episodeChoice - 1];

        ListeningRecord record = listeningService.GetListeningRecord(user, selectedEpisode);
        if (record != null && record.Completed)
        {
            appMenu.DisplayAlreadyFinished();
            return false;
        }

        if (checkPremium && selectedEpisode.PremmiumEpisode && user is not PremiumListener)
        {
            appMenu.DisplayPremiumOnlyError();
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
                appMenu.DisplayInvalidDateError();
            }
            else if (listenDate.Value.Date < requiredDate.Date)
            {
                appMenu.DisplayDateAfterError(requiredDate);
                listenDate = null;
            }
        } while (listenDate == null);

        int minutesListened;
        do
        {
            minutesListened = appMenu.GetListenMinutes();
            if (minutesListened < 1 || minutesListened > remaining)
                appMenu.DisplayOutOfRangeError();
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

