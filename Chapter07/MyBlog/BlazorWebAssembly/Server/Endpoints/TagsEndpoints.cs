using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints;
public static class TagEndpoints
{
    public static void MapTagApi(this WebApplication app)
    {
        app.MapGet("/api/Tags",
        async (IBlogApi api) =>
        {
            return Results.Ok(await api.GetTagsAsync());
        });

        app.MapGet("/api/Tags/{*id}",
        async (IBlogApi api, string id) =>
        {
            return Results.Ok(await api.GetTagAsync(id));
        });

        app.MapDelete("/api/Tags", [Authorize]
        async (IBlogApi api, [FromBody] Tag item) =>
        {
            await api.DeleteTagAsync(item);
            return Results.Ok();
        });

        app.MapPut("/api/Tags", [Authorize]
        async (IBlogApi api, [FromBody] Tag item) =>
        {
            return Results.Ok(await api.SaveTagAsync(item));
        });
    }
}
