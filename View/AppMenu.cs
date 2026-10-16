using System.Diagnostics;

/// <summary>
/// Implements the application's console-based user interface,
/// providing menus, input prompts, and display methods.
/// </summary>
public class AppMenu : IAppMenu
{
   // ============================================================
   // Header
   // ============================================================

   /// <summary>
   /// Displays the application header banner.
   /// </summary>
   public void DisplayHeader()
   {
      CmdLineUI.DisplayString(MessageBox.HeaderBorder);
      CmdLineUI.DisplayString("         Welcome to PodCastWorld!");
      CmdLineUI.DisplayString("      Podcasting all over the world.");
      CmdLineUI.DisplayString(MessageBox.HeaderBorder);
   }

   // ============================================================
   // Menus (display options and return the user's choice)
   // ============================================================

   /// <summary>
   /// Displays the main menu and returns the user's choice.
   /// </summary>
   public int DisplayMenuOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Main Menu.");
      CmdLineUI.DisplayString(MessageBox.MenuPrompt);
      CmdLineUI.DisplayString("1. Log in as a registered user.");
      CmdLineUI.DisplayString("2. Register as a new user.");
      CmdLineUI.DisplayString("3. Exit.");

      int userChoice = CmdLineUI.GetInt(MessageBox.ChoiceBetween1And3);

