using EntertainLog.Models.Database;
using EntertainLog.Models.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//add database
builder.Services.AddDbContext<EntertainLogDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:ELDBConn"]);
});
//Interface conncection
builder.Services.AddScoped<IEntertainLogRepo, EntertainLogRepo>();

//Add Swagger doc
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//LoadData.LoadInitialData(app);

app.Run();
