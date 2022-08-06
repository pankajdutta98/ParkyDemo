using ParkyWebAPI.Models;

namespace ParkyWebAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        public bool userExist (string username);
        public User addUser (string username, string password);
        public User authenticate (string username, string password);
        public List<User> getUsers();
        public User getUser(int id);
    }
}
