using Microsoft.EntityFrameworkCore;

namespace EventHub.Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        /*
         *This is a generic class repository
         *this (where T : class) means where T is a class
         */

        private readonly EventHubDbContext _dbContext;
        private DbSet<T> table;

        public CommonRepository(EventHubDbContext context)
        {
            _dbContext = context;
            table = _dbContext.Set<T>();
        }

        //--get all items
        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        //--get 1 item by item id
        public async Task<T>  GetDetails(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        //--insert item
        public async Task<T> Insert(T item)
        {
            _dbContext.Set<T>().Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        //--update item
        public async Task<T> Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return item;
        }

        //--delete item
        public async Task<T> Delete(int id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);

            if (item == null)
            {
                return item;
            }

            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        ////--save changes
        //public int SaveChanges()
        //{
        //    return _dbContext.SaveChanges();
        //}
        
    }
}
