using Microsoft.EntityFrameworkCore;
using Npgsql;
using PostServices.Data;
using PostServices.Models.Domains;

namespace PostServices.Services
{
    public class BlogPostService : IBlogPostService
    {

        private readonly ApplicationDbContext _context;

        public BlogPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> Create(BlogPost post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPost()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetPostById(Guid id)
        {
            var find = await _context.BlogPosts.FindAsync(id);
            return find;
        }

        public async Task<BlogPost> UpdatePost(Guid id, BlogPost post)
        {
            var find = await _context.BlogPosts.FindAsync(id);

            if (find != null)
            {
                find.Title = post.Title;
                find.FeaturedImageUrl = post.FeaturedImageUrl;
                find.Author = post.Author;
                find.ShortDescription = post.ShortDescription;
                find.PublishedDate = post.PublishedDate;
                find.Content = post.Content;
                find.UserHandle = post.UserHandle;
                find.IsVisible = post.IsVisible;
            }
            await _context.SaveChangesAsync();
            return find;
        }

        public async Task<int> DeletePost(Guid id)
        {
            var find = await _context.BlogPosts.FindAsync(id);

            if (find is not null)
            {
                _context.BlogPosts.Remove(find);
                return await _context.SaveChangesAsync();
            }
            return -1;

        }
    }
}
