using Microsoft.EntityFrameworkCore;

namespace EntertainLog.Models.Database
{
    public class LoadData
    {
        public static void LoadInitialData(IApplicationBuilder app)
        {
            //make users here first, example below
            //new User
            //{
            //    FirstName = "Darryl",
            //    LastName = "Dixon",
            //    Email = "dd@email.com",
            //    Phone = "905-774-4352",
            //    Password = "Password",
            //    ComparePassword = "Password"

            //}

            EntertainLogDBContext context = app.ApplicationServices
               .CreateScope().ServiceProvider.GetRequiredService<EntertainLogDBContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.UsersSet.Any())
            {
                //add each user from above, example below
                //context.UsersSet.Add(user1);
            }
            if (!context.MoviesSet.Any())
            {
                context.MoviesSet.AddRange(
                    //Add Movies Here
                    //set the foreign key user to users above
                    );
                context.SaveChanges();

            }
            if (!context.MusicSet.Any())
            {
                context.MusicSet.AddRange(
                    //Add Music Here
                    new Music
                    {
                        Title = "16 CARRIAGES",
                        Artist = "Beyoncé",
                        Year = "2024",
                        Genre = "Country",
                        Runtime = "3:56",
                        Album = "Cowboy Carter",
                        Listened = true,
                        Rating = 5,
                        Notes = "Mournful",
                        UserID = 1 // Example user ID
                    },
                    new Music
                    {
                        Title = "Ignorance",
                        Artist = "Paramore",
                        Year = "2009",
                        Genre = "Punk",
                        Runtime = "3:39",
                        Album = "Brand New Eyes",
                        Listened = true,
                        Favourited = true,
                        Rating = 5,
                        Notes = "Best Song Ever",
                        UserID = 5
                    },
                    new Music
                    {
                        Title = "I'm So Sick",
                        Artist = "Flyleaf",
                        Year = "2005",
                        Genre = "Alternative Metal",
                        Runtime = "2:57",
                        Album = "Flyleaf",
                        Listened = true,
                        Rating = 5,
                        Notes = "Energetic and emotional rock anthem.",
                        UserID = 2 // Example user ID
                    },
                    new Music
                    {
                        Title = "Are You That Somebody",
                        Artist = "Aaliyah",
                        Year = "1998",
                        Genre = "R&B",
                        Runtime = "4:27",
                        Album = "Aaliyah",
                        Listened = true,
                        Rating = 5,
                        Notes = "Catchy beat and smooth vocals.",
                        UserID = 3 // Example user ID
                    },
                    new Music
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
                        UserID = 4 // Example user ID
                    },
                    new Music
                    {
                        Title = "Bam Bam",
                        Artist = "Sister Nancy",
                        Year = "1982",
                        Genre = "Reggae",
                        Runtime = "3:16",
                        Album = "One, Two",
                        Listened = true,
                        Favourited = true,
                        Rating = 5,
                        Notes = "Classic reggae anthem with infectious rhythm.",
                        UserID = 5 // Example user ID
                    }


                    );
                context.SaveChanges();

            }
            if (!context.TVShowsSet.Any())
            {
                context.TVShowsSet.AddRange(
                    //Add TV Shows here
                    new TVShow
                    {
                        Title = "The Walking Dead",
                        Creator = "Frank Darabont",
                        Year = "2010",
                        Genre = "Zombie",
                        Seasons = 11,
                        Watched = true,
                        Favourited= true,
                        Rating= 5,
                        Notes = "Car OL",
                        UserID = 5
                    },
                    new TVShow
                    {
                        Title = "Game of Thrones",
                        Creator = "David Benioff, D. B. Weiss",
                        Year = "2011",
                        Genre = "Fantasy",
                        Seasons = 8,
                        Watched = true,
                        Rating = 4,
                        Notes = "Winter is Coming",
                        UserID = 5
                    },
                    new TVShow
                    {
                    Title = "The Office",
                    Creator = "Greg Daniels",
                    Year = "2005",
                    Genre = "Comedy",
                    Seasons = 9,
                    Watched = true,
                    Rating = 5,
                    Notes = "Dunder Mifflin",
                    UserID = 2
                    },
                    new TVShow
                    {
                        Title = "Sherlock",
                        Creator = "Mark Gatiss, Steven Moffat",
                        Year = "2010",
                        Genre = "Crime",
                        Seasons = 4,
                        Watched = true,
                        Rating = 5,
                        Notes = "Modern adaptation of Sherlock Holmes",
                        UserID = 4
                    },
                    new TVShow
                    {
                        Title = "Scandal",
                        Creator = "Shonda Rhimes",
                        Year = "2012",
                        Genre = "Political",
                        Seasons = 7,
                        Watched = true,
                        Favourited = true,
                        Rating = 5,
                        Notes = "Mr.President",
                        UserID = 5
                    }

                    );
                context.SaveChanges();

            }
            if (!context.BooksSet.Any())
            {
                context.BooksSet.AddRange(
                    //Add books here
                    new Book
                    {
                        Title = "Dawn",
                        Author = "Octavia E. Butler",
                        Year = "1987",
                        Genre = "Science Fiction",
                        PageCount = 248,
                        Read = true,
                        Favourited = true,
                        Rating = 5,
                        Notes = "Thought-provoking exploration of humanity and alien culture.",
                        UserID = 1 // Example user ID
                    },
                    new Book
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
                        UserID = 5
                    },
                    new Book
                    {
                        Title = "Get a Life, Chloe Brown",
                        Author = "Talia Hibbert",
                        Year = "2019",
                        Genre = "Romance",
                        PageCount = 384,
                        Read = true,
                        Rating = 5,
                        Notes = "Heartwarming and funny contemporary romance.",
                        UserID = 2 // Example user ID
                    },
                    new Book
                    {
                        Title = "The Rage of Dragons",
                        Author = "Evan Winter",
                        Year = "2019",
                        Genre = "Fantasy",
                        PageCount = 544,
                        Read = false,
                        Queued = true,
                        Notes = "Epic fantasy with intense action and rich world-building.",
                        UserID = 3 // Example user ID
                    },
                    new Book
                    {
                        Title = "Binti",
                        Author = "Nnedi Okorafor",
                        Year = "2015",
                        Genre = "Science Fiction",
                        PageCount = 96,
                        Read = true,
                        Rating = 5,
                        Notes = "Inventive and captivating novella set in a vibrant futuristic world.",
                        UserID = 4 // Example user ID
                    },
                    new Book
                    {
                        Title = "Black Leopard, Red Wolf",
                        Author = "Marlon James",
                        Year = "2019",
                        Genre = "Fantasy",
                        PageCount = 640,
                        Read = false,
                        Notes = "Dark and immersive epic fantasy with rich mythology.",
                        UserID = 5 // Example user ID
                    }


                    );
                context.SaveChanges();
            }
        }
    }
}
