using AppForMovies.API.Models;
using AppForMovies.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForMovies.UT.MoviesController_test
{
    public static class UtilitiesForMovies
    {


        public static void InitializeDbMoviesForTests(AppDbContext db)
        {

            db.Movies.AddRange(GetMovies(0, 4));
            //genres with id=1 and id=2 are already added to db because it is related to the movies
            db.Genres.AddRange(GetGenres(2, 2));
            db.SaveChanges();

            db.SaveChanges();
        }

        public static void ReInitializeDbMoviesForTests(AppDbContext db)
        {
            db.Movies.RemoveRange(db.Movies);
            db.Genres.RemoveRange(db.Genres);
            db.SaveChanges();
        }

        /// <summary>
        /// Method <c>GetMovies</c> retrieves as much instances of Movies as stated in 
        /// <c>numOfMovies</c> starting at <c>index</c>.
        /// </summary>
        public static IList<Movie> GetMovies(int index, int numOfMovies)
        {
            Genre genre = GetGenres(0, 1).First();
            Genre genre2 = GetGenres(1, 1).First();
            var allMovies = new List<Movie>
            {
                new Movie { MovieID = 1, Title = "The lord of the rings", Genre= genre, ReleaseDate = new DateTime(2011, 10, 20), PriceForPurchase = 10, QuantityForPurchase = 5, PriceForRenting=1, QuantityForRenting=1 },
                new Movie { MovieID = 2, Title = "The mechanic orange", Genre = genre, ReleaseDate = new DateTime(1988, 02, 23), PriceForPurchase = 15, QuantityForPurchase = 10, PriceForRenting=2, QuantityForRenting=5 },
                new Movie { MovieID = 3, Title = "The flying castle", Genre = genre2, ReleaseDate = new DateTime(2007, 04, 04), PriceForPurchase = 20, QuantityForPurchase = 15, PriceForRenting=3, QuantityForRenting=10 },
                new Movie { MovieID = 4, Title = "The man in the high castle", Genre = genre, ReleaseDate = new DateTime(2015, 01, 01), PriceForPurchase = 10, QuantityForPurchase = 0, PriceForRenting=4, QuantityForRenting=15 }

            };

            return allMovies.GetRange(index, numOfMovies);
        }

        /// <summary>
        /// Method <c>GetGenres</c> retrieves as much instances of Genres as stated in 
        /// <c>numOfGenes</c> starting at <c>index</c>.
        /// </summary>
        public static IList<Genre> GetGenres(int index, int numOfGenes)
        {
            var allGenres = new List<Genre>
                {
                    new Genre { GenreID=1, Name = "Sci-fi" } ,
                    new Genre { GenreID=2, Name = "Drama" },
                    new Genre { GenreID=3, Name = "Sitcom" },
                    new Genre { GenreID=4, Name = "Comedy" }
                };
            return allGenres.GetRange(index, numOfGenes);
        }

    }
}