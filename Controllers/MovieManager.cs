using MoviesApplication.Models;
using MoviesApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApplication.Controllers
{
    internal class MovieManager
    {
        List<Movie> movies;
        const int CAPACITY = 5; // Magic Number concept :)
        // Construtor For Movie Manager
        public MovieManager() {
            movies = Serializer.LoadMovies();
        }



        // To Get Movies List
        public List<Movie> GetMovies()
        { 
            return movies;
        }


        // To Add Movies 
        public void AddMovie(string movieName, string genre, double rating, int year) 
        { 
            movies.Add(new Movie(movieName, genre, rating, year));
            Serializer.SaveMovies(movies);
        }


        // To Rate the Movie 
        public double RateMovie(string movieId, double userRating)
        {
            Movie toRate = movies.Find(movie => movie.MovieId == movieId);
            toRate.Rating.Add(userRating);
            Serializer.SaveMovies(movies);
            return toRate.Rating.Average();
        }


        // Save Data 
        public void SaveData()
        {
            Serializer.SaveMovies(movies);
        }


        // Checking The Storage Capacity... For Now We Just Storing 5 Movies 
        public bool IsFull()
        {
            if(movies.Count >= CAPACITY) // Hanling the Logic of having 5 Movies in the Movie Hub
                return true;
                return false;
        }

        // Delete Movies and One Movie Section
        public void DeleteMovies()
        {
            movies.Clear();
            Serializer.SaveMovies(movies);
        }

        public void DeleteMovie(string movieId) 
        {
            Movie toRemove = movies.Find(movie => movie.MovieId == movieId);
            movies.Remove(toRemove);
            Serializer.SaveMovies(movies); 
        }
    }
}
