using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppForMovies.Models
{
    public class Movie
    {
        [Key]
        public virtual int MovieID
        {
            get;
            set;
        }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]        
        public virtual String Title
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Currency)]     
        [Range(1,100, ErrorMessage ="Minimum is 1 and 100, respectively")]
        [Display(Name = "Price For Renting")]
        public virtual int PriceForRenting
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Minimum price is 1 ")]
        [Display(Name = "Price For Purchase")]
        public virtual int PriceForPurchase
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Release Date")]        
        public virtual DateTime ReleaseDate
        {
            get;
            set;
        }

        [Required]
        public virtual Genre Genre
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Quantity For Purchase")]
        [Range(1,int.MaxValue , ErrorMessage = "Minimum quantity for Purchase is 1")]        
        public virtual int QuantityForPurchase
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Quantity For Renting")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum quantity for renting is 1")]
        public virtual int QuantityForRenting
        {
            get;
            set;
        }

        public virtual IList<PurchaseItem> PurchasedMovies
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is Movie movie &&
                   MovieID == movie.MovieID &&
                   Title == movie.Title &&
                   PriceForRenting == movie.PriceForRenting &&
                   PriceForPurchase == movie.PriceForPurchase &&
                   ReleaseDate == movie.ReleaseDate &&
                   EqualityComparer<Genre>.Default.Equals(Genre, movie.Genre) &&
                   QuantityForPurchase == movie.QuantityForPurchase &&
                   QuantityForRenting == movie.QuantityForRenting;
        }
    }
}
