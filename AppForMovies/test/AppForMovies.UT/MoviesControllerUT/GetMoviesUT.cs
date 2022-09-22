using AppForMovies.API.Controllers;
using AppForMovies.API.Models;
using AppForMovies.API.Models.DTOs;
using AppForMovies.UT.MoviesController_test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForMovies.UT.MoviesControllerUT
{
    public class GetMoviesUT
    {
        private DbContextOptions<AppDbContext> _contextOptions;
        private AppDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext purchaseContext;

        public GetMoviesUT()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new AppDbContext(_contextOptions);
            context.Database.EnsureCreated();

            // Seed the InMemory database with test data.
            UtilitiesForMovies.InitializeDbMoviesForTests(context);

        }
        public static IEnumerable<object[]> TestCasesForGetMoviesForPurchase()
        {
            var allTests = new List<object[]>
            {
                new object[] { UtilitiesForMovies.GetMovies(0,3).Select(movie => new MovieForPurchaseDTO(movie)), null, null },
                new object[] { UtilitiesForMovies.GetMovies(0,1).Select(movie => new MovieForPurchaseDTO(movie)), "lord", null},
                new object[] { UtilitiesForMovies.GetMovies(2,1).Select(movie => new MovieForPurchaseDTO(movie)),  null, "Drama"},
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesForGetMoviesForPurchase))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMoviesForPurchaseUT(IEnumerable<MovieForPurchaseDTO> expectedMovies,  string filterTitle, string filterGenre)
        {
            using (context)
            {

                // Arrange
                var controller = new MoviesController(context);
                controller.ControllerContext.HttpContext = purchaseContext;


                // Act
                var result = controller.GetMoviesForPurchase(filterTitle, filterGenre);

                //Assert
          //      var action_result = Assert.IsType<ActionResult>(result); // Check the controller returns a view
             //   IEnumerable<MovieForPurchaseDTO> movies = result.Result as IEnumerable<MovieForPurchaseDTO>;

                // Check that both collections (expected and result returned) have the same elements with the same name
                // You must implement Equals in Movies, otherwise Assert will fail
               // Assert.Equal(expectedMovies, movies);

            }
        }


    }
}
