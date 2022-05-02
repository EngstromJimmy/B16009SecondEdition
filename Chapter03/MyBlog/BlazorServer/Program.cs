using BlazorServer.Data;
using Data;
using Data.Models.Interfaces;
//<Chapter2 Server Program>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//<Chapter3 BlogApiJsonDirectAccessSetting>
builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>()
    .Configure(options =>
    {
        options.DataPath = @"C:\Code\B16009SecondEdition\Data\";
        options.BlogPostsFolder = "Blogposts";
        options.TagsFolder = "Tags";
        options.CategoriesFolder = "Categories";
    });
builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
//</Chapter3 BlogApiJsonDirectAccessSetting>

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
//</Chapter2 Server Program>