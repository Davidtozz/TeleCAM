namespace Api.Services;

using Api.Data;
using Api.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

public sealed class ContactService : IContactService
{
    private readonly IUserService<User> _userService;
    private readonly TelecamContext _context;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IUserService<User> userService, TelecamContext context, ILogger<ContactService> logger)
    {
        _userService = userService;
        _context = context;
        _logger = logger;
    }

    public async Task AddContactAsync(User user, NewContactDto contact)
    {
        await _context.Contacts.AddAsync(new Contact
        {
            Name = contact.Name,
            User = user
        });
        await _context.SaveChangesAsync();    
    }

    public async Task<Contact?> GetContactByIdAsync(Guid userId, Guid contactId)
    {
        return await _context.Contacts
            .Where(c => c.Id == contactId && c.User.Id == userId)
            .Select(c => new Contact
            {
                Id = c.Id,
                Name = c.Name,
                User = c.User
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Contact>> GetContactsByUsernameAsync(string username)
    {
        return await _context.Contacts
            .Where(c => c.User.Username == username)
            .ToArrayAsync();
    }
}

public interface IContactService
{
    Task AddContactAsync(User user, NewContactDto contact);
    Task<Contact?> GetContactByIdAsync(Guid userId, Guid contactId);
    Task<ICollection<Contact>> GetContactsByUsernameAsync(string username);
}