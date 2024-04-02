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
                    );
                context.SaveChanges();

            }
            if (!context.TVShowsSet.Any())
            {
                context.TVShowsSet.AddRange(
                    //Add TV Shows here
                    );
                context.SaveChanges();

            }
            if (!context.BooksSet.Any())
            {
                context.BooksSet.AddRange(
                    //Add books here
                    );
                context.SaveChanges();
            }
        }
    }
}
