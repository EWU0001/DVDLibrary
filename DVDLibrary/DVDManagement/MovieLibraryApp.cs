using System;

namespace DVDLibrary
{
    public class MovieLibraryApp
    {
        static Member member = new();
        static Movie movie = new();
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
                        Console.WriteLine("Staff Menu selected");
                        StaffMenu();
                        break;
                    case 2:
                        Console.WriteLine("Member Menu selected");
                        MemberMenu();
                        break;
                    case 0:
                        Console.WriteLine("Exiting program...");
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
                Console.WriteLine("------------------------");

                int staffChoice;
                do
                {
                    Console.WriteLine("1. Add DVDs of a new movie to the system");
                    Console.WriteLine("2. Add DVDs of an existing movie to the system");
                    Console.WriteLine("3. Remove DVDs of a movie from the system");
                    Console.WriteLine("4. Register a new member with the system");
                    Console.WriteLine("5. Remove a registered member from the system");
                    Console.WriteLine("6. Find a member’s contact phone number, given the member’s full name");
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
                            Console.WriteLine("Selected 1 - add movies to the system");
                            // Populate the movie library with auto-generate movies data
                            List<Movie> movies = PopulateData.PopulateMovies();
                            foreach (Movie movie in movies)
                            {
                                movieCollection.AddMovie(movie);
                            }
                            Console.WriteLine($"Added {movies.Count} movies to the system.");
                            break;

                        case 2:
                            Console.WriteLine("Selected 2 - add DVDs to existing movies");
                            movieCollection.AddDVD();
                            break;
                        case 3:
                            Console.WriteLine("Selected 3 - remove DVDs from system");
                            movieCollection.RemoveDVD("", 0);
                            break;
                        case 4:
                            Console.WriteLine("Selected 4 - register new members");
                            break;
                            memberCollection.AddMember(); //manually input member details
                        case 5:
                            Console.WriteLine("Selected 5 - remove members");
                            memberCollection.Remove("", "");
                            break;
                        case 6:
                            Console.WriteLine("Selected 6 - Find member's contact number");
                            memberCollection.GetMemberNumber("", "");
                            break;
                        case 7:
                            Console.WriteLine("Selected 7 - display members renting a movie");
                            memberCollection.GetListOfBorrowers();
                            break;
                        case 0:
                            Console.WriteLine("Exiting program...");
                            MainMenu();
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                    }
                } while (staffChoice != 0);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Returning to main menu....");
                MainMenu();
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
                Console.WriteLine("-------------------------");

                int memberChoice;

                do
                {
                    Console.WriteLine("1. Browse all the movies");
                    Console.WriteLine("2. Display the information about a movie, given the title of the movie");
                    Console.WriteLine("3. Borrow a movie DVD");
                    Console.WriteLine("4. Return a movie DVD");
                    Console.WriteLine("5. List current borrowing movies");
                    Console.WriteLine("6. Display the top 3 movies rented by the member");
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
                            Console.WriteLine("Selected 1 - Browse all the movies");
                            break;
                        case 2:
                            Console.WriteLine("Selected 2 - look up a specific movie's information");
                            break;
                        case 3:
                            Console.WriteLine("Selected 3 - Borrow a movie DVD");
                            //call: 
                            //memberCollection.CheckMemberAuth("","","");
                            //if(MovieCollection.BorrowMovie())
                            //{
                            //    MemberCollection.MemberBorrowDVD();
                            //}
                            break;
                        case 4:
                            Console.WriteLine("Selected 4 - return a movie DVD");
                            //call: 
                            //memberCollection.CheckMemberAuth("","","");
                            //if(MovieCollection.ReturnMovie())
                            //{
                            //    MemberCollection.MemberReturnDVD();
                            //}
                            break;
                        case 5:
                            Console.WriteLine("Selected 5 - list of current borrowing movie DVD");
                            break;
                        case 6:
                            Console.WriteLine("Selected 6 - Top 3 most borrowed movies");
                            break;
                        case 0:
                            Console.WriteLine("Exiting program...");
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
            string defaultUserName = "staff";
            string defaultPassword = "today123";
            Console.Write("Enter staff username >> ");
            string? inputUserName = Console.ReadLine();
            Console.Write("Enter password >> ");
            string? inputPassword = Console.ReadLine();
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