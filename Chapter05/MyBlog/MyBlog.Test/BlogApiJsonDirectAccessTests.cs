using Data.Models;

namespace MyBlog.Test
{

    public class BlogApiJsonDirectAccessTests : IClassFixture<BlogApiJsonDirectAccessFixture>
    {
        private readonly BlogApiJsonDirectAccessFixture _fixture;

        public BlogApiJsonDirectAccessTests(BlogApiJsonDirectAccessFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task SaveBlogpostTest()
        {
            BlogPost post = new();
            post.Id = "TestPost1";
            post.Title = "Test Title1";
            post.PublishDate = DateTime.Now;
            post.Category = new() { Id = "TestCategory1", Name = "Test Category" };
            post.Tags.Add(new() { Id = "TestTag1", Name = "Test tag 1" });
            post.Tags.Add(new() { Id = "TestTag2", Name = "Test tag 2" });
            await _fixture.Api.SaveBlogPostAsync(post);

            var posts = await _fixture.Api.GetBlogPostsAsync(100, 0);
            Assert.Contains(posts, p => p.Id == post.Id);
        }

        [Fact]
        public async Task GetBlogpostTest()
        {
            var post = await _fixture.Api.GetBlogPostAsync("TestPost1");
            Assert.NotNull(post);
        }

        [Fact]
        public async Task SaveCategoryTest()
        {
            Category item = new();
            item.Id = "Category1";
            item.Name = "Category Name 1";
            await _fixture.Api.SaveCategoryAsync(item);

            var cat = await _fixture.Api.GetCategoryAsync(item.Id);
            Assert.True(cat != null);
            Assert.True(cat.Id == item.Id);
            Assert.True(cat.Name == item.Name);

        }

        [Fact]
        public async Task SaveTagTest()
        {
            Tag item = new();
            item.Id = "Tag1";
            item.Name = "Tag Name 1";
            await _fixture.Api.SaveTagAsync(item);

            var tag = await _fixture.Api.GetTagAsync(item.Id);
            Assert.True(tag != null);
            Assert.True(tag.Id == item.Id);
            Assert.True(tag.Name == item.Name);

        }
        [Fact]
        public async Task DeleteTagTest()
        {
            Tag item = new();
            item.Id = "TagToBeDelete";
            item.Name = "Delete me";
            await _fixture.Api.SaveTagAsync(item);

            var tag = await _fixture.Api.GetTagAsync(item.Id);
            Assert.True(tag != null);
            Assert.True(tag.Id == item.Id);
            Assert.True(tag.Name == item.Name);

            await _fixture.Api.DeleteTagAsync(item.Id);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _fixture.Api.GetTagAsync(item.Id));

        }

        [Fact]
        public async Task DeleteCategoryTest()
        {
            Category item = new();
            item.Id = "CategoryToBeDelete";
            item.Name = "Delete me";
            await _fixture.Api.SaveCategoryAsync(item);

            var category = await _fixture.Api.GetCategoryAsync(item.Id);
            Assert.True(category != null);
            Assert.True(category.Id == item.Id);
            Assert.True(category.Name == item.Name);

            await _fixture.Api.DeleteCategoryAsync(item.Id);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _fixture.Api.GetCategoryAsync(item.Id));

        }
    }
}
