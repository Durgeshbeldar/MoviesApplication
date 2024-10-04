using MoviesApplication.Controllers;
using MoviesApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApplication.Presentation
{
    internal class MovieHub
    {
        public static MovieManager movieManager = new MovieManager();
        public static void MoviesApp()
        {
            Console.WriteLine("\n***************** Welcome to Movies Application *****************\n");
            while (true)
            {
                // Menu Implementation...
                Console.WriteLine(
                    $"\nChoose The Options From The Following List : \n\n"
                        + $" 1. Add Movie\n"
                        + $" 2. Display Movies\n"
                        + $" 3. Operate Movies\n"
                        + $" 4. Clear All Movies\n"
                        + $" 5. Exit/Cancel\n"
                );

                int option = int.Parse(Console.ReadLine());
                ExecuteMenuOptions(option);
            }
        }

        public static void ExecuteMenuOptions(int option)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine(AddMovie());
                    break;
                case 2:
                    DisplayMovies();
                    break;
                case 3:
                    OperateMovies();
                    break;
                case 4:
                    ClearAllMovies();
                    break;
                case 5:
                    SaveMovies();
                    Console.WriteLine("Exited Successfully....!");
                    return;
                default:
                    Console.WriteLine("Invalid Input, Please Try Again\n");
                    break;
            }
        }


        // To add the Movie
        public static string AddMovie()
        {
            if (movieManager.IsFull())
                return "\nSorry, Dont Have The Capacity To Add New Movie...!";

            Console.WriteLine("Enter The Movie Name :");
            string movieName = Console.ReadLine();

            Console.WriteLine("Enter Genre : ");
            string genre = Console.ReadLine();

            Console.WriteLine("Enter the Rating : ");
            double rating = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Movie Released Year :");
            int year = int.Parse(Console.ReadLine());

            movieManager.AddMovie(movieName, genre, rating,year);
               return "\nMovie Added Successfully to MovieHub Store...!";
        }


        // Used For Saving the Data
        public static void SaveMovies()
        {
            movieManager.SaveData();
            Console.WriteLine("All Data Saved Successfully...!");
        }


        // To Clear or delete All Movies 
        public static void ClearAllMovies()
        {
            movieManager.DeleteMovies();
            Console.WriteLine("All Movies Deleted Successfully");
        }


        // To Display the Movies ...
        public static void DisplayMovies()
        {
            List<Movie>movies = movieManager.GetMovies();

            if (movies.Count == 0)
            {
                Console.WriteLine("No Movies Found");
                return;
            }     
            int index = 1;

            Console.WriteLine("\n****** All Movie List ******\n");
            foreach (Movie movie in movies)
            Console.WriteLine($"{index++})\n{movie.ToString()}");
        }

        // Operate Individual Movie

        public static void OperateMovies()
        {
            List<Movie> movies = movieManager.GetMovies();
            Movie selectedMovie = SelectMovie(movies);
            while (true)
            {
                Console.WriteLine(
                    $"\nChoose The Options From The Following List : \n\n"
                        + $" 1. Show Movie Details \n"
                        + $" 2. Rate The Movie\n"
                        + $" 3. Delete The Movie\n"
                        + $" 4. Exit/Cancel\n"
                );
                int option = int.Parse(Console.ReadLine());
                ExecuteMovieOptions(selectedMovie, option);
            }
        }

        // You Can Perform Edit, Read & Update Operations From this user-interface 
        public static void ExecuteMovieOptions(Movie selectedMovie, int option)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine(selectedMovie.ToString());
                    break;
                case 2:
                    RateTheMovie(selectedMovie.MovieId);
                    break;
                case 3:
                    DeleteTheMovie(selectedMovie.MovieId);
                    break;
                case 4:
                    Console.WriteLine("Exited Successfully...!");
                    return;
                default:
                    Console.WriteLine("Invalid Input, Please Select the Correct Option\n");
                    break;
            }
        }


        // For Deletion of Movie
        public static void DeleteTheMovie(string movieId)
        {
            movieManager.DeleteMovie(movieId);
            Console.WriteLine("Movie Deleted Successfully...!\n");
        }

        // To Rate the movie the following function will be use...
        public static void RateTheMovie(string movieId)
        {
            double movieRating = movieManager.RateMovie(movieId, GetValidRating());
            Console.WriteLine($"Rating Submitted Successfully...!, New Rating is : {movieRating}\n");
        }


        // to take valid rating from user ...
        public static double GetValidRating()
        {
            Console.WriteLine("Enter Your Rating For this Movie (1 to 5) : ");
            double userRating = double.Parse(Console.ReadLine());
            if (userRating >= 1 && userRating <= 5)
                return userRating;
            Console.WriteLine("Please Enter the Valid Rating**");
            return GetValidRating();
        }

        // To select the Movie
        public static Movie SelectMovie(List<Movie> movies)
        {
            if (movies.Count == 0)
                return null;
            int index;
            for (index = 0; index < movies.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {movies[index].MovieName}");
            }
            int selectMovie = GetUserSelection(movies.Count); // Here We can handle it using exception handling as well
            return movies[selectMovie - 1];
        }
        

        // Method to get user selection
        public  static int GetUserSelection(int max)
        { 
            Console.WriteLine("Select The Movie From The Above List :");
            while (true)
            {
                int select = int.Parse(Console.ReadLine());
                if( select >= 1 && select <= max)
                    return select;
                Console.WriteLine($"Please Enter The Number Between 1 to {max}**");
            }
        }


    }
}
