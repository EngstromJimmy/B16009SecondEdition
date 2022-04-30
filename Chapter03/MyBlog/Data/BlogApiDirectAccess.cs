using Data.Models;
using Data.Models.Interfaces;

namespace Data;
internal class BlogApiDirectAccess : IBlogApi
{
    public List<BlogPost>? BlogPosts { get; set; }

    public Task<BlogPost> GetBlogPostAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetBlogPostCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<BlogPost>> GetBlogPostsAsync(int numberofposts, int startindex)
    {
        throw new NotImplementedException();
    }

}
