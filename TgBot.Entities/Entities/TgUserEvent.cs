
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace TgBot.Entities.Entities;

public class TgUserEvent
{
    public Guid TgUserId { get; private set; }
    public int EventId { get; private set; }
    public TgUser TgUser { get; private set; }
    public Event Event { get; private set; }
    public bool Shown { get; private set; }
    public bool Liked { get; private set; }
    public void MarkShown() => Shown = true;
    public void MarkLiked() => Liked = true;
}