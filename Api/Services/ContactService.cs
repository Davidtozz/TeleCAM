namespace Api.Services;

using Api.Data;
using Api.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

public sealed class ContactService : IContactService
{
    private readonly IUserService _userService;
    private readonly TelecamContext _context;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IUserService userService, TelecamContext context, ILogger<ContactService> logger)
    {
        _userService = userService;
        _context = context;
        _logger = logger;
    }

    public async Task AddContactAsync(User user, NewContactDto contact)
    {
        var targetUser = await _context.Users
            .Where(u => u.Username == contact.Username)
            .FirstOrDefaultAsync();

        await _context.Contacts.AddAsync(new Contact
        {
            Name = contact.Name,
            User = user,
            TargetUser = targetUser
        });
        await _context.SaveChangesAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(Guid userId, Guid contactId)
    {
        return await _context.Contacts
            .Where(c => c.Id == contactId)
            .Where(c => c.User.Id == userId)
            .Include(c => c.User.Contacts)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Contact>> GetContactsByUsernameAsync(string username)
    {
        try 
        {
            return await _context.Contacts
                .Where(c => c.User.Username == username)
                .Include(c => c.TargetUser)
                .ToArrayAsync();
        } 
        catch (ArgumentNullException e) 
        {
            _logger.LogError(e, "Error getting contacts by username");
            return Array.Empty<Contact>();
        }
    }
}

public interface IContactService
{
    Task AddContactAsync(User user, NewContactDto contact);
    Task<Contact?> GetContactByIdAsync(Guid userId, Guid contactId);
    Task<ICollection<Contact>> GetContactsByUsernameAsync(string username);
}