      return userChoice;
   }

   /// <summary>
   /// Displays the registration type menu and returns the user's choice.
   /// </summary>
   public int DisplayRegisterOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Registration Menu.");
      CmdLineUI.DisplayString("Please enter your user type.");
      CmdLineUI.DisplayString("1. A regular listener.");
      CmdLineUI.DisplayString("2. A premium listener.");
      CmdLineUI.DisplayString("3. A podcaster.");

      int userChoice = CmdLineUI.GetInt(MessageBox.ChoiceBetween1And3);
      return userChoice;
   }

   /// <summary>
   /// Displays the login menu header.
   /// </summary>
   public void DisplayLoginMenu()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Log in Menu.");
   }

   /// <summary>
   /// Displays the regular listener menu and returns the user's choice.
   /// </summary>
   public int DisplayRegularListenerMenuOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Regular Listener Menu.");
      CmdLineUI.DisplayString(MessageBox.MenuPrompt);
      CmdLineUI.DisplayString(MessageBox.MenuSeeDetails);
      CmdLineUI.DisplayString(MessageBox.MenuChangePassword);
      CmdLineUI.DisplayString(MessageBox.MenuViewAllPodcasts);
      CmdLineUI.DisplayString(MessageBox.MenuListen);
      CmdLineUI.DisplayString("5. Provide feedback on a podcast episode.");
      CmdLineUI.DisplayString("6. Log out.");

      int userChoice = CmdLineUI.GetInt("Please enter a choice between 1 and 6:");

      return userChoice;
   }

   /// <summary>
   /// Displays the premium listener menu and returns the user's choice.
   /// </summary>
   public int DisplayPremiumListenerMenuOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Premium Listener Menu.");
      CmdLineUI.DisplayString(MessageBox.MenuPrompt);
      CmdLineUI.DisplayString(MessageBox.MenuSeeDetails);
      CmdLineUI.DisplayString(MessageBox.MenuChangePassword);
      CmdLineUI.DisplayString(MessageBox.MenuViewAllPodcasts);
      CmdLineUI.DisplayString(MessageBox.MenuListen);
      CmdLineUI.DisplayString("5. Listen to a subscribed podcast episode.");
      CmdLineUI.DisplayString("6. Subscribe to a podcast.");
      CmdLineUI.DisplayString("7. Unsubscribe from a podcast.");
      CmdLineUI.DisplayString("8. Provide feedback on a podcast episode.");
      CmdLineUI.DisplayString("9. Log out.");

      return CmdLineUI.GetInt("Please enter a choice between 1 and 9:");
   }

   /// <summary>
   /// Displays the podcaster menu and returns the user's choice.
   /// </summary>
   public int DisplayPodcasterMenu()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Podcaster Menu.");
      CmdLineUI.DisplayString(MessageBox.MenuPrompt);
      CmdLineUI.DisplayString(MessageBox.MenuSeeDetails);
      CmdLineUI.DisplayString(MessageBox.MenuChangePassword);
      CmdLineUI.DisplayString("3. Create an episode.");
      CmdLineUI.DisplayString("4. View my episodes.");
      CmdLineUI.DisplayString("5. View statistics about my episodes.");
      CmdLineUI.DisplayString("6. View feedback about my episodes.");
      CmdLineUI.DisplayString("7. Log out.");

      return CmdLineUI.GetInt("Please enter a choice between 1 and 7:");
   }

   /// <summary>
   /// Displays the podcast genre selection menu and returns the user's choice.
   /// </summary>
   public int DisplayPodcastGenre()
   {
      CmdLineUI.DisplayString("Please enter your podcast genre:");

      Genre[] genres = Enum.GetValues<Genre>();
      for (int i = 0; i < genres.Length; i++)
      {
         CmdLineUI.DisplayString($"{i + 1}. {genres[i]}.");
      }

      return CmdLineUI.GetInt($"Please enter a choice between 1 and {genres.Length}:");
   }

   // ============================================================
   // Input prompts (ask the user and return what they typed)
   // ============================================================

   /// <summary>
   /// Prompts the user for their name and returns it.
   /// </summary>
   public string GetName()
   {
      CmdLineUI.DisplayString("Please enter your name:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for their age and returns it.
   /// </summary>
   public int GetAge()
   {
      return CmdLineUI.GetInt("Please enter your age between 18 and 99:");
   }

   /// <summary>
   /// Prompts the user for their mobile number and returns it.
   /// </summary>
   public string GetMobileNumber()
   {
      CmdLineUI.DisplayString("Please enter your mobile number:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for their email address and returns it.
   /// </summary>
   public string GetEmail()
   {
      CmdLineUI.DisplayString("Please enter your email:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for a new password (with requirements) and returns it.
   /// </summary>
   public string GetPassword()
   {
      CmdLineUI.DisplayString(MessageBox.EnterPassword);
      DisplayPasswordRequirements();
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for their password (without requirements) and returns it.
   /// </summary>
   public string GetNewPassword()
   {
      CmdLineUI.DisplayString(MessageBox.EnterPassword);
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for a new replacement password and returns it.
   /// </summary>
   public string GetChangedPassword()
   {
      CmdLineUI.DisplayString("Please enter your new password:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for their current password and returns it.
   /// </summary>
   public string GetCurrentPassword()
   {
      CmdLineUI.DisplayString("Please enter your current password:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for a registration date and returns it.
   /// </summary>
   public DateTime? GetRegistrationDate()
   {
      CmdLineUI.DisplayString("Please enter the registration date in dd/MM/yyyy format:");
      return CmdLineUI.GetDateTime();
   }

   /// <summary>
   /// Prompts the user for their pay ID and returns it.
   /// </summary>
   public int GetPayId()
   {
      CmdLineUI.DisplayString("Please enter your pay ID between 100000 and 999999:");
      return CmdLineUI.GetInt();
   }

   /// <summary>
   /// Prompts the user for a podcast name and returns it.
   /// </summary>
   public string GetPodcastName()
   {
      CmdLineUI.DisplayString("Please enter your podcast name:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for a podcast description and returns it.
   /// </summary>
   public string GetPodcastDescription()
   {
      CmdLineUI.DisplayString("Please enter your podcast description:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for an episode name and returns it.
   /// </summary>
   public string GetEpisodeName()
   {
      CmdLineUI.DisplayGetData("episode's name");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user for an episode recorded date and returns it.
   /// </summary>
   public DateTime? GetEpisodeRecordedDate()
   {
      CmdLineUI.DisplayGetData("episode's recorded date in dd/MM/yyyy format");
      return CmdLineUI.GetDateTime();
   }

   /// <summary>
   /// Prompts the user for an episode length in minutes and returns it.
   /// </summary>
   public int GetEpisodeLength()
   {
      CmdLineUI.DisplayGetData("episode's length in minutes between 1 and 300");
      return CmdLineUI.GetInt();
   }

   /// <summary>
   /// Prompts the user for whether the episode is premium-only and returns the input.
   /// </summary>
   public string GetPremiumEpisodeInput()
   {
      CmdLineUI.DisplayString("Please enter if episode is reserved for premium listeners only (true/false):");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Re-prompts the user for a genre choice and returns it.
   /// </summary>
   public int GetGenreChoiceReprompt(int genreCount)
   {
      return CmdLineUI.GetInt($"Please enter a choice between 1 and {genreCount}:");
   }

   /// <summary>
   /// Prompts the user to select a podcast by number and returns the choice.
   /// </summary>
   public int GetPodcastMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the podcast's menu number to view more information or enter 0 to cancel:");
   }

   /// <summary>
   /// Prompts the user to select a podcast to subscribe to and returns the choice.
   /// </summary>
   public int GetSubscribeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the podcast's menu number to subscribe or enter 0 to cancel:");
   }

   /// <summary>
   /// Prompts the user to select a podcast to unsubscribe from and returns the choice.
   /// </summary>
   public int GetUnsubscribeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the podcast's menu number to unsubscribe or enter 0 to cancel:");
   }

   /// <summary>
   /// Prompts the user to select an episode to listen to and returns the choice.
   /// </summary>
   public int GetEpisodeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the episode's menu number to listen to the episode or enter 0 to cancel:");
   }

   /// <summary>
   /// Prompts the user for the date they listened and returns it.
   /// </summary>
   public DateTime? GetListenDate()
   {
      CmdLineUI.DisplayString("Please enter the date in dd/MM/yyyy format that you listened to the episode:");
      return CmdLineUI.GetDateTime();
   }

   /// <summary>
   /// Prompts the user for how many minutes they listened and returns it.
   /// </summary>
   public int GetListenMinutes()
   {
      CmdLineUI.DisplayString("Please enter how many minutes you listened to the episode:");
      return CmdLineUI.GetInt();
   }

   /// <summary>
   /// Prompts the user to select an episode for feedback and returns the choice.
   /// </summary>
   public int GetFeedbackEpisodeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the episode's menu number to provide feedback or 0 to cancel:");
   }

   /// <summary>
   /// Prompts the user for a thumbs-up or thumbs-down rating and returns it.
   /// </summary>
   public bool GetThumbsUp()
   {
      CmdLineUI.DisplayString("Do you give the episode a thumbs-up (true) or a thumbs-down (false) rating:");
      return CmdLineUI.GetBool();
   }

   /// <summary>
   /// Prompts the user for text feedback and returns it.
   /// </summary>
   public string GetFeedbackText()
   {
      CmdLineUI.DisplayString("Please enter some text feedback about the episode:");
      return CmdLineUI.GetString();
   }

   /// <summary>
   /// Prompts the user to select an episode to view feedback for and returns the choice.
   /// </summary>
   public int GetFeedbackViewMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the episode's menu number to view the text feedback or enter 0 to cancel:");
   }

   // ============================================================
   // Requirements shown to the user
   // ============================================================

   /// <summary>
   /// Displays the password complexity requirements.
   /// </summary>
   private void DisplayPasswordRequirements()
   {
      CmdLineUI.DisplayString("Your password must:");
      CmdLineUI.DisplayString("- be at least 8 characters long");
      CmdLineUI.DisplayString("- contain a number");
      CmdLineUI.DisplayString("- contain a lowercase letter");
      CmdLineUI.DisplayString("- contain an uppercase letter");
   }

   // ============================================================
   // Error messages
   // ============================================================

   /// <summary>
   /// Displays an error message followed by "Please try again".
   /// </summary>
   public void DisplayRetryError(string message)
   {
      CmdLineUI.DisplayErrorAgain(message);
   }

   /// <summary>
   /// Displays an error message without a retry prompt.
   /// </summary>
   public void DisplayErrorMessage(string message)
   {
      CmdLineUI.DisplayError(message);
   }

   // ============================================================
   // Status and confirmation messages
   // ============================================================

   /// <summary>
   /// Displays the premium registration header.
   /// </summary>
   public void DisplayPremiumRegisteration()
   {
      CmdLineUI.DisplayString("Registering as a premium listener.");
   }

   /// <summary>
   /// Displays the regular listener registration header.
   /// </summary>
   public void DisplayRegularRegisteration()
   {
      CmdLineUI.DisplayString("Registering as a regular listener.");
   }

   /// <summary>
   /// Displays the podcaster registration header.
   /// </summary>
   public void DisplayPodcasterRegistration()
   {
      CmdLineUI.DisplayString("Registering as a podcaster.");
   }

   /// <summary>
   /// Displays a congratulations message for regular listener registration.
   /// </summary>
   public void DisplayCongratulationsMessage(string name)
   {
      CmdLineUI.DisplayString(string.Format("Congratulations {0}. You have registered as a regular listener.", name));
   }

   /// <summary>
   /// Displays a congratulations message for premium listener registration.
   /// </summary>
   public void DisplayCongratulationsMessagePremium(string name)
   {
      CmdLineUI.DisplayString(string.Format("Congratulations {0}. You have registered as a premium listener.", name));
   }

   /// <summary>
   /// Displays a congratulations message for podcaster registration.
   /// </summary>
   public void DisplayCongratsPD(string name)
   {
      CmdLineUI.DisplayString(string.Format("Congratulations {0}. You have registered as a podcaster.", name));
   }

   /// <summary>
   /// Displays a message confirming the password was updated.
   /// </summary>
   public void DisplayPasswordUpdateSuccess()
   {
      CmdLineUI.DisplayString("Password has been updated.");
   }

   /// <summary>
   /// Displays the application exit message.
   /// </summary>
   public void DisplayExit()
   {
      CmdLineUI.DisplayString("See you on the next rotation.");
   }

   // ============================================================
   // Welcome and account details
   // ============================================================

   /// <summary>
   /// Displays a welcome-back greeting for the logged-in user.
   /// </summary>
   public void DisplayWelcomeBack(string name)
   {
      CmdLineUI.DisplayString($"Welcome back {name}.");
   }

   /// <summary>
   /// Displays the shared personal details for any user.
   /// </summary>
   private void DisplayPersonalDetails(User user)
   {
      CmdLineUI.DisplayString(MessageBox.DetailsHeader);
      CmdLineUI.DisplayString($"Name: {user.Name}");
      CmdLineUI.DisplayString($"Age: {user.Age}");
      CmdLineUI.DisplayString($"Mobile phone number: {user.MobileNumber}");
      CmdLineUI.DisplayString($"Email: {user.Email}");
   }

   /// <summary>
   /// Displays listening statistics shared across listener types.
   /// </summary>
   private void DisplayListeningStats(int episodesStarted, int episodesCompleted, int totalMinutes)
   {
      CmdLineUI.DisplayString($"Episodes started: {episodesStarted}");
      CmdLineUI.DisplayString($"Episodes completed: {episodesCompleted}");
      CmdLineUI.DisplayString($"Total minutes listened to: {totalMinutes}");
   }

   /// <summary>
   /// Displays a regular listener's personal details and listening statistics.
   /// </summary>
   public void DisplayUserDetails(User user, int episodesStarted, int episodesCompleted, int totalMinutes, int advertisements)
   {
      DisplayPersonalDetails(user);
      DisplayListeningStats(episodesStarted, episodesCompleted, totalMinutes);
      CmdLineUI.DisplayString($"Advertisements listened to: {advertisements}");
   }

   /// <summary>
   /// Displays a premium listener's personal details and listening statistics.
   /// </summary>
   public void DisplayPremiumUserDetails(PremiumListener premiumListener, int episodesStarted, int episodesCompleted, int totalMinutes)
   {
      DisplayPersonalDetails(premiumListener);
      DisplayListeningStats(episodesStarted, episodesCompleted, totalMinutes);
      CmdLineUI.DisplayString($"Registration Date: {premiumListener.RegistrationDate:dd/MM/yyyy}");
      CmdLineUI.DisplayString($"Pay ID: {premiumListener.PayId}");
   }

   /// <summary>
   /// Displays a podcaster's personal and podcast details.
   /// </summary>
   public void DisplayPodcasterDetails(Podcaster podcaster)
   {
      DisplayPersonalDetails(podcaster);
      foreach (Podcast podcast in podcaster.Podcasts)
      {
         CmdLineUI.DisplayString($"Podcast name: {podcast.Title}");
         CmdLineUI.DisplayString($"Podcast genre: {podcast.Genre}");
         CmdLineUI.DisplayString($"Podcast description: {podcast.Description}");
         CmdLineUI.DisplayString($"Number of podcast subscribers: {podcast.Subscribers}");
      }
   }

   // ============================================================
   // Podcast and episode display
   // ============================================================

   /// <summary>
   /// Displays a numbered list of podcasts with a header.
   /// </summary>
   public void DisplayPodcastList(string header, List<Podcast> podcasts)
   {
      CmdLineUI.DisplayString(header);
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-25}{"Podcaster",-20}{"Subscribers",-15}");
      for (int i = 0; i < podcasts.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{podcasts[i].Title,-25}{podcasts[i].Host.Name,-20}{podcasts[i].Subscribers,-15}");
      }
   }

   /// <summary>
   /// Displays a podcaster's episode list in short format.
   /// </summary>
   public void DisplayEpisodeList(IReadOnlyList<Episode> episodes)
   {
      CmdLineUI.DisplayString("Your episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Recorded Date",-15}{"Length (min)",-15}{"Premium Only",-15}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{episodes[i].RecordedDate,-15:dd/MM/yyyy}{episodes[i].EpisodeLength,-15}{episodes[i].PremmiumEpisode,-15}");
      }
   }

   /// <summary>
   /// Displays an episode list with full details including descriptions.
   /// </summary>
   public void DisplayEpisodeListFull(IReadOnlyList<Episode> episodes)
   {
      CmdLineUI.DisplayString("List of episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Recorded Date",-15}{"Length (min)",-15}{"Premium Only",-15}{"Description",-30}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{episodes[i].RecordedDate,-15:dd/MM/yyyy}{episodes[i].EpisodeLength,-15}{episodes[i].PremmiumEpisode,-15}{episodes[i].EpisodeDescription}");
      }
   }

   /// <summary>
   /// Displays detailed information about a single podcast.
   /// </summary>
   public void DisplayPodcastDetails(Podcast podcast)
   {
      CmdLineUI.DisplayString("Podcast Information.");
      CmdLineUI.DisplayString($"Name: {podcast.Title}");
      CmdLineUI.DisplayString($"Podcaster: {podcast.Host.Name}");
      CmdLineUI.DisplayString($"Podcast description: {podcast.Description}");
   }

   // ============================================================
   // Subscription display
   // ============================================================

   /// <summary>
   /// Displays the subscribed podcasts section header.
   /// </summary>
   public void DisplaySubscriptedHeader()
   {
      CmdLineUI.DisplayString("Subscribed podcasts.");
   }

   /// <summary>
   /// Displays a message indicating the user has no subscriptions.
   /// </summary>
   public void DisplayNotSubscribed()
   {
      CmdLineUI.DisplayString("You are not subscribed to any podcasts.");
   }

   /// <summary>
   /// Displays the available podcasts section header.
   /// </summary>
   public void DisplayAvailableHeader()
   {
      CmdLineUI.DisplayString("Available podcasts.");
   }

   /// <summary>
   /// Displays a message indicating the user is subscribed to all podcasts.
   /// </summary>
   public void DisplaySubscribedToAll()
   {
      CmdLineUI.DisplayString("You are subscribed to all available podcasts.");
   }

   /// <summary>
   /// Displays a confirmation message after subscribing to a podcast.
   /// </summary>
   public void DisplaySubscribeSuccess(string podcastTitle)
   {
      CmdLineUI.DisplayString($"Congratulations, you have been subscribed to {podcastTitle}.");
   }

   /// <summary>
   /// Displays a confirmation message after unsubscribing from a podcast.
   /// </summary>
   public void DisplayUnsubscribeSuccess(string podcastTitle)
   {
      CmdLineUI.DisplayString($"You have been unsubscribed to {podcastTitle}.");
   }

   // ============================================================
   // Listening display
   // ============================================================

   /// <summary>
   /// Displays a message indicating the user finished an episode.
   /// </summary>
   public void DisplayEpisodeFinished(string userName, string episodeName)
   {
      CmdLineUI.DisplayString($"Congratulations {userName}. You have finished the episode {episodeName}.");
   }

   /// <summary>
   /// Displays the user's listening progress for an episode.
   /// </summary>
   public void DisplayListeningProgress(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining)
   {
      CmdLineUI.DisplayString($"You have listened to podcast {podcastName}, episode {episodeName}, for {totalMinutes} minutes. The last time you listened was on {lastDate:dd/MM/yyyy}. You have {remaining} minutes remaining.");
   }

   /// <summary>
   /// Displays the user's total listening progress for an episode.
   /// </summary>
   public void DisplayListeningProgressTotal(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining)
   {
      CmdLineUI.DisplayString($"In total you have listened to podcast {podcastName}, episode {episodeName}, for {totalMinutes} minutes. The last time you listened was on {lastDate:dd/MM/yyyy}. You have {remaining} minutes remaining.");
   }

   // ============================================================
   // Feedback and statistics display
   // ============================================================

   /// <summary>
   /// Displays episode listening statistics for a podcaster.
   /// </summary>
   public void DisplayStatistics(IReadOnlyList<Episode> episodes, List<int> listenersStarted, List<int> listenersCompleted)
   {
      CmdLineUI.DisplayString("Statistics about your episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Listeners Started",-20}{"Listeners Completed",-20}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{listenersStarted[i],-20}{listenersCompleted[i],-20}");
      }
   }

   /// <summary>
   /// Displays a feedback summary with thumbs-up counts per episode.
   /// </summary>
   public void DisplayFeedbackSummary(IReadOnlyList<Episode> episodes, List<int> thumbsUpCounts)
   {
      CmdLineUI.DisplayString("Feedback about your episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Thumbs Up",-20}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{thumbsUpCounts[i],-20}");
      }
   }

   /// <summary>
   /// Displays a list of text feedback entries.
   /// </summary>
   public void DisplayFeedbackTexts(List<string> feedbackTexts)
   {
      for (int i = 0; i < feedbackTexts.Count; i++)
      {
         CmdLineUI.DisplayString($"Listener {i + 1}: {feedbackTexts[i]}");
      }
   }
}
