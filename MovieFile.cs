using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{
    public class movieFile
    {
        public string filePath{get; set;}
        public List<Movie> Movies { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        public movieFile(string movieFilePath){
            filePath = movieFilePath;
            Movies = new List<Movie>();
            using(StreamReader sr = new StreamReader(filePath)){
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Movie movie= new Movie();
                    string line = sr.ReadLine();
                    int idx = line.IndexOf('"');
                    if (idx == -1){
                        String[] movieDetails = line.Split(',');
                        movie.mediaId = UInt64.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                        movie.director = movieDetails[3];
                        string time = movie.runningTime.ToString();
                        time = movieDetails[4];
                    }
                    else{
                         movie.mediaId = UInt64.Parse(line.Substring(0, idx - 1));
                         line = line.Substring(idx + 1);
                         idx = line.IndexOf('"');
                        // movie.title = line.Substring(0, idx);
                         line = line.Substring(idx + 2);
                         movie.genres = line.Split('|').ToList();
                        // movie.director = line.Substring(0, idx);
                         string time = movie.runningTime.ToString();
                        // time = line.Substring(0, idx);
                    }
                    Movies.Add(movie);
                }
            }
            
        }
         public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddMovie(Movie movie){
            try
            {
                movie.mediaId=Movies.Max(m => m.mediaId) + 1;
                using(StreamWriter sw = new StreamWriter(filePath, true)){
                    sw.WriteLine($"{movie.mediaId},{movie.title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                }
                Movies.Add(movie);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}