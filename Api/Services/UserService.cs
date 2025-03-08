namespace Api.Services;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Domain.Entities;

public sealed class UserService : IUserService<User>
{
    private readonly TelecamContext _context;

    public UserService(TelecamContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.Include(u => u.Contacts)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddContactAsync(string username, Contact contact)
    {
        var user = await GetUserByUsernameAsync(username);
        if (user is not null)
        {
            user.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Contact>> GetContactsAsync(string username)
    {
        var user = await GetUserByUsernameAsync(username);
        return user?.Contacts ?? new List<Contact>();
    }


    public async Task<bool> DeleteUserAsync(Guid id) 
    {
        var user = await GetUserByIdAsync(id);
        if (user is null) {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        User? dbUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == user.Username);
        if(dbUser is null) {
            if(user.Id == Guid.Empty) {
                user.Id = Guid.NewGuid();
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        } 
        return null;
    }
}



public interface IUserService<TUser> where TUser : class
{
    Task<TUser?> GetUserByIdAsync(Guid id);
    Task<TUser?> GetUserByUsernameAsync(string username);
    Task AddContactAsync(string username, Contact contact);
    Task<ICollection<Contact>> GetContactsAsync(string username);
    Task<bool> DeleteUserAsync(Guid id);
    Task<User?> CreateUserAsync(User user);
}