namespace EventHub.Dal
{
    public interface ICommonRepository<T>
    {
         /*
          *This is a generic interface 
          *where T is Type  
          *has 6 methods, first 5 are used for CRUD operations
          *last method is used to save changes
          */

        //--Lists all Items
        Task<List<T>> GetAll();

        //--Get 1 item using item Id
        Task<T> GetDetails(int id);

        //--Insert an item 
        Task<T> Insert (T item);

        //--Update an item
        Task<T> Update (T item);

        //--Delete an item
        Task<T> Delete (int id);

        ////--Save changes
        //int SaveChanges();
    }
}
