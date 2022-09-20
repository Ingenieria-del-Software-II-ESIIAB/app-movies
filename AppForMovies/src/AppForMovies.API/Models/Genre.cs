using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppForMovies.Models
{
    public class Genre
    {
        [Key]
        public virtual int GenreID
        {
            get;
            set;
        }

        [Required]
        [Remote(action: "VerifyName", controller: "Genres", AdditionalFields =("GenreID"))]
        public virtual String Name
        {
            get;
            set;
        }

        public virtual ICollection<Movie> Movies
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is Genre genre &&
                   GenreID == genre.GenreID &&
                   Name == genre.Name;
        }
    }
}
