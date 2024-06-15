using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Interfaces;

public interface ICrudBase<TEntity> where TEntity : IdEntity
{
    Task<ActionResult<IReadOnlyList<TEntity>>> GetAll();
    Task<ActionResult> Create(TEntity dto);
    Task<ActionResult<TEntity>> Get(int id);
    Task<ActionResult<TEntity>> Update(int id, TEntity dto);
    Task<ActionResult> Delete(int id);
}