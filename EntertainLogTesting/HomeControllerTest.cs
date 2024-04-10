using EntertainLog.Models;
using EntertainLog.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntertainLog.Models.Repos;
using Microsoft.AspNetCore.Mvc;
using EntertainLog.Models.ViewModel;

namespace EntertainLogTesting
{
    public class HomeControllerTest
    {

        //[Fact]
        //public void TesTest()
        //{
        //    //Arrange
        //    var repoMock = new Mock<IEntertainLogRepo>();
        //    repoMock.Setup(m => m.AddUser(It.IsAny<User>())).Returns(It.IsAny<User>());
        //    var controller = new HomeController(repoMock.Object);

        //    //Act
        //    var result = controller.SignUp(new User());

        //    //Assert
        //    repoMock.Verify(m => m.AddUser(It.IsAny<User>()), Times.Once());
        //    Assert.NotNull(result);
            
        //}

        [Fact]
        public void DashboardActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User {UserID = 123 }, new User {UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Books).Returns(
                new[] { 
                    //matches
                    new Book { BookID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->match, queued->false, favourited->true
                    new Book { BookID = 123, UserID = 123, Queued = false, Favourited = true},
                    //userid-> match, queued->match, favourited->no match
                    new Book { BookID = 111, UserID = 123, Queued = true, Favourited = false},
                    //userid-> match, queued->no match, favourited->no match
                    new Book { BookID = 789, UserID = 123, Queued = false, Favourited = false},
                    //userid->no match, queued->no match, favourited->no match
                    new Book { BookID = 101, UserID = 456, Queued = false, Favourited = false },
                    //userid->no match, queued->match, favourited->match, 
                    new Book { BookID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());
            repoMock.SetupGet(m => m.TVShows).Returns(
                new[] { 
                    //matches
                    new TVShow { TVShowID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->match, queued->false, favourited->true
                    new TVShow { TVShowID = 123, UserID = 123, Queued = false, Favourited = true},
                    //userid-> match, queued->match, favourited->no match
                    new TVShow { TVShowID = 111, UserID = 123, Queued = true, Favourited = false},
                    //userid-> match, queued->no match, favourited->no match
                    new TVShow { TVShowID = 789, UserID = 123, Queued = false, Favourited = false},
                    //userid->no match, queued->no match, favourited->no match
                    new TVShow { TVShowID = 101, UserID = 456, Queued = false, Favourited = false },
                    //userid->no match, queued->match, favourited->match, 
                    new TVShow { TVShowID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());
            repoMock.SetupGet(m => m.Musics).Returns(
                new[] { 
                    //matches
                    new Music { MusicID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->match, queued->false, favourited->true
                    new Music { MusicID = 123, UserID = 123, Queued = false, Favourited = true},
                    //userid-> match, queued->match, favourited->no match
                    new Music { MusicID = 111, UserID = 123, Queued = true, Favourited = false},
                    //userid-> match, queued->no match, favourited->no match
                    new Music { MusicID = 789, UserID = 123, Queued = false, Favourited = false},
                    //userid->no match, queued->no match, favourited->no match
                    new Music { MusicID = 101, UserID = 456, Queued = false, Favourited = false },
                    //userid->no match, queued->match, favourited->match, 
                    new Music { MusicID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());
            repoMock.SetupGet(m => m.Movies).Returns(
                new[] { 
                    //matches
                    new Movie { MovieID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->match, queued->false, favourited->true
                    new Movie { MovieID = 123, UserID = 123, Queued = false, Favourited = true},
                    //userid-> match, queued->match, favourited->no match
                    new Movie { MovieID = 111, UserID = 123, Queued = true, Favourited = false},
                    //userid-> match, queued->no match, favourited->no match
                    new Movie { MovieID = 789, UserID = 123, Queued = false, Favourited = false},
                    //userid->no match, queued->no match, favourited->no match
                    new Movie { MovieID = 101, UserID = 456, Queued = false, Favourited = false },
                    //userid->no match, queued->match, favourited->match, 
                    new Movie { MovieID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.Dashboard(123) as ViewResult)?.ViewData.Model as DashboardViewModel;

            //Assert
            //checking that the return is correct in terms of size
            Assert.Equal(model.BooksQueue.Count(), 2);
            Assert.Equal(model.TVShowsQueue.Count(), 2);
            Assert.Equal(model.MoviesQueue.Count(), 2);
            Assert.Equal(model.MusicsQueue.Count(), 2);

            Assert.Equal(model.BooksFaves.Count(), 2);
            Assert.Equal(model.TVShowsFaves.Count(), 2);
            Assert.Equal(model.MoviesFaves.Count(), 2);
            Assert.Equal(model.MusicsFaves.Count(), 2);


            //returning the expected obj
            Assert.Equal(model.BooksQueue.First(), repoMock.Object.Books.First());
            Assert.Equal(model.TVShowsQueue.First(), repoMock.Object.TVShows.First());
            Assert.Equal(model.MoviesQueue.First(), repoMock.Object.Movies.First());
            Assert.Equal(model.MusicsQueue.First(), repoMock.Object.Musics.First());

            Assert.Equal(model.BooksFaves.First(), repoMock.Object.Books.First());
            Assert.Equal(model.TVShowsFaves.First(), repoMock.Object.TVShows.First());
            Assert.Equal(model.MoviesFaves.First(), repoMock.Object.Movies.First());
            Assert.Equal(model.MusicsFaves.First(), repoMock.Object.Musics.First());

        }

        [Fact]
        public void DashboardActionModelBadUser()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.Dashboard(444) as ViewResult;

            //Assert
            Assert.Equal("Login", model.ViewName);
        }

        [Fact]
        public void BookActionModelBadID()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.Book(444) as RedirectToActionResult;

            //Assert
            Assert.Equal("Login", model.ActionName);
        }

        [Fact]
        public void BookActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Books).Returns(
                new[]
                {
                    //matches
                    new Book { BookID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new Book { BookID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.Book(123) as ViewResult)?.ViewData.Model as BookViewModel;

            //Assert
            Assert.Equal(1, model.Books.Count());

        }

        [Fact]
        public void AddBookActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Books).Returns(
                new[]
                {
                    //matches
                    new Book { BookID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new Book { BookID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var newBook = new Book
                {
                    Title = "Fifth Season",
                    Author = "N. K. Jemisin",
                    Year = "2015",
                    Genre = "Fantasy",
                    PageCount = 512,
                    Read = true,
                    Favourited = true,
                    Rating = 5,
                    Notes = "Best Book Ever",
                    UserID = 123
                };

            var controller = new HomeController(repoMock.Object);

            //Act
            var result = controller.Book(newBook) as ViewResult;
            var model = (result)?.ViewData.Model as BookViewModel;

            //Assert
            Assert.Single(model.Books);
            Assert.Equal(repoMock.Object.Users.First(), model.CurrUser);
        }

        [Fact]
        public void EditBookActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Books).Returns(
                new[]
                {
                    //matches
                    new Book { BookID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new Book { BookID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());
            var newBook = new Book { BookID = 123, UserID = 123, Queued = true, Favourited = true };


            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.EditBook(123) as ViewResult)?.ViewData.Model as BookViewModel;

            //Assert
            Assert.Equal(newBook.BookID, model.CurrBook.BookID);

        }


        [Fact]
        public void UpdateEditBookActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            var bvm = new BookViewModel { CurrBook = new Book
            {
                BookID = 5,
                UserID = 123
            }
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.EditBook(bvm) as RedirectToActionResult;

            //Assert
            Assert.Equal("Book", model.ActionName);

        }

        [Fact]
        public void DeleteBookActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            var bvm = new BookViewModel
            {
                CurrBook = new Book
                {
                    BookID = 5,
                    UserID = 123
                }
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.EditBook(bvm) as RedirectToActionResult;

            //Assert
            Assert.Equal("Book", model.ActionName);
        }


        //Music Tests
        [Fact]
        public void MusicActionModelBadID()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.Music(444) as RedirectToActionResult;

            //Assert
            Assert.Equal("Login", model.ActionName);
        }

        [Fact]
        public void MusicActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Musics).Returns(
                new[]
                {
                    //matches
                    new Music { MusicID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new Music { MusicID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.Music(123) as ViewResult)?.ViewData.Model as MusicViewModel;

            //Assert
            Assert.Equal(1, model.Musics.Count());

        }

        [Fact]
        public void AddMusicActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Musics).Returns(
                new[]
                {
                    //matches
                    new Music { MusicID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new Music { MusicID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var newMusic = new Music
            {
                Title = "The Scientist",
                Artist = "Coldplay",
                Year = "2002",
                Genre = "Alternative Rock",
                Runtime = "5:09",
                Album = "A Rush of Blood to the Head",
                Listened = true,
                Rating = 5,
                Notes = "Beautiful piano melody and poignant lyrics.",
                UserID = 123
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var result = controller.Music(newMusic) as ViewResult;
            var model = (result)?.ViewData.Model as MusicViewModel;

            //Assert
            Assert.Single(model.Musics);
            Assert.Equal(repoMock.Object.Users.First(), model.CurrUser);
        }

        [Fact]
        public void EditMusicActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.Musics).Returns(
                new[]
                {
                    //matches
                    new Music { MusicID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new Music { MusicID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());
            var newMusic = new Music { MusicID = 123, UserID = 123, Queued = true, Favourited = true };


            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.EditMusic(123) as ViewResult)?.ViewData.Model as MusicViewModel;

            //Assert
            Assert.Equal(newMusic.MusicID, model.CurrMusic.MusicID);

        }


        [Fact]
        public void UpdateEditMusicActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            var bvm = new MusicViewModel
            {
                CurrMusic = new Music
                {
                    MusicID = 5,
                    UserID = 123
                }
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.EditMusic(bvm) as RedirectToActionResult;

            //Assert
            Assert.Equal("Music", model.ActionName);

        }

        [Fact]
        public void DeleteMusicActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            var bvm = new MusicViewModel
            {
                CurrMusic = new Music
                {
                    MusicID = 5,
                    UserID = 123
                }
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.EditMusic(bvm) as RedirectToActionResult;

            //Assert
            Assert.Equal("Music", model.ActionName);
        }


        //TVShow Tests
        [Fact]
        public void TVShowActionModelBadID()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.TVShow(444) as RedirectToActionResult;

            //Assert
            Assert.Equal("Login", model.ActionName);
        }

        [Fact]
        public void TVShowActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.TVShows).Returns(
                new[]
                {
                    //matches
                    new TVShow { TVShowID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new TVShow { TVShowID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.TVShow(123) as ViewResult)?.ViewData.Model as TVShowViewModel;

            //Assert
            Assert.Equal(1, model.TVShows.Count());

        }

        [Fact]
        public void AddTVShowActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.TVShows).Returns(
                new[]
                {
                    //matches
                    new TVShow { TVShowID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new TVShow { TVShowID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());

            var newTVShow = new TVShow
            {
                Title = "The Walking Dead",
                Creator = "Frank Darabont",
                Year = "2010",
                Genre = "Zombie",
                Seasons = 11,
                Watched = true,
                Favourited = true,
                Rating = 5,
                Notes = "Car OL",
                UserID = 123
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var result = controller.TVShow(newTVShow) as ViewResult;
            var model = (result)?.ViewData.Model as TVShowViewModel;

            //Assert
            Assert.Single(model.TVShows);
            Assert.Equal(repoMock.Object.Users.First(), model.CurrUser);
        }

        [Fact]
        public void EditTVShowActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            repoMock.SetupGet(m => m.TVShows).Returns(
                new[]
                {
                    //matches
                    new TVShow { TVShowID = 123, UserID = 123, Queued = true, Favourited = true},
                    //userid->no match, queued->match, favourited->match, 
                    new TVShow { TVShowID = 456, UserID = 456, Queued = true, Favourited = true}
                }.AsQueryable());
            var newTVShow = new TVShow { TVShowID = 123, UserID = 123, Queued = true, Favourited = true };


            var controller = new HomeController(repoMock.Object);

            //Act
            var model = (controller.EditTVShow(123) as ViewResult)?.ViewData.Model as TVShowViewModel;

            //Assert
            Assert.Equal(newTVShow.TVShowID, model.CurrTVShow.TVShowID);

        }


        [Fact]
        public void UpdateEditTVShowActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            var bvm = new TVShowViewModel
            {
                CurrTVShow = new TVShow
                {
                    TVShowID = 5,
                    UserID = 123
                }
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.EditTVShow(bvm) as RedirectToActionResult;

            //Assert
            Assert.Equal("TVShow", model.ActionName);

        }

        [Fact]
        public void DeleteTVShowActionModelIsComplete()
        {
            //Arrange
            var repoMock = new Mock<IEntertainLogRepo>();

            repoMock.SetupGet(m => m.Users).Returns(new[] { new User { UserID = 123 }, new User { UserID = 456 } }.AsQueryable());
            var bvm = new TVShowViewModel
            {
                CurrTVShow = new TVShow
                {
                    TVShowID = 5,
                    UserID = 123
                }
            };

            var controller = new HomeController(repoMock.Object);

            //Act
            var model = controller.EditTVShow(bvm) as RedirectToActionResult;

            //Assert
            Assert.Equal("TVShow", model.ActionName);
        }
    }
}
