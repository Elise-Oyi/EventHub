using EventHub.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Dal
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        /*
         *This is a non-generic class repository
         */
        private readonly EventHubDbContext _dbContext;
        public AuthenticationRepository(EventHubDbContext context)
        {
            _dbContext = context;
        }

        //--register user
        public int RegisterUser(User user)
        {
            _dbContext.Users.Add(user);
            return  _dbContext.SaveChanges();

        }

        //--check credentials
        public User? CheckCredentials(User user)
        {
            var userCredentials = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
            return userCredentials;
        }

        //--get user role
        public string GetUserRole(int roleId)
        {
            return _dbContext.Roles.SingleOrDefault(role => role.RoleId == roleId).RoleName;
        }

        //--get all users
        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

       

      
    }
}
