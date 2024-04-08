using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntertainLog.Migrations
{
    /// <inheritdoc />
    public partial class EntertainLogSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooksSet",
                columns: table => new
                {
                    BookID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageCount = table.Column<long>(type: "bigint", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Favourited = table.Column<bool>(type: "bit", nullable: false),
                    Queued = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksSet", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "MoviesSet",
                columns: table => new
                {
                    MovieID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Runtime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Watched = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Favourited = table.Column<bool>(type: "bit", nullable: false),
                    Queued = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesSet", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "MusicSet",
                columns: table => new
                {
                    MusicID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Runtime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Album = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Listened = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Favourited = table.Column<bool>(type: "bit", nullable: false),
                    Queued = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicSet", x => x.MusicID);
                });

            migrationBuilder.CreateTable(
                name: "TVShowsSet",
                columns: table => new
                {
                    TVShowID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seasons = table.Column<int>(type: "int", nullable: false),
                    Watched = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Favourited = table.Column<bool>(type: "bit", nullable: false),
                    Queued = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowsSet", x => x.TVShowID);
                });

            migrationBuilder.CreateTable(
                name: "UsersSet",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSet", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksSet");

            migrationBuilder.DropTable(
                name: "MoviesSet");

            migrationBuilder.DropTable(
                name: "MusicSet");

            migrationBuilder.DropTable(
                name: "TVShowsSet");

            migrationBuilder.DropTable(
                name: "UsersSet");
        }
    }
}
