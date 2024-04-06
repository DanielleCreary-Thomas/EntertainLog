using EntertainLog.Models;
using EntertainLog.Models.Repos;
using EntertainLog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.Controllers
{
    public class HomeController : Controller
    {
        private IEntertainLogRepo _entertainLogRepo;
        private User _CurrUser;
        public HomeController(IEntertainLogRepo entertainLogRepo) 
        {
            _entertainLogRepo = entertainLogRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Dashboard
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

        //Books
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

        [HttpGet]
        public IActionResult EditBook(long BookID) 
        {
            return View(new BookViewModel
            {
                CurrBook = _entertainLogRepo.GetBookByIDAsync(BookID).Result,
                CurrUser = _CurrUser

            });
        }

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

        //Music
        [HttpGet]
        public IActionResult Music()
        {

            return View(new MusicViewModel
            {
                Musics = _entertainLogRepo.Musics,
                CurrUser = _CurrUser
            });
        }

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

        [HttpGet]
        public IActionResult EditMusic(long MusicID)
        {
            return View(new MusicViewModel
            {
                CurrMusic = _entertainLogRepo.GetMusicByIDAsync(MusicID).Result,
                CurrUser = _CurrUser

            });
        }

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

        //TVShow
        [HttpGet]
        public IActionResult TVShow()
        {

            return View(new TVShowViewModel
            {
                TVShows = _entertainLogRepo.TVShows,
                CurrUser = _CurrUser
            });
        }

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

        [HttpGet]
        public IActionResult EditTVShow(long TVShowID)
        {
            return View(new TVShowViewModel
            {
                CurrTVShow = _entertainLogRepo.GetTVShowByIDAsync(TVShowID).Result,
                CurrUser = _CurrUser

            });
        }

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
    }
}
