/// <summary>
/// Manages podcast storage, ordering, subscriptions, and episode creation.
/// </summary>
public class PodcastRepository : IPodcastService
{
    private readonly List<Podcast> Podcasts = new List<Podcast>();

    public void AddPodcast(Podcast podcast)
    {
        Podcasts.Add(podcast);
    }

    public bool CheckPodcastExistence()
    {
        return Podcasts.Any();
    }

    public List<Podcast> GetPodcastInAlphabecticOrder()
    {
        return Podcasts.OrderBy(p => p.Title).ToList();
    }

    public List<Podcast> GetAvailablePodcasts(PremiumListener user)
    {
        return GetPodcastInAlphabecticOrder().Where(p => !user.SubscribedPodcasts.Contains(p)).ToList();
    }

    public void SubscribeToPodcast(PremiumListener user, Podcast podcast)
    {
        user.Subscribe(podcast);
        podcast.Subscribers++;
    }

    public void UnsubscribeFromPodcast(PremiumListener user, Podcast podcast)
    {
        user.Unsubscribe(podcast);
        podcast.Subscribers--;
    }

    public Episode RegisterEpisode(Podcast podcast, string episodeName, string podcastDescription, DateTime? episodeRecorded, int episodeLength, bool premiumEpisode)
    {
        Episode newEpisode = new Episode(episodeName, podcastDescription, episodeRecorded, episodeLength, premiumEpisode);
        podcast.AddEpisode(newEpisode);
        return newEpisode;
    }
}
