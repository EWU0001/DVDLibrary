using System;

namespace DVDLibrary
{
    public class MovieLibraryApp
    {
        static MovieCollection movieCollection = new();
        static MemberCollection memberCollection = new();

        static void Main(string[] args)
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("Welcome to COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            Console.WriteLine("=========================================================");
            MainMenu(); //initiate menu selection
        }
        static void MainMenu()
        {
            Console.WriteLine("\nMain Menu");
            Console.WriteLine("-----------------");

            int mainChoice;
            do
            {
                Console.WriteLine("\nSelect from the following:");
                Console.WriteLine("1. Staff");
                Console.WriteLine("2. Member");
                Console.WriteLine("0. End the program");
                Console.Write("\nEnter your choice ==> ");

                while (!int.TryParse(Console.ReadLine(), out mainChoice) || (mainChoice != 1 && mainChoice != 2 && mainChoice != 0))
                {
                    Console.WriteLine("Invalid input, please enter a valid integer.");
                    Console.Write("Enter your choice ==> ");
                }
                switch (mainChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("--------------------");
                        Console.WriteLine("Staff Menu selected");
                        Console.WriteLine("--------------------");
                        StaffMenu();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("--------------------");
                        Console.WriteLine("Member Menu selected");
                        Console.WriteLine("--------------------");
                        MemberMenu();
                        break;
                    case 0:
                        Console.WriteLine("Exiting program...");
                        Console.WriteLine("!-----Good Bye-----!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            } while (mainChoice != 0);
            Console.ReadKey();
        }

        static void StaffMenu()
        {
            if (StaffAuth())
            {
                Console.WriteLine("\nYou are in Staff Menu");
                int staffChoice;
                do
                {
                    Console.WriteLine("\n------------------------");
                    Console.WriteLine("1. Insert DVDs of a new movie to the system");
                    Console.WriteLine("2. Insert DVDs of an existing movie to the system");
                    Console.WriteLine("3. Remove DVDs of a movie from the system");
                    Console.WriteLine("4. Register a new member with the system");
                    Console.WriteLine("5. Remove a registered member from the system");
                    Console.WriteLine("6. Find a member's contact phone number, given the member's full name");
                    Console.WriteLine("7. Find all the members who are currently renting a particular movie");
                    Console.WriteLine("0. Return to main menu");
                    while (true)
                    {
                        Console.Write("Enter your choice ==> ");
                        if (!int.TryParse(Console.ReadLine(), out staffChoice) || staffChoice < 0 || staffChoice > 7)
                        {
                            Console.WriteLine("Invalid input, please enter a valid integer between 0 and 7.");
                        }
                        break;
                    }
                    switch (staffChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 1 - add movies to the system");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("-------auto-generate------");
                            // Populate the movie library with auto-generate movies data
                            List<Movie> movies = PopulateData.GenerateMovies();
                            foreach (Movie movie in movies)
                            {
                                movieCollection.AddMovie(movie);
                                // Console.WriteLine($"Added {movies.Count} movies to the system.");
                            }
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 2 - add DVDs to existing movies");
                            Console.WriteLine("--------------------------");
                            // Console.WriteLine("Enter movie title >> ");
                            // string? inputTitle = Console.ReadLine();
                            movieCollection.AddDVD("Python", 1); // dvd from an example movie
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 3 - remove DVDs from system");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Enter movie title >> (press enter auto-generate)");
                            string? inputTitle = Console.ReadLine();
                            Console.WriteLine("Enter number of DVD to be removed >> (press enter auto-generate)");
                            Console.ReadLine();
                            movieCollection.RemoveDVD("Python", 1); //remove 1 dvd from an example movie
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 4 - register new members");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("input first name (press enter auto-generate first name'aaa')");
                            string? inputFirstName = Console.ReadLine();
                            Console.WriteLine("input last name (press enter auto-generate last name 'bbb')");
                            string? inputLastName = Console.ReadLine();
                            Console.WriteLine("input phone number (press enter auto-generate number '0000000')");
                            string? inputPhoneNumber = Console.ReadLine();
                            Console.WriteLine("input 4 digits pin (enter pin manually)");
                            string? inputPin = Console.ReadLine();
                            while (inputPin?.Length != 4)
                            {
                                Console.WriteLine("Invalid PIN length. PIN must be exactly 4 digits.");
                                inputPin = Console.ReadLine();
                            }
                            // Member memberToAdd = new("aaa", "bbb", "0000000", $"{inputPin}");
                            Member memberToAdd = new($"{inputFirstName}", $"{inputLastName}", $"{inputPhoneNumber}", $"{inputPin}");
                            memberCollection.AddMember(memberToAdd);
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 5 - remove members");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("input first name");
                            string? inputFirstNameToRemove = Console.ReadLine();
                            Console.WriteLine("input last name");
                            string? inputLastNameToRemove = Console.ReadLine();

                            Member memberToRemove = new(inputFirstNameToRemove, inputLastNameToRemove, null, null);
                            memberCollection.Remove(memberToRemove); //all member details
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 6 - Find member's contact number");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("input first name");
                            string? inputFirstNameToFind = Console.ReadLine();
                            Console.WriteLine("input last name");
                            string? inputLastNameToFind = Console.ReadLine();
                            Member memberToFind = new(inputFirstNameToFind, inputLastNameToFind, null, null);
                            memberCollection.GetMemberNumber(memberToFind);
                            break;
                        case 7:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 7 - display members renting a movie");
                            Console.WriteLine("--------------------------");
                            memberCollection.GetListOfBorrowers("Python"); //get members renting an example movie
                            break;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Returned to Main Menu....");
                            Console.WriteLine("--------------------------");
                            MainMenu(); //return to main menu
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                } while (staffChoice != 0);
                Console.ReadKey();
            }
        }
        static void MemberMenu()
        {
            Console.WriteLine("enter member first name");
            string? inputFirstName = Console.ReadLine();
            Console.WriteLine("enter member last name");
            string? inputLastName = Console.ReadLine();
            Console.WriteLine("enter 4 digits pin");
            string? inputpin = Console.ReadLine();
            if (memberCollection.CheckMemberAuth(inputFirstName!, inputLastName!, inputpin!))
            {
                Console.WriteLine("\nWelcome to Member Menu");
                int memberChoice;
                do
                {   //display Member menu options
                    Console.WriteLine("\n-------------------------");
                    Console.WriteLine("1. Browse all the movies");
                    Console.WriteLine("2. Display the information about a movie, given the title of the movie");
                    Console.WriteLine("3. Borrow a movie DVD");
                    Console.WriteLine("4. Return a movie DVD");
                    Console.WriteLine("5. List current borrowing movies");
                    Console.WriteLine("6. Display the top 3 movies most borrowed movies");
                    Console.WriteLine("0. Return to main menu");
                    Console.Write("\nEnter your choice ==> ");
                    while (!int.TryParse(Console.ReadLine(), out memberChoice) || (memberChoice != 0 && memberChoice != 1 && memberChoice != 2 && memberChoice != 3 && memberChoice != 4 && memberChoice != 5 && memberChoice != 6))
                    {
                        Console.WriteLine("Invalid input, please enter a valid integer.");
                        Console.Write("Enter your choice ==> ");
                    }
                    switch (memberChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 1 - Browse all the movies"); //display all movies in the system in dictionary order
                            Console.WriteLine("--------------------------");
                            movieCollection.DisplayMovies();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 2 - look up a specific movie's information"); //display a movie details
                            Console.WriteLine("--------------------------");
                            movieCollection.GetMovieDetails();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 3 - Borrow a movie DVD"); //check and borrow a movie DVD
                            Console.WriteLine("--------------------------");

                            Console.Write("Enter movie title (press enter to auto-generate): ");
                            string? movieTitle = Console.ReadLine();
                            if (string.IsNullOrEmpty(movieTitle))
                            {
                                movieTitle = "Python"; // auto-generate an exmaple of a movie title 
                            }
                            memberCollection.MemberBorrowDVD(inputFirstName, inputLastName, movieTitle, movieCollection); //take member's name from login input
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 4 - return a movie DVD"); //check movie then deduct from member's currentBorrowing array
                            Console.WriteLine("--------------------------");
                            Console.Write("Enter movie title (press enter to auto-generate): ");
                            string? returnMovieTitle = Console.ReadLine();
                            if (string.IsNullOrEmpty(returnMovieTitle))
                            {
                                returnMovieTitle = "Python"; // auto-generate an exmaple of a movie title 
                            }

                            if (memberCollection.MemberReturnDVD(inputFirstName, inputLastName, returnMovieTitle) == true) //take member's name from login input
                            {
                                movieCollection.ReturnMovie(returnMovieTitle);
                            }
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 5 - list of current borrowing movie DVD"); //list of current borrwing of a member 
                            Console.WriteLine("--------------------------");
                            memberCollection.GetBorrowedDVDsForMember(inputFirstName!, inputLastName!);
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Selected 6 - Top 3 the most borrowed movies");
                            Console.WriteLine("--------------------------");
                            movieCollection.DisplayTopBorrowedMovies();
                            break;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Returned to MainMenu...");
                            Console.WriteLine("--------------------------");
                            MainMenu();
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                } while (memberChoice != 0);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Member log in failed. Returning to main menu.");
                MainMenu();
            }
        }

        static bool StaffAuth()
        {
            string defaultUserName = "staff"; //default staff username
            string defaultPassword = "today123"; //default today123 password
            Console.Write("Enter staff username >> ");
            string? inputUserName = defaultUserName; //set it auto login for tesing purpose
            Console.Write("Enter password >> ");
            string? inputPassword = defaultPassword;
            while (true)
            {
                if (inputUserName == defaultUserName && inputPassword == defaultPassword)
                {
                    Console.WriteLine("\nLog In successful!");
                    return true;
                }
                else if (inputUserName!.ToLower() == "0" || inputPassword!.ToLower() == "0")
                {
                    Console.WriteLine("Authentication cancelled.");
                    return false;
                }
                else
                {
                    Console.WriteLine("User name or password is incorrect. Input again or Enter '0' to cancel.");
                    Console.Write("Enter staff username >> ");
                    inputUserName = Console.ReadLine();
                    Console.Write("Enter password >> ");
                    inputPassword = Console.ReadLine();
                }
            }
        }

    }

}