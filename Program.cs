using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            MovieFile movieFile = new MovieFile(scrubbedFile);

            Console.ForegroundColor = ConsoleColor.Green;

            // LINQ - Where filter operator & Contains quantifier operator
            var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
            // LINQ - Count aggregation method
            Console.WriteLine($"There are {Movies.Count()} movies from 1990");

            // LINQ - Any quantifier operator & Contains quantifier operator
            var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
            Console.WriteLine($"Any movies from 1921? {validate}");

            Console.ForegroundColor = ConsoleColor.White;

            logger.Info("Program ended");
        }
    }
}
