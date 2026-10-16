/// <summary>
/// Defines the ability for a listener to subscribe and unsubscribe from podcasts.
/// </summary>
public interface ISubscribable
{
    /// <summary>The podcasts this listener is currently subscribed to.</summary>
    IReadOnlyList<Podcast> SubscribedPodcasts { get; }

    /// <summary>Subscribes this listener to a podcast.</summary>
    void Subscribe(Podcast podcast);

    /// <summary>Unsubscribes this listener from a podcast.</summary>
    void Unsubscribe(Podcast podcast);
}
