using MoviesApplication.Models;
using MoviesApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "\n***************** Welcome to Movies Application *****************\n"
            );

            List<Movie> movies = Serializer.LoadMovies();

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
                switch (choose)
                {
                    case 1:
                        Console.WriteLine(MovieServices.AddMovie(movies));
                        break;
                    case 2:
                        MovieServices.DisplayMovies(movies);
                        break;
                    case 3:
                        MovieServices.OperateMovie(movies);
                        break;
                    case 4:
                        Console.WriteLine(MovieServices.ClearAllMovies(movies));
                        break;
                    case 5:
                        Serializer.SaveMovies(movies);
                        operate = false;
                        Console.WriteLine("Exited Successfully....!");
                        break;
                    default:
                        Console.WriteLine("Invalid Input, Please Try Again\n");
                        break;
                }
            }
        }
    }
}
