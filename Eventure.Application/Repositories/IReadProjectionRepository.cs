using System;
using System.Threading.Tasks;

namespace Eventure.Application.Repositories
{
    public interface IReadProjectionRepository<TReadModel>
    {
        Task<TReadModel> FetchByIdAsync(Guid id);
    }
}