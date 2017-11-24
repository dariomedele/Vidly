using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels; 
namespace Vidly.Controllers
{
    public class MovieController : Controller
    {

        private ApplicationDbContext _context;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()

        {
             
            var movies = _context.Movies.Include(m  => m.Genre).ToList(); 
            
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
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };
                return View("New", viewModel);
            }
            if(movie.Id==0)
            {
                movie.DateAdded = DateTime.Today;

                _context.Movies.Add(movie);

            }
            else
            {

                var editMovie = _context.Movies.Single(m => m.Id == movie.Id);
                editMovie.GenreId = movie.GenreId;
                editMovie.NumberInStock = movie.NumberInStock;
                editMovie.Name = movie.Name;
                editMovie.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges(); 


            return RedirectToAction("Index","Movie");
        }
        public ActionResult New()
        {
           
            var viewModel = new MovieViewModel
            {
                Movie = new Models.Movie(),
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }
        public ActionResult Edit(int id)

        {
            var movie = _context.Movies.Include(e => e.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()

            };

            return View("New",viewModel);
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