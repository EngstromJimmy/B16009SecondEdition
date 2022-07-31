using Data;
using Data.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MyBlog.Test
{
    public class BlogApiJsonDirectAccessFixture : IAsyncLifetime
    {
        public IBlogApi Api { get; private set; } = default!;
        public async Task InitializeAsync()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions<BlogApiJsonDirectAccessSetting>()
                .Configure(options =>
                {
                    options.DataPath = @"C:\Code\B16009SecondEdition\TestData\";
                    options.BlogPostsFolder = "Blogposts";
                    options.TagsFolder = "Tags";
                    options.CategoriesFolder = "Categories";
                });
            serviceCollection.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
            var provider = serviceCollection.BuildServiceProvider();
            Api = provider.GetRequiredService<IBlogApi>();

            await Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
