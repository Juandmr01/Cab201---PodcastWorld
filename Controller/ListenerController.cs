/// <summary>
/// Handles the menu loop and actions for regular (non-premium) listeners.
/// </summary>
class ListenerController : BaseController
{
    private const int ViewAllPodcasts = 3;
    private const int ListenerListen = 4;
    private const int ListenerFeedback = 5;
    private const int ListenerLogOut = 6;

    /// <summary>
    /// Initialises the listener controller with the required service dependencies.
    /// </summary>
    public ListenerController(IAppMenu appMenu, IPodcastService podcastService, IListeningService listeningService)
        : base(appMenu, podcastService, listeningService) { }

    /// <summary>
    /// Runs the regular listener menu loop until the user logs out.
    /// </summary>
    public void Run(User user)
    {
        appMenu.DisplayWelcomeBack(user.Name);
        int choice;
        do
        {
            choice = appMenu.DisplayRegularListenerMenuOptions();
            if (choice == SeeDetails)
                HandleSeeDetails(user);
            else if (choice == ChangePassword)
                HandleChangePassword(user);
            else if (choice == ViewAllPodcasts)
                HandleViewAllPodcasts(user);
            else if (choice == ListenerListen)
                HandleListen(user);
            else if (choice == ListenerFeedback)
                HandleFeedback(user);
            else if (choice != ListenerLogOut)
                appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
        } while (choice != ListenerLogOut);
    }

    /// <summary>
    /// Displays the listener's personal details and listening statistics.
    /// </summary>
    private void HandleSeeDetails(User user)
    {
        appMenu.DisplayUserDetails(user,
            listeningService.GetEpisodesStarted(user),
            listeningService.GetEpisodesCompleted(user),
            listeningService.GetTotalMinutesListened(user),
            listeningService.GetAdvertisementsListened(user));
    }

    /// <summary>
    /// Handles the password change flow: verifies current password,
    /// then prompts for a new one.
    /// </summary>
    private void HandleChangePassword(User user)
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

    /// <summary>
    /// Displays all podcasts and allows the listener to view episode details.
    /// </summary>
    private void HandleViewAllPodcasts(User user)
    {
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
        }
        List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
        appMenu.DisplayPodcastList($"Displaying all podcasts for {user.Name}.", podcasts);
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
    /// Lets the listener select a podcast and episode to listen to.
    /// </summary>
    private void HandleListen(User user)
    {
        List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
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

        if (podcastChoice == 0) return;

        HandleListenFlow(user, podcasts[podcastChoice - 1], true);
    }

    /// <summary>
    /// Lets the listener select a podcast and episode to provide feedback on.
    /// </summary>
    private void HandleFeedback(User user)
    {
        List<Podcast> podcasts = podcastService.GetPodcastInAlphabecticOrder();
        if (!podcastService.CheckPodcastExistence())
        {
            appMenu.DisplayErrorMessage(MessageBox.NoPodcasts);
            return;
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
}
