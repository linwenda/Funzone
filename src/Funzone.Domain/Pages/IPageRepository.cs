using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<Page> GetByIdAsync(PageId pageId);

        Task<Page> GetByParentIdAsync(PageId pageId);

        Task AddAsync(Page page);
    }
}