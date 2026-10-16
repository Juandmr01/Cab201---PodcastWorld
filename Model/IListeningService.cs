public interface IListeningService
{
    ListeningRecord GetListeningRecord(User user, Episode episode);
    ListeningRecord ListenToEpisode(User user, Podcast podcast, Episode episode, int minutesListened, DateTime listenDate);
    void AddFeedback(User user, Episode episode, bool thumbsUp, string textFeedback);
    List<Feedback> GetFeedbackForEpisode(Episode episode);
    int GetThumbsUpCount(Episode episode);
    int GetListenersStarted(Episode episode);
    int GetListenersCompleted(Episode episode);
    int GetEpisodesStarted(User user);
    int GetEpisodesCompleted(User user);
    int GetTotalMinutesListened(User user);
    int GetAdvertisementsListened(User user);
}
