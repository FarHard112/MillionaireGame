using WhoWantsToBeAMillionaireGame.Core;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Data.Abstractions.Repositories;

public interface IRepository<T> where T : IBaseEntity
{
    //READ
    Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> Get();

    //CREATE
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    //UPDATE
    Task UpdateAsync(T entity);
    Task PatchAsync(Guid id, List<PatchModel> patchData);

    //DELETE
    void Remove(T entity);
    Task<T?> FirstOrDefaultAsync(Func<T, bool> value);
}