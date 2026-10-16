public interface ISubscribable
{
    IReadOnlyList<Podcast> SubscribedPodcasts { get; }
    void Subscribe(Podcast podcast);
    void Unsubscribe(Podcast podcast);
}
