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
        
        public static void MoviesApp()
        {
            Console.WriteLine("\n***************** Welcome to Movies Application *****************\n");
            bool operate = true;
            while (operate)
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

                int choose = int.Parse(Console.ReadLine());
                operate = DoTask(choose);
            }
        }

        public static bool DoTask(int choose)
        {
            switch (choose)
            {
                case 1:
                    AddMovie();
                    return true;
                case 2:
                    DisplayMovies();
                    return true;
                case 3:
                    OperateMovies();
                    return true;
                case 4:
                    ClearAllMovies();
                    return true;
                case 5:
                    SaveMovies();
                    Console.WriteLine("Exited Successfully....!");
                    return false;
                default:
                    Console.WriteLine("Invalid Input, Please Try Again\n");
                    return true;
            }
        }


        // To add the Movie
        public static void AddMovie()
        { 
            Console.WriteLine("Enter The Movie Name :");
            string movieName = Console.ReadLine();

            Console.WriteLine("Enter Genre : ");
            string genre = Console.ReadLine();

            Console.WriteLine("Enter the Rating : ");
            double rating = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Movie Released Year :");
            int year = int.Parse(Console.ReadLine());

            MovieManager movieManager = new MovieManager();
            movieManager.AddMovie(movieName, genre, rating,year);    
            Console.WriteLine("\nMovie Added Successfully...\n");
        }

        public static void SaveMovies()
        {
            MovieManager movieManager = new MovieManager();
            movieManager.SaveData();
            Console.WriteLine("All Data Saved Successfully...!");
        }



        // To Clear or delete All Movies 

        public static void ClearAllMovies()
        {
            MovieManager movieManager = new MovieManager();
            movieManager.DeleteMovies();
            Console.WriteLine("All Movies Deleted Successfully");
        }

        // To Display the Movies ...

        public static void DisplayMovies()
        {
            MovieManager movieManager = new MovieManager();
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
            MovieManager movieManager = new MovieManager();
            List<Movie> movies = movieManager.GetMovies();
            Movie selectedMovie = SelectMovie(movies);
            bool operate = true;
            while (operate)
            {
                Console.WriteLine(
                    $"\nChoose The Options From The Following List : \n\n"
                        + $" 1. Show Movie Details \n"
                        + $" 2. Rate The Movie\n"
                        + $" 3. Delete The Movie\n"
                        + $" 4. Exit/Cancel\n"
                );
                int choose = int.Parse(Console.ReadLine());
                operate = DoTask1(selectedMovie, choose);
            }
        }

        public static bool DoTask1(Movie selectedMovie, int choose)
        {
            switch (choose)
            {
                case 1:
                    Console.WriteLine(selectedMovie.ToString());
                    return true;
                case 2:
                    RateTheMovie(selectedMovie.MovieId);
                    return true;
                case 3:
                    DeleteOneMovie(selectedMovie.MovieId);
                    return true;
                case 4:
                    Console.WriteLine("Exited Successfully...!");
                    return false;
                default:
                    Console.WriteLine("Invalid Input, Please Select the Correct Option\n");
                    return true;
            }
        }

        // For deletion of one movie
        public static void DeleteOneMovie(string movieId)
        {
            MovieManager movieManager = new MovieManager();
            movieManager.DeleteMovie(movieId);
            Console.WriteLine("Movie Deleted Successfully...!\n");
        }

        // To rate the movie the following function will be use...
        public static void RateTheMovie(string movieId)
        {
            MovieManager movieManager = new MovieManager();
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
            Console.WriteLine("Select The Movie From The List :");
            int i;
            for (i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movies[i].MovieName}");
            }
            int selectMovie = int.Parse(Console.ReadLine());
            return movies[selectMovie - 1];
        }
    }
}
