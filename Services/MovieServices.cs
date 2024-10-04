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
