using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppForMovies.API.Models.DTOs
{
    public class MovieForPurchaseDTO
    {
        public MovieForPurchaseDTO(Movie movie)
        { 
            MovieID = movie.MovieID;
            Title = movie.Title;
            PriceForPurchase = movie.PriceForPurchase;
            QuantityForPurchase = movie.QuantityForPurchase;
            Genre = movie.Genre.Name;
            ReleaseDate = movie.ReleaseDate;
        }
        public virtual int MovieID
        {
            get;
            set;
        }

        public virtual String Title
        {
            get;
            set;
        }


        public virtual int PriceForPurchase
        {
            get;
            set;
        }

        public virtual DateTime ReleaseDate
        {
            get;
            set;
        }

        [Required]
        public virtual String Genre
        {
            get;
            set;
        }

        public virtual int QuantityForPurchase
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is MovieForPurchaseDTO movie &&
                   MovieID == movie.MovieID &&
                   PriceForPurchase == movie.PriceForPurchase &&
                   ReleaseDate == movie.ReleaseDate &&
                   Genre== movie.Genre &&
                   QuantityForPurchase == movie.QuantityForPurchase;

        }

    }
}
