using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints;
public static class CategoryEndpoints
{
    public static void MapCategoryApi(this WebApplication app)
    {
        app.MapGet("/api/Categories",
        async (IBlogApi api) =>
        {
            return Results.Ok(await api.GetCategoriesAsync());
        });

        app.MapGet("/api/Categories/{*id}",
        async (IBlogApi api, string id) =>
        {
            return Results.Ok(await api.GetCategoryAsync(id));
        });

        app.MapDelete("/api/Categories", [Authorize]
        async (IBlogApi api, [FromBody] Category item) =>
        {
            await api.DeleteCategoryAsync(item);
            return Results.Ok();
        });

        app.MapPut("/api/Categories", [Authorize]
        async (IBlogApi api, [FromBody] Category item) =>
        {
            return Results.Ok(await api.SaveCategoryAsync(item));
        });
    }
}
