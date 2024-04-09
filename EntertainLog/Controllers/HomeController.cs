using EntertainLog.Models;
using EntertainLog.Models.Repos;
using EntertainLog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using System.Net;


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
            //user.UserID = _entertainLogRepo.Users.Count<User>() + 1;
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
        public IActionResult Dashboard(long id)
        {
            //var test = _entertainLogRepo.Books.Where(b => b.Queued == true && b.UserID == dvm.CurrUserId).Take(10).ToList() ?? [];
            var user = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id);
            if (user != null)
            {
                var dashView = new DashboardViewModel
                {
                    CurrUser = user,

                    BooksQueue = _entertainLogRepo.Books.Where(b => b.Queued == true && b.UserID == id).Take(10).ToList() ?? [],
                    MoviesQueue = _entertainLogRepo.Movies.Where(m => m.Queued == true && m.UserID == id).Take(10).ToList() ?? [],
                    TVShowsQueue = _entertainLogRepo.TVShows.Where(t => t.Queued == true && t.UserID == id).Take(10).ToList() ?? [],
                    MusicsQueue = _entertainLogRepo.Musics.Where(m => m.Queued == true && m.UserID == id).Take(10).ToList() ?? [],

                    BooksFaves = _entertainLogRepo.Books.Where(b => b.Favourited == true && b.UserID == id).Take(10).ToList() ?? [],
                    MoviesFaves = _entertainLogRepo.Movies.Where(m => m.Favourited == true && m.UserID == id).Take(10).ToList() ?? [],
                    TVShowsFaves = _entertainLogRepo.TVShows.Where(t => t.Favourited == true && t.UserID == id).Take(10).ToList() ?? [],
                    MusicsFaves = _entertainLogRepo.Musics.Where(m => m.Favourited == true && m.UserID == id).Take(10).ToList() ?? [],

                };
                return View(dashView);
            }
            else
            {
                return View("Login");
            }
        }
        //Login
        [HttpGet]
        public IActionResult Login() { return View(); }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            User user = _entertainLogRepo.GetUserByNameAsync(loginViewModel.Username);
            if(user != null)
            {
                if (user.Password == loginViewModel.Password && user.UserName == loginViewModel.Username)
                {
                    return RedirectToAction("Dashboard", new {id = user.UserID});
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
                return View();
            }
            
            

        }


        //Logout
        /// <summary>
        /// Logs out the current user
        /// </summary>
        /// <returns>The Login Page</returns>
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        //Account
        /// <summary>
        /// Shows the current user's account page to edit their user details and view their Recent Entertainment History
        /// </summary>
        /// <param name="id">Given User ID</param>
        /// <returns>The account page with the given account viewmodel</returns>
        [HttpGet]
        public IActionResult Account(long id)
        {
            return View(new AccountViewModel
            {
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id),

                BooksHist = _entertainLogRepo.Books.Where(b => b.UserID == id).Take(3).ToList(),
                MoviesHist = _entertainLogRepo.Movies.Where(b => b.UserID == id).Take(3).ToList(),
                TVShowsHist = _entertainLogRepo.TVShows.Where(b => b.UserID == id).Take(3).ToList(),
                MusicsHist = _entertainLogRepo.Musics.Where(m => m.UserID == id).Take(3).ToList(),
            });
        }

        /// <summary>
        /// updates the user information
        /// </summary>
        /// <param name="avm">account view model</param>
        /// <returns>redirect to the account page with the updated user id</returns>
        [HttpPost]
        public IActionResult Account(AccountViewModel avm)
        {
            _entertainLogRepo.UpdateUser(avm.CurrUser);
            ModelState.Clear();
            return RedirectToAction("Account", new { id = avm.CurrUser.UserID });
        }

        //Books
        /// <summary>
        /// Displays the Book page with the current User's Books
        /// </summary>
        /// <returns> Current User's Books in a BookViewModel</returns>
        [HttpGet]
        public IActionResult Book(long id)
        {
            var user = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id);
            if (user != null)
            {
                return View(new BookViewModel
                {
                    Books = _entertainLogRepo.Books.Where(b => b.UserID == id),
                    CurrUser = user
                });
            }
            else
            {
                return RedirectToAction("Dashboard", id);
            }
        }

        //Add a new Book
        /// <summary>
        /// Adds a new Book and Displays the Book Page
        /// </summary>
        /// <param name="bookModel"> A BookViewModel with the a NewBook Book created</param>
        /// <returns>Current User's Books in a BookViewModel</returns>
        [HttpPost]
        public IActionResult Book(Book newBook)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddBook(newBook);

                ModelState.Clear();
            }
            return View(new BookViewModel
            {
                Books = _entertainLogRepo.Books.Where(b=>b.UserID == newBook.UserID),
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == newBook.UserID)
            });
        }

        /// <summary>
        /// Displays the Edit Book Page for a Book with the matching ID
        /// </summary>
        /// <param name="BookID">BookID for clicked Book</param>
        /// <returns>Current Book to be Edited in a BookViewModel</returns>
        [HttpGet]
        public IActionResult EditBook(long bookID) 
        {
            var currBook = _entertainLogRepo.Books.Where(b=>b.BookID == bookID).FirstOrDefault();
            return View(new BookViewModel
            {
                CurrBook = currBook,
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == currBook.UserID)

            });
        }

        /// <summary>
        /// Updates the Current Book from the Book View Model
        /// </summary>
        /// <param name="bookModel">A BookViewModel with the Current Book altered</param>
        /// <returns>The Main Book Page</returns>
        [HttpPost]
        public IActionResult EditBook(BookViewModel bvm)
        {
                _entertainLogRepo.UpdateBook(bvm.CurrBook);
                ModelState.Clear();
                return RedirectToAction("Book", new { id = bvm.CurrBook.UserID });

            //}
            //return RedirectToAction("EditBook", bvm.CurrBook.BookID);

        }

        /// <summary>
        /// Deletes the Book with the matching ID and redirects back to the book page
        /// </summary>
        /// <param name="BookID">BookID of the book to delete</param>
        /// <returns>The book page</returns>
        [HttpPost]
        public IActionResult DeleteBook(long BookID)
        {
            var currbook = _entertainLogRepo.GetBookByIDAsync(BookID).Result;
            _entertainLogRepo.DeleteBook(currbook);
            return RedirectToAction("Book", new {id = currbook.UserID});

        }

        //Music
        /// <summary>
        /// Displays the Music page with the current User's Musics
        /// </summary>
        /// <returns> Current User's Musics in a MusicViewModel</returns>
        [HttpGet]
        public IActionResult Music(long id)
        {
            var user = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id);
            if (user != null)
            {
                return View(new MusicViewModel
                {
                    Musics = _entertainLogRepo.Musics.Where(b => b.UserID == id),
                    CurrUser = user
                });
            }
            else
            {
                return RedirectToAction("Dashboard", id);
            }
        }

        /// <summary>
        /// Adds a new Music and Displays the Music Page
        /// </summary>
        /// <param name="musicModel"> A MusicViewModel with the a NewMusic Music created</param>
        /// <returns>Current User's Musics in a MusicViewModel</returns>
        [HttpPost]
        public IActionResult Music(Music newMusic)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddMusic(newMusic);

                ModelState.Clear();
            }
            return View(new MusicViewModel
            {
                Musics = _entertainLogRepo.Musics.Where(b => b.UserID == newMusic.UserID),
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == newMusic.UserID)
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
            var currSong = _entertainLogRepo.Musics.Where(m => m.MusicID == MusicID).FirstOrDefault();
            return View(new MusicViewModel
            {
                CurrMusic = currSong,
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == currSong.UserID)

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
            _entertainLogRepo.UpdateMusic(musicModel.CurrMusic);
            ModelState.Clear();
            return RedirectToAction("Music", new { id = musicModel.CurrMusic.MusicID });
        }
        
        /// <summary>
        /// Deletes the Music with the matching ID and redirects back to the Music page
        /// </summary>
        /// <param name="MusicID">MusicID of the Music to delete</param>
        /// <returns>The Music page</returns>
        [HttpPost]
        public IActionResult DeleteMusic(long MusicID)
        {
            var currSong = _entertainLogRepo.GetMusicByIDAsync(MusicID).Result;
            _entertainLogRepo.DeleteMusic(currSong);
            return RedirectToAction("Music", new {id = currSong.UserID});

        }


        //TVShow
        /// <summary>
        /// Displays the TV Show page with the current User's TVShows
        /// </summary>
        /// <returns> Current User's TVShows in a TVShowViewModel</returns>
        [HttpGet]
        public IActionResult TVShow(long id)
        {
            var user = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id);
            if (user != null)
            {
                return View(new TVShowViewModel
                {
                    TVShows = _entertainLogRepo.TVShows.Where(b => b.UserID == id),
                    CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id)
                });
            }
            else
            {
                return RedirectToAction("Dashboard", id);
            }
        }

        /// <summary>
        /// Adds a new TV Show and Displays the TV Show Page
        /// </summary>
        /// <param name="tvshowModel"> A TVShowViewModel with the a NewTVShow TVShow created</param>
        /// <returns>Current User's TVShows in a TVShowViewModel</returns>
        [HttpPost]
        public IActionResult TVShow(TVShow newTVShow)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddTVShow(newTVShow);

                ModelState.Clear();
            }
            return View(new TVShowViewModel
            {
                TVShows = _entertainLogRepo.TVShows.Where(b => b.UserID == newTVShow.UserID),
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == newTVShow.UserID)
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
            var currShow = _entertainLogRepo.TVShows.Where(t => t.TVShowID == TVShowID).FirstOrDefault();
            return View(new TVShowViewModel
            {
                CurrTVShow = currShow,
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == currShow.UserID)

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
            _entertainLogRepo.UpdateTVShow(tvshowModel.CurrTVShow);
            ModelState.Clear();
            return RedirectToAction("TVShow", new { id = tvshowModel.CurrTVShow.TVShowID });
        }

        /// <summary>
        /// Deletes the TVShow with the matching ID and redirects back to the TvShow page
        /// </summary>
        /// <param name="TVShowID">TVShowID of the Tv Show to delete</param>
        /// <returns>The TV Show page</returns>
        [HttpPost]
        public IActionResult DeleteTVShow(long TVShowID)
        {
            var currShow = _entertainLogRepo.GetTVShowByIDAsync(TVShowID).Result;
            _entertainLogRepo.DeleteTVShow(currShow);
            return RedirectToAction("TVShow", new TVShowViewModel
            {
                TVShows = _entertainLogRepo.TVShows,
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == currShow.UserID)
            });

        }

        //Movie
        [HttpGet]
        public IActionResult Movie(long id)
        {
            var user = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id);
            if (user != null)
            {
                return View(new MovieViewModel
                {
                    Movies = _entertainLogRepo.Movies.Where(b => b.UserID == id),
                    CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == id)
                });
            }
            else
            {
                return RedirectToAction("Dashboard", id);
            }
        }

        [HttpPost]
        public IActionResult Movie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                _entertainLogRepo.AddMovie(newMovie);

                ModelState.Clear();
            }
            return View(new MovieViewModel
            {
                Movies = _entertainLogRepo.Movies.Where(b => b.UserID == newMovie.UserID),
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == newMovie.UserID)
            });
        }

        [HttpGet]
        public IActionResult EditMovie(long MovieID)
        {
            var currMovieFound = _entertainLogRepo.Movies.Where(m => m.MovieID == MovieID).FirstOrDefault();
            return View(new MovieViewModel
            {
                CurrMovie = currMovieFound,
                CurrUser = _entertainLogRepo.Users.FirstOrDefault(u => u.UserID == currMovieFound.UserID)

            });
        }

        [HttpPost]
        public IActionResult EditMovie(MovieViewModel movieViewModel)
        {
            _entertainLogRepo.UpdateMovie(movieViewModel.CurrMovie);
            ModelState.Clear();
            return RedirectToAction("Movie", new { id = movieViewModel.CurrMovie.MovieID });
        }

        /// <summary>
        /// Deletes the Movie with the matching ID and redirects back to the Movie page
        /// </summary>
        /// <param name="MovieID">MovieID of the Movie to delete</param>
        /// <returns>The Movie page</returns>
        [HttpPost]
        public IActionResult DeleteMovie(long MovieID)
        {
            var currMov = _entertainLogRepo.GetMovieByIDAsync(MovieID).Result;
            _entertainLogRepo.DeleteMovie(currMov);
            return RedirectToAction("Movie", new {id = currMov.UserID});

        }
    }
}
