using System;
using NLog.Web;
using System.IO;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");
//           string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
//            logger.Info(scrubbedFile);
            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.scrubbed.csv";

            MovieFile movieFile = new MovieFile(movieFilePath);
            
            string choice = "";
            Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                if (choice == "1")
                {
                    Movie movie = new Movie();
                        Console.WriteLine("Enter movie title");
                        movie.title = Console.ReadLine();
                        movie.genres.Add("This is a test");
                        movie.director=("test");
                        TimeSpan t=new TimeSpan(1,1,1);
                        movie.runningTime=(t);
                        movieFile.AddMovie(movie);
                        logger.Info(movie.title+" Created");
                }
                 else if (choice == "2")
                {
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            logger.Info("Program ended");
        }
    }
}
