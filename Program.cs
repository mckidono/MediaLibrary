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
//           string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
//            logger.Info(scrubbedFile);
            string movieFilePath = Directory.GetCurrentDirectory() + "//movies.scrubbed.csv";

            movieFile movieFile = new movieFile(movieFilePath);
            
            string choice = "";
            Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                System.Console.WriteLine("3) Search for a movie");
                Console.WriteLine("Enter to quit");
                System.Console.Write(">");
                // input selection
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                if (choice == "1")
                {
                    Movie movie = new Movie();
                        Console.WriteLine("Enter movie title");
                        System.Console.Write(">");
                        movie.title = Console.ReadLine();
                        string input;
                        do
                        {
                            Console.WriteLine("Enter genre (or done to quit)");
                            System.Console.Write(">");
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
                        System.Console.Write(">");
                        movie.director=Console.ReadLine();
                        System.Console.WriteLine("Enter the duration Hour:Minute:Second");
                        System.Console.Write(">");
                        movie.runningTime = TimeSpan.Parse(Console.ReadLine());
                        
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


                else if (choice == "3"){
                    Console.WriteLine("Enter your search term:");
                    string search = Console.ReadLine();

                    
                        
                    var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
                    // LINQ - Count aggregation method
                    Console.WriteLine($"There are {titles.Count()} movies with \"Shark\" in the title:");
                    foreach(string t in titles)
                    {
                        Console.WriteLine($"  {t}");
                    }


            logger.Info("Program ended");
        }
    }
    }
    }
