using PostServices.Models.Domains;

namespace PostServices.Services
{
    public interface IPostCategoryService
    {
        Task<Category> AddCategory(Category c);
        Task<int> DeleteCategory(Guid id);
        Task<IEnumerable<Category>> GetAllCategory();
    }
}