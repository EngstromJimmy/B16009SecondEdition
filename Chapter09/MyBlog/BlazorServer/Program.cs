//<Chapter8 Namespaces>
using Auth0.AspNetCore.Authentication;
using BlazorServer.Data;
using Components.RazorComponents;
using Data;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
//</Chapter8 Namespaces>
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//<Chapter8 LoginStatus>
builder.Services.AddTransient<ILoginStatus, LoginStatus>();
//</Chapter8 LoginStatus>

//Chapter3 BlogApiJsonDirectAccessSetting>
builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>()
    .Configure(options =>
    {
        options.DataPath = @"..\..\..\Data\";
        options.BlogPostsFolder = "Blogposts";
        options.TagsFolder = "Tags";
        options.CategoriesFolder = "Categories";
    });
builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
//</Chapter3 BlogApiJsonDirectAccessSetting>
//<Chapter8 AddAuth0WebAppAuthentication>
builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Authority"] ?? "";
        options.ClientId = builder.Configuration["Auth0:ClientId"] ?? "";
    });

//</Chapter8 AddAuth0WebAppAuthentication>
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

//<Chapter8 AddAuth>
app.UseAuthentication();
app.UseAuthorization();
//</Chapter8 AddAuth>
//<Chapter8 MinimalApis>
app.MapGet("authentication/login", async (string redirectUri, HttpContext context) =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
         .WithRedirectUri(redirectUri)
         .Build();

    await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});
app.MapGet("authentication/logout", async (HttpContext context) =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
         .WithRedirectUri("/")
         .Build();

    await context.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

//</Chapter8 MinimalApis>
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
