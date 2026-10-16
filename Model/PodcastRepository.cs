/// <summary>
/// Manages podcast storage, ordering, subscriptions, and episode creation.
/// </summary>
public class PodcastRepository : IPodcastService
{
    private readonly List<Podcast> Podcasts = new List<Podcast>();

    /// <summary>
    /// Adds a podcast to the repository.
    /// </summary>
    public void AddPodcast(Podcast podcast)
    {
        Podcasts.Add(podcast);
    }

    /// <summary>
    /// Returns true if at least one podcast exists.
    /// </summary>
    public bool CheckPodcastExistence()
    {
        return Podcasts.Any();
    }

    /// <summary>
    /// Returns all podcasts sorted alphabetically by title.
    /// </summary>
    public List<Podcast> GetPodcastInAlphabecticOrder()
    {
        return Podcasts.OrderBy(p => p.Title).ToList();
    }

    /// <summary>
    /// Returns podcasts the premium listener is not yet subscribed to.
    /// </summary>
    public List<Podcast> GetAvailablePodcasts(PremiumListener user)
    {
        return GetPodcastInAlphabecticOrder().Where(p => !user.SubscribedPodcasts.Contains(p)).ToList();
    }

    /// <summary>
    /// Subscribes a premium listener to a podcast and increments its subscriber count.
    /// </summary>
    public void SubscribeToPodcast(PremiumListener user, Podcast podcast)
    {
        user.Subscribe(podcast);
        podcast.IncrementSubscribers();
    }

    /// <summary>
    /// Unsubscribes a premium listener from a podcast and decrements its subscriber count.
    /// </summary>
    public void UnsubscribeFromPodcast(PremiumListener user, Podcast podcast)
    {
        user.Unsubscribe(podcast);
        podcast.DecrementSubscribers();
    }

    /// <summary>
    /// Creates a new episode and adds it to the specified podcast.
    /// </summary>
    public Episode RegisterEpisode(Podcast podcast, string episodeName, string podcastDescription, DateTime? episodeRecorded, int episodeLength, bool premiumEpisode)
    {
        Episode newEpisode = new Episode(episodeName, podcastDescription, episodeRecorded, episodeLength, premiumEpisode);
        podcast.AddEpisode(newEpisode);
        return newEpisode;
    }
}
