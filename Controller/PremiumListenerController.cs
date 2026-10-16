/// <summary>
/// Handles the menu loop and actions for premium listeners.
/// </summary>
class PremiumListenerController : BaseController
{
    private const int ViewAllPodcasts = 3;
    private const int PremiumListen = 4;
    private const int PremiumListenSubscribed = 5;
    private const int PremiumSubscribe = 6;
    private const int PremiumUnsubscribe = 7;
    private const int PremiumFeedback = 8;
    private const int PremiumLogOut = 9;

    /// <summary>
    /// Initialises the premium listener controller with the required service dependencies.
    /// </summary>
    public PremiumListenerController(IAppMenu appMenu, IPodcastService podcastService, IListeningService listeningService)
        : base(appMenu, podcastService, listeningService) { }

    /// <summary>
    /// Runs the premium listener menu loop until the user logs out.
    /// </summary>
    public void Run(PremiumListener premiumUser)
    {
        appMenu.DisplayWelcomeBack(premiumUser.Name);
        int choice;
        do
        {
            choice = appMenu.DisplayPremiumListenerMenuOptions();
            if (choice == SeeDetails)
                HandleSeeDetails(premiumUser);
            else if (choice == ViewAllPodcasts)
                HandleViewAllPodcasts(premiumUser);
            else if (choice == PremiumListen)
                HandleListen(premiumUser);
            else if (choice == PremiumListenSubscribed)
                HandleListenSubscribed(premiumUser);
            else if (choice == PremiumSubscribe)
                HandleSubscribe(premiumUser);
            else if (choice == PremiumUnsubscribe)
                HandleUnsubscribe(premiumUser);
            else if (choice == PremiumFeedback)
                HandleFeedback(premiumUser);
            else if (choice != PremiumLogOut && choice != ChangePassword)
                appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
        } while (choice != PremiumLogOut);
    }

    /// <summary>
    /// Displays the premium listener's personal details and listening statistics.
    /// </summary>
    private void HandleSeeDetails(PremiumListener premiumUser)
    {
        appMenu.DisplayPremiumUserDetails(premiumUser,
            listeningService.GetEpisodesStarted(premiumUser),
            listeningService.GetEpisodesCompleted(premiumUser),
            listeningService.GetTotalMinutesListened(premiumUser));
    }

    /// <summary>
    /// Displays all podcasts and allows the premium listener to view episode details.
    /// </summary>
    private void HandleViewAllPodcasts(PremiumListener premiumUser)
    {
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
        }
        List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
        appMenu.DisplayPodcastList($"Displaying all podcasts for {premiumUser.Name}.", podcasts);
        int podcastChoice = appMenu.GetPodcastMenuChoice();
        if (podcastChoice > 0 && podcastChoice <= podcasts.Count)
        {
            Podcast selectedPodcast = podcasts[podcastChoice - 1];
            appMenu.DisplayPodcastDetails(selectedPodcast);
            if (selectedPodcast.Episodes.Count == 0)
                appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
            else
                appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);
        }
    }

    /// <summary>
    /// Lets the premium listener select any podcast and episode to listen to.
    /// </summary>
    private void HandleListen(PremiumListener premiumUser)
    {
        List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
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

        if (podcastChoice == 0) return;

        HandleListenFlow(premiumUser, podcasts[podcastChoice - 1], false);
    }

    /// <summary>
    /// Lets the premium listener listen to an episode from their subscribed podcasts.
    /// </summary>
    private void HandleListenSubscribed(PremiumListener premiumUser)
    {
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
        }
        List<Podcast> subscribedPodcasts = premiumUser.SubscribedPodcasts.OrderBy(p => p.Title).ToList();
        if (subscribedPodcasts.Count == 0)
        {
            appMenu.DisplayErrorMessage(MessageBox.NotSubscribed);
            return;
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

        if (podcastChoice == 0) return;

        HandleListenFlow(premiumUser, subscribedPodcasts[podcastChoice - 1], false);
    }

    /// <summary>
    /// Shows current subscriptions and allows subscribing to an available podcast.
    /// </summary>
    private void HandleSubscribe(PremiumListener premiumUser)
    {
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
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

    /// <summary>
    /// Shows current subscriptions and allows unsubscribing from a podcast.
    /// </summary>
    private void HandleUnsubscribe(PremiumListener premiumUser)
    {
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
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

    /// <summary>
    /// Lets the premium listener select a podcast and episode to provide feedback on.
    /// </summary>
    private void HandleFeedback(PremiumListener premiumUser)
    {
        List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
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
}
