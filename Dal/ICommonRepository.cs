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
        List<T> GetAll();

        //--Get 1 item using item Id
        T GetDetails(int id);

        //--Insert an item 
        void Insert (T item);

        //--Update an item
        void Update (T item);

        //--Delete an item
        void Delete (T item);

        //--Save changes
        int SaveChanges();
    }
}
