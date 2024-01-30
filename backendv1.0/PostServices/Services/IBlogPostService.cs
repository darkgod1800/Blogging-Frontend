using PostServices.Models.Domains;

namespace PostServices.Services
{
    public interface IBlogPostService
    {
        Task<BlogPost> Create(BlogPost post);
        Task<IEnumerable<BlogPost>> GetAllPost();
        Task<BlogPost> GetPostById(Guid id);
        Task<BlogPost> UpdatePost(Guid id, BlogPost post);
        Task<int> DeletePost(Guid id);
    }
}