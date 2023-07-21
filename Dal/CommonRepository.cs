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
        public List<T> GetAll()
        {
            return table.ToList();
        }

        //--get 1 item by item id
        public T GetDetails(int id)
        {
            return table.Find(id);
        }

        //--insert item
        public void Insert(T item)
        {
           table.Add(item);
        }

        //--update item
        public void Update(T item)
        {
            table.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        //--delete item
        public void Delete(T item)
        {
            table.Remove(item);
        }

        //--save changes
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        
    }
}
