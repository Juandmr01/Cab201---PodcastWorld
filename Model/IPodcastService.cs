/// <summary>
/// Defines the contract for podcast management, subscriptions, and episode creation.
/// </summary>
public interface IPodcastService
{
    /// <summary>Adds a podcast to the repository.</summary>
    void AddPodcast(Podcast podcast);

    /// <summary>Returns true if at least one podcast exists in the system.</summary>
    bool CheckPodcastExistence();

    /// <summary>Returns all podcasts sorted alphabetically by title.</summary>
    List<Podcast> GetPodcastInAlphabecticOrder();

    /// <summary>Returns podcasts the premium listener is not yet subscribed to.</summary>
    List<Podcast> GetAvailablePodcasts(PremiumListener user);

    /// <summary>Subscribes a premium listener to a podcast.</summary>
    void SubscribeToPodcast(PremiumListener user, Podcast podcast);

    /// <summary>Unsubscribes a premium listener from a podcast.</summary>
    void UnsubscribeFromPodcast(PremiumListener user, Podcast podcast);

    /// <summary>Creates and adds a new episode to a podcast.</summary>
    Episode RegisterEpisode(Podcast podcast, string episodeName, string podcastDescription, DateTime? episodeRecorded, int episodeLength, bool premiumEpisode);
}
