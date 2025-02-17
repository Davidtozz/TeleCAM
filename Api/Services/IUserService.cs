namespace Api.Services;
using Api.Models;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task AddContactAsync(string username, Contact contact);
    Task<ICollection<Contact>> GetContactsAsync(string username);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> CreateUserAsync(User user);
}