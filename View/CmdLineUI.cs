using System.Globalization;

/// <summary>
/// The class will use the console to write and read user input and output. 
/// 
/// RESPONSIBILITY: User interaction.
/// </summary>
public static class CmdLineUI 
{


    /// <summary>
    /// Displays a blank line to the console screen.
    /// </summary>
    public static void DisplayString()
    {
        Console.WriteLine();
    }

    /// <summary>
    /// Displays a message to the console screen.
    /// </summary>
    /// <param name="msg">The message to display</param>
    public static void DisplayString(string msg)
    {
        Console.WriteLine(msg);
    }

    /// <summary>
    /// Displays a an object's ToString method 
    /// </summary>
    /// <param name="msg">The message to display</param>
    public static void DisplayString(object o)
    {
        Console.WriteLine(o.ToString());
    }

    /// <summary>
    /// Displays an error message to the console screen.
    /// </summary>
    /// <param name="msg">The message to display</param>
    public static void DisplayError(string msg)
    {
        Console.WriteLine("#####");
        Console.WriteLine($"# Error - {msg}.");
        Console.WriteLine("#####");
    }

    /// <summary>
    /// Displays an error message to the console screen and asks the user to try again.
    /// </summary>
    /// <param name="msg">The message to display</param>
    public static void DisplayErrorAgain(string msg)
    {
        Console.WriteLine("#####");
        Console.WriteLine($"# Error - {msg}.");
        Console.WriteLine("# Please try again.");
        Console.WriteLine("#####");
    }
    /// <summary>
    /// Displays a prompt asking the user to enter the specified data.
    /// </summary>
    public static void DisplayGetData(string msg)
    {
        Console.WriteLine($"Please enter your {msg}:");
    }



    /// <summary>
    /// Reads in a string from console and returns it
    /// </summary>
    /// <returns>A string representation of the users input</returns>
    public static string GetString()
    {
        string input = Console.ReadLine();
        return input;
    }

    /// <summary>
    /// Reads in a string from console, converts it to a Int32
    /// and returns the converted value
    /// </summary>
    /// <returns>A Int32 representation of the users input</returns>
    public static int GetInt(string msg)
    {
        Console.WriteLine(msg);
        int i;
        string input;
        while (!int.TryParse(input = Console.ReadLine(), out i))
        {
            if (input == null) ExitOnEndOfInput();
            Console.WriteLine("#####");
            Console.WriteLine("# Error - Supplied value must be an integral number.");
            Console.WriteLine("# Please try again.");
            Console.WriteLine("#####");
            Console.WriteLine(msg);
        }
        return i;
    }

    /// <summary>
    /// Reads an integer from console without displaying a prompt first.
    /// </summary>
    public static int GetInt()
    {
        int i;
        string input;
        while (!int.TryParse(input = Console.ReadLine(), out i))
        {
            if (input == null) ExitOnEndOfInput();
            Console.WriteLine("#####");
            Console.WriteLine("# Error - Supplied value must be an integral number.");
            Console.WriteLine("# Please try again.");
            Console.WriteLine("#####");
        }
        return i;
    }

         

    /// <summary>
    /// Reads in a string from console, converts it to a double
    /// and returns the converted value
    /// </summary>
    /// <returns>A double floating point representation of the users input</returns>
    public static double GetDouble()
    {
        double d;
        string input;
        while (!double.TryParse(input = Console.ReadLine(), out d))
        {
            if (input == null) ExitOnEndOfInput();
            Console.WriteLine("Please enter a valid number:");
        }
        return d;
    }

    /// <summary>
    /// Reads in a string from console, converts it to a boolean
    /// and returns the converted value
    /// </summary>
    /// <returns>A boolean representation of the users input</returns>
    public static bool GetBool()
    {
        bool b;
        string input;
        while (!bool.TryParse(input = Console.ReadLine(), out b))
        {
            if (input == null) ExitOnEndOfInput();
            Console.WriteLine("Please enter true or false:");
        }
        return b;
    }

    /// <summary>
    /// Called when Console.ReadLine() returns null, meaning the input stream has closed.
    /// Exits the application instead of looping forever re-prompting for input that will never arrive.
    /// </summary>
    private static void ExitOnEndOfInput()
    {
        Console.WriteLine("No more input available. Exiting.");
        Environment.Exit(0);
    }

    /// <summary>
    /// Reads in a string and converts it to a DateTime using the dd/MM/yyyy format
    /// All dates must be in this format so 1/2/2000 should be 01/02/2000 or you will ge an error
    /// https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-8.0
    /// </summary>
    /// <returns>The parsed date, or null if the input is not in the required format</returns>
    public static DateTime? GetDateTime()
    {
        string input = Console.ReadLine();
        DateTime result;
        string format = "dd/MM/yyyy";
        bool dtWorked = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        if (!dtWorked)
        {
            return null;
        }
        return result;
    }

}

