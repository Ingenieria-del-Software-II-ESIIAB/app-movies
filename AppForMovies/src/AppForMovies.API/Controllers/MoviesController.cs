using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForMovies.API.Data;
using AppForMovies.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppForMovies.API.Models.DTOs;
using System.Net;

namespace AppForMovies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public MoviesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        [ProducesResponseType(typeof(List<MovieForPurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesForRenting(
            [FromQuery]string movieTitle, string movieGenreSelected)
        {
            
            var Movies = _context.Movie
                .Include(movie => movie.Genre) //join table Movie and table Genre
                .Where(movie => movie.QuantityForPurchase > 0 // where clause
                && (movie.Title.Contains(movieTitle) || movieTitle == null)
                && (movie.Genre.Name.Contains(movieGenreSelected) || movieGenreSelected == null))
                .Select(movie=>new MovieForPurchaseDTO(movie))
                .OrderBy(movie=>movie.Title);


            return Ok(Movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieForPurchaseDTO>> GetMovieForPurchase(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return new MovieForPurchaseDTO(movie);
        }

        //// PUT: api/Movies/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovie(int id, Movie movie)
        //{
        //    if (id != movie.MovieID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Movies
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        //{
        //    _context.Movie.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = movie.MovieID }, movie);
        //}

        //// DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovie(int id)
        //{
        //    var movie = await _context.Movie.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movie.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieID == id);
        }
    }
}
