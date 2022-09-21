﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForMovies.API.Models;
using AppForMovies.API.Models.DTOs;
using System.Net;

namespace AppForMovies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<MovieForPurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MovieForPurchaseDTO>>> GetMoviesForPurchase(
            [FromQuery] string? title, [FromQuery] string? genre)
        {
            var movies = _context.Movies
                 .Where(m => m.QuantityForPurchase > 1 
                    && (title == null|| m.Title.Contains(title))
                    && (genre == null|| m.Genre.Name.Contains(genre)))
                .Include(movie => movie.Genre) //join table Movie and table Genre
                .Select(movie => new MovieForPurchaseDTO(movie))                
                .ToListAsync();

            return await movies;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
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
        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = movie.MovieID }, movie);
        //}

        //// DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovie(int id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieID == id);
        }
    }
}
