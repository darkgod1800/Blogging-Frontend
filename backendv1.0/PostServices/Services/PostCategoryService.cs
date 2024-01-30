using Microsoft.EntityFrameworkCore;
using PostServices.Data;
using PostServices.Models.Domains;
using System.Diagnostics;

namespace PostServices.Services
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly ApplicationDbContext _context;
        public PostCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category c)
        {
            await _context.Categories.AddAsync(c);
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<int> DeleteCategory(Guid id)
        {
            var find = await _context.Categories.FindAsync(id);
            if (find != null)
            {
                _context.Categories.Remove(find);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Categories.ToListAsync();
        }


    }
}
