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

        [HttpGet]
        public IActionResult Book()
        {
            
            return View(new BookViewModel
            {
                Books = _entertainLogRepo.Books,
                CurrUser = _CurrUser
            });
        }

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
                _entertainLogRepo.UpdateBook(bookModel.NewBook);
                ModelState.Clear();
            }
            return RedirectToAction("Book", bookModel);
        }

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
                _entertainLogRepo.UpdateMusic(musicModel.NewMusic);
                ModelState.Clear();
            }
            return RedirectToAction("Music", musicModel);
        }
    }
}
