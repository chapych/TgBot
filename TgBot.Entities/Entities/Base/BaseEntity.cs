namespace TgBot.Entities.Entities.Base;

public abstract class BaseEntity<T>
{
    public T Id { get; protected init; }
}