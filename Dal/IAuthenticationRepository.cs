using EventHub.Models;

namespace EventHub.Dal
{
    public interface IAuthenticationRepository
    {
        int RegisterUser (User user);
        User? CheckCredentials(User user);
        List<User> GetUsers ();

        string GetUserRole(int roleId);
    }
}
