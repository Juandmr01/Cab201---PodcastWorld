/*
Header
Menus (main, registration, login, listener, premium, podcaster, genres)
Input prompts (Get... methods)
Password requirements
Error messages (DisplayError... and DisplayInvalid...)
Status and confirmation messages (congratulations, password updated, exit)
Welcome and account details

*/

using System.Diagnostics;

public class AppMenu : IAppMenu
{
   // ============================================================
   // Header
   // ============================================================

   public void DisplayHeader()
   {
      CmdLineUI.DisplayString("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+==");
      CmdLineUI.DisplayString("         Welcome to PodCastWorld!");
      CmdLineUI.DisplayString("      Podcasting all over the world.");
      CmdLineUI.DisplayString("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+==");
   }

   // ============================================================
   // Menus (display options and return the user's choice)
   // ============================================================

   public int DisplayMenuOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Main Menu.");
      CmdLineUI.DisplayString("Please make a choice from the menu below.");
      CmdLineUI.DisplayString("1. Log in as a registered user.");
      CmdLineUI.DisplayString("2. Register as a new user.");
      CmdLineUI.DisplayString("3. Exit.");

      int userChoice = CmdLineUI.GetInt("Please enter a choice between 1 and 3:");

      return userChoice;
   }

   public int DisplayRegisterOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Registration Menu.");
      CmdLineUI.DisplayString("Please enter your user type.");
      CmdLineUI.DisplayString("1. A regular listener.");
      CmdLineUI.DisplayString("2. A premium listener.");
      CmdLineUI.DisplayString("3. A podcaster.");

      int userChoice = CmdLineUI.GetInt("Please enter a choice between 1 and 3:");
      return userChoice;
   }

   public void DisplayLoginMenu()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Log in Menu.");
   }
   

   public int DisplayRegularListenerMenuOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Regular Listener Menu.");
      CmdLineUI.DisplayString("Please make a choice from the menu below.");
      CmdLineUI.DisplayString("1. See my details.");
      CmdLineUI.DisplayString("2. Change my password.");
      CmdLineUI.DisplayString("3. View all podcasts.");
      CmdLineUI.DisplayString("4. Listen to a podcast episode.");
      CmdLineUI.DisplayString("5. Provide feedback on a podcast episode.");
      CmdLineUI.DisplayString("6. Log out.");

      int userChoice = CmdLineUI.GetInt("Please enter a choice between 1 and 6:");

      return userChoice;
   }

   public int DisplayPremiumListenerMenuOptions()
   {
      CmdLineUI.DisplayString();
      CmdLineUI.DisplayString("Premium Listener Menu.");
      CmdLineUI.DisplayString("Please make a choice from the menu below.");
      CmdLineUI.DisplayString("1. See my details.");
      CmdLineUI.DisplayString("2. Change my password.");
      CmdLineUI.DisplayString("3. View all podcasts.");
      CmdLineUI.DisplayString("4. Listen to a podcast episode.");
      CmdLineUI.DisplayString("5. Listen to a subscribed podcast episode.");
      CmdLineUI.DisplayString("6. Subscribe to a podcast.");
      CmdLineUI.DisplayString("7. Unsubscribe from a podcast.");
      CmdLineUI.DisplayString("8. Provide feedback on a podcast episode.");
      CmdLineUI.DisplayString("9. Log out.");

      return CmdLineUI.GetInt("Please enter a choice between 1 and 9:");
   }
      public int DisplayPodcasterMenu()
         {
            CmdLineUI.DisplayString();
            CmdLineUI.DisplayString("Podcaster Menu.");
            CmdLineUI.DisplayString("Please make a choice from the menu below.");
            CmdLineUI.DisplayString("1. See my details.");
            CmdLineUI.DisplayString("2. Change my password.");
            CmdLineUI.DisplayString("3. Create an episode.");
            CmdLineUI.DisplayString("4. View my episodes.");
            CmdLineUI.DisplayString("5. View statistics about my episodes.");
            CmdLineUI.DisplayString("6. View feedback about my episodes.");
            CmdLineUI.DisplayString("7. Log out.");

            return CmdLineUI.GetInt("Please enter a choice between 1 and 7:");

         }
   //Podcaster Menu.
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

   public string GetName()
   {
      CmdLineUI.DisplayString("Please enter your name:");
      return CmdLineUI.GetString();
   }

   public int GetAge()
   {
      return CmdLineUI.GetInt("Please enter your age between 18 and 99:");
   }

   public string GetMobileNumber()
   {
      CmdLineUI.DisplayString("Please enter your mobile number:");
      return CmdLineUI.GetString();
   }

   public string GetEmail()
   {
      CmdLineUI.DisplayString("Please enter your email:");
      return CmdLineUI.GetString();
   }

   public string GetPassword()
   {
      CmdLineUI.DisplayString("Please enter your password:");
      DisplayPasswordRequirements();
      return CmdLineUI.GetString();
   }

   public string GetNewPassword()
   {
      CmdLineUI.DisplayString("Please enter your password:");
      return CmdLineUI.GetString();
   }

   public string GetChangedPassword()
   {
      CmdLineUI.DisplayString("Please enter your new password:");
      return CmdLineUI.GetString();
   }

   public string GetCurrentPassword()
   {
      CmdLineUI.DisplayString("Please enter your current password:");
      return CmdLineUI.GetString();
   }

   public DateTime? GetRegistrationDate()
   {
      CmdLineUI.DisplayString("Please enter the registration date in dd/MM/yyyy format:");
      return CmdLineUI.GetDateTime();
   }

   public int GetPayId()
   {
      CmdLineUI.DisplayString("Please enter your pay ID between 100000 and 999999:");
      return CmdLineUI.GetInt();
   }

   public string GetPodcastName()
   {
      CmdLineUI.DisplayString("Please enter your podcast name:");
      return CmdLineUI.GetString();
   }
   public string GetPodcastDescription()
   {
      CmdLineUI.DisplayString("Please enter your podcast description:");
      return CmdLineUI.GetString();
   }

   public string GetEpisodeName()
   {
      CmdLineUI.DisplayGetData("episode's name");
      return CmdLineUI.GetString();
   }


    public  DateTime? GetEpisodeRecordedDate()
   {
      CmdLineUI.DisplayGetData("episode's recorded date in dd/MM/yyyy format");
      return CmdLineUI.GetDateTime();
   }
   public int GetEpisodeLength()
   {
      CmdLineUI.DisplayGetData("episode's length in minutes between 1 and 300");
      return CmdLineUI.GetInt();
   }

   public string GetPremiumEpisodeInput()
   {
      CmdLineUI.DisplayString("Please enter if episode is reserved for premium listeners only (true/false):");
      return CmdLineUI.GetString();
   }
   public int GetGenreChoiceReprompt(int genreCount)
   {
      return CmdLineUI.GetInt($"Please enter a choice between 1 and {genreCount}:");
   }

      public int GetPodcastMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the podcast's menu number to view more information or enter 0 to cancel:");
   }



   //


   // ============================================================
   // Requirements shown to the user
   // ============================================================

   public void DisplayPasswordRequirements()
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

   public void DisplayErrorNoUser()
   {
      CmdLineUI.DisplayError("There are no people registered");
   }

   public void DisplayErrorEmailNotRegister()
   {
      CmdLineUI.DisplayError("Email is not registered");
   }

   public void DisplayErrorEmailRegister()
   {
      CmdLineUI.DisplayErrorAgain("Email already registered");
   }

   public void DisplayInvalidName()
   {
      CmdLineUI.DisplayErrorAgain("Supplied name is invalid");
   }

   public void DisplayInvalidAge()
   {
      CmdLineUI.DisplayErrorAgain("Supplied age is invalid");
   }

   public void DisplayInvalidMobileNumber()
   {

       CmdLineUI.DisplayErrorAgain("Supplied mobile number is invalid");
   }

   public void DisplayInvalidMenuChoice()
   {
      CmdLineUI.DisplayErrorAgain("Supplied menu input is out of range");
   }

   public void DisplayInvalidEmail()
   {
      CmdLineUI.DisplayErrorAgain("Supplied email is invalid");
   }

   public void DisplayInvalidPassword()
   {
      CmdLineUI.DisplayErrorAgain("Supplied password is invalid");
   }

   public void DisplayPasswordNotMatch()
   {
      CmdLineUI.DisplayError("Entered password does not match existing password");
   }

   public void DisplayPasswordNotMatchAgain()
   {
      CmdLineUI.DisplayErrorAgain("Entered password does not match existing password");
   }

   public void DisplaySamePasswordError()
   {
      CmdLineUI.DisplayErrorAgain("New password is the same as the existing password");
   }

   
   public void DisplayInvalidPayId()
   {
      CmdLineUI.DisplayErrorAgain("Supplied pay ID is invalid");
   }

   public void DisplayNoPodcasts()
   {
      CmdLineUI.DisplayError("There are no podcasts");
   }

   public void DisplayNoEpisodesListener()
   {
      CmdLineUI.DisplayError("The podcaster has not recorded any episodes");
   }

   public void DisplayNoEpisodesPodcaster()
   {
      CmdLineUI.DisplayError("You have not recorded any episodes");
   }

   public void DisplayInvalidEpisodeName()
   {
      CmdLineUI.DisplayErrorAgain("Supplied episode's name is invalid");
   }

   public void DisplayInvalidPodcastName()
   {
      CmdLineUI.DisplayErrorAgain("Supplied podcast name is invalid");
   }

   public void DisplayInvalidPodcastDescription()
   {
      CmdLineUI.DisplayErrorAgain("Supplied podcast description is invalid");
   }

   public void DisplayInvalidEpisodeDate()
   {
      CmdLineUI.DisplayErrorAgain("Supplied date is invalid");
   }

   public void DisplayInvalidEpisodeLength()
   {
      CmdLineUI.DisplayErrorAgain("Supplied value is out of range");
   }

   public void DisplayInvalidBoolean()
   {
      CmdLineUI.DisplayErrorAgain("Supplied value must be a boolean");
   }
   // ============================================================
   // Status and confirmation messages
   // ============================================================

   public void DisplayPremiumRegisteration()
   {
      CmdLineUI.DisplayString("Registering as a premium listener.");
   }

   public void DisplayRegularRegisteration()
   {
      CmdLineUI.DisplayString("Registering as a regular listener.");
   }

   public void DisplayPodcasterRegistration()
   {
      CmdLineUI.DisplayString("Registering as a podcaster.");
   }

   public void DisplayCongratulationsMessage(string name)
   {
      CmdLineUI.DisplayString(string.Format("Congratulations {0}. You have registered as a regular listener.", name));
   }

   public void DisplayCongratulationsMessagePremium(string name)
   {
      CmdLineUI.DisplayString(string.Format("Congratulations {0}. You have registered as a premium listener.", name));
   }

   public void DisplayCongratsPD(string name)
   {
      CmdLineUI.DisplayString(string.Format("Congratulations {0}. You have registered as a podcaster.", name));
   }

   public void DisplayRegistrationSuccess(string name)
   {
      CmdLineUI.DisplayString($"Congratulations {name}. You have successfully registered as a regular listener.");
   }

   public void DisplayPasswordUpdateSuccess()
   {
      CmdLineUI.DisplayString("Password has been updated.");
   }

   public void DisplayExit()
   {
      CmdLineUI.DisplayString("See you on the next rotation.");
   }

   // ============================================================
   // Welcome and account details
   // ============================================================

   public void DisplayWelcomeBack(string name)
   {
      CmdLineUI.DisplayString($"Welcome back {name}.");
   }

   public void DisplayUserDetails(User user)
   {
      CmdLineUI.DisplayString("Your details.");
      CmdLineUI.DisplayString($"Name: {user.Name}");
      CmdLineUI.DisplayString($"Age: {user.Age}");
      CmdLineUI.DisplayString($"Mobile phone number: {user.MobileNumber}");
      CmdLineUI.DisplayString($"Email: {user.Email}");
      CmdLineUI.DisplayString($"Episodes started: {user.EpisodesStarted}");
      CmdLineUI.DisplayString($"Episodes completed: {user.EpisodesCompleted}");
      CmdLineUI.DisplayString($"Total minutes listened to: {user.TotalMinutesListenedTo}");
      CmdLineUI.DisplayString($"Advertisements listened to: {user.AdvertisementsListenedTo}");
   }

   public void DisplayPremiumUserDetails(PremiumListener premiumListener)
   {
      CmdLineUI.DisplayString("Your details.");
      CmdLineUI.DisplayString($"Name: {premiumListener.Name}");
      CmdLineUI.DisplayString($"Age: {premiumListener.Age}");
      CmdLineUI.DisplayString($"Mobile phone number: {premiumListener.MobileNumber}");
      CmdLineUI.DisplayString($"Email: {premiumListener.Email}");
      CmdLineUI.DisplayString($"Episodes started: {premiumListener.EpisodesStarted}");
      CmdLineUI.DisplayString($"Episodes completed: {premiumListener.EpisodesCompleted}");
      CmdLineUI.DisplayString($"Total minutes listened to: {premiumListener.TotalMinutesListenedTo}");
      CmdLineUI.DisplayString($"Registration Date: {premiumListener.RegistrationDate:dd/MM/yyyy}");
      CmdLineUI.DisplayString($"Pay ID: {premiumListener.PayId}");
   }

   public void DisplayPodcasterDetails(Podcaster podcaster)
   {
      CmdLineUI.DisplayString("Your details.");
      CmdLineUI.DisplayString($"Name: {podcaster.Name}");
      CmdLineUI.DisplayString($"Age: {podcaster.Age}");
      CmdLineUI.DisplayString($"Mobile phone number: {podcaster.MobileNumber}");
      CmdLineUI.DisplayString($"Email: {podcaster.Email}");
      foreach (Podcast podcast in podcaster.Podcasts)
      {
         CmdLineUI.DisplayString($"Podcast name: {podcast.Title}");
         CmdLineUI.DisplayString($"Podcast genre: {podcast.Genre}");
         CmdLineUI.DisplayString($"Podcast description: {podcast.Description}");
         CmdLineUI.DisplayString($"Number of podcast subscribers: {podcast.Subscribers}");
      }
   }

   public void DisplayPodcastList(string header, List<Podcast> podcasts)
   {
      CmdLineUI.DisplayString(header);
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-25}{"Podcaster",-20}{"Subscribers",-15}");
      for (int i = 0; i < podcasts.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{podcasts[i].Title,-25}{podcasts[i].Host.Name,-20}{podcasts[i].Subscribers,-15}");
      }
   }
   public void DisplayEpisodeList(IReadOnlyList<Episode> episodes)
   {
      CmdLineUI.DisplayString("Your episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Recorded Date",-15}{"Length (min)",-15}{"Premium Only",-15}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{episodes[i].RecordedDate,-15:dd/MM/yyyy}{episodes[i].EpisodeLength,-15}{episodes[i].PremmiumEpisode,-15}");
      }
   }

   public void DisplayEpisodeListFull(IReadOnlyList<Episode> episodes)
   {
      CmdLineUI.DisplayString("List of episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Recorded Date",-15}{"Length (min)",-15}{"Premium Only",-15}{"Description",-30}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{episodes[i].RecordedDate,-15:dd/MM/yyyy}{episodes[i].EpisodeLength,-15}{episodes[i].PremmiumEpisode,-15}{episodes[i].EpisodeDescription}");
      }
   }

   public void DisplayPodcastDetails(Podcast podcast)
   {
      CmdLineUI.DisplayString("Podcast Information.");
      CmdLineUI.DisplayString($"Name: {podcast.Title}");
      CmdLineUI.DisplayString($"Podcaster: {podcast.Host.Name}");
      CmdLineUI.DisplayString($"Podcast description: {podcast.Description}");
   }

   public void DisplaySubscriptedHeader()
   {
      CmdLineUI.DisplayString("Subscribed podcasts.");
   }

   public void DisplayNotSubscribed()
   {
      CmdLineUI.DisplayString("You are not subscribed to any podcasts.");
   }

   public void DisplayAvailableHeader()
   {
      CmdLineUI.DisplayString("Available podcasts.");
   }

   public void DisplaySubscribedToAll()
   {
      CmdLineUI.DisplayString("You are subscribed to all available podcasts.");
   }

   public int GetSubscribeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the podcast's menu number to subscribe or enter 0 to cancel:");
   }

   public void DisplaySubscribeSuccess(string podcastTitle)
   {
      CmdLineUI.DisplayString($"Congratulations, you have been subscribed to {podcastTitle}.");
   }

   public int GetUnsubscribeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the podcast's menu number to unsubscribe or enter 0 to cancel:");
   }

   public void DisplayUnsubscribeSuccess(string podcastTitle)
   {
      CmdLineUI.DisplayString($"You have been unsubscribed to {podcastTitle}.");
   }

   public int GetEpisodeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the episode's menu number to listen to the episode or enter 0 to cancel:");
   }

   public DateTime? GetListenDate()
   {
      CmdLineUI.DisplayString("Please enter the date in dd/MM/yyyy format that you listened to the episode:");
      return CmdLineUI.GetDateTime();
   }

   public int GetListenMinutes()
   {
      CmdLineUI.DisplayString("Please enter how many minutes you listened to the episode:");
      return CmdLineUI.GetInt();
   }

   public void DisplayEpisodeFinished(string userName, string episodeName)
   {
      CmdLineUI.DisplayString($"Congratulations {userName}. You have finished the episode {episodeName}.");
   }

   public void DisplayListeningProgress(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining)
   {
      CmdLineUI.DisplayString($"You have listened to podcast {podcastName}, episode {episodeName}, for {totalMinutes} minutes. The last time you listened was on {lastDate:dd/MM/yyyy}. You have {remaining} minutes remaining.");
   }

   public void DisplayListeningProgressTotal(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining)
   {
      CmdLineUI.DisplayString($"In total you have listened to podcast {podcastName}, episode {episodeName}, for {totalMinutes} minutes. The last time you listened was on {lastDate:dd/MM/yyyy}. You have {remaining} minutes remaining.");
   }

   public void DisplayAlreadyFinished()
   {
      CmdLineUI.DisplayError("You have already finished this episode and cannot listen to it again");
   }

   public void DisplayPremiumOnlyError()
   {
      CmdLineUI.DisplayError("This episode can only be view by premium listeners");
   }

   public void DisplayNotSubscribedError()
   {
      CmdLineUI.DisplayError("You are not subscribed to any podcasts");
   }

   public void DisplayInvalidDateError()
   {
      CmdLineUI.DisplayErrorAgain("Supplied date is invalid");
   }

   public void DisplayDateAfterError(DateTime date)
   {
      CmdLineUI.DisplayErrorAgain($"Supplied date must be after {date.ToString("d/MM/yyyy")}");
   }

   public void DisplayOutOfRangeError()
   {
      CmdLineUI.DisplayErrorAgain("Supplied value is out of range");
   }

   public int GetFeedbackEpisodeMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the episode's menu number to provide feedback or 0 to cancel:");
   }

   public bool GetThumbsUp()
   {
      CmdLineUI.DisplayString("Do you give the episode a thumbs-up (true) or a thumbs-down (false) rating:");
      return CmdLineUI.GetBool();
   }

   public string GetFeedbackText()
   {
      CmdLineUI.DisplayString("Please enter some text feedback about the episode:");
      return CmdLineUI.GetString();
   }

   public void DisplayStatistics(IReadOnlyList<Episode> episodes, List<int> listenersStarted, List<int> listenersCompleted)
   {
      CmdLineUI.DisplayString("Statistics about your episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Listeners Started",-20}{"Listeners Completed",-20}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{listenersStarted[i],-20}{listenersCompleted[i],-20}");
      }
   }

   public void DisplayFeedbackSummary(IReadOnlyList<Episode> episodes, List<int> thumbsUpCounts)
   {
      CmdLineUI.DisplayString("Feedback about your episodes.");
      CmdLineUI.DisplayString($"{"Num",-5}{"Name",-30}{"Thumbs Up",-20}");
      for (int i = 0; i < episodes.Count; i++)
      {
         CmdLineUI.DisplayString($"{i + 1 + ".",-5}{episodes[i].Name,-30}{thumbsUpCounts[i],-20}");
      }
   }

   public int GetFeedbackViewMenuChoice()
   {
      return CmdLineUI.GetInt("Please enter the episode's menu number to view the text feedback or enter 0 to cancel:");
   }

   public void DisplayFeedbackTexts(List<string> feedbackTexts)
   {
      for (int i = 0; i < feedbackTexts.Count; i++)
      {
         CmdLineUI.DisplayString($"Listener {i + 1}: {feedbackTexts[i]}");
      }
   }
}
