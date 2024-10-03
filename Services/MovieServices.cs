using MoviesApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApplication.Services
{
    internal class MovieServices
    {
        // To Operate The Functionality 
        public static void OperateMovie(List<Movie> movies)
        {
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
                operate = DoTask(movies, selectedMovie, choose);
            }
        }

        // Doing Tasks for Operate Funciton

        public static bool DoTask(List<Movie> movies, Movie selectedMovie, int choose)
        {
            switch (choose)
            {
                case 1:
                    Console.WriteLine(selectedMovie.ToString());
                    return true;
                case 2:
                    Console.WriteLine(RateTheMovie(movies, selectedMovie.MovieId));
                    return true;
                case 3:
                    Console.WriteLine(DeleteOneMovie(movies, selectedMovie.MovieId));
                    return true;
                case 4:
                    Serializer.SaveMovies(movies);
                    Console.WriteLine("Exited Successfully...!");
                    return false;
                default:
                    Console.WriteLine("Invalid Input, Please Select the Correct Option\n");
                    return true;
            }
        }


        // Display Movies ..
        public static void DisplayMovies(List<Movie> movies)
        {
            if (movies.Count == 0)
            {
                Console.WriteLine("No Movies Found");
            }
            else
            {
                int index = 1;
                Console.WriteLine("\n****** All Movie List ******\n");
                foreach (Movie movie in movies)
                    Console.WriteLine($"{index++})\n{movie.ToString()}");
            }
        }


        // To select the Movie
        public static Movie SelectMovie(List<Movie> movies)
        {
            if (movies.Count == 0)
                return null;
            Console.WriteLine("Select The Movie...");
            int i;
            for (i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movies[i].MovieName}");
            }
            int selectMovie = int.Parse(Console.ReadLine());
            return movies[selectMovie - 1];
        }


        // To add Movie
        public static string AddMovie(List<Movie> movies)
        {
            if (movies.Count > 5)
                return "You reached the maximum limit, You cant add new movies";

            Console.WriteLine("Enter The Movie Name :");
            string movieName = Console.ReadLine();
            Console.WriteLine("Enter Genre : ");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter the Rating : ");
            double rating = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Movie Released Year :");
            int year = int.Parse(Console.ReadLine());
            Movie newMovie = new Movie(movieName, genre, rating, year);
            movies.Add(newMovie);
            Serializer.SaveMovies(movies);
            return "\nMovie Added Successfully...\n";
        }

        // To Delet All the Movies in File
        public static string ClearAllMovies(List<Movie> movies)
        {
            movies.Clear();
            Serializer.SaveMovies(movies);
            return "\nAll Movies Deleted/Cleared...!\n";
        }


        // For deletion of one movie
        public static string DeleteOneMovie(List<Movie> movies, string movieId)
        {
            Movie toRemove = movies.Find(movie => movie.MovieId == movieId);
            if (toRemove == null)
                return "Movie Not Found.\n";
            movies.Remove(toRemove);
            Serializer.SaveMovies(movies);
            return "Movie Deleted Successfully...!\n";
        }


        // To rate the movie the following function will be use...
        public static string RateTheMovie(List<Movie> movies, string movieId)
        {
            Movie toRate = movies.Find(movie => movie.MovieId == movieId);

            if (toRate == null)
                return "Movie Not Found.\n";

            toRate.Rating.Add(GetValidRating());
            Serializer.SaveMovies(movies);
            return $"Rating Submitted Successfully...!, New Rating is : {toRate.Rating.Average()}\n";
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
    }
}
