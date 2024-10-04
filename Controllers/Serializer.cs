using MoviesApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApplication.Controllers
{
    internal class Serializer
    {
        // File Path where we are storing the data...
        static string filePath = ConfigurationManager.AppSettings["myFilePath"];


        // To Load Movies
        public static List<Movie> LoadMovies()
        {
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                return JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(filePath));
            }
            return new List<Movie>(); //its Return an empty list if file does not exist
        }


        // To save changes in database...
        public static void SaveMovies(List<Movie> movies)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(movies, Formatting.Indented));
        }
    }
}
