using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels; 
namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            
            List<Movie> movies = new List<Movie>
            {
                new Movie() {Name="Casablanca" },
                new Movie() {Name="Titanic" },
                new Movie() {Name="Hulk" },
                new Movie() {Name="Independence Day" }

            };
            return View(movies);
        }
        public ActionResult Random()
        {
            
            var movie = new Movie() { Name = "Shrek" };
            var customers = new List<Customer>
            {
                new Customer() {Name="Customer 1" },
                new Customer() {Name="Customer 2" }
            };
        
            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers=customers
            };
           
            return View(viewModel);
            
        }
        public ActionResult Edit(int id )
        {
            return Content("ID: " + id);
        }
        public ActionResult SortBy(int? pageIndex , string sortBy )
        {
            if (!pageIndex.HasValue )
            {
                pageIndex = 1; 
            }
            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "Name";

            }
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }
        [Route("movies/release/{year}/{month}")]
        public ActionResult ByReleaseDate(int year,int month)
        { 
                return Content(year + "/" + month);

            
        }

    }
}