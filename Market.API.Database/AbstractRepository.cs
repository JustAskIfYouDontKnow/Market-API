using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database
{
    public abstract class AbstractRepository<T>
        where T : AbstractModel
    {
        protected readonly DbSet<T> DbModel;

        protected readonly PostgresContext Context;


        protected AbstractRepository(PostgresContext context)
        {
            Context = context;
            DbModel = context.Set<T>();
        }


        async protected Task<T> CreateModelAsync(T model)
        {
            await Context.AddAsync(model);
            var result = await Context.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("Db error. Not Create any model");
            }

            return model;
        }


        public async Task<int> UpdateModelAsync(T model)
        {
            DbModel.Update(model);
            return await Context.SaveChangesAsync();
        }


        private void Delete(T entity)
        {
            DbModel.Remove(entity);
        }


        public async Task DeleteModel(T model)
        {
            Delete(model);
            var result = await Context.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("Db error. Not deleted");
            }
        }
    }
}

