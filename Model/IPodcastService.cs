public interface IPodcastService
{
    void AddPodcast(Podcast podcast);
    bool CheckPodcastExistence();
    List<Podcast> GetPodcastInAlphabecticOrder();
    List<Podcast> GetAvailablePodcasts(PremiumListener user);
    void SubscribeToPodcast(PremiumListener user, Podcast podcast);
    void UnsubscribeFromPodcast(PremiumListener user, Podcast podcast);
    Episode RegisterEpisode(Podcast podcast, string episodeName, string podcastDescription, DateTime? episodeRecorded, int episodeLength, bool premiumEpisode);
}
