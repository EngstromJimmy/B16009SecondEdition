using BlazorWebAssembly.Server.Endpoints;
using BlazorWebAssembly.Server.Hubs;
using Data;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//<Chapter7 BlogApiJsonDirectAccessSetting>
builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>()
    .Configure(options =>
    {
        options.DataPath = @"..\..\..\..\Data\";
        options.BlogPostsFolder = "Blogposts";
        options.TagsFolder = "Tags";
        options.CategoriesFolder = "Categories";
    });
builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
//</Chapter7 BlogApiJsonDirectAccessSetting>

//<Chapter8 Authentication>
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"https://{builder.Configuration["Auth0:Authority"]}";
        c.TokenValidationParameters = new()
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"https://{builder.Configuration["Auth0:Authority"]}"
        };
    });
builder.Services.AddAuthorization();
//<//Chapter8 Authentication>

builder.Services.AddSignalR();
var app = builder.Build();
//<Chapter7 MapApis>
app.MapBlogPostApi();
app.MapCategoryApi();
app.MapTagApi();
//</Chapter7 MapApis>

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapHub<BlogNotificationHub>("/BlogNotificationHub");
app.MapFallbackToFile("index.html");

app.Run();