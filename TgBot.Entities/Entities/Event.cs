using TgBot.Entities.Entities.Base;
using TgBot.Entities.Enums;

namespace TgBot.Entities.Entities;

public class Event : BaseEntity<int>
{
    private readonly List<TgUser> _users;
    private readonly List<Category> _categories;
    
    public string Name { get; init; }
    public string Description { get; init; }
    public IEnumerable<TgUser> Users => _users.AsReadOnly();
    public IEnumerable<Category> Categories
    {
        get => _categories.AsReadOnly();
        private init => _categories = value.ToList();
    }

    public Event(int id, 
        string name,
        string description,
        IEnumerable<Category> categories)
    {
        Id = id;
        Name = name;
        Description = description;
        Categories = categories;
        
        _users = [];
    }
    
    public override bool Equals(object obj) => 
        obj is Event @event && @event.Id == Id;

    public override int GetHashCode() => 
        HashCode.Combine(_users, Name, Description, Categories);
}