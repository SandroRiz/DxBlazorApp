using Microsoft.EntityFrameworkCore;

namespace Microgate.Extranet.Services;

public class BaseService<T, TKey> :  IBaseService<T, TKey>
where T : class
{
    protected IDbContextFactory<ExtranetDbContext> CtxFactory { get; }
    public BaseService(IDbContextFactory<ExtranetDbContext> ctxFactory)
    {
        CtxFactory = ctxFactory;
    }

    public virtual async Task<T> GetAsync(TKey id)
    {
        using var ctx = await CtxFactory.CreateDbContextAsync();
        var set = ctx.Set<T>();
        if (set == null) throw new ArgumentException(nameof(set));

        var entity = await set.FindAsync(id);
       // if (entity == null) throw new ArgumentException($"Cannot find id {id}");

        return entity;
    }




    public virtual async Task<List<T>> ListAsync()
    {
        using var ctx = await CtxFactory.CreateDbContextAsync();
        var set = ctx.Set<T>();
        return await set.AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<T>> ListCurrentAsync(TKey id)
    {
        var entity = await GetAsync(id);
        List<T> list = new();
        list.Add(entity);
        return list;
    }

    public virtual async Task AddAsync(T entity)
    {
        using var ctx = await CtxFactory.CreateDbContextAsync();
        var set = ctx.Set<T>();
        set.Add(entity);
        await ctx.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        using var ctx = await CtxFactory.CreateDbContextAsync();
        var set = ctx.Set<T>();
        set.Update(entity);
        //ctx.Entry(entity).State = EntityState.Modified;
        await ctx.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        using var ctx = await CtxFactory.CreateDbContextAsync();
        var set = ctx.Set<T>();
        set.Remove(entity);
        //ctx.Entry(entity).State = EntityState.Deleted;
        await ctx.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        using var ctx = await CtxFactory.CreateDbContextAsync();
        var set = ctx.Set<T>();
        T? entity = await set.FindAsync(id);
        if (entity == null) throw new ArgumentException($"Cannot find id {id}");
        set.Remove(entity);
        //ctx.Entry(entity).State = EntityState.Deleted;
        await ctx.SaveChangesAsync();
    }

    public virtual void Detach(T entity)
    {
        using var ctx =  CtxFactory.CreateDbContext();
        ctx.Entry(entity).State = EntityState.Detached;

    }






}