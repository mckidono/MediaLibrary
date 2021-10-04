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
                        string input;
                        do
                        {
                            Console.WriteLine("Enter genre (or done to quit)");
                            
                            input = Console.ReadLine();
                            
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        System.Console.WriteLine("Enter Director name");
                        movie.director=Console.ReadLine();
                        System.Console.Write("Enter the duration\nHour/s:");
                        int hour=Console.Read();
                        
                        System.Console.Write("Minutes:");
                        int minutes= Console.Read();

                        TimeSpan t=new TimeSpan(hour,minutes,00);
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
