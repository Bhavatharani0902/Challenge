using Challenge.Entities;

namespace Challenge.Service
{
    public interface IUserService
    {
        void CreateUser(User user);

        void DeleteUser(int userId);

        void EditUser(User user);

        List<User> GetAllUsers();


        User ValidateUser(string email, string password);

        User GetUserById(int userId);
    }
}
