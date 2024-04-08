using EntertainLog.Models;
using EntertainLog.Models.Repos;
using EntertainLog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;


namespace EntertainLog.Controllers
{
    /// <summary>
    ///  Created by: Danielle Creary-Thomas
    ///  Controller for EntertainLog App, controls the route requests and view displays    
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// reference to the EntertainLog Repository
        /// </summary>
        private IEntertainLogRepo _entertainLogRepo;
        /// <summary>
        /// The current user of the App
        /// </summary>
        private User _CurrUser;

        /// <summary>
        /// Initializes the home controller with the EntertainLogRepo getting constructor Injected
        /// </summary>
        /// <param name="entertainLogRepo"></param>
        public HomeController(IEntertainLogRepo entertainLogRepo) 
        {
            _entertainLogRepo = entertainLogRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            user.UserID = _entertainLogRepo.Users.Count<User>() + 1;
            _entertainLogRepo.AddUser(user);
            return Redirect("Login");
        }


        //Dashboard
        /// <summary>
        /// Created by: Danielle Creary-Thomas
        /// Displays the Dashboard with the Queues & Faves of the Current User
        /// </summary>
        /// <returns> the Dashboard with the current users Queues & Faves in the DashboardViewModel</returns>
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View(new DashboardViewModel
            {
                CurrUser = _CurrUser,

                BooksQueue = _entertainLogRepo.Books.Where(b => b.Queued == true).Take(10).ToList(),
                MoviesQueue = _entertainLogRepo.Movies.Where(m => m.Queued == true).Take(10).ToList(),
                TVShowsQueue = _entertainLogRepo.TVShows.Where(t => t.Queued == true).Take(10).ToList(),
                MusicsQueue = _entertainLogRepo.Musics.Where(m => m.Queued == true).Take(10).ToList(),

                BooksFaves = _entertainLogRepo.Books.Where(b => b.Favourited == true).Take(10).ToList(),
                MoviesFaves = _entertainLogRepo.Movies.Where(m => m.Favourited == true).Take(10).ToList(),
                TVShowsFaves = _entertainLogRepo.TVShows.Where(t => t.Favourited == true).Take(10).ToList(),
                MusicsFaves = _entertainLogRepo.Musics.Where(m => m.Favourited == true).Take(10).ToList(),

            });
        }
        //Login
        [HttpGet]
        public IActionResult Login() { return View(); }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            User user = _entertainLogRepo.GetUserByNameAsync(loginViewModel.Username);
            if(user.Password == loginViewModel.Password && user.UserName == loginViewModel.Username)
            {
                _CurrUser = user;
                return Dashboard();
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
            
            

        }

        //Account
        [HttpGet]
        public IActionResult Account()
        {
            return View(new AccountViewModel
            {
                CurrUser = _CurrUser,

                BooksFaves = _entertainLogRepo.Books.Where(b => b.Favourited == true).Take(3).ToList(),
                MoviesFaves = _entertainLogRepo.Movies.Where(b => b.Favourited == true).Take(3).ToList(),
                TVShowsFaves = _entertainLogRepo.TVShows.Where(b => b.Favourited == true).Take(3).ToList(),
                MusicsFaves = _entertainLogRepo.Musics.Where(m => m.Favourited == true).Take(3).ToList(),
            });
        }

        //Books
        /// <summary>
        /// Displays the Book page with the current User's Books
        /// </summary>
        /// <returns> Current User's Books in a BookViewModel</returns>
        [HttpGet]
        public IActionResult Book()
        {
            
            return View(new BookViewModel
            {
                Books = _entertainLogRepo.Books,
                CurrUser = _CurrUser
            });
        }

        //Add a new Book
        /// <summary>
        /// Adds a new Book and Displays the Book Page
        /// </summary>
        /// <param name="bookModel"> A BookViewModel with the a NewBook Book created</param>
        /// <returns>Current User's Books in a BookViewModel</returns>
        [HttpPost]
        public IActionResult Book(BookViewModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddBook(bookModel.NewBook);

                ModelState.Clear();
            }
            return View(new BookViewModel
            {
                Books = _entertainLogRepo.Books,
                CurrUser = bookModel.CurrUser
            });
        }

        /// <summary>
        /// Displays the Edit Book Page for a Book with the matching ID
        /// </summary>
        /// <param name="BookID">BookID for clicked Book</param>
        /// <returns>Current Book to be Edited in a BookViewModel</returns>
        [HttpGet]
        public IActionResult EditBook(long BookID) 
        {
            return View(new BookViewModel
            {
                CurrBook = _entertainLogRepo.GetBookByIDAsync(BookID).Result,
                CurrUser = _CurrUser

            });
        }

        /// <summary>
        /// Updates the Current Book from the Book View Model
        /// </summary>
        /// <param name="bookModel">A BookViewModel with the Current Book altered</param>
        /// <returns>The Main Book Page</returns>
        [HttpPost]
        public IActionResult EditBook(BookViewModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.UpdateBook(bookModel.CurrBook);
                ModelState.Clear();
            }
            return RedirectToAction("Book", bookModel);
        }

        /// <summary>
        /// Deletes the Book with the matching ID and redirects back to the book page
        /// </summary>
        /// <param name="BookID">BookID of the book to delete</param>
        /// <returns>The book page</returns>
        [HttpPost]
        public IActionResult DeleteBook(long BookID)
        {
            _entertainLogRepo.DeleteBook(_entertainLogRepo.GetBookByIDAsync(BookID).Result);
            return RedirectToAction("Book", new BookViewModel
            {
                Books = _entertainLogRepo.Books,
                CurrUser = _CurrUser
            });

        }

        //Music
        /// <summary>
        /// Displays the Music page with the current User's Musics
        /// </summary>
        /// <returns> Current User's Musics in a MusicViewModel</returns>
        [HttpGet]
        public IActionResult Music()
        {

            return View(new MusicViewModel
            {
                Musics = _entertainLogRepo.Musics,
                CurrUser = _CurrUser
            });
        }

        /// <summary>
        /// Adds a new Music and Displays the Music Page
        /// </summary>
        /// <param name="musicModel"> A MusicViewModel with the a NewMusic Music created</param>
        /// <returns>Current User's Musics in a MusicViewModel</returns>
        [HttpPost]
        public IActionResult Music(MusicViewModel musicModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddMusic(musicModel.NewMusic);

                ModelState.Clear();
            }
            return View(new MusicViewModel
            {
                Musics = _entertainLogRepo.Musics,
                CurrUser = musicModel.CurrUser
            });
        }

        /// <summary>
        /// Displays the Edit Music Page for a Music with the matching ID
        /// </summary>
        /// <param name="MusicID">MusicID for clicked Music</param>
        /// <returns>Current Music to be Edited in a MusicViewModel</returns>
        [HttpGet]
        public IActionResult EditMusic(long MusicID)
        {
            return View(new MusicViewModel
            {
                CurrMusic = _entertainLogRepo.GetMusicByIDAsync(MusicID).Result,
                CurrUser = _CurrUser

            });
        }

        /// <summary>
        /// Updates the Current Music from the Music View Model
        /// </summary>
        /// <param name="musicModel">A MusicViewModel with the Current Music altered</param>
        /// <returns>The Main Music Page</returns>
        [HttpPost]
        public IActionResult EditMusic(MusicViewModel musicModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.UpdateMusic(musicModel.CurrMusic);
                ModelState.Clear();
            }
            return RedirectToAction("Music", musicModel);
        }
        
        /// <summary>
        /// Deletes the Music with the matching ID and redirects back to the Music page
        /// </summary>
        /// <param name="MusicID">MusicID of the Music to delete</param>
        /// <returns>The Music page</returns>
        [HttpPost]
        public IActionResult DeleteMusic(long MusicID)
        {
            _entertainLogRepo.DeleteMusic(_entertainLogRepo.GetMusicByIDAsync(MusicID).Result);
            return RedirectToAction("Music", new MusicViewModel
            {
                Musics = _entertainLogRepo.Musics,
                CurrUser = _CurrUser
            });

        }


        //TVShow
        /// <summary>
        /// Displays the TV Show page with the current User's TVShows
        /// </summary>
        /// <returns> Current User's TVShows in a TVShowViewModel</returns>
        [HttpGet]
        public IActionResult TVShow()
        {

            return View(new TVShowViewModel
            {
                TVShows = _entertainLogRepo.TVShows,
                CurrUser = _CurrUser
            });
        }

        /// <summary>
        /// Adds a new TV Show and Displays the TV Show Page
        /// </summary>
        /// <param name="tvshowModel"> A TVShowViewModel with the a NewTVShow TVShow created</param>
        /// <returns>Current User's TVShows in a TVShowViewModel</returns>
        [HttpPost]
        public IActionResult TVShow(TVShowViewModel tvshowModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddTVShow(tvshowModel.NewTVShow);

                ModelState.Clear();
            }
            return View(new TVShowViewModel
            {
                TVShows = _entertainLogRepo.TVShows,
                CurrUser = tvshowModel.CurrUser
            });
        }

        /// <summary>
        /// Displays the Edit TV Show Page for a TV Show with the matching ID
        /// </summary>
        /// <param name="TVShowID">TVShowID for clicked TVShow</param>
        /// <returns>Current TV Show to be Edited in a TVShowViewModel</returns>
        [HttpGet]
        public IActionResult EditTVShow(long TVShowID)
        {
            return View(new TVShowViewModel
            {
                CurrTVShow = _entertainLogRepo.GetTVShowByIDAsync(TVShowID).Result,
                CurrUser = _CurrUser

            });
        }

        /// <summary>
        /// Updates the Current TV Show from the TVShow View Model
        /// </summary>
        /// <param name="tvshowModel">A TVShowViewModel with the Current TV Show altered</param>
        /// <returns>The Main TV Show Page</returns>
        [HttpPost]
        public IActionResult EditTVShow(TVShowViewModel tvshowModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.UpdateTVShow(tvshowModel.CurrTVShow);
                ModelState.Clear();
            }
            return RedirectToAction("TVShow", tvshowModel);
        }

        /// <summary>
        /// Deletes the TVShow with the matching ID and redirects back to the TvShow page
        /// </summary>
        /// <param name="TVShowID">TVShowID of the Tv Show to delete</param>
        /// <returns>The TV Show page</returns>
        [HttpPost]
        public IActionResult DeleteTVShow(long TVShowID)
        {
            _entertainLogRepo.DeleteTVShow(_entertainLogRepo.GetTVShowByIDAsync(TVShowID).Result);
            return RedirectToAction("TVShow", new TVShowViewModel
            {
                TVShows = _entertainLogRepo.TVShows,
                CurrUser = _CurrUser
            });

        }

        //Movie
        [HttpGet]
        public IActionResult Movie()
        {

            return View(new MovieViewModel
            {
                Movies = _entertainLogRepo.Movies,
                CurrUser = _CurrUser
            });
        }

        [HttpPost]
        public IActionResult Movie(MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddMovie(movieViewModel.NewMovie);

                ModelState.Clear();
            }
            return View(new MovieViewModel
            {
                Movies = _entertainLogRepo.Movies,
                CurrUser = movieViewModel.CurrUser
            });
        }

        [HttpGet]
        public IActionResult EditMovie(long MovieID)
        {
            return View(new MovieViewModel
            {
                CurrMovie = _entertainLogRepo.GetMovieByIDAsync(MovieID).Result,
                CurrUser = _CurrUser

            });
        }

        [HttpPost]
        public IActionResult EditMovie(MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.UpdateMovie(movieViewModel.CurrMovie);
                ModelState.Clear();
            }
            return RedirectToAction("Movie", movieViewModel);
        }
    }
}